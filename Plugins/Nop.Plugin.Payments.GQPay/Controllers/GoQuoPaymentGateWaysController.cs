using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.GQPay.Domain;
using Nop.Plugin.Payments.GQPay.Models;
using Nop.Plugin.Payments.GQPay.Services;
using Nop.Plugin.Payments.GQPay.Validators;
using Nop.Services.Catalog;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Security;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Security;

namespace Nop.Plugin.Payments.GQPay.Controllers
{
    public class GoQuoPaymentGateWaysController : BasePaymentController
    {

        private readonly IGoQuoPaymentProcessorService _processorService;
        private readonly IProcessorKeyValueService _processorKeyValueService;
        private readonly ILocalizationService _localizationService;
        private readonly IOrderService _orderService;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IProductService _productService;


        public GoQuoPaymentGateWaysController(IGoQuoPaymentProcessorService processorService, IProcessorKeyValueService processorKeyValueService, ILocalizationService localizationService, OrderSettings orderSettings, IOrderService orderService, IStoreContext storeContext, IOrderProcessingService orderProcessingService, IProductService productService, IShoppingCartService shoppingCartService, IPermissionService permissionService)
        {

            _processorService = processorService;
            _processorKeyValueService = processorKeyValueService;
            _localizationService = localizationService;
            _orderProcessingService = orderProcessingService;
            _productService = productService;
            _orderService = orderService;
        }

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            var model = new ConfigurationModel();
            return View("~/Plugins/Payments.GQPay/Views/GoQuoPaymentGateWays/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAntiForgery]
        public ActionResult GetAllProcess(DataSourceRequest comand)
        {
            var process = _processorService.GetAllProcessors().Select(x => new GoQuoPayProcessor
            {
                Id = x.Id,
                Name = x.Name,
                Active = x.Active,
                Currency = x.Currency
            }).ToList();

            var gridModel = new DataSourceResult
            {
                Data = process,
                Total = process.Count()
            };

            return new JsonResult
            {
                Data = gridModel
            };
        }

        [HttpPost]
        [AdminAntiForgery]
        public ActionResult UpdateProcess(GoQuoPayProcessor processUpdate)
        {
            if (processUpdate != null && processUpdate.Id > 0)
            {
                var process = _processorService.GetById(processUpdate.Id);
                if (process != null)
                {
                    process.Name = processUpdate.Name;
                    process.Active = processUpdate.Active;
                    process.Currency = process.Currency;
                    _processorService.UpdateProcessor(process);
                }
            }

            return new NullJsonResult();
        }

        [HttpPost]
        [AdminAntiForgery]
        public ActionResult DeleteProcess(DataSourceRequest comand, int id)
        {
            if (id > 0)
            {
                var process = _processorService.GetById(id);
                if (process != null)
                {
                    _processorService.DeleteProcessor(process);
                }
            }
            return new NullJsonResult();
        }

        public ActionResult CreateProcess()
        {
            return View("~/Plugins/Payments.GQPay/Views/GoQuoPaymentGateWays/CreateProcess.cshtml", new ConfigurationModel());
        }

        [HttpPost]
        [AdminAntiForgery]
        public ActionResult CreateProcess(string btnId, string formId, ConfigurationModel model)
        {
            if (model != null)
            {
                var crt = new GoQuoPayProcessor
                {
                    Name = model.Name,
                    Currency = model.Currency,
                    Active = model.Active,
                };

                _processorService.Insert(crt);

                ViewBag.RefreshPage = true;
                ViewBag.btnId = btnId;
                ViewBag.formId = formId;
                return View("~/Plugins/Payments.GQPay/Views/GoQuoPaymentGateWays/CreateProcess.cshtml", model);
            }
            return View("~/Plugins/Payments.GQPay/Views/GoQuoPaymentGateWays/CreateProcess.cshtml");
        }

        public ActionResult ProcessKeyValue(int processId)
        {
            ViewBag.AllProcess = _processorService.GetAllProcessors()
                 .Select(x => new SelectListItem
                 {
                     Selected = (x.Id.Equals(processId)),
                     Text = x.Name,
                     Value = x.Id.ToString()
                 }).ToList();
            if (processId > 0)
            {
                ViewBag.ProcessId = processId;
                var process = _processorService.GetById(processId);
                if (process != null)
                {
                    ViewBag.ProcessName = process.Name;
                }
            }
            return View("~/Plugins/Payments.GQPay/Views/GoQuoPaymentGateWays/ProcessKeyValue.cshtml");
        }

        [HttpPost]
        [AdminAntiForgery]
        public ActionResult ProcessKeyValue(int processId, DataSourceRequest command, Web.Framework.Kendoui.Filter filter = null, IEnumerable<Sort> sort = null)
        {
            var resources = _processorKeyValueService
             .GetAllProcessKeyValue(processId)
             .OrderBy(x => x.Id)
             .Select(x => new ProcessorKeyValueModel
             {
                 ProcessKey = x.ProcessKey,
                 Id = x.Id,
                 ProcessValue = x.ProcessValue,
                 ProcessId = x.GoQuoPayProcessors.Id,
                 ProcessName = x.GoQuoPayProcessors.Name
             })
                 .AsQueryable()
                 .Filter(filter)
                 .Sort(sort);

            var gridModel = new DataSourceResult
            {
                Data = resources,
                Total = resources.Count(),
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult AddProcessKeyValue(int processId, [Bind(Exclude = "Id")] ProcessorKeyValueModel model)
        {

            if (model == null) return new NullJsonResult();

            if (model.ProcessKey != null)
                model.ProcessKey = model.ProcessKey.Trim();
            if (model.ProcessValue != null)
                model.ProcessValue = model.ProcessValue.Trim();

            var processKeyValue = new ProcessorKeyValue
            {
                ProcessId = processId,
                ProcessKey = model.ProcessKey,
                ProcessValue = model.ProcessValue
            };
            _processorKeyValueService.InssertProcessKeyValue(processKeyValue);
            //if (!ModelState.IsValid)
            //{
            //    return Json(new DataSourceResult { Errors = ModelState.SerializeErrors() });
            //}

            //var res = _processorKeyValueService.GetLocaleStringProcessByProcessKey(model.ProcessKey, model.ProcessId, false);
            //if (res == null)
            //{
            //    var processKeyValue = new ProcessorKeyValue { ProcessId = processId };
            //    processKeyValue.ProcessKey = model.ProcessKey;
            //    processKeyValue.ProcessValue = model.ProcessName;
            //    _processorKeyValueService.InssertProcessKeyValue(processKeyValue);
            //}

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult UpdateProcessKeyValue(ProcessorKeyValueModel model, int id)
        {
            if (id <= 0 || model == null) return new NullJsonResult();
            var processKeyValue = _processorKeyValueService.GetProcessorKeyValueById(id);
            if (processKeyValue != null)
            {
                processKeyValue.ProcessKey = model.ProcessKey;
                processKeyValue.ProcessValue = model.ProcessValue;
                _processorKeyValueService.UpdateProcessKeyValue(processKeyValue);
            }

            return new NullJsonResult();
        }

        public ActionResult DeleteProcessKeyValue(DataSourceRequest comand, int id)
        {
            if (id > 0)
            {
                var processKeyValue = _processorKeyValueService.GetProcessorKeyValueById(id);
                if (processKeyValue != null)
                    _processorKeyValueService.DeleteProcessKeyValue(processKeyValue);
            }
            return new NullJsonResult();
        }

        #region Payment 
        [ChildActionOnly]
        public ActionResult PaymentInfo()
        {
            var model = new PaymentInfoModel();

            //CC types
            model.CreditCardTypes.Add(new SelectListItem
            {
                Text = "Visa",
                Value = "Visa",
            });
            model.CreditCardTypes.Add(new SelectListItem
            {
                Text = "Master card",
                Value = "Master",
            });

            //years
            for (int i = 0; i < 15; i++)
            {
                string year = Convert.ToString(DateTime.Now.Year + i);
                model.ExpireYears.Add(new SelectListItem
                {
                    Text = year,
                    Value = year,
                });
            }

            //months
            for (int i = 1; i <= 12; i++)
            {
                string text = (i < 10) ? "0" + i : i.ToString();
                model.ExpireMonths.Add(new SelectListItem
                {
                    Text = text,
                    Value = i.ToString(),
                });
            }

            //set postback values
            var form = Request.Form;
            model.CardholderName = form["CardholderName"];
            model.CardNumber = form["CardNumber"];
            model.CardCode = form["CardCode"];

            var selectedCcType = model.CreditCardTypes.FirstOrDefault(x => x.Value.Equals(form["CreditCardType"], StringComparison.InvariantCultureIgnoreCase));
            if (selectedCcType != null)
                selectedCcType.Selected = true;
            var selectedMonth = model.ExpireMonths.FirstOrDefault(x => x.Value.Equals(form["ExpireMonth"], StringComparison.InvariantCultureIgnoreCase));
            if (selectedMonth != null)
                selectedMonth.Selected = true;
            var selectedYear = model.ExpireYears.FirstOrDefault(x => x.Value.Equals(form["ExpireYear"], StringComparison.InvariantCultureIgnoreCase));
            if (selectedYear != null)
                selectedYear.Selected = true;

            return View("~/Plugins/Payments.GQPay/Views/GoQuoPaymentGateWays/PaymentInfo.cshtml", model);
        }
        #endregion 

        [NonAction]
        public override IList<string> ValidatePaymentForm(FormCollection form)
        {
            var warnings = new List<string>();

            //validate
            var validator = new PaymentInfoValidator(_localizationService);
            var model = new PaymentInfoModel
            {
                CardholderName = form["CardholderName"],
                CardNumber = form["CardNumber"],
                CardCode = form["CardCode"],
                ExpireMonth = form["ExpireMonth"],
                ExpireYear = form["ExpireYear"]
            };
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
                warnings.AddRange(validationResult.Errors.Select(error => error.ErrorMessage));
            return warnings;
        }

        [NonAction]
        public override ProcessPaymentRequest GetPaymentInfo(FormCollection form)
        {
            //validation
            var paymentInfo = new ProcessPaymentRequest
            {
                CreditCardType = form["CreditCardType"],
                CreditCardName = form["CardholderName"],
                CreditCardNumber = form["CardNumber"],
                CreditCardExpireMonth = int.Parse(form["ExpireMonth"]),
                CreditCardExpireYear = int.Parse(form["ExpireYear"]),
                CreditCardCvv2 = form["CardCode"]
            };
            return paymentInfo;
        }

        #region Payment Response
        public ActionResult ResponsesResult(FormCollection form)
        {
            var goquoResponse = form["GoquoResponse"];

            decimal amount;
            decimal.TryParse(form["PurAmount"], out amount);
            var currency = form["Currency"];

            if (string.IsNullOrEmpty(currency))
            {
                return RedirectToAction("PaymentFailuResult", new { error = "Error" });
            }

            var processor = _processorService.GetByCurrency(currency);

            if (processor == null) return RedirectToAction("PaymentFailuResult", new { error = "Error" });

            var processKeyValues = _processorKeyValueService.GetAllProcessKeyValue(processor.Id);

            var keyvalues = new Dictionary<string, string>();

            if (processKeyValues != null)
            {
                foreach (var processKeyValue in processKeyValues)
                {
                    keyvalues.Add(processKeyValue.ProcessKey, processKeyValue.ProcessValue);
                }
            }

            string gqLogin, gqPassword;

            keyvalues.TryGetValue("GQLogin", out gqLogin);

            keyvalues.TryGetValue("GQPassword", out gqPassword);

            var key = goquoResponse + form["Trans_Auth_Code"] + form["Currency"] + Convert.ToInt64(Math.Floor(amount)) + form["OrderID"] + gqLogin + gqPassword;

            var hash = _processorService.GetSha512Hash(key);

            var getOrderId = form["OrderID"].Replace("GQ", "");

            int orderId = Convert.ToInt32(getOrderId);

            Order order = _orderService.GetOrderById(orderId);

            if (order == null)
            {
                return RedirectToAction("PaymentFailuResult", new { error = "No order exits" });
            }

            Guid orderGuiId = order.OrderGuid;

            if (!string.Equals(hash, form["GQKey"]))
            {
                return RedirectToAction("PaymentFailuResult", new { error = "Invalid GQKey", orderId = orderGuiId });
            }

            if (goquoResponse == "1")
            {
                var cardNo = form["Card_No_Partial"];
                if (!string.IsNullOrEmpty(cardNo))
                {
                    if (_orderProcessingService.CanMarkOrderAsPaid(order))
                    {
                        order.AuthorizationTransactionCode = form["Trans_Auth_Code"];
                        order.AuthorizationTransactionId = form["TransactionID"];
                        _orderProcessingService.MarkOrderAsPaid(order);
                    }
                    return RedirectToRoute("CheckoutCompleted", new { orderId = order.Id });
                }
            }

            return RedirectToAction("PaymentFailuResult", new { error = form["ReasonCode"], orderId = orderGuiId });
        }
        #endregion

        #region Payment Fail
        public ActionResult PaymentFailuResult(string error, Guid? orderId = null)
        {
            if (orderId != null && orderId != Guid.Empty)
            {
                Order order = _orderService.GetOrderByGuid(orderId.Value);
                if (order != null)
                {
                    //Adjust inventory
                    foreach (var orderItem in order.OrderItems)
                    {
                        _productService.AdjustInventory(orderItem.Product, orderItem.Quantity, orderItem.AttributesXml);
                    }

                    _orderProcessingService.ReOrder(order);
                }
            }
            ViewBag.Error = error;
            return View("~/Plugins/Payments.GQPay/Views/GoQuoPaymentGateWays/FalsePayments.cshtml");
        }
        #endregion
    }
}
