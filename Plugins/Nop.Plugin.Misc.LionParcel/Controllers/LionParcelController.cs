using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Security;
using Nop.Web.Models;
using Nop.Plugin.Misc.LionParcel.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Stores;

namespace Nop.Plugin.Misc.LionParcel.Controllers
{
    public class LionParcelController : BaseController
    {

        private readonly IQueuedEmailService _queuedEmailService;
        private readonly IRepository<QueuedEmail> _queuedEmailRepository;
        private readonly IEmailAccountService _emailAccountService;
        private readonly EmailAccountSettings _emailAccountSettings;
        private readonly ISettingService _settingService;
        private readonly IStoreService _storeService;
        private readonly IWorkContext _workContext;

        public LionParcelController(IQueuedEmailService queuedEmailService, IRepository<QueuedEmail> queuedEmailRepository, EmailAccountSettings emailAccountSettings, IEmailAccountService emailAccountService,  ISettingService settingService, IWorkContext workContext, IStoreService storeService)
        {
            _queuedEmailService = queuedEmailService;
            _queuedEmailRepository = queuedEmailRepository;
            _emailAccountSettings = emailAccountSettings;
            _emailAccountService = emailAccountService;
            _settingService = settingService;
            _workContext = workContext;
            _storeService = storeService;
        }
        public ActionResult Index()
        {
            return View("~/Plugins/Misc.LionParcel/Views/LionParcel/Index.cshtml");
        }

        public ActionResult Configure()
        {
            return View("~/Plugins/Misc.LionParcel/Views/LionParcel/Configure.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> PingGetRequestHttpClient(string url, string data = null, string mthd = null)
        {
            if (string.IsNullOrEmpty(url)) return null;
            using (var httpClient = new HttpClient(new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            }))
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res;
                if (!string.IsNullOrEmpty(mthd) && mthd == "p")
                {
                    var content = string.IsNullOrEmpty(data)
                        ? new StringContent(String.Empty)
                        : new StringContent(data, Encoding.UTF8, "application/json");

                    res = await httpClient.PostAsync(url, content);
                }
                else
                {
                    res = await httpClient.GetAsync(url);
                }

                if (res.IsSuccessStatusCode)
                {
                    var dataRes = await res.Content.ReadAsStringAsync();
                    return new JsonResult { Data = dataRes };
                }

                return new NullJsonResult();
            }
        }

        [NopHttpsRequirement(SslRequirement.No)]
        [HttpPost]
        public async Task<ActionResult> TariffSearch(string url, string reqs)
        {

            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(reqs)) return new JsonResult()
            {
                Data = url + "_" + reqs
            };

            try
            {
                using (var httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var postContent = new StringContent(reqs, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(url, postContent);

                    response.EnsureSuccessStatusCode();

                    var str = await response.Content.ReadAsStringAsync();

                    return new JsonResult()
                    {
                        Data = str
                    };
                }
            }
            catch (Exception ex)
            {
                return new JsonResult()
                {
                    Data = ex.GetBaseException()
                };
            }

        }

        [NopHttpsRequirement(SslRequirement.No)]
        [HttpPost]
        public async Task<ActionResult> TrackingSearch(string url, string number)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(number))
            {
                return new NullJsonResult();
            }

            var tracking = new TrackingService { sttNumber = number };

            string dataReqs = JsonConvert.SerializeObject(tracking);

            using (var httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var bytes = Encoding.UTF8.GetBytes(dataReqs);
                var postContent = new ByteArrayContent(bytes);

                postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await httpClient.PostAsync(url, postContent);
                response.EnsureSuccessStatusCode();

                var str = await response.Content.ReadAsStringAsync();

                return new JsonResult()
                {
                    Data = str
                };
            }
        }

        #region PosRegistration
        public ActionResult PosRegistration()
        {
            return View("~/Plugins/Misc.LionParcel/Views/LionParcel/PosRegistration/PosRegistration.cshtml");
        }

        [HttpPost, ActionName("PosRegistration")]
        [PublicAntiForgery]
        //[CaptchaValidator]
        //available even when a store is closed
        [StoreClosed(true)]
        public ActionResult PosRegistrationSend(FormCollection form)
        {
            if (form != null)
            {
                //get step
                if (form.Get("step") == null) return new NullJsonResult();
                int step;
                var check = int.TryParse(form.Get("step"), out step);
                if (!check) return new NullJsonResult();
                var id = !string.IsNullOrEmpty(form.Get("id_pos")) ? form.Get("id_pos") : string.Empty;
                var mess = new MessageResponse();
                switch (step)
                {
                    //LOCATION: start insert data to form.
                    case 1:
                        var queueEmail = new QueuedEmail
                        {
                            Priority = QueuedEmailPriority.Low,
                            Subject = Guid.NewGuid().ToString(),
                            Body = RenderTable(form),
                            SentTries = 999,
                            SentOnUtc = DateTime.UtcNow,
                            CreatedOnUtc = DateTime.UtcNow
                        };
                        var emailAccount = _emailAccountService.GetEmailAccountById(_emailAccountSettings.DefaultEmailAccountId);
                        if (emailAccount == null)
                            emailAccount = _emailAccountService.GetAllEmailAccounts().FirstOrDefault();
                        if (emailAccount == null)
                        {
                            // break email if No email account could be loaded
                            mess.message.message = "Email Account is not config";
                            mess.status = false;
                            return new JsonResult
                            {
                                Data = mess
                            };
                        }
                        queueEmail.From = emailAccount.Email;
                        queueEmail.FromName = emailAccount.DisplayName;
                        queueEmail.To = emailAccount.Email;
                        queueEmail.ToName = emailAccount.DisplayName;
                        queueEmail.EmailAccountId = emailAccount.Id;
                        _queuedEmailRepository.Insert(queueEmail);
                        mess.status = true;
                        mess.message.id_pos = queueEmail.Subject;
                        mess.message.message = "Propose location saved. Follow the next step!";
                        return new JsonResult
                        {
                            Data = mess
                        };
                    case 2:
                        //BIOS
                        if (!string.IsNullOrEmpty(id))
                        {
                            var queueEmailExits = _queuedEmailRepository.Table.FirstOrDefault(x => x.Subject == id);
                            if (queueEmailExits != null)
                            {
                                var str = queueEmailExits.Body + RenderTable(form);
                                queueEmailExits.Body = str;
                                _queuedEmailRepository.Update(queueEmailExits);
                                mess.status = true;
                                mess.message.id_pos = queueEmailExits.Subject;
                                mess.message.message = "Propose BIOS saved. Follow the next step!";
                                return new JsonResult()
                                {
                                    Data = mess
                                };
                            }
                        }
                        mess.message.message = "Location is not complete";
                        mess.status = false;
                        return new JsonResult
                        {
                            Data = mess
                        };
                    case 3:

                        //final step.
                        if (!string.IsNullOrEmpty(id))
                        {
                            var queueEmailExits = _queuedEmailRepository.Table.FirstOrDefault(x => x.Subject == id);
                            if (queueEmailExits != null)
                            {
                                //zip all files to 1 file.
                                var files = Request.Files;
                                var download = UploadFiles(files);
                                if (download != null)
                                {
                                    queueEmailExits.Subject = "POS Registration";
                                    queueEmailExits.Priority = QueuedEmailPriority.High;
                                    queueEmailExits.CreatedOnUtc = DateTime.UtcNow;
                                    queueEmailExits.SentTries = 0;
                                    queueEmailExits.SentOnUtc = null;
                                    queueEmailExits.AttachmentFileName = download.Filename + download.Extension;
                                    queueEmailExits.AttachmentFilePath = download.DownloadUrl;
                                    _queuedEmailRepository.Update(queueEmailExits);
                                    return View("~/Plugins/Misc.LionParcel/Views/LionParcel/PosRegistration/ThankYou.cshtml");
                                }
                            }
                        }
                        break;
                }
                return View("~/Plugins/Misc.LionParcel/Views/LionParcel/PosRegistration/ThankYou.cshtml");
            }
            return null;
        }

        [NonAction]
        public Download UploadFiles(HttpFileCollectionBase files)
        {
            if (files != null && files.Count > 0)
            {
                string fileName = string.Empty;
                byte[] bytes = null;
                string url = string.Empty;
                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        for (int i = 0; i < files.Count; i++)
                        {
                            if ((files[i] != null) && (!string.IsNullOrEmpty(files[i].FileName)))
                            {
                                //compare in bytes (Maximum 5 MB)
                                var maxFileSizeBytes = 5e+6;
                                if (files[i].ContentLength > maxFileSizeBytes)
                                {
                                    continue;
                                }
                                var file = archive.CreateEntry(files[i].FileName);
                                using (var entryStream = file.Open())
                                using (var b = new BinaryWriter(entryStream))
                                {
                                    b.Write(files[i].GetDownloadBits());
                                }
                            }
                        }

                    }
                    //get full path where store file rar which zip files.
                    var fullpath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Uploadfiles");

                    if (fullpath != null)
                    {
                        // If the directory doesn't exist, create it.
                        if (!Directory.Exists(fullpath))
                        {
                            Directory.CreateDirectory(fullpath);
                        }

                        string filePath = Path.Combine(fullpath, $"File_POS_Registration_{DateTime.Now:yyyyMMddhhmmss}.zip");
                        url = filePath;
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            memoryStream.Seek(0, SeekOrigin.Begin);
                            memoryStream.CopyTo(fileStream);
                            bytes = memoryStream.ToArray();
                            fileName = fileStream.Name;
                        }
                    }
                }
                if (bytes != null && bytes.Length > 0 && !string.IsNullOrEmpty(fileName))
                {
                    //save an uploaded file
                    var download = new Download
                    {
                        DownloadUrl = url,
                        Filename = Path.GetFileNameWithoutExtension(fileName),
                        Extension = Path.GetExtension(fileName),
                    };

                    return download;
                }
            }
            return null;
        }

        [NonAction]
        public string RenderTable(FormCollection form)
        {
            if (form == null) return string.Empty;

            var str = string.Empty;
            str += "<table style='width: 100%'>";
            str += "<tr>";
            str += "<th colspan='2'>" + form.Get("title") + "</th>";
            str += "</tr>";

            form.Remove("title");
            form.Remove("__RequestVerificationToken");
            foreach (var key in form.AllKeys)
            {
                str += "<tr>";
                str += "<td>" + key + "</th>";
                str += "<td>" + form[key] + "</th>";
                str += "</tr>";
            }
            str += "</table></br>";
            return str;
        }
        #endregion

        #region Network page
        public ActionResult Network()
        {
            return View("~/Plugins/Misc.LionParcel/Views/LionParcel/Network/Network.cshtml");
        }

        public ActionResult Address()
        {
            return View("~/Plugins/Misc.LionParcel/Views/LionParcel/Network/Address.cshtml");
        }

        [HttpPost]
        public ActionResult GetAddressLocation(string lat, string logt)
        {
            if (string.IsNullOrEmpty(lat) || string.IsNullOrEmpty(logt)) return new NullJsonResult();
            double addressLat, addressLogt;
            double.TryParse(lat, out addressLat);
            double.TryParse(logt, out addressLogt);
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var setting = _settingService.GetSetting("networksettings.network", storeScope, false);
            if (setting != null && !string.IsNullOrEmpty(setting.Value))
            {
                var networkAddresses = JsonConvert.DeserializeObject<Addreses>(setting.Value);
                if (networkAddresses != null && networkAddresses.Addresses != null)
                {
                    var groupAddresses =
                        networkAddresses.Addresses.GroupBy(x => x.Latitude + "," + x.Longtitude).Select(x => x.First());

                    var dictionary = new Dictionary<string, double>();
                    foreach (var address in groupAddresses)
                    {
                        if (address.Latitude == null || address.Longtitude == null) continue;
                        double thisLat, thisLogt;
                        double.TryParse(address.Latitude, out thisLat);
                        double.TryParse(address.Longtitude, out thisLogt);
                        var distance = Math.Sqrt(Math.Pow(addressLat - thisLat, 2) + Math.Pow(addressLogt - thisLogt, 2));
                        dictionary.Add(address.No, distance);
                    }
                    var orderDictionary = dictionary.OrderBy(x => x.Value).Take(20).ToList();
                    var keys = orderDictionary.Select(x => x.Key);
                    var results = networkAddresses.Addresses.Where(x => keys.Contains(x.No));
                    return new JsonResult() { Data = results };
                }
            }

            return new JsonResult() { Data = null };
        }

        [HttpPost]
        public async Task<ActionResult> Address(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                var networkAddresses = JsonConvert.DeserializeObject<Addreses>(str);
                if (networkAddresses != null && networkAddresses.Addresses.Any())
                {
                    foreach (var networkAddress in networkAddresses.Addresses)
                    {
                        using (var httpClient = new HttpClient(new HttpClientHandler()
                        {
                            AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                        }))
                        {
                            var api =
                                string.Format(
                                    "https://maps.googleapis.com/maps/api/geocode/json?address={0}&components=country:ID&key=AIzaSyDORlwdJ3-n9TCOcsgNyZkAVo1m0zKKtgQ",
                                    networkAddress.Address + " , " + networkAddress.City);
                            var response = await httpClient.GetAsync(api);
                            if (!response.IsSuccessStatusCode) continue;
                            var responseStr = await response.Content.ReadAsStringAsync();
                            var googleResponse = JsonConvert.DeserializeObject<GoogleMapResponse>(responseStr);
                            if (googleResponse != null && googleResponse.status == "OK" && googleResponse.results[0] != null)
                            {
                                networkAddress.Latitude = "" + googleResponse.results[0].geometry.location.lat;
                                networkAddress.Longtitude = "" + googleResponse.results[0].geometry.location.lng;
                            }
                        }
                    }

                    var ser = JsonConvert.SerializeObject(networkAddresses);
                    var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
                    _settingService.SetSetting("networksettings.network", ser, storeScope, true);
                }

            }
            return new NullJsonResult();
        }
        #endregion
    }
}
