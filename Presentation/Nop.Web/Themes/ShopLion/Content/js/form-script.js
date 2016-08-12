	$(document).ready(function() {
		// button loader
		$('#packageForm, #hotelForm').on('hidden.bs.modal', function () {
    		//$(this).children().find(".add-loader").removeClass("disabled add-loader");
		});
		
		$(".validateForm").submit(function()
		{
			var formId = $(this).attr("id")
			var success = true;
			$(this).children().find(".form-control").each(function()
			{
				
				var validate = $(this).attr("data-validate");
				var fieldVal = $(this).val();
				//alert($(this).attr("class"));
				if(validate == "required" && (fieldVal == "" || fieldVal == "0"))
				{
					$(this).addClass("error-field");
					success = false;
				}
				else if(validate == "required" && (fieldVal != "" || fieldVal != "0"))
				{
					$(this).removeClass("error-field");
				}
				
				
			});
			var noOfrooms = $("#"+formId+" .roomSel option:selected").val();
			var noOfPerson = (noOfrooms * 3);
			var personCount = 0;
			var totalPerson = 0;
			//console.log("noOfrooms : "+noOfrooms)
			
			$("#"+formId+" .persons.visible select:not(.infant)").each(function() {
				personCount++;
				totalPerson += parseInt($(this).children("option:selected").val());
				console.log("test : "+$(this).children("option:selected").val());
				switch(formId)
				{
					case "hotelpack" : 
						break;
					case "hotelpop" :
						break;
					default:
					if(noOfPerson <= personCount)
					{
						return false;
					}	
				}
			});
			//console.log("totalPerson: "+totalPerson)
			switch(formId)
			{
				case "hotelpack" : 
					break;
				case "hotelpop" :
					break;
				default:
					if(totalPerson > 7)
					{
						$('#groupBook').modal("show");
						success = false;
					}			
			}
			if(!success)
				return false;
			else
			{
			 // $(this).children().find("button[type='submit']").addClass("disabled add-loader");
			}
		});
		/*$(".airline_city").autocomplete('AC.ashx');
		$(".airline_city").result(function (event, data, formatted) {
			$(this).val(formatted.split(",")[0] + ' (' + formatted.split(",")[1] + ')');
			var curTextboxID = $(this).attr("id");
			//
			if (curTextboxID == "FHD") {

				$("#FHDC").val(formatted.split(",")[1]);
				$("#FHA").focus();
				//alert(curTextboxID);
			}
			else if (curTextboxID == "FHA") {

				$("#FHAC").val(formatted.split(",")[1]);
				$("#dpd1").click().focus();
			}
			else if (curTextboxID == "FHD") {
				$("#FHDC").val(formatted.split(",")[1]);
				//$("#FHA").focus();
			}
			else if (curTextboxID == "FHA") {
				$("#FHAC").val(formatted.split(",")[1]);
				$("#dpd1").click().focus();
			}
			//
			//$("#councode").val(formatted.split(",")[2]);
		});*/
		$(".validate").submit(function(){
		var formSuccess = true;
		var formId = $(this).attr("id");
		$("#"+formId+".validate [data-validation='required']").each(function(){
			var fieldValue = $(this).val();
			
			var fieldType = $(this).attr("data-type");
			formSuccess = true;
			
			//var test = isNumeric(elmVal);
			//alert(fieldValue)
			if(fieldValue == "")
			{
				$(this).parent(".form-group").removeClass("has-success");
				$(this).parent(".form-group").addClass("has-error");
				formSuccess = false;
				return false;
			}
			
			else
			{
				if(validateType(fieldType, fieldValue))
				{
					$(this).parent(".form-group").addClass("has-success");
					$(this).parent(".form-group").removeClass("has-error");
				}
				else
				{
					$(this).parent(".form-group").addClass("has-error");
					$(this).parent(".form-group").removeClass("has-success");
					formSuccess = false;
					return false;
				}
			}
		});
		//alert(formSuccess)
		if(formSuccess)
		{
			return true;
			//$("#recaptcha_response_field").removeAttr("name");
		}
		else
			return false;
		
	});
	function validateType(type, fieldValue) {
    ///console.log("fieldValue : " + fieldValue)
		if (type == "email")
		{
			var emailreg = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
			return fieldValue.match(emailreg);
		}
		else if (type == "string") {
			var stringreg = /^[a-zA-Z\s]*$/;
			return fieldValue.match(stringreg);
		}
		else if (type == "numString") {
			var strnumreg = /^[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]*$/;
			return fieldValue.match(strnumreg);
		}
		else if (type == "number") {
			var numreg = /^[0-9\b]+$/;
			return fieldValue.match(numreg);
		}
		else if(type == "dropdown")
		{
			if(fieldValue == 0)
				return false;
			else
				return true;
		}
		else if(type == "any")
		{
			return true;
		}
	}

		$(".hotel_city").autocomplete('HC.ashx');
		$(".hotel_city").result(function (event, data, formatted) {
			$(this).val(formatted.split(",")[0] + ' (' + formatted.split(",")[1] + ')');
			var curTextboxID = $(this).attr("id");
			//alert(curTextboxID);
			if (curTextboxID == "HC") {
				var hotelCity = formatted.split(",")[1];
				if(hotelCity == "ABAL")
				{
					$("#hotelpack #hotelcode").val("2060");
				}
				else if(hotelCity == "BKK")
				{
					$("#hotelpack #hotelcode").val("2053");
				}
				else if(hotelCity == "HKT")
				{
					$("#hotelpack #hotelcode").val("2046");
				}
				else if(hotelCity == "BKI")
				{
					$("#hotelpack #hotelcode").val("2045");
				}
				$("#cityname").val(formatted.split(",")[0]);
				$("#HCC").val(hotelCity);
				$("#dpd5").click().focus();
				
				//alert(hotelCity)
			}
			/*else if (curTextboxID == "TRD") {
				$("#TRDC").val(formatted.split(",")[1]);
				$("#date07").click().focus();
			}
			else if (curTextboxID == "TD") {
				$("#TDC").val(formatted.split(",")[1]);
				$("#date09").click().focus();
			}*/
		});
		
		$(".roomSel").change(function(){
			var dataRef = $(this).attr("data-ref");
			var noOfrooms = $(this).val();
			$("#"+dataRef+" .room-selection").removeClass("visible");
			$(".room-adult").val("0");
			$(".adult").attr("data-ref");
			//alert(dataRef);
			if(noOfrooms > 1)
			{
				$("#"+dataRef+".hidden-panel").addClass("show");
				//$("body").addClass("overflow-hide");
				
			}
			else
			{
				$("#"+dataRef+".hidden-panel").removeClass("show");
				//$("body").removeClass("overflow-hide");
			}
			for(i=2;i<=noOfrooms;i++)			
			{
				$("#"+dataRef+" .room"+i).addClass("visible");
				var roomAdultId = $("#"+dataRef+" .room"+i).children().find(".adult").attr("data-ref");
				$("#"+roomAdultId).val("1");
				
			}
			for(i=5; i>noOfrooms;i--)			
			{
				//alert("#childAge"+i);
				$("#childAge"+i).removeClass("visible");
			}
		});
		$(".children").change(function(){
			var dataRef = $(this).attr("data-ref");
			var noOfchild = $(this).val();
			$("#"+dataRef+".chidAgeSel").removeClass("visible");
			$("#"+dataRef+".chidAgeSel .form-group").hide();
			if(noOfchild >= 1)
			{
				//alert(noOfchild)
				$("#"+dataRef).addClass("visible")
				for(i=1;i<=noOfchild;i++)			
				{
					$("#"+dataRef+".chidAgeSel .form-group.child"+i).show();
				}
			}
		});
		$(".city-search").click(function(){
			var cityCode = $(this).attr("data-code");
			var cityName = $(this).attr("data-name");
			$("#myModalLabel span").text(cityName);
			$("#AutodepCity option").removeAttr("selected");
			$("#AutodepCity option").each(function(){
				var selVal = $(this).val();
				if(selVal == cityCode)
				{
					//alert("got it !!");
					$(this).attr("selected", "selected");
				}
			});
		});
		$(".adult").change(function(){
			var adultRef = $(this).attr("data-ref");
			var noOfadult =  $(this).val();
			$("#"+adultRef).val(noOfadult);
		});
		
		$(".close-pop").click(function(){
			var dataRef = $(this).attr("data-ref");
			$("#"+dataRef).removeClass("show");
			$("body").removeClass("overflow-hide");
		});
		
		$(".toggle-menu").click(function(){
			$("#main_menu").slideToggle();
		});
		/*$(".trigger-sel").click(function() {
			open($(this).prev());
		});*/
			
		});
	function open(elem) {
		//console.log("open triggered")
		if (document.createEvent) {
			var e = document.createEvent("MouseEvents");
			e.initMouseEvent("mousedown", true, true, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
			elem[0].dispatchEvent(e);
		} else if (element.fireEvent) {
			elem[0].fireEvent("onmousedown");
		}
	}
		