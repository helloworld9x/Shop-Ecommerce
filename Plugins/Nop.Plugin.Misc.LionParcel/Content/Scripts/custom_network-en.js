$(document).ready(function () {
    var base = $('#base').text();
    var keyDisplay = $('#key-display');
    var formNetwork = $('form#get-network');
    var resultArea = $('#result-network');

    var slideButton = $('dt#slide-province');
    area = $('.area-result');
    provinceChoose = $('.area-result #province-drop span');

    slideButton.on('click', function () {
        $('.result-block').css({ 'margin-top': '20px' });
        area.fadeIn(function () {
            area.children('#province-drop').show();
            area.children('#city-drop').hide();

        });
    });

    provinceChoose.on('click', function () {

        var value = $(this).attr('data-id');
        area.attr('data-province', value);
        var city = $.ajax({
            type: 'post',
            url: "/ping-get-request",
            dataType: 'json',
            data: { url: 'http://www.lionexpress.co.id/Network/GetCity?province=' + value },
            timeout: 15000,
            beforeSend: function () {
                var replace = str_replace('_', ' ', value);
                var status = replace.toUpperCase();
                keyDisplay.hide().html(status + ' > <span><i class="fa fa-refresh fa-spin"></i> Choose the city.</span>').fadeIn();
                area.children('#province-drop').hide();
                area.children('#city-drop').html('<span><i class="fa fa-refresh fa-spin"></i> Loading...</span>').fadeIn();

            }
        });
        city.done(function (data) {
            var response = JSON.parse(data);
            var option = '';
            if (response.status) {
                $.each(response.message, function (key, value) {
                    option += '<span data-cityname="' + value.Nama_Kota + '" data-citycode="' + value.CityCode + '" data-id="' + value['Alias'] + '">' + value['Nama_Kota'] + '</span> | ';
                });
                option += '<span data-id="all"  style="float:right"> CHANGE PROVINCE</span>';
                area.children('#city-drop').html(option);
            }
            else {
                option += '<span data-id="all"  style="float:right"> GANTI PROVINSI </span>';
                area.children('#city-drop').html(option);
            }

            $("span[data-id='all']").on('click', function () {
                slideButton.click();
            })
        });
    });

    $(document).on('click', '.area-result #city-drop  span:not([data-id=all])', function () {
        //var province = area.attr('data-province');
        var city = $(this).attr('data-id');
        var ctnm = $(this).attr('data-cityname');
        var ctcd = $(this).attr('data-citycode');
        var replace = str_replace('_', ' ', city);
        replace = (replace == 'all') ? 'All City' : replace;
        var status = replace.toUpperCase();
        keyDisplay.find('span').hide().html(status).fadeIn();

        var pos = $.ajax({
            type: 'POST',
            //url: 'Network/GetPosNetWork',
            url: 'https://system.lionexpress.co.id/ewebportal/eLexysPOSInformationService.svc/GetPOS?cf_signature=8QXhFzBvC4GhcU4oV2bcUed2zU=',
            //dataType: 'json',
            contentType: "application/json",
            data: JSON.stringify({ CityCode: ctcd }),//{ CityCode: ctcd, CityName: ctnm },
            //timeout: 15000,
            beforeSend: function () {
                resultArea.fadeOut(function () {
                    $(this).html('<i class="fa fa-refresh fa-spin"></i> Please wait...').fadeIn();
                });
            }
        });
        pos.done(function (rslt) {
            console.log(JSON.parse(rslt));
            var display = '';
            var data = JSON.parse(rslt);
            console.log(data.ErrorStatus[0].ErrorMessage);
            if (data.ErrorStatus[0].ErrorMessage == 'OK') {
                data = data.POSResult;
                data = data.filter(function (el) {
                    return el.Territory.toLowerCase().indexOf(ctnm.toLowerCase()) >= 0 || el.Address.toLowerCase().indexOf(ctnm.toLowerCase()) >= 0;
                });

                data = data.sort(function (a, b) {
                    return ((a.POSName < b.POSName) ? -1 : ((a.POSName > b.POSName) ? 1 : 0));
                });

                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/ping-get-request",
                    data: { url: 'http://www.lionexpress.co.id/Network/getConsolidator?scode=' + ctcd, mthd: 'p' },
                    success: function (data) {
                        if (!data) return;
                        var rslt = JSON.parse(data);
                        if (rslt.data.length > 0) {
                            display += '<div class="col-sm-12"><h4 style="display: block;text-decoration:underline" id="key-display" class="hfoot">KONSOLIDATOR</h4></div>';
                            $.each(rslt.data, function (key, value) {
                                var tlp_lc = (value['Phone'] == '') ? '' : '<tr><td width="13%" valign="top">Tlp : </td><td width="87%">' + value['Phone'] + '</td></tr>';
                                display += '<div class="col-sm-12">';
                                display += '<p><b>' + (value['DistName'] == null ? "" : value['DistName'].toUpperCase()) + '</b></p>';
                                display += '<p style="display:inline-block;"><span style="float:left;width:499px;"><table width="100%" border="0" cellpadding="1"><tbody><tr>' + ((value['Address'] != '') ? '<td width="13%" valign="top">Alamat : </td>' : '') + ((value['Address'] != '') ? '<td width="87%">' + value['Address'] + '</td>' : '') + '<td rowspan="3"><a class="push-right" href="' + base + 'FAQ/ContactUs?pos_email=' + (value['Email'] == '' ? 'agent.info@lionexpress.co.id' : value['Email']) + '"><button class="button-1 btn btn-danger">EMAIL</button></a></td></tr>' + tlp_lc + '</tbody></table></span>';
                                display += '<span style="float:left;">';
                                display += '</span>';
                                display += '</p>';
                                display += '</div>';
                            });

                            display += '<br/><div class="col-sm-12"><h4 style="display: block;text-decoration:underline" id="key-display" class="hfoot">POINT OF SALES</h4></div>';
                        }
                    }
                });

                $.each(data, function (key, value) {
                    /*var tlp_lc = (value['PhoneNo'] == '') ? '' : ', Tlp. ' + value['PhoneNo'];
                    var handphone_lc = (value['MobileNo'] == '') ? '' : ', Hp. ' + value['MobileNo'];
                    display += '<div class="col-sm-12">';
                    display += '<p><b>' + (value['POSName'] == null ? "" : value['POSName'].toUpperCase()) + '</b></p>';
                    display += '<p style="display:inline-block;"><span style="float:left;width:499px;">' + value['Address'] + ' ' + value['ZipCode'] + ' ' + tlp_lc + ' ' + handphone_lc + '</span>';
                    display += '<span style="float:left;"><a class="push-right" href="' + base + 'FAQ/ContactUs?pos_email=' + (value['EmailAddress'] == '' ? 'agent.info@lionexpress.co.id' : value['EmailAddress']) + '"><button class="button-1">EMAIL ME</button></a>';*/
                    //display += '<a class="show-map-pins push-right" href="#" data-region="' + value['Territory'] + '" data-city="' + value['CityName'] + '" data-id=""><img src="' + base + '/Images/pin-sm.png" /></a>'
                    var tlp_lc = (value['PhoneNo'] == '') ? '' : '<tr><td width="13%" valign="top">Phone No : </td><td width="87%">' + value['PhoneNo'] + '</td></tr>';
                    var handphone_lc = (value['MobileNo'] == '') ? '' : '<tr><td width="13%" valign="top">Mobile No : </td><td width="87%">' + value['MobileNo'] + '</td></tr>';;
                    display += '<div class="col-sm-12">';
                    display += '<p><b>' + (value['POSName'] == null ? "" : value['POSName'].toUpperCase()) + '</b></p>';
                    display += '<p style="display:inline-block;"><span style="float:left;width:499px;"><table width="100%" border="0" cellpadding="1"><tbody><tr>' + ((value['Address'] != '') ? '<td width="13%" valign="top">Address : </td>' : '') + ((value['Address'] != '') ? '<td width="87%">' + value['Address'] + '</td>' : '') + '<td rowspan="3"><a class="push-right" href="' + base + 'FAQ/ContactUs?pos_email=' + (value['EmailAddress'] == '' ? 'agent.info@lionexpress.co.id' : value['EmailAddress']) + '"><button class="button-1 btn btn-danger">EMAIL</button></a></td></tr>' + ((value['ZipCode'] != '') ? '<tr><td width="13%" valign="top">ZipCode : </td>' : '') + ((value['ZipCode'] != '') ? '<td width="87%">' + value['ZipCpde'] + '</td></tr>' : '') + tlp_lc + ' ' + handphone_lc + '</tbody></table></span>';
                    display += '<span style="float:left;">';
                    display += '</span>';
                    display += '</p>';
                    display += '</div><hr />';
                })
                resultArea.fadeOut(function () {
                    $(this).html(display).fadeIn();
                });
            }
            else {
                resultArea.fadeOut(function () {
                    $(this).html(data.ErrorStatus[0].ErrorMessage).fadeIn();
                });
            }
        });
        pos.fail(function (xhr, response) {
            resultArea.hide().html(response).fadeIn();
        });

    });


    $(document).on('click', '.show-map-pins', function () {
        var width = 800;
        var height = 600;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ', height=' + height;
        params += ', top=' + top + ', left=' + left;
        params += ', directories=no';
        params += ', location=no';
        params += ', menubar=no';
        params += ', resizable=no';
        params += ', scrollbars=no';
        params += ', status=no';
        params += ', toolbar=no';
        newwin = window.open(base + 'Network/ShowPins?current=' + $(this).attr('data-id') + '&region=' + $(this).attr('data-region') + '&city=' + $(this).attr('data-city'), 'windowname5', params);
        if (window.focus) { newwin.focus() }
        return false;
    });

});