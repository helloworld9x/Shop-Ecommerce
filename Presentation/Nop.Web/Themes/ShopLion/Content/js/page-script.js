$(document).ready(function() {
	/*$(".banner-action").live("click",function(){
		//alert("calling")
		var tabRef = $(this).attr("data-ref");
		if(tabRef == "flightandhotel")
		{
			$("a[data-tabid='"+tabRef+"']").click();
			$("#s2id_depCity").select2("open");
		}
		else if(tabRef == "hotels")
		{
			$("a[data-tabid='"+tabRef+"']").click();
			$("#HC").focus();
		}
	});*/
	$(".toggle-menu").click(function(){
		var menuStatus = $(this).attr("data-toggle");
		if(menuStatus == "closed")
		{
			$("#main_menu").addClass("show");
			$(this).attr("data-toggle","opened");
		}
		else if(menuStatus == "opened")
		{
			$("#main_menu").removeClass("show");
			$(this).attr("data-toggle","closed");
		}
	});
	$(".trigger-tab").click(function()
	{
		var triggerHref = $(this).attr("href");
		var triggerTarget = triggerHref.substring(1,triggerHref.length);
		//alert(triggerTarget);
		$("[data-tabid='"+triggerTarget+"']").click();
		
	});
	$(".updateForm").click(function(){
		var CityName = $(this).attr("data-city");
		var CityCode = $(this).attr("data-code");
		var hotelCode = $(this).attr("data-hcode");
		var dataTarget =  $(this).attr("data-target");
		$(dataTarget+" .modal-title label").text(CityName);
		if(dataTarget == "#packageForm")
		{
			$(dataTarget+" #arrCity1 option").text(CityName).val(CityCode);
			updateDepartureList(CityCode)
		}
		else
		{
			$(dataTarget+" #hotelcode").val(hotelCode)
			$(dataTarget+" #PHC").val(CityName)
			$(dataTarget+" #PHCC").val(CityCode);
		}
	});
	$("#arrCity, #depCity").select2();
	/*$(".form-control").live({click:function(){
			var dateTarget = $(this).attr("data-target");
			var formId = dateTarget.split("-")
			//alert("formId : "+formId[0]);
		}
	});*/
	
	var KEYCODE_ENTER = 13;
	var KEYCODE_ESC = 27;
	
	$(document).keyup(function(e) {
	  if (e.keyCode == KEYCODE_ESC) { 
	  	var hasLoader = $(".btn-primary, .btn-default").hasClass("disabled");
		if(hasLoader)
		{
			$(".btn-primary, .btn-default").removeClass("disabled add-loader");
		}
	  } 
	});
});

function updateDepartureList(removalCode)
{
	$("#depCity1").html("");
	$("#depCity").children().each(function(){
		var tagName = $(this).prop("tagName");
		console.log(tagName)
		if(tagName == 'OPTGROUP')
		{
			if($(this).children().val() != removalCode)
			{
				var grpLabel = $(this).attr("label");
				$("#depCity1").append("<optgroup label='"+grpLabel+"'>");
					$(this).children().each(function(){
						var listVal = $(this).val();
						var listText = $(this).text();
						//console.log("listVal: "+$(this).html()+" listText: "+listText);
						//$("#depCity1").append("test")
						if($(this).val() != removalCode)
							$("#depCity1").append("<option value='"+listVal+"'>"+listText+"</option>");
					});
				$("#depCity1").append("</optgroup>");
			}
		}
		else
		{
			var listVal = $(this).val();
			var listText = $(this).text();
			if($(this).val() != removalCode)
			$("#depCity1").append("<option value='"+listVal+"'>"+listText+"</option>");
		}
	});
}
