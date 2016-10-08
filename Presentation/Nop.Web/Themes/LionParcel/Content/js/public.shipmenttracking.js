var Cookies = {

    setCookie: function (cname, cvalue, dayExpires, domain) {
        var d = new Date();
        d.setTime(d.getTime() + (dayExpires * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + "; " + expires + ";domain=" + domain;
    },

    getCookie: function (cname) {
        var name = cname + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) === 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }
}

var ShipmentTracking = {
    loadWaiting: false,
    cookieName: '_Shiptrack',
    url: 'https://system.lionexpress.co.id/ewebportal/eLexysTrackingService.svc/GetTracking?cf_signature=TeUnxzqUs8DkWNjPo4WtGI5jxU=',
    htmlIdRender: '',
    dayOfWeek: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
    monthOfYear: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
    sttNumber: '',
    init: function (sttNumber) {
        this.loadWaiting = false;
        this.sttNumber = sttNumber;
        this.cookieName = '_Shiptrack';
        this.setCookie();
    },

    setLoadWaiting: function (display) {
        displayAjaxLoading(display);
        this.loadWaiting = display;
    },

    setCookie: function () {
        Cookies.setCookie(this.cookieName, this.sttNumber, 1, document.domain);
    },

    getCookie: function () {
        return Cookies.getCookie(this.cookieName);
    },

    formatDate: function (date) {
        var dayOfWeek = this.dayOfWeek[date.getDay()];
        var str = dayOfWeek + " - " + date.getDate() + " / " + this.monthOfYear[date.getMonth()] + " / " + date.getFullYear();
        return str;
    },

    formatAMPM: function (date) {
        var hours = date.getHours();
        var minutes = date.getMinutes();
        var ampm = hours >= 12 ? 'pm' : 'am';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;
        var strTime = hours + ':' + minutes + ' ' + ampm;
        return strTime;
    },

    loadShipmentTracking: function (htmlIdRender) {
        if (this.loadWaiting != false) {
            return;
        }
        this.setLoadWaiting(true);
        var number = this.getCookie();
        if (!number) return;
        this.htmlIdRender = htmlIdRender;
        $.ajax({
            url: '/tracking-checking',
            type: 'post',
            data: {url:this.url,number:number},
            success: this.success_process,
            complete: this.resetLoadWaiting,
            error: this.ajaxFailure
        });
        //$.ajax({
        //    async: false,
        //    cache: false,
        //    url: '/',
        //    type: 'post',
        //    data: JSON.stringify({ sttNumber: number }),
        //    success: this.success_process,
        //    complete: this.resetLoadWaiting,
        //    error: this.ajaxFailure
        //});
    },

    resetLoadWaiting: function () {
        AjaxCart.setLoadWaiting(false);
    },

    success_process: function (response) {
        if (!response) return;
        var result = JSON.parse(response);
        if (result !== []) {
            var myData = JSON.parse(result);
            var sttDetails = myData.STTDetails;
            var shipmentTravelHistory = myData.ShipmentTravelHistory;
            var allStr = "";
            if (sttDetails.length > 0 || shipmentTravelHistory.length > 0) {
                for (var i = 0; i < sttDetails.length; i++) {
                    var sttnumber = sttDetails[i].STTNumber;
                    var str = '';
                    str += "<span class='boxred'>ITEM NO : " + sttnumber + "</span>";
                    str += "<table class='table table-hover tabletracking'>";
                    str += "<thead><tr><th>Date</th><th>Time</th><th>Process</th></tr></thead>";
                    str += "<tbody>";
                    for (var j = 0; j < shipmentTravelHistory.length; j++) {
                        if (shipmentTravelHistory[j].STTNumber === sttnumber) {
                            var d = new Date(shipmentTravelHistory[j].Date);
                            str += "<tr>";
                            str += "<td>" + ShipmentTracking.formatDate(d) + "</td>";
                            str += "<td>" + ShipmentTracking.formatAMPM(d) + "</td>";
                            str += "<td>" + shipmentTravelHistory[j].Remarks + "</td>";
                            str += "</tr>";
                        }
                    }
                    str += "</tbody>";
                    str += "</table>";
                    str += "</br>";
                    str += "</br>";
                    allStr += str;
                }
            } else {
                allStr += "<span class='boxred'>No Item Found</span>";
            }

            $(ShipmentTracking.htmlIdRender).html(allStr);
        }
    },
    ajaxFailure: function (response) {
        console.log(response);
    }
}

var TariffSearch = {
    from: '',
    to: '',
    weight: 0,
    cookieName: '_Tariffsearch',
    url: 'https://system.lionexpress.co.id/ewebportal/eLexysTariffService.svc/CalculateTariff?cf_signature=H7KoLR7PTz94uS9/pKuJIJ6PbJM=',
    htmlIdRender: '',
    loadWaiting: false,

    init: function(from, to, weight) {
        this.loadWaiting = false;
        this.from = from;
        this.to = to;
        this.weight = weight;
        this.htmlIdRender = '';
        this.url = 'https://system.lionexpress.co.id/ewebportal/eLexysTariffService.svc/CalculateTariff?cf_signature=H7KoLR7PTz94uS9/pKuJIJ6PbJM=';
        this.setCookie();
    },

    setLoadWaiting: function(display) {
        displayAjaxLoading(display);
        this.loadWaiting = display;
    },

    stringifyCookieObject: function() {
        return JSON.stringify({ Origin: this.from, Destination: this.to, Weight: this.weight });
    },

    setCookie: function() {
        Cookies.setCookie(this.cookieName, this.stringifyCookieObject(), 1, document.domain);
    },

    getCookie: function() {
        return Cookies.getCookie(this.cookieName);
    },

    addCommas: function(nStr) {
        nStr += '';
        var x = nStr.split('.');
        var x1 = x[0];
        var x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        return x1 + x2;
    },

    loadTariffSearch: function(htmlIdRender) {
        if (this.loadWaiting != false) {
            return;
        }
        this.setLoadWaiting(true);

        var request = this.getCookie(this.cookieName);
        if (!request) window.location.href = "/";
        this.htmlIdRender = htmlIdRender;
        var posUrl = this.url;
        //$.ajax({
        //    async: false,
        //    cache: false,
        //    url: this.url,
        //    type: 'post',
        //    dataType: "json",
        //    contentType: "application/json; charset=UTF-8",
        //    data: data,
        //    success: this.success_process,
        //    complete: this.resetLoadWaiting,
        //    error: this.ajaxFailure
        //});
        $.ajax({
            url: '/tariff-checking',
            type: 'post',
            data: { url: posUrl, reqs: request },
            success: this.success_process,
            complete: this.resetLoadWaiting,
            error: this.ajaxFailure
        });
    },

    resetLoadWaiting: function() {
        AjaxCart.setLoadWaiting(false);
    },

    success_process: function (response) {
        if (!response) return;
        var result = JSON.parse(response);
        if (result !== "[]") {
            var myData = JSON.parse(result);
            if (myData.ErrorStatus[0]["ErrorMessage"] !== "OK") {
                $(TariffSearch.htmlIdRender).html(myData.ErrorStatus[0]["ErrorMessage"]);
                //ERROR
            } else {
                var str = "";
                str += "<span class='boxred'>Check Our Tarif</span>";
                str += "<table class='table table-hover tabletracking'>";
                str += "<thead><tr><th>Product</th><th>Description</th><th>Price</th><th>Section</th></tr></thead>";
                str += "<tbody>";
                var results = myData.TariffResult;
                for (var i = 0; i < results.length; i++) {
                    str += "<tr>";
                    str += "<td>" + results[i].Product + "</td>";
                    str += "<td>" + results[i].ServiceType + "</td>";
                    str += "<td>Rp. " + TariffSearch.addCommas("" + results[i].TotalAmountTariff) + "</td>";
                    str += "<td>" + results[i].TariffSection + "</td>";
                    str += "</tr>";
                }
                str += "</tbody>";
                str += "</table>";
                $(TariffSearch.htmlIdRender).html(str);
            }
        }
    },
    ajaxFailure: function (xhr, status, mess) {
        alert(xhr.responseText + ":" + mess);
    }
}
