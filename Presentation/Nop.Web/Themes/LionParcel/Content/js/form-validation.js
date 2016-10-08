$(document).ready(function() {
  $("form .btn").prop("disabled", false).removeClass("loader-bar");
	$(".validateForm").submit(function(){
		var formSuccess = true;
		var formId = $(this).attr("id");
		var paxValidate = true;
		$("#"+formId+" [data-validation='required']").each(function(){
			var fieldValue = $(this).val();
			var fieldType = $(this).attr("data-type");
			if(fieldValue == "")
			{
				$(this).addClass("has-error");
				formSuccess = false;
			}
			else
			{
				if(validateType(fieldType, fieldValue))
				{
					//$(this).addClass("has-success");
					$(this).removeClass("has-error");
				}
				else
				{
					$(this).addClass("has-error");
					//$(this).removeClass("has-success");
					formSuccess = false;
				}
			}
		});
		if(formId == "flightSearchForm" || formId == "FHform")
		{
			paxValidate = totalPaxValidation(formId, 7);
			if(!paxValidate)
			{
				formSuccess = false;
			}
		}
		if(formSuccess)
		{
			$("#"+formId+" button[type='submit']").prop("disabled", false).removeClass("loader-bar");
			return true;
			/*if(formId == "webCheckInform")
			{
				var querystring = $( "#"+formId).serialize();
				var submitUrl = $( "#"+formId).attr("action")+"#"+querystring;
				window.location.href = submitUrl;
				//$( "#"+formId).sumbit();
				console.log(submitUrl);
				return false;	
			}
			else
			{
					
			}*/
			
		}
		else
			return false;
	});
	
	
	function totalPaxValidation(formId, limitedMax)
	{
		var personCount = 0;
		var totalPerson = 0;
		$("#"+formId+" .custom-dropdown select").each(function() {
			personCount++;
			totalPerson += parseInt($(this).children("option:selected").val());
			if(totalPerson > limitedMax)
			{
				$(this).addClass("error-field");
				
			}
			else
			{
				$(this).removeClass("error-field");
			}
		});
		switch(formId)
		{
			case "hotelpack" : 
				break;
			case "hotelpop" :
				break;
			default:
			if(totalPerson > limitedMax)
			{
				$('#groupBook').modal("show");
				return false;
			}	
			else
			{
				return true;
			}
		}
	}
	
	function validateType(type, fieldValue) {
    	console.log("fieldValue : " + fieldValue)
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
			
			var strnumreg = /^[a-zA-Z0-9]*$/;
			return fieldValue.match(strnumreg);
		}
		else if (type == "number") {
			var numreg = /^[0-9\b]+$/;
			return fieldValue.match(numreg);
		}
		else if(type == "date")
		{
			// /^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$/
			var date = /^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)\d\d$/;
			return fieldValue.match(date);
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
			var anyChar = /^[a-zA-Z0-9\s!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]*$/;
			return fieldValue.match(anyChar);
			//return true;
		}
	}
	/*input Key restriction event script*/
	$(document).on("keypress", "input[data-restriction]", function(ev){
		var keyRestriction = $(this).attr("data-restriction")
		return restrictInput(ev, keyRestriction);
	});
	/*input Key restriction event script ends*/
});
/*input Key restriction function script*/
function restrictInput(e, kr) {
	var k = e.charCode;// || window.event;;
	//alert(k)
	if(kr == "alpha")
		return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || k == 0); //|| (k >= 48 && k <= 57)
	else if(kr == "onlyAlpha")
		return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 0);
	else if(kr == "alphaNum")
		return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57) || k == 0);
	else if(kr == "onlyAlphaNum")
		return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || (k >= 48 && k <= 57) || k == 0);
	else if(kr == "numeric")
		return (k == 8 || (k >= 48 && k <= 57) || k == 0);
}
/*input Key restriction function script ends*/