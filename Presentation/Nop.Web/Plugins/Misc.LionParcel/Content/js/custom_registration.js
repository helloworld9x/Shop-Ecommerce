var base = $('#base').text();
var formPos = $('form#pos-form'),
        formCus = $('form#customer-form'),
		mesPos  = $('#message-pos'),
		mesCus  = $('#message-customer');
var message   = {
    Seletor:function(sel){
        if(typeof(sel)==='undefined') sel = $('#message-return');
        return sel;
    },
    T_error:function(mess){
        return '<div class="alert alert-dismissable alert-danger" style="margin-bottom:4px;padding:2px;"><button type="button" class="close" data-dismiss="alert">×</button>'+mess+'</div>';
    },
    T_success:function(mess){
        return '<div class="alert alert-dismissable alert-success" style="margin-bottom:4px;padding:2px;"><button type="button" class="close" data-dismiss="alert">×</button>'+mess+'</div>';
    },
    Error: function (mess, mode, sel) {

        if(typeof(mode)==='undefined') mode = 'replace';
        def_selector = message.Seletor(sel);
        if(mode == 'add'){
            def_selector.append(message.T_error(mess)).fadeIn();
        }
        else if(mode == 'replace'){
            def_selector.html(message.T_error(mess)).fadeIn();   
        }
        //$('html, body').animate({
        //    scrollTop: def_selector.offset().top
        //}, 500);
    },
    Success: function (mess, mode, sel) {
    
        if(typeof(mode)==='undefined') mode = 'replace';
        def_selector = message.Seletor(sel);
        if(mode == 'add'){
            def_selector.append(message.T_success(mess)).fadeIn();
        }
        else if(mode == 'replace'){
            def_selector.html(message.T_success(mess)).fadeIn(); 
        }
        //$('html, body').animate({
        //    scrollTop: def_selector.offset().top
        //}, 1000);
    },
    Clean:function(){
        var sel = message.Seletor();
        sel.html('');
    },
    Loading:function(){
        var sel = message.Seletor();  
        sel.hide().html('<img src="/Themes/LionParcel/Content/images/ajax-loader.gif"> Please Wait..').fadeIn('fast');
    }
};

$(document).ready(function(){
	$(function() {
		$('.datepickers').datepicker();
	 });

	$("#warga").hide();
	$("#warga2").hide();
    $('#lokasi').hide();
	$("#luas").hide();

    $('#pl_location_type').on('change',function(){
        var field = $('input[name="pl_location_other"]');
        if(!$(this).val()){
            field.fadeIn(500);
        }
        else{
            field.fadeOut(500);
        }
    });

    $('.province_change').on('change',function(){
        var c_selector = $(this).attr('data-selector');
        $.ajax({
            type: 'post',
            url: "/ping-get-request?url=" + 'http://www.lionexpress.co.id/Network/GetCity?province=' + $(this).val(),
            timeout  : 15000,
            //work with the response
            success: function (data) {
                var response = JSON.parse(data);
                var option = '<option value="">-Pilih Kota-</option>';
                    if(response.status){
                        $.each(response.message,function(key,value){
                               option += '<option value="'+value['Alias']+'">'+value['Nama_Kota']+'</option>';
                        }); 
                        var select = $('select[name="'+c_selector+'"]');
                        select.empty();
                        select.append(option);
                    }
                    else{
                        var select = $('select[name="'+c_selector+'"]');
                        select.empty();
                        select.append(option);
                    }
            }
        });
    });
    
//	var formCus = $('form#customer-form'),
//        proposeLocation = $('form#propose-location-form'),
//        personalRegistration = $('form#personal-reg'),
//        companyRegistration = $('form#company-reg'),

//		formPos = $('form#pos-form'),
//		companyAtt = $('form#company-attachment'),
//        personalAtt = $('form#personal-attachment'),
//		iconCus = $('#customer-icon'),
//		iconPos = $('#pos-icon'),
//        b__s2   = $('button.b_to_step2'),
//        b__s3   = $('button.b_to_step3');

var proposeLocation = $('form#propose-location-form'),
        personalRegistration = $('form#personal-reg'),
        companyRegistration = $('form#company-reg'),

	
		companyAtt = $('form#company-attachment'),
        personalAtt = $('form#personal-attachment'),
		
        b__s2   = $('button.b_to_step2'),
        b__s3   = $('button.b_to_step3');
    var submit = {
        p_location : function(ref){
            var location    =   $.ajax({
                type     : proposeLocation.attr('method'),
                url      : proposeLocation.attr('action')+'?submit_type='+ref,
                dataType : 'json',
                data     : proposeLocation.serialize(),
                processData: false,
                cache: false,
                timeout  : 1500000,
                beforeSend : function(){
                    message.Loading();
                }
            });
            location.done(function (response) {
               
                if(response.status){
                    message.Success(response.message.message,'replace',mesPos);
                    $('#id_pos_pers').val(response.message.id_pos);
                    $('.step-1').fadeOut(function(){
                        $('.step-2[data-ref="'+ref+'"]').fadeIn();
                    });
                }
                else{
                    if(response.message.error_all){
                        message.Error(response.message.error_all,'replace',mesPos);
                        $.each(response.message.error,function(key,value){
                            if(value) $('[name="'+key+'"]').css({"background-color":"#f2dede","color":"#b94a48"});
                        }); 
                    }
                }
            });
            location.fail(function(xhr,response){
                message.Error(response,'replace',mesPos);
            });
        },
        p_registration: function (ref) {
            var personal    =   $.ajax({
                type     : personalRegistration.attr('method'),
                url      : personalRegistration.attr('action')+'?submit_type='+ref,
                dataType : 'json',
                data     : personalRegistration.serialize(),
                timeout  : 15000000,
                processData: false,
                cache: false,
                beforeSend : function(){
                    message.Loading();
                }
            }); 
            personal.done(function (response) {
                debugger;
                if(response.status){
                    message.Success(response.message.message, 'replace', mesPos);
                    $('#id_pos_pers_att').val(response.message.id_pos);
                    $('.step-2').fadeOut(function(){
                        personalAtt.fadeIn();
                    });
                }
                else{
                    if(response.message.error_all){
                        message.Error(response.message.error_all,'replace',mesPos);
                        $.each(response.message.error,function(key,value){
                            if(value) $('[name="'+key+'"]').css({"background-color":"#f2dede","color":"#b94a48"});
                        }); 
                    }
                }
            });
            personal.fail(function(xhr,response){
                message.Error(response,'replace',mesPos);
            });
        },
        c_registration : function(ref){
            var company    =   $.ajax({
                type     : companyRegistration.attr('method'),
                url      : companyRegistration.attr('action')+'?submit_type='+ref,
                dataType : 'json',
                data     : companyRegistration.serialize(),
                timeout  : 15000000,
                processData: false,
                cache: false,
                beforeSend : function(){
                    message.Loading();
                }
            }); 
            company.done(function(data){
                if(data.status){
                    message.Success(data.message.message, 'replace', mesPos);
                    $('#id_pos_comp_att').val(response.message.id_pos);
                    $('.step-2').fadeOut(function(){
                        companyAtt.fadeIn();
                    });
                }
                else{
                    if(response.message.error_all){
                        message.Error(response.message.error_all,'replace',mesPos);
                        $.each(response.message.error,function(key,value){
                            if(value) $('[name="'+key+'"]').css({"background-color":"#f2dede","color":"#b94a48"});
                        }); 
                    }
                }
            });
            company.fail(function(xhr,response){
                message.Error(response,'replace',mesPos);
            });
        }
    };

    var locationButton = proposeLocation.find('button[type="submit"]'),
        personalButton = personalRegistration.find('button[type="submit"]'),
        companyButton = companyRegistration.find('button[type="submit"]');

    locationButton.on('click', function () {
            var type_location = $(this).attr('ref');
            proposeLocation.attr('data-submit',type_location);
             proposeLocation.validate();
             if (!proposeLocation.valid()) return;
            proposeLocation.on('submit',function(event){
                event.preventDefault();
                submit.p_location(type_location);
            });

        });
        
        personalButton.on('click',function(){
        var type_location = $(this).attr('ref');
            personalRegistration.validate();
                if (!personalRegistration.valid()) return;
            personalRegistration.on('submit',function(event){
                event.preventDefault();
                submit.p_registration(type_location);
            });

        });

        companyButton.on('click',function(){
        var type_location = $(this).attr('ref');
         c_registration.validate();
                if (!c_registration.valid()) return;
            companyRegistration.on('submit',function(event){
                event.preventDefault();
                submit.c_registration(type_location);
            });

        });
        

	
    /*formPos.on('submit',function(event){
        event.preventDefault();
        message.Loading();

        var pos    =   $.ajax({
            type     : $(this).attr('method'),
            url      : $(this).attr('action'),
            dataType : 'json',
            data     : $(this).serialize(),
            timeout  : 15000
        }); 
        pos.done(function(response){
            if(response.status){
                message.Success(response.message.message,'replace',mesPos);
                formPos.find('input, textarea, select').val('');
                formPos.addClass('finish');
                formAtt.addClass('show-on');
                $('input[name="id_pos"]').val(response.message.id_pos);

                formAtt.fadeIn(function(){
                    $(this).addClass('animated fadeInUp');
                });
                
                $("#tax_button").uploadify('settings','formData', {'id_exist':$('input[name="id_pos"]').val()} );
                $("#license_button").uploadify('settings','formData', {'id_exist':$('input[name="id_pos"]').val()} );
                $("#identity_button").uploadify('settings','formData', {'id_exist':$('input[name="id_pos"]').val()} );
                $("#certificate_button").uploadify('settings','formData', {'id_exist':$('input[name="id_pos"]').val()} );
            }
            else{
                if(response.message.error_all){
					message.Error(response.message.error_all,'replace',mesPos);
					$.each(response.message.error,function(key,value){
						if(value) $('[name="'+key+'"]').css({"background-color":"#f2dede","color":"#b94a48"});
					});	
				}
            }
        });
        pos.fail(function(xhr,response){
            message.Error(response,'replace',mesPos);
        });
    });*/

//    companyAtt.on('submit',function(event){
//        event.preventDefault();
//        message.Loading();

//        var pos    =   $.ajax({
//            type     : $(this).attr('method'),
//            url      : $(this).attr('action')+'?submit_type=company_3',
//            dataType : 'json',
//             processData: false,
//                cache: false,
//            data     : $(this).serialize()
//        }); 
//        pos.done(function(response){
//            if(response.status){
//                companyAtt.addClass('finish');
//                message.Success(response.message,'replace',mesPos);
//                slc.html(message.T_success('All attachment saved.'));
//            }
//            else{
//                message.Error(response.message,'replace',mesPos);
//            }
//        });
//        pos.fail(function(xhr,response){
//            message.Error(response,'replace',mesPos);
//        });
//    });

//    personalAtt.on('submit',function(event){
//        event.preventDefault();
//        message.Loading();
//      alert('')
//        var pos    =   $.ajax({
//            type     : $(this).attr('method'),
//            url      : $(this).attr('action')+'?submit_type=personal_3',
//            dataType : 'json',
//            processData: false,
//            cache: false,
//            data     : $(this).serialize()
//        }); 
//        pos.done(function(response){
//            if(response.status){
//                personalAtt.addClass('finish');
//                message.Success(response.message,'replace',mesPos);
//                slc.html(message.T_success('All attachment saved.'));
//            }
//            else{
//                message.Error(response.message,'replace',mesPos);
//            }
//        });
//        pos.fail(function(xhr,response){
//            message.Error(response,'replace',mesPos);
//        });
//    });

	formCus.on('submit', function (event) {
	    debugger;
        event.preventDefault();
        message.Loading();

        var cus    =   $.ajax({
            type     : $(this).attr('method'),
            url      : $(this).attr('action'),
            dataType : 'json',
            data     : $(this).serialize(),
            timeout  : 15000
        }); 
        cus.done(function(response){
            if(response.status){
                message.Success(response.message.message,'replace',mesCus);
                formCus.find('input, textarea, select').val('');
            }
            else{
                if(response.message.error_all){
                    message.Error(response.message.error_all,'replace',mesCus);
                    $.each(response.message.error,function(key,value){
                        if(value) $('[name="'+key+'"]').css({"background-color":"#f2dede","color":"#b94a48"});
                    }); 
                }
            }
        });
        cus.fail(function(xhr,response){
            message.Error(response,'replace',mesCus);
        });
    });

});

var map;
  var geocoder;
  var centerChangedLast;
  var reverseGeocodedLast;
  var currentReverseGeocodeResponse;

  function initialize() {
    var latlng = new google.maps.LatLng(-6.258092209473512,106.79673067700197);
    var myOptions = {
      zoom: 13,
      center: latlng,
      mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
    geocoder = new google.maps.Geocoder();


    setupEvents();
    centerChanged();
  }

  function setupEvents() {
    reverseGeocodedLast = new Date();
    centerChangedLast = new Date();

    setInterval(function() {
      if((new Date()).getSeconds() - centerChangedLast.getSeconds() > 1) {
        if(reverseGeocodedLast.getTime() < centerChangedLast.getTime())
          reverseGeocode();
      }
    }, 1000);

    google.maps.event.addListener(map, 'zoom_changed', function() {
      document.getElementById("zoom_level").innerHTML = map.getZoom();
    });

    google.maps.event.addListener(map, 'center_changed', centerChanged);

    google.maps.event.addDomListener(document.getElementById('crosshair'),'dblclick', function() {
       map.setZoom(map.getZoom() + 1);
    });

  }

  function getCenterLatLngText() {
    return map.getCenter().lat() +', '+ map.getCenter().lng();
  }

  function centerChanged() {
    centerChangedLast = new Date();
    var latlng = getCenterLatLngText();
    document.getElementById('latlng').value = latlng;
    document.getElementById('formatedAddress').innerHTML = '';
    currentReverseGeocodeResponse = null;
  }

  function reverseGeocode() {
    reverseGeocodedLast = new Date();
    geocoder.geocode({latLng:map.getCenter()},reverseGeocodeResult);
  }

  function reverseGeocodeResult(results, status) {
    currentReverseGeocodeResponse = results;
    if(status == 'OK') {
      if(results.length == 0) {
        document.getElementById('formatedAddress').innerHTML = 'None';
      } else {
        document.getElementById('formatedAddress').innerHTML = results[0].formatted_address;
      }
    } else {
      document.getElementById('formatedAddress').innerHTML = 'Error';
    }
  }


  function geocode() {
    var address = document.getElementById("address").value;
    geocoder.geocode({
      'address': address,
      'partialmatch': true}, geocodeResult);
  }

  function geocodeResult(results, status) {
    if (status == 'OK' && results.length > 0) {
      map.fitBounds(results[0].geometry.viewport);
    } else {
      alert("Geocode was not successful for the following reason: " + status);
    }
  }

  function addMarkerAtCenter() {
    var marker = new google.maps.Marker({
        position: map.getCenter(),
        map: map
    });

    var text = 'Lat/Lng : (' + getCenterLatLngText()+')';
    if(currentReverseGeocodeResponse) {
      var addr = '';
      if(currentReverseGeocodeResponse.size == 0) {
        addr = 'None';
      } else {
        addr = currentReverseGeocodeResponse[0].formatted_address;
      }
      text = text + '<br>' + 'Alamat : ' + addr;
    }

    var infowindow = new google.maps.InfoWindow({ content: text });

    google.maps.event.addListener(marker, 'click', function() {
      infowindow.open(map,marker);
    });
  }

function show_map(){
    $('#map-show').show();
    initialize();
}

function showwarga(){var nat=$("#rb_nationality option:selected").val();   if(nat=='other'){jQuery('#warga').fadeIn(500);}else{jQuery('#warga').fadeOut(500)}}
function hidewarga(){jQuery('#warga').fadeOut(500);}
function showwarga2(){var nat=$("#ci_nationality option:selected").val();   if(nat=='other'){jQuery('#warga2').fadeIn(500);}else{jQuery('#warga2').fadeOut(500)}}
function hidewarga2(){jQuery('#warga2').fadeOut(500);}	
function showluas(){jQuery('#luas').fadeIn(500);}
function hideluas(){jQuery('#luas').fadeOut(500);}	

function validateFiles(type)
{
//debugger;
var maxSize=5242880;
var mesPos1  = $('#message-pos');

if(type=="company_3")
{


var file1=document.getElementById("certificate_button").value;
var file2=document.getElementById("tax_button").value;
var file3=document.getElementById("license_button").value;
var file4=document.getElementById("identity_button").value;
if(file1)
{
    var size = document.getElementById("certificate_button").files[0].size;
    if(size>maxSize)
    {
     message.Error('Max upload for each attachment is 5 MB.','replace',mesPos1);
    return false;
    }
}
else if(file2)
{
   var size = document.getElementById("tax_button").files[0].size;
    if(size>maxSize)
    {
     message.Error('Max upload for each attachment is 5 MB.','replace',mesPos1);
    return false;
    }
}
else if(file3)
{
    var size = document.getElementById("license_button").files[0].size;
    if(size>maxSize)
    {
     message.Error('Max upload for each attachment is 5 MB.','replace',mesPos1);
    return false;
    }
}
else if(file4)
{
var size = document.getElementById("identity_button").files[0].size;
    if(size>maxSize)
    {
     message.Error('Max upload for each attachment is 5 MB.','replace',mesPos1);
    return false;
    }
}
}


else
{

var file1=document.getElementById("family_card").value;
var file2=document.getElementById("tax").value;
var file3=document.getElementById("id_card").value;

if(file1)
{
    var size = document.getElementById("family_card").files[0].size;
    if(size>maxSize)
    {
     message.Error('Max upload for each attachment is 5 MB.','replace',mesPos1);
    return false;
    }
}
else if(file2)
{
   var size = document.getElementById("tax").files[0].size;
    if(size>maxSize)
    {
     message.Error('Max upload for each attachment is 5 MB.','replace',mesPos1);
    return false;
    }
}
else if(file3)
{
    var size = document.getElementById("id_card").files[0].size;
    if(size>maxSize)
    {
     message.Error('Max upload for each attachment is 5 MB.','replace',mesPos1);
    return false;
    }
}

}


}