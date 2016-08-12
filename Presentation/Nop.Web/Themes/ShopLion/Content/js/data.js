 /*Dropdown Menu*/
$(function(){
	$(".dropdown").hover(            
		
		function() {
			if ($(window).width() > 768) {
			   $('.dropdown-menu', this).stop( true, true ).fadeIn("500");
				$(this).toggleClass('open');    
			}
			           
		},
		function() {
			  
			if ($(window).width() > 768) {
			   $('.dropdown-menu', this).stop( true, true ).fadeOut("500");
			$(this).toggleClass('open');    
			}            
		});
	});
 	/*Dropdown Menu ends*/  
	     
$(document).ready(function() {
	
	var curPageUrl = document.URL;
	var paramId = curPageUrl.split("#");
	if(paramId[1] == "success")
	{
		$(".notification-bar").addClass("notify-success");
		setTimeout(function(){$(".notification-bar").removeClass("notify-success")},5000);
	}
	$(".close-notify").click(function(){
		$(".notification-bar").removeClass("notify-success");
	});
	
	/*bootstrap Main Banner*/
    $('.carousel').carousel({
	    interval: 5000
	});

 	/*bootstrap Tool TIP*/
	$('[data-toggle="tooltip"]').tooltip()
	/*bootstrap Main Banner ends */
	/*tab swipe */
	/*$(".tab-content").swiperight(function() {
			
            var $tabCheck = $('#bookTab .active').prev();
			var tab = $tabCheck.find('a').attr("href");
            if ($tabCheck.length > 0)
			{
				$('#bookTab a[href="'+tab+'"]').tab('show')
			}
        });
        $(".tab-content").swipeleft(function() {
            var $tabCheck = $('#bookTab .active').next();
			var tab = $tabCheck.find('a').attr("href");
			console.log("test left : "+tab)
            if ($tabCheck.length > 0)
                $('#bookTab a[href="'+tab+'"]').tab('show')
        });*/
		/*tab swipe ends*/
	
	
	/*oneway roundtrip script*/
	$(".trip-opt").click(function(){
		var target = $(this).attr("data-target");
		var parent = $(this).attr("for");
		$(".radio-group label").removeClass("checked");
		if(parent == "roundTrip")
		{
			$(target).prop("disabled", false).removeClass("disabled").attr("data-validation","required");
			$(this).addClass("checked");
		}
		else
		{
			$(target).prop("disabled", true).addClass("disabled").attr("data-validation","");
			$(this).addClass("checked");
		}
	});
	/*oneway roundtrip script ends*/
	
	/*Adult Infant script*/
	$(document).on("change", ".adult-count", function(){
		var adultCount = $(this).val();
		var infantRef = $(this).attr("data-ref");
		$(infantRef).html("")
		if(adultCount > 2)
			adultCount = 2;
		for(i=0;i<=adultCount;i++)
		{
			$(infantRef).append("<option value="+i+">"+i+"</option>")
		}
	});
	/*Adult Infant script ends*/
	
	/*Add Persons Drop down*/
	$(document).on("click", ".btn-text-group .btn", function(){
		var dataRef = $(this).attr("data-ref")
		$(".btn-text-group .btn").removeClass("checked");
		$(this).addClass("checked");
		$("#bookingRef").val("");
		if(dataRef === "bookingReference")
		{
			$("#bookingRef").attr("placeholder","Booking Reference (PNR)").attr("maxlength","6");
			$("div[data-ref='class-change']").removeClass("clm2").addClass("clm1");
			$("div[data-ref='remove-class']").addClass("hide");
		}
		else if(dataRef === "malindoMiles")
		{
			$("#bookingRef").attr("placeholder","Malindo Miles Number").attr("maxlength","15");
			$("div[data-ref='class-change']").removeClass("clm1").addClass("clm2");
			$("div[data-ref='remove-class']").removeClass("hide");
		}
	});
	
	var triggerId = "";
	$(".addChildage").change(function(){
		var curRoom = $(this).attr("data-ref");
		var noOfChild = $(this).val();
		if(noOfChild > 0)
		{
			$(triggerId+" "+curRoom+"Childage").removeClass("hide");
			$(curRoom+" .childAge").addClass("hide");
			for(i=1;i<=noOfChild;i++)
			{
				$(triggerId+" "+curRoom+" .child"+i).removeClass("hide");
			}
		}
		else
		{
			$(triggerId+" "+curRoom+"Childage, "+curRoom+" .childAge").addClass("hide");
		}
	});
	$(".add-rooms").click(function(){
		var curRoom = $(this).attr("data-rooms");
		var rooms = parseInt(curRoom);
		var maxRooms =  parseInt($(this).attr("data-maxrooms"));
		if(rooms > 1 && rooms < maxRooms)
		{
			$(triggerId+" .remove-room").addClass("hide");
			$(this).attr("data-rooms",rooms+1);
			$(triggerId+" .room"+rooms).removeClass("hide");
			$(triggerId+" .room"+rooms+" .remove-room").removeClass("hide");
		}
		else if(rooms >= maxRooms)
		{
			$(triggerId+" .remove-room").addClass("hide");
			$(triggerId+" .room"+rooms).removeClass("hide");
			$(this).attr("data-rooms",rooms+1);
			$(this).addClass("hide");
			$(triggerId+" .room"+rooms+" .remove-room").removeClass("hide");
		}
		countRoomPersons(triggerId)
	});
	$(".remove-room").click(function(){
		var curRoom = $(this).attr("data-ref");
		var previousRoom = $(this).attr("data-prevroom");
		var btnRoom = parseInt($(triggerId+" .add-rooms").attr("data-rooms"));
		$(curRoom).addClass("hide");
		$(triggerId+" .add-rooms").removeClass("hide");
		$(triggerId+" .add-rooms").attr("data-rooms",btnRoom-1);
		$(".room"+previousRoom+" .remove-room").removeClass("hide");
		countRoomPersons(triggerId);
	});
	
	$(".add-persons").click(function(){
		triggerId = $(this).attr("data-trigger");
		$(triggerId).toggleClass("show");
	});
	$(".label-input select[data-field]").change(function(){
		countRoomPersons(triggerId)
	});
	$(".close-dropdown").click(function(){
		var target = $(this).attr("data-target");
		$(target).removeClass("show");
	});
	function countRoomPersons(target)
	{
		var adultCount = 0;
		var childCount = 0;
		var infantCount = 0;
		$(target+" .room-container:not('.hide') .label-input select[data-field]").each(function(){
			
			var personType = $(this).attr("data-field");
			if(personType.toLowerCase() == 'adult')
			{
				adultCount += parseInt($(this).val());
			}
			else if(personType.toLowerCase() == 'child')
			{
				childCount += parseInt($(this).val());
			}
			else if(personType.toLowerCase() == 'infant')
			{
				infantCount += parseInt($(this).val());
			}
		});
		var noOfRooms = parseInt($(target+" .add-rooms").attr("data-rooms"))-1;
		var roomField = $(target+" .add-rooms").attr("data-field");
		var totalRooms = noOfRooms;
		switch(target)
		{
			case "#tourpersonDropdown" :
			case "#transferpersonDropdown" :
				noOfRooms = "";
				break;
			default: 
				if(noOfRooms >1)
				{
					noOfRooms = noOfRooms+" Rooms,"
				}
				else
				{
					noOfRooms = noOfRooms+" Room,"
				}
				break;
		}
		var totalPerson = adultCount+childCount+infantCount;
		var personsExceeds = false;
		if(totalPerson > 7)
		{
			$('#groupBook').modal("show");
			personsExceeds = true;
		}
		if(totalPerson > 1)
		{
			totalPerson = totalPerson+" Persons"
		}
		else
		{
			totalPerson = totalPerson+" Person"
		}
		/*if(adultCount > 1)
		{
			adultCount = adultCount+" Adults,"
		}
		else
		{
			adultCount = adultCount+" Adult,"
		}
		if(childCount > 1)
		{
			childCount = childCount+" Children,"
		}
		else if(childCount == 1)
		{
			childCount = childCount+" Child,"
		}
		else
		{
			childCount = "";
		}
		if(infantCount > 1)
		{
			infantCount = infantCount+" Infants"
		}
		else if(infantCount == 1)
		{
			infantCount = infantCount+" Infant"
		}
		else
		{
			infantCount = "";
		}*/
		$(roomField).val(totalRooms);
		if(totalRooms > 0)
			$(".add-persons[data-trigger="+target+"]").val(noOfRooms+" "+totalPerson);
		else
			$(".add-persons[data-trigger="+target+"]").val(totalPerson);
		/*alert("no of rooms : "+(noOfRooms-1))
		alert("adultCount: "+adultCount+" childCount: "+childCount+" infantCount: "+infantCount)*/
	}
	/*Add Persons Drop down ends*/
	
	/*Persons Drop down autohide*/
	$(document).click(function(event) { 
		if(!$(event.target).closest('.custom-dropdown').length && !$(event.target).closest('.add-persons').length) {
			if($('.custom-dropdown').is(":visible")) {
				$('.custom-dropdown').removeClass("show");
			}
		}      
	});
	/*Persons Drop down autohide ends*/
	
	
	
	/*Main Menu Page selection script*/
	$(".navi .navbar-nav ul>li").each(function() {
        var activePage = $(this).hasClass("active");
		if(activePage)
			$(this).parent().parent("li").addClass("active");
    });
	/*Main Menu Page selection script ends*/
	
	/*Collapes Expand script*/
	/*$(".collapseClick").click(function() { 
		var status = $(this).attr("data-status"); 
		if(status == "on")
		{
			$(this).text('Collapse');
			$(this).attr("data-status","off").addClass("open");
		}
		else
		{
			$(this).text('Expand');
			$(this).attr("data-status","on").removeClass("open");
		}
	}); */
	/*Collapes Expand script ends*/
	
	$(".collapseTogle").click(function() { 
		var status = $(this).attr("data-status"); 
		var target = $(this).attr("data-target");
		var targetId = $(target).attr("id");  

		var CurElt = $(this);
		$(target).each(function(){
			if(status == "on")
			{
				CurElt.text('Collapse all');
				CurElt.attr("data-status","off").addClass("open");
				$(this).addClass("in").attr("aria-expanded",true).removeAttr("style");
				$("[href='#"+$(this).attr('id')+"']").removeClass("collapsed").attr("aria-expanded",true);  
			}
			else
			{
				CurElt.text('Expand all');
				CurElt.attr("data-status","on").removeClass("open");
				$(this).removeClass("in").attr("aria-expanded",false).css("height","0px");
				$("[href='#"+$(this).attr('id')+"']").addClass("collapsed").attr("aria-expanded",false);
			}
		});
		
	}); 
	
	
	
	
	/*Highlight text on Search script*/
	$('.text-search').bind('keyup change', function(ev) {
        var searchTerm = $(this).val();
        $('.find-text').removeHighlight();
        if (searchTerm) {
            $('.find-text').highlight( searchTerm );
        }
    });
	/*Highlight text on Search script ends*/
	
	/*Popup Booking form script*/
	$(".updateForm").click(function(){
			var depcity = $(this).attr("data-depcity");
			var depcode = $(this).attr("data-depcode");
			var arrcity = $(this).attr("data-arrcity");
			var arrcode = $(this).attr("data-arrcode");
			var target = $(this).attr("data-target");
			//$(target+" #departCity, "+target+" #arrivalCity").removeAttr("data-ref");
			$(target+" #departCity").val(depcity+" ("+depcode+")").next("input[type='hidden']").val(depcode);
			$(target+" #arrivalCity").val(arrcity+" ("+arrcode+")").next("input[type='hidden']").val(arrcode);
			updateArrivalList("#arrivalCity", depcode);
	});
	/*Popup Booking form script ends*/
	
	/*Smooth page Scroll script*/
	  $('.smooth-scroll').click(function() {
		if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') && location.hostname == this.hostname) {
		  var target = $(this.hash);
		  target = target.length ? target : $('[name=' + this.hash.slice(1) +']');
		  if (target.length) {
			$('html,body').animate({
			  scrollTop: target.offset().top
			}, 500);
			return false;
		  }
		}
	  });
	/*Smooth page Scroll script ends*/
	
	/*Escape Key press event script*/
	$(document).keyup(function(e) {
	  	if (e.keyCode == 27) { 
			$('.modal').modal('hide');
		}
	});
	/*Escape Key press event script ends*/
		//var test = $('.main-menu').offset().top;
		//console.log(test)
	 // browser window scroll (in pixels) after which the "back to top" link is shown
		 var offset = 300,
		  //browser window scroll (in pixels) after which the "back to top" link opacity is reduced
		  offset_opacity = 1200,
		  //duration of the top scrolling animation (in ms)
		  scroll_top_duration = 700,
		  bodyOffset = 10,
		  //formOffset = 79;
		  homeTabOffset = 350;
			$header_to_top = $('.scroll-header, .inner-menu');
			$back_to_top = $('.cd-top');
			//$form_to_top = $('.floating');
			$home_tabs = $('.home-tabs');
		  //grab the "back to top" link
		 
		 //hide or show the "back to top" link
		 $(window).scroll(function(){
		  ( $(this).scrollTop() > offset ) ? $back_to_top.addClass('cd-is-visible') : $back_to_top.removeClass('cd-is-visible cd-fade-out');
		  ( $(this).scrollTop() > bodyOffset ) ? $header_to_top.addClass('fixed') : $header_to_top.removeClass('fixed');
		 // ( $(this).scrollTop() > formOffset ) ? $form_to_top.addClass('fixed') : $form_to_top.removeClass('fixed');
		 //console.log($(this).scrollTop())
		  if($(this).scrollTop() > homeTabOffset)
		  {
			 $home_tabs.addClass("fixed") 
		  }
		  else
		  {
			  $home_tabs.removeClass("fixed") 
		  }
		  	//bodyOffset = $("body").offset();
			// console.log(bodyOffset)
		 });
		
		 //smooth scroll to top
		 $back_to_top.on('click', function(event){
		  event.preventDefault();
		  $('body,html').animate({
		   scrollTop: 0 ,
			}, scroll_top_duration
		  );
		 });
		 
		/* Menu highlight Script */
		var href = $(location).attr('href');
		var splitUrl = href.split("/");
		if(splitUrl[3] == "")
		{
			$(".nav.navbar-nav>li:first-child").addClass("active");
		}
		else if(splitUrl[3] == "news-events" && splitUrl.length == 4)
		{
			$(".nav.navbar-nav>li a[href='/"+splitUrl[3]+"']").parent().addClass("active");
		}
		else if(splitUrl[5] != "" && splitUrl[5] != undefined && splitUrl[5] != "undefined")
		{
			$(".nav.navbar-nav>li a[href='/"+splitUrl[3]+"']").parent().addClass("active");
			$(".nav.navbar-nav>li li a[href='/"+splitUrl[3]+"/"+splitUrl[4]+"']").parent().addClass("active")
			//console.log()
		}
		/* Menu highlight Script ends*/
		
		/*get query string parameter*/
		
		var departCity = GetURLParameter("dcity");
		var arrCity = GetURLParameter("acity");
		var departCode = GetURLParameter("dcode");
		var arrCode = GetURLParameter("acode");
		var updateCityExist = $(".update-city").length;	 
		if(updateCityExist && (departCity != "" && departCity != undefined))
		{
			$(".update-city input#departCity").val(departCity.replace("%20"," ")+" ("+departCode+")").next("input[type='hidden']").val(departCode);
			$(".update-city input#arrivalCity").val(arrCity.replace("%20"," ")+" ("+arrCode+")").next("input[type='hidden']").val(arrCode);
			updateArrivalList("#arrivalCity", departCode);
		}
		//console.log(departCity+" : "+arrCity)
		function GetURLParameter(sParam)
		{
			var sPageURL = window.location.search.substring(1);
			var sURLVariables = sPageURL.split('&');
			for (var i = 0; i < sURLVariables.length; i++) 
			{
				var sParameterName = sURLVariables[i].split('=');
				if (sParameterName[0] == sParam) 
				{
					return sParameterName[1];
				}
			}
		}
		/*get query string parameter ends*/	
});

$(document).ready(function() {
	//activate flags
    $('#options').flagStrap({
        countries: {
            "AU": "Australia",
            "GB": "United Kingdom", 
            "US": "United States"
        },
        buttonSize: "btn-sm",
        buttonType: "btn-link",
        labelMargin: "10px",
        scrollable: false,
        scrollableHeight: "350px"
    });
});
