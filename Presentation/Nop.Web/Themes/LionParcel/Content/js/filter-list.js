$(document).ready(function() {
	$('.search').keyup(function(){
		var display = 0;
		var listCount = 0;
		var noResult = false;
		var searchText = $(this).val().toLowerCase();
		var dataRef = $(this).attr("data-ref");
		$(".country").removeClass("hide");
		$('.city-list ul > li').each(function(){
			var country = $(this).hasClass("country");
			if(country && searchText != "")
			{
				$(".country").addClass("hide");
			}
			var currentLiText = $(this).text().toLowerCase();
			//alert(currentLiText)
			//console.log(currentLiText)
			var showCurrentLi = currentLiText.indexOf(searchText) !== -1;
			//console.log(showCurrentLi)
			if(showCurrentLi)
				noResult = true;
				
			$(this).toggle(showCurrentLi);
			display = $(this).css("display");
			if(display == "none")
			{
				$(this).addClass("empty");
			}
			else
			{
				$(this).removeClass("empty");
			}
		}); 
		
		if(!noResult)
		{
			///console.log("if : "+noResult);
			$(dataRef+" .no-result").removeClass("hide");
		}
		else
		{
			//console.log("else : "+noResult);
			$(dataRef+" .no-result").addClass("hide");
		}
		$('.city-list ul').each(function(){
			display = $(this).children("li").length;
			listCount = $(this).children(".empty").length;
			var currentListLength = display - listCount;
			//console.log(display+" : "+listCount);
			if(display == listCount && (display != 0 && listCount !=0))
			{
				$(this).addClass("hide");
				$(this).children(".country").addClass("hide");//.css("display","none");
			}
			else
			{
				//console.log("else");
				$(this).removeClass("hide");
				$(this).children(".country").removeClass("hide").css("display","list-item");
			}
		});
		
	});
		$(document.body).on('click', '.city-list ul li:not(.country)', function() {
		//alert("test")
		var selectedItem = $(this).text();
		var splitItem = selectedItem.split("(");
		var selCityname = splitItem[0].trim();
		var selCitycode = splitItem[1].substring(splitItem[1].indexOf("("), splitItem[1].length-1);
		var refInput = $(this).parent().parent().parent().attr("data-ref");
		var nextTarget =  $(this).parent().parent().parent().attr("data-target");
		$(refInput).val(selectedItem);
		$(refInput).next("input[type='hidden']").val(selCitycode);
		var dropDownRef =$(refInput).attr("data-ref");
		 $(dropDownRef).addClass("hide");
		 if(nextTarget == "#arrivalCity" || nextTarget == "#FHarrivalCity")
		 	updateArrivalList(nextTarget, selCitycode);
		$(nextTarget).focus();
	});
	$(".filter-dropdown").focus(function(){
		$(".filter-list").addClass("hide");
		$(".no-result").addClass("hide");
		var refId = $(this).attr("data-ref");
		$(refId).removeClass("hide");
		var filterInput = $(refId).children().find(".search").focus().val(""); 
		resetFilterList();
		
	});
	$(".close-filter").click(function(){
		$(".filter-list").addClass("hide");
		resetFilterList();
	});
	/*$(document).click(function(event) { 
		if(!$(event.target).closest('.filter-list').length && !$(event.target).closest('.search').length) {
			if($('.filter-list').is(":visible")) {
				
				$('.filter-list').addClass("hide");
			}
		}  
		if(!$(event.target).closest('.custom-dropdown, .fake-input').length) {
			if($('.custom-dropdown').is(":visible")) {
				$('.custom-dropdown').removeClass("show");
			}
		}      
	});*/
	function resetFilterList()
	{
		$('.city-list ul li').each(function(){
			$(this).removeClass("empty hide").removeAttr("style");
			$(this).parent("ul").removeClass("empty hide");
		});
	}
	
});
function updateArrivalList(listTarget, selectedCity)
	{
		///alert(listTarget)
		if(selectedCity == "PER")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">India</li><li data-value=="ATQ">Amritsar (ATQ)</li><li data-value=="DEL">Delhi (DEL)</li><li data-value=="COK">Kochi (COK)</li><li data-value=="BOM">Mumbai (BOM)</li><li data-value=="TRZ">Tiruchirappalli (TRZ)</li><li data-value=="TRV">Trivandrum (TRV)</li><li data-value=="VTZ">Visakhapatnam (VTZ)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="BDO">Bandung (BDO)</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="DPS">Denpasar Bali (DPS)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="PKU">Pekanbaru (PKU)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="AOR">Alor Setar (AOR)</li><li data-value=="IPH">Ipoh (IPH)</li><li data-value=="JHB">Johor Bahru (JHB)</li><li data-value=="KTE">Kerteh (KTE)</li><li data-value=="KBR">Kota Bahru (KBR)</li><li data-value=="BKI">Kota Kinabalu (BKI)</li><li data-value=="KUL">Kuala Lumpur (KUL)</li><li data-value=="TGG">Kuala Terengganu (TGG)</li></ul></div><div class="city-list"><ul> <li class="country">Malaysia</li><li data-value=="KUA">Kuantan (KUA)</li><li data-value=="KCH">Kuching (KCH)</li><li data-value=="LGK">Langkawi (LGK)</li><li data-value=="MKZ">Melaka (MKZ)</li><li data-value=="PEN">Penang (PEN)</li><li data-value=="SZB">Subang (SZB)</li></ul> <ul> <li class="country">Singapore</li><li data-value=="SIN">Singapore (SIN)</li></ul></div>');
		}
		else if(selectedCity == "DAC")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="DPS">Denpasar Bali (DPS)</li><li data-value=="CGK">Jakarta (CGK)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="BKI">Kota Kinabalu (BKI)</li><li data-value=="KUL">Kuala Lumpur (KUL)</li><li data-value=="KCH">Kuching (KCH)</li><li data-value=="LGK">Langkawi (LGK)</li><li data-value=="PEN">Penang (PEN)</li></ul></div><div class="city-list"> <ul> <li class="country">Singapore</li><li data-value=="SIN">Singapore (SIN)</li></ul> <ul> <li class="Thailand"></li><li data-value=="DMK">Bangkok (DMK)</li></ul></div>');
		}
		else if(selectedCity == "CAN")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="MDC">Manado (MDC)</li></ul></div>');
		}
		else if(selectedCity == "HKG")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="DPS">Denpasar (Bali) (DPS)</li></ul></div>');
			
		}
		else if(selectedCity == "ATQ" || selectedCity == "DEL" || selectedCity == "BOM" || selectedCity == "TRZ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="DPS">Denpasar Bali (DPS)</li><li data-value=="CGK">Jakarta (CGK)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="BKI">Kota Kinabalu (BKI)</li><li data-value=="KUL">Kuala Lumpur (KUL)</li><li data-value=="KCH">Kuching (KCH)</li><li data-value=="LGK">Langkawi (LGK)</li><li data-value=="PEN">Penang (PEN)</li></ul></div><div class="city-list"> <ul> <li class="country">Singapore</li><li data-value=="SIN">Singapore (SIN)</li></ul></div>'); 
		}
		else if(selectedCity == "COK" || selectedCity == "TRV" || selectedCity == "VTZ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="DPS">Denpasar Bali (DPS)</li><li data-value=="CGK">Jakarta (CGK)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="BKI">Kota Kinabalu (BKI)</li><li data-value=="KUL">Kuala Lumpur (KUL)</li><li data-value=="KCH">Kuching (KCH)</li><li data-value=="LGK">Langkawi (LGK)</li><li data-value=="PEN">Penang (PEN)</li></ul></div><div class="city-list"> <ul> <li class="country">Singapore</li><li data-value=="SIN">Singapore (SIN)</li></ul></div><div class="city-list"> <ul> <li class="country">Thailand</li><li data-value=="DMK">Bangkok (DMK)</li></ul></div>');
		}
		else if(selectedCity == "AMQ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="FKQ">Fak Fak (FKQ)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="DJJ">Jayapura (DJJ)</li><li data-value=="KNG">Kaimana (KNG)</li><li data-value=="MKW">Manokwari (MKW)</li><li data-value=="NBX">Nabire (NBX)</li><li data-value=="SOQ">Sorong (SOQ)</li><li data-value=="SUB">Surabaya (SUB)</li><li data-value=="LUV">Tual (LUV)</li><li data-value=="UPG">Ujung Pandang (UPG)</li></ul></div>');
		}
		else if(selectedCity == "BXB")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="SOQ">Sorong (SOQ)</li></ul></div>');
		}
		else if(selectedCity == "BJW")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="KOE">Kupang (KOE)</li><li data-value=="LBJ">Labuan Bajo (LBJ)</li></ul></div>');
		}
		else if(selectedCity == "BPN")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="BDJ">Banjarmasin (BDJ)</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="BEJ">Berau (BEJ)</li><li data-value=="DPS">DENPASAR BALI (DPS)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="HLP">JAKARTA HALIM PERDANAKUSUMA (HLP)</li><li data-value=="DJJ">Jayapura (DJJ)</li><li data-value=="JOG">Jogjakarta (JOG)</li><li data-value=="LOP">Lombok, Mataram (LOP)</li><li data-value=="MDC">Manado (MDC)</li><li data-value=="PLW">Palu (PLW)</li><li data-value=="SRG">Semarang (SRG)</li><li data-value=="SUB">Surabaya (SUB)</li><li data-value=="TRK">Tarakan (TRK)</li><li data-value=="UPG">Ujung Pandang (UPG)</li></ul></div>');
		}
		else if(selectedCity == "BTJ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li></ul></div>');
		}
		else if(selectedCity == "TKG")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="CGK">Jakarta (CGK)</li></ul></div>');
		}
		else if(selectedCity == "BDO")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul> <ul> <li class="country">India</li><li data-value=="TRV">Trivandrum (TRV)</li><li data-value=="VTZ">Visakhapatnam (VTZ)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="BDJ">Banjarmasin (BDJ)</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="DPS">Denpasar (Bali) (DPS)</li><li data-value=="JOG">Jogjakarta (JOG)</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li><li data-value=="PLM">PALEMBANG (PLM)</li><li data-value=="SRG">Semarang (SRG)</li><li data-value=="SUB">Surabaya (SUB)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="KUL">Kuala Lumpur (KUL)</li></ul></div>');
		}
		else if(selectedCity == "BDJ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="BPN">Balikpapan (BPN)</li><li data-value=="BDO">Bandung (BDO)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="JOG">Jogjakarta (JOG)</li><li data-value=="KBU">Kotabaru (KBU)</li><li data-value=="PLM">Palembang (PLM)</li><li data-value=="SRG">Semarang (SRG)</li><li data-value=="SUB">Surabaya (SUB)</li></ul></div>');
		}
		else if(selectedCity == "DQJ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="SUB">Surabaya (SUB)</li></ul></div>');
		}
		else if(selectedCity == "BTH")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value="PER">Perth (PER)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="BPN">Balikpapan (BPN)</li><li data-value=="BDO">Bandung (BDO)</li><li data-value=="BKS">Bengkulu (BKS)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="DJB">Jambi (DJB)</li><li data-value=="JOG">Jogjakarta (JOG)</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li><li data-value=="NTX">Natuna Ranai (NTX)</li><li data-value=="PDG">Padang (PDG)</li><li data-value=="PLM">Palembang (PLM)</li><li data-value=="PGK">Pangkal Pinang (PGK)</li><li data-value=="PKU">Pekan Baru (PKU)</li><li data-value=="PNK">Pontianak (PNK)</li><li data-value=="SRG">Semarang (SRG)</li><li data-value=="DTB">Silangit (DTB)</li><li data-value=="SUB">Surabaya (SUB)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value="SZB">Subang (SZB)</li></ul></div>');
		}
		else if(selectedCity == "BUW")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="CGK">Jakarta (CGK)</li></ul></div>');
		}
		else if(selectedCity == "BKS")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="CGK">Jakarta (CGK)</li></ul></div>');
		} 
		else if(selectedCity == "BEJ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="BPN">Balikpapan (BPN)</li></ul></div>');
		}
		else if(selectedCity == "BMU")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="DPS">Denpasar (Bali) (DPS)</li></ul></div>');
		}
		else if(selectedCity == "WUB")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="TTE">Ternate (TTE)</li></ul></div>');
		}
		else if(selectedCity == "DPS")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul> <ul> <li class="country">Bangladesh</li><li data-value=="DAC">Dhaka (DAC)</li></ul> <ul> <li class="country">China</li><li data-value=="HKG">Hong Kong International Airport (HKG)</li></ul></div><div class="city-list"> <ul> <li class="country">India</li><li data-value=="ATQ">Amritsar (ATQ)</li><li data-value=="DEL">Delhi (DEL)</li><li data-value=="COK">Kochi (COK)</li><li data-value=="BOM">Mumbai (BOM)</li><li data-value=="TRZ">Tiruchirappalli (TRZ)</li><li data-value=="TRV">Trivandrum (TRV)</li><li data-value=="VTZ">Visakhapatnam (VTZ)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="MOF">Maumere (MOF)</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li><li data-value=="PKY">Palangkaraya (PKY)</li><li data-value=="PLM">Palembang (PLM)</li><li data-value=="SRG">Semarang (SRG)</li><li data-value=="SUB">Surabaya (SUB)</li><li data-value=="TMC">Tambolaka (TMC)</li><li data-value=="UPG">Ujung Pandang (UPG)</li><li data-value=="WGP">Waingapu (WGP)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="MLG">Malang (MLG)</li><li data-value=="BPN">BALIKPAPAN (BPN)</li><li data-value=="BDO">Bandung (BDO)</li><li data-value=="BMU">Bima (BMU)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="JOG">Jogjakarta (JOG)</li><li data-value=="LBJ">Labuan Bajo (LBJ)</li><li data-value=="LOP">Lombok, Mataram (LOP)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="KUL">Kuala Lumpur (KUL)</li></ul> <ul> <li class="country">Nepal</li><li data-value=="KTM">Kathmandu (KTM)</li></ul> <ul> <li class="country">Singapore</li><li data-value=="SIN">Singapore (SIN)</li></ul></div>');
		}
		else if(selectedCity == "ENE")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="KOE">Kupang (KOE)</li><li data-value=="LBJ">Labuan Bajo (LBJ)</li></ul></div>');
		}
		else if(selectedCity == "FKQ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="AMQ">Ambon (AMQ)</li><li data-value=="KNG">Kaimana (KNG)</li><li data-value=="MKW">Manokwari (MKW)</li><li data-value=="SOQ">SORONG (SOQ)</li></ul></div>');
		}
		else if(selectedCity == "GTO")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="MDC">Manado (MDC)</li><li data-value=="UPG">Ujung Pandang (UPG)</li></ul></div>');
		}
		else if(selectedCity == "GNS")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li></ul></div>');
		}
		else if(selectedCity == "HLP")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="BPN">BALIKPAPAN (BPN)</li><li data-value=="UPG">MAKASSAR (UPG)</li><li data-value=="MLG">MALANG (MLG)</li><li data-value=="MDC">Manado (MDC)</li><li data-value=="KNO">MEDAN KUALA NAMU (KNO)</li><li data-value=="SOC">SOLO (SOC)</li></ul></div>');
		}
		else if(selectedCity == "GNS")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li></ul></div>');
		}
		else if(selectedCity == "CGK")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country"> Australia </li><li data-value=="PER">Perth (PER)</li></ul> <ul> <li class="country"> Bangladesh </li><li data-value=="DAC">Dhaka (DAC)</li></ul> <ul> <li class="country"> India </li><li data-value=="ATQ">Amritsar (ATQ)</li><li data-value=="DEL">Delhi (DEL)</li><li data-value=="COK">Kochi (COK)</li><li data-value=="BOM">Mumbai (BOM)</li><li data-value=="TRZ">Tiruchirappalli (TRZ)</li><li data-value=="TRV">Trivandrum (TRV)</li><li data-value=="VTZ">Visakhapatnam (VTZ)</li></ul></div><div class="city-list"> <ul> <li class="country"> Indonesia </li><li data-value=="AMQ">Ambon (AMQ)</li><li data-value=="BPN">Balikpapan (BPN)</li><li data-value=="BTJ">Banda Aceh (BTJ)</li><li data-value=="TKG">Bandar Lampung (TKG)</li><li data-value=="BDJ">Banjarmasin (BDJ)</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="BKS">Bengkulu (BKS)</li><li data-value=="DPS">Denpasar (Bali) (DPS)</li><li data-value=="DJB">Jambi (DJB)</li><li data-value=="DJJ">Jayapura (DJJ)</li></ul></div><div class="city-list"> <ul> <li class="country"> Indonesia </li><li data-value=="JOG">Jogjakarta (JOG)</li><li data-value=="KDI">Kendari (KDI)</li><li data-value=="KOE">KUPANG (KOE)</li><li data-value=="LOP">Lombok, Mataram (LOP)</li><li data-value=="MDC">Manado (MDC)</li><li data-value=="MES">Medan (MES)</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li><li data-value=="BIK">Mokmer Airport (BIK)</li><li data-value=="PDG">Padang (PDG)</li><li data-value=="PKY">Palangkaraya (PKY)</li></ul></div><div class="city-list"> <ul> <li class="country"> Indonesia </li><li data-value=="PLM">Palembang (PLM)</li><li data-value=="PLW">Palu (PLW)</li><li data-value=="PGK">Pangkal Pinang (PGK)</li><li data-value=="PKU">Pekan Baru (PKU)</li><li data-value=="PNK">Pontianak (PNK)</li><li data-value=="SRG">Semarang (SRG)</li><li data-value=="SOC">Solo (SOC)</li><li data-value=="SUB">Surabaya (SUB)</li><li data-value=="TNJ">Tanjung Pinang (TNJ)</li><li data-value=="TRK">Tarakan (TRK)</li><li data-value=="TTE">TERNATE (TTE)</li><li data-value=="UPG">Ujung Pandang (UPG)</li></ul></div><div class="city-list"> <ul> <li class="country"> Malaysia </li><li data-value=="KUL">Kuala Lumpur (KUL)</li></ul> <ul> <li class="country"Nepal> </li><li data-value=="KTM">Kathmandu (KTM)</li></ul> <ul> <li class="country"> Saudi Arabia </li><li data-value=="JED">Jeddah (JED)</li></ul> <ul> <li class="country"> Singapore </li><li data-value=="SIN">Singapore (SIN)</li></ul><ul>  <li class="country"> Thailand </li><li data-value=="BKK">Suvarnambhumi Airport (BKK)</li></ul></div>');
		}
		else if(selectedCity == "DJB")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="PLM">Palembang (PLM)</li></ul></div>');
		}
		else if(selectedCity == "DJJ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="AMQ">Ambon (AMQ)</li><li data-value=="BPN">Balikpapan (BPN)</li><li data-value=="CGK">JAKARTA (CGK)</li><li data-value=="MKQ">Merauke (MKQ)</li><li data-value=="NBX">Nabire (NBX)</li><li data-value=="UPG">Ujung Pandang (UPG)</li></ul></div>');
		}
		else if(selectedCity == "JOG")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="BPN">Balikpapan (BPN)</li><li data-value=="BDO">Bandung (BDO)</li><li data-value=="BDJ">Banjarmasin (BDJ)</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="DPS">Denpasar (Bali) (DPS)</li><li data-value=="CGK">Jakarta (CGK)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="LOP">Lombok, Mataram (LOP)</li><li data-value=="PLM">Palembang (PLM)</li><li data-value=="SOC">Solo (SOC)</li><li data-value=="SUB">Surabaya (SUB)</li><li data-value=="UPG">Ujung Pandang (UPG)</li></ul></div>');
		}
		else if(selectedCity == "KNG")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="AMQ">Ambon (AMQ)</li><li data-value=="FKQ">Fak Fak (FKQ)</li><li data-value=="NBX">Nabire (NBX)</li><li data-value=="SOQ">Sorong (SOQ)</li></ul></div>');
		}
		else if(selectedCity == "KAZ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="MDC">Manado (MDC)</li></ul></div>');
		}
		else if(selectedCity == "KDI")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="UPG">Ujung Pandang (UPG)</li><li data-value=="WNI">Wakatobi (WNI)</li></ul></div>');
		}
		else if(selectedCity == "KBU")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="BDJ">Banjarmasin (BDJ)</li><li data-value=="UPG">Ujung Pandang (UPG)</li></ul></div>');
		}
		else if(selectedCity == "KOE")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="BJW">Bajawa (BJW)</li><li data-value=="ENE">Ende (ENE)</li><li data-value=="CGK">JAKARTA (CGK)</li><li data-value=="LOP">Lombok, Mataram (LOP)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="MOF">Maumere (MOF)</li><li data-value=="SUB">Surabaya (SUB)</li><li data-value=="TMC">Tambolaka (TMC)</li></ul></div>');
		}
		else if(selectedCity == "LBJ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="BJW">Bajawa (BJW)</li><li data-value=="DPS">Denpasar (Bali) (DPS)</li><li data-value=="ENE">Ende (ENE)</li></ul></div>');
		}
		else if(selectedCity == "LSW")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li></ul></div>');
		}
		else if(selectedCity == "LOP")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="BPN">Balikpapan (BPN)</li><li data-value=="DPS">Denpasar (Bali) (DPS)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="JOG">Jogjakarta (JOG)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="KOE">Kupang (KOE)</li><li data-value=="PLM">Palembang (PLM)</li><li data-value=="SUB">Surabaya (SUB</li></ul></div>');
		}
		else if(selectedCity == "LUW")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="UPG">Ujung Pandang (UPG)</li></ul></div>');
		}
		else if(selectedCity == "MLG")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="DPS">Denpasar(Bali) (DPS)</li><li data-value=="HLP">JAKARTA HALIM PERDANAKUSUMA (HLP)</li><li data-value=="UPG">MAKASSAR (UPG)</li><li data-value=="SUB">Surabaya (SUB)</li></ul></div>');
		}
		 else if(selectedCity == "MJU")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="UPG">Ujung Pandang (UPG)</li></ul></div>');
		}
		 else if(selectedCity == "MDC")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">China</li><li data-value=="CAN">Guangzhou (CAN)</li></ul> <ul><li class="country">Indonesia</li><li data-value=="BPN">Balikpapan (BPN)</li><li data-value=="GTO">Gorontalo (GTO)</li><li data-value=="HLP">Halim Perdanakusuma International Airport (HLP)</li><li data-value=="CGK">Jakarta (CGK)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="KAZ">Kau (KAZ)</li><li data-value=="MNA">Melangguane (MNA)</li><li data-value=="SOQ">Sorong (SOQ)</li><li data-value=="SUB">Surabaya (SUB)</li><li data-value=="NAH">Tahuna (NAH)</li><li data-value=="TTE">Ternate (TTE)</li><li data-value=="UPG">Ujung Pandang (UPG)</li></ul></div>');
		}
		 else if(selectedCity == "MKW")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="AMQ">Ambon (AMQ)</li><li data-value=="FKQ">Fak Fak (FKQ)</li></ul></div>');
		}
		 else if(selectedCity == "MOF")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="DPS">Denpasar(Bali) (DPS)</li><li data-value=="KOE">Kupang (KOE)</li></ul></div>');
		}
		 else if(selectedCity == "MES")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="PKU">Pekan Baru (PKU)</li></ul></div>');
		}
		 else if(selectedCity == "KNO")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="BTJ">Banda Aceh (BTJ)</li><li data-value=="BDO">Bandung (BDO)</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="DPS">Denpasar (Bali) (DPS)</li><li data-value=="GNS">Gunung Sitoli (GNS)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="HLP">JAKARTA HALIM PERDANAKUSUMA (HLP)</li><li data-value=="LSW">Lhokseumawe (LSW)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="MEQ">Meulaboh (MEQ)</li><li data-value=="PDG">Padang (PDG)</li><li data-value=="PLM">Palembang (PLM)</li><li data-value=="PKU">Pekan Baru (PKU)</li><li data-value=="RRZ">Sibolga (RRZ)</li><li data-value=="SUB">Surabaya (SUB)</li></ul> <ul> <li class="country">Malaysia</li><li data-value=="PEN">Penang (PEN)</li></ul></div>');
		}
		else if(selectedCity == "MNA")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="MDC">Manado (MDC)</li></ul></div>');
		}
		else if(selectedCity == "MKQ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="DJJ">Jayapura (DJJ)</li><li data-value=="BIK">Mokmer Airport (BIK)</li></ul></div>');
		}
		else if(selectedCity == "MEQ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li></ul></div>');
		}
		else if(selectedCity == "BIK")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="MKQ">Merauke (MKQ)</li></ul></div>');
		}
		else if(selectedCity == "NBX")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="AMQ">Ambon (AMQ)</li><li data-value=="DJJ">Jayapura (DJJ)</li><li data-value=="KNG">Kaimana (KNG)</li></ul></div>');
		}
		else if(selectedCity == "NTX")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="BTH">Batam (BTH)</li></ul></div>');
		}
		else if(selectedCity == "PDG")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li></ul></div>');
		}
		else if(selectedCity == "PKY")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="DPS">Denpasar (Bali) (DPS)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="SUB">Surabaya (SUB)</li></ul></div>');
		}
		else if(selectedCity == "PLM")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="BDJ">Banjarmasin (BDJ)</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="DPS">Denpasar (Bali) (DPS)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="DJB">Jambi (DJB)</li><li data-value=="JOG">Jogjakarta (JOG)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="LOP">Lombok, Mataram (LOP)</li><li data-value=="UPG">MAKASSAR (UPG)</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li><li data-value=="PKU">Pekan Baru (PKU)</li><li data-value=="SUB">Surabaya (SUB)</li></ul></div>');
		}
		else if(selectedCity == "PLW")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="BPN">Balikpapan (BPN)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="SUB">Surabaya (SUB)</li><li data-value=="UPG">Ujung Pandang (UPG)</li></ul></div>');
		}
		else if(selectedCity == "PGK")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="TJQ">Tanjung Pandan (TJQ)</li></ul></div>');
		}
		else if(selectedCity == "PKU")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul> <ul> <li class="country">Indonesia</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="MES">Medan (MES)</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li><li data-value=="PLM">Palembang (PLM)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="MKZ">Melaka (MKZ)</li></ul> <ul> <li class="country">Singapore</li><li data-value=="SIN">Singapore (SIN)</li></ul></div>');
		}
		else if(selectedCity == "PUM")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="UPG">Ujung Pandang (UPG)</li></ul></div>');
		}
		else if(selectedCity == "PNK")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="CGK">Jakarta (CGK)</li></ul></div>');
		}
		else if(selectedCity == "PSJ" || selectedCity == "YKR")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="UPG">Ujung Pandang (UPG)</li></ul></div>');
		}
		else if(selectedCity == "SRG")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="BPN">Balikpapan (BPN)</li><li data-value=="BDO">Bandung (BDO)</li><li data-value=="BDJ">Banjarmasin (BDJ)</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="DPS">Denpasar (Bali) (DPS)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="SUB">Surabaya (SUB)</li><li data-value=="UPG">Ujung Pandang (UPG)</li </ul></div>');
		}
		else if(selectedCity == "RRZ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li></ul></div>');
		}
		else if(selectedCity == "DTB")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="BTH">Batam (BTH)</li></ul></div>');
		}
		else if(selectedCity == "SOC")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="HLP">JAKARTA HALIM PERDANAKUSUMA (HLP)</li><li data-value=="JOG">Jogjakarta (JOG)</li></ul></div>');
		}
		else if(selectedCity == "SOQ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="AMQ">Ambon (AMQ)</li><li data-value=="BXB">Babo Airport (BXB)</li><li data-value=="FKQ">Fak Fak (FKQ)</li><li data-value=="KNG">Kaimana (KNG)</li><li data-value=="MDC">Manado (MDC)</li></ul></div>');
		}
		else if(selectedCity == "SUB")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="AMQ">Ambon (AMQ)</li><li data-value=="BPN">Balikpapan (BPN)</li><li data-value=="BDO">Bandung (BDO)</li><li data-value=="BDJ">Banjarmasin (BDJ)</li><li data-value=="DQJ">Banyuwangi (DQJ)</li><li data-value=="BTH">Batam (BTH)</li><li data-value=="DPS">Denpasar (Bali) (DPS)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="JOG">Jogjakarta (JOG)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="KOE">Kupang (KOE)</li><li data-value=="LOP">Lombok, Mataram (LOP)</li><li data-value=="MLG">Malang (MLG)</li><li data-value=="MDC">Manado (MDC)</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li><li data-value=="PKY">Palangkaraya (PKY)</li><li data-value=="PLM">Palembang (PLM)</li><li data-value=="PLW">Palu (PLW)</li><li data-value=="SRG">Semarang (SRG)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="TRK">Tarakan (TRK)</li><li data-value=="UPG">Ujung Pandang (UPG)</li></ul> <ul> <li class="country">Saudi Arabia</li><li data-value=="JED">Jeddah (JED)</li></ul> <ul> <li class="country">Singapore</li><li data-value=="SIN">Singapore (SIN)</li></ul></div>');
		}
		else if(selectedCity == "NAH")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="MDC">Manado (MDC)</li></ul></div>');
		}
		else if(selectedCity == "TMC")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="DPS">Denpasar (Bali) (DPS)</li><li data-value=="KOE">Kupang (KOE)</li></ul></div>');
		}
		else if(selectedCity == "TJQ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="PGK">Pangkal Pinang (PGK)</li></ul></div>');
		}
		else if(selectedCity == "TNJ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="PGK">Pangkal Pinang (PGK)</li></ul></div>');
		}
		else if(selectedCity == "TRK")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="BPN">Balikpapan (BPN)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="SUB">Surabaya (SUB)</li></ul></div>');
		}
		else if(selectedCity == "TTE")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="WUB">Buli (WUB)</li><li data-value=="CGK">JAKARTA (CGK)</li><li data-value=="UPG">MAKASSAR (UPG)</li><li data-value=="MDC">Manado (MDC)</li></ul></div>');
		}
		else if(selectedCity == "LUV")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="AMQ">Ambon (AMQ)</li></ul></div>');
		}
		else if(selectedCity == "UPG")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="AMQ">Ambon (AMQ)</li><li data-value=="BPN">Balikpapan (BPN)</li><li data-value=="BUW">Baubau (BUW)</li><li data-value=="DPS">Denpasar (Bali) (DPS)</li><li data-value=="GTO">Gorontalo (GTO)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="HLP">JAKARTA HALIM PERDANAKUSUMA (HLP)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="DJJ">Jayapura (DJJ)</li><li data-value=="JOG">Jogjakarta (JOG)</li><li data-value=="KDI">Kendari (KDI)</li><li data-value=="KBU">Kotabaru (KBU)</li><li data-value=="LUW">Luwuk (LUW)</li><li data-value=="MLG">MALANG (MLG)</li><li data-value=="MJU">Mamuju (MJU)</li><li data-value=="MDC">Manado (MDC)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="PLW">Palu (PLW)</li><li data-value=="PUM">Pomalaa (PUM)</li><li data-value=="PSJ">Poso (PSJ)</li><li data-value=="YKR">Selayar (YKR)</li><li data-value=="SRG">Semarang (SRG)</li><li data-value=="SUB">Surabaya (SUB)</li><li data-value=="TTE">TERNATE (TTE)</li></ul></div>');
		}
		else if(selectedCity == "WGP")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="DPS">Denpasar (Bali) (DPS)</li></ul></div>');
		}
		else if(selectedCity == "WNI")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="KDI">Kendari (KDI)</li></ul></div>');
		}
		else if(selectedCity == "AOR")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="SZB">Subang (SZB)</li></ul></div>');
		}
		else if(selectedCity == "IPH")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="JHB">Johor Bahru (JHB)</li><li data-value=="SZB">Subang (SZB)</li></ul></div><div class="city-list"> <ul> <li class="country">Singapore</li><li data-value=="SIN">Singapore (SIN)</li></ul></div>');
		}
		else if(selectedCity == "JHB")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="IPH">Ipoh (IPH)</li><li data-value=="PEN">Penang (PEN)</li><li data-value=="SZB">Subang (SZB)</li></ul></div>');
		}
		else if(selectedCity == "KTE")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="SZB">Subang (SZB)</li></ul></div>');
		}
		else if(selectedCity == "JHB" || selectedCity == "KBR")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="KUL">Kuala Lumpur (KUL)</li><li data-value=="MKZ">Melaka (MKZ)</li><li data-value=="PEN">Penang (PEN)</li><li data-value=="SZB">Subang (SZB)</li></ul></div>');
		}
		else if(selectedCity == "KTE" || selectedCity == "TGG" || selectedCity == "KUA")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="SZB">Subang (SZB)</li></ul></div>');
		}
		else if(selectedCity == "BKI" || selectedCity == "KCH")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul> <ul> <li class="country">Bangladesh</li><li data-value=="DAC">Dhaka (DAC)</li></ul></div><div class="city-list"> <ul> <li class="country">India</li><li data-value=="ATQ">Amritsar (ATQ)</li><li data-value=="DEL">Delhi (DEL)</li><li data-value=="COK">Kochi (COK)</li><li data-value=="BOM">Mumbai (BOM)</li><li data-value=="TRZ">Tiruchirappalli (TRZ)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="KUL">Kuala Lumpur (KUL)</li><li data-value=="PEN">Penang (PEN)</li></ul></div><div class="city-list"><ul> <li class="country">Nepal</li><li data-value=="KTM">Kathmandu (KTM)</li></ul> <ul> <li class="country">Singapore</li><li data-value=="SIN">Singapore (SIN)</li></ul> </div>');
		}
		else if(selectedCity == "KUL")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul> <ul> <li class="country">Bangladesh</li><li data-value=="DAC">Dhaka (DAC)</li></ul></div><div class="city-list"> <ul> <li class="country">India</li><li data-value=="ATQ">Amritsar (ATQ)</li><li data-value=="DEL">Delhi (DEL)</li><li data-value=="COK">Kochi (COK)</li><li data-value=="BOM">Mumbai (BOM)</li><li data-value=="TRZ">Tiruchirappalli (TRZ)</li></ul></div><div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="BDO">Bandung (BDO)</li><li data-value=="DPS">Denpasar Bali (DPS)</li><li data-value=="CGK">Jakarta (CGK)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="KBR">Kota Bharu (KBR)</li><li data-value=="BKI">Kota Kinabalu (BKI)</li><li data-value=="KCH">Kuching (KCH)</li><li data-value=="LGK">Langkawi (LGK)</li><li data-value=="PEN">Penang (PEN)</li></ul></div><div class="city-list"> <ul> <li class="country">Nepal</li><li data-value=="KTM">Kathmandu (KTM)</li></ul><ul> <li class="country">Singapore</li><li data-value=="SIN">Singapore (SIN)</li></ul> <ul> <li class="country">Thailand</li><li data-value=="DMK">Bangkok (DMK)</li></ul> </div>');
		}
		else if(selectedCity == "LGK")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul> <ul> <li class="country">Bangladesh</li><li data-value=="DAC">Dhaka (DAC)</li></ul></div><div class="city-list"> <ul> <li class="country">India</li><li data-value=="ATQ">Amritsar (ATQ)</li><li data-value=="DEL">Delhi (DEL)</li><li data-value=="COK">Kochi (COK)</li><li data-value=="BOM">Mumbai (BOM)</li><li data-value=="TRZ">Tiruchirappalli (TRZ)</li><li data-value=="TRV">Trivandrum (TRV)</li><li data-value=="VTZ">Visakhapatnam (VTZ)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="KUL">Kuala Lumpur (KUL)</li><li data-value=="PEN">Penang (PEN)</li><li data-value=="SZB">Subang (SZB)</li></ul></div><div class="city-list"> <ul> <li class="country">Nepal</li><li data-value=="KTM">Kathmandu (KTM)</li></ul><ul> <li class="country">Singapore</li><li data-value=="SIN">Singapore (SIN)</li></ul> </div>');
		}
		else if(selectedCity == "MKZ")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul><ul> <li class="country">Indonesia</li><li data-value=="PKU">Pekanbaru (PKU)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="KBR">Kota Bahru (KBR)</li><li data-value=="PEN">Penang (PEN)</li></ul></div>');
		}
		else if(selectedCity == "PEN")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul> <ul> <li class="country">Bangladesh</li><li data-value=="DAC">Dhaka (DAC)</li></ul></div><div class="city-list"> <ul> <li class="country">India</li><li data-value=="ATQ">Amritsar (ATQ)</li><li data-value=="DEL">Delhi (DEL)</li><li data-value=="COK">Kochi (COK)</li><li data-value=="BOM">Mumbai (BOM)</li><li data-value=="TRZ">Tiruchirappalli (TRZ)</li><li data-value=="TRV">Trivandrum (TRV)</li><li data-value=="VTZ">Visakhapatnam (VTZ)</li></ul><ul> <li class="country">Indonesia</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="JHB">Johor Bahru (JHB)</li><li data-value=="KBR">Kota Bharu (KBR)</li><li data-value=="BKI">Kota Kinabalu (BKI)</li><li data-value=="KUL">Kuala Lumpur (KUL)</li><li data-value=="KCH">Kuching (KCH)</li><li data-value=="LGK">Langkawi (LGK)</li><li data-value=="MKZ">Melaka (MKZ)</li><li data-value=="SZB">Subang (SZB)</li></ul></div><div class="city-list"> <ul> <li class="country">Nepal</li><li data-value=="KTM">Kathmandu (KTM)</li></ul><ul> <li class="country">Singapore</li><li data-value=="SIN">Singapore (SIN)</li></ul> </div>');
		}
		else if(selectedCity == "SZB")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul><ul> <li class="country">Indonesia</li><li data-value=="KNO">Medan Kuala Namu (KNO)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="AOR">Alor Setar (AOR)</li><li data-value=="IPH">Ipoh (IPH)</li><li data-value=="JHB">Johor Bahru (JHB)</li><li data-value=="KTE">Kerteh (KTE)</li><li data-value=="KBR">Kota Bahru (KBR)</li><li data-value=="TGG">Kuala Terengganu (TGG)</li><li data-value=="KUA">Kuantan (KUA)</li><li data-value=="LGK">Langkawi (LGK)</li><li data-value=="PEN">Penang (PEN)</li></ul></div>');
		}
		else if(selectedCity == "KTM")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Indonesia</li><li data-value=="DPS">Denpasar Bali (DPS)</li><li data-value=="CGK">Jakarta (CGK)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="BKI">Kota Kinabalu (BKI)</li><li data-value=="KUL">Kuala Lumpur (KUL)</li><li data-value=="KCH">Kuching (KCH)</li><li data-value=="LGK">Langkawi (LGK)</li><li data-value=="PEN">Penang (PEN)</li></ul></div><div class="city-list"> <ul> <li class="country">Singapore</li><li data-value=="SIN">Singapore (SIN)</li></ul></div>');
		}
		else if(selectedCity == "JED")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="SUB">Surabaya (SUB)</li></ul></div>');
		}
		else if(selectedCity == "SIN")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Australia</li><li data-value=="PER">Perth (PER)</li></ul> <ul> <li class="country">Bangladesh</li><li data-value=="DAC">Dhaka (DAC)</li></ul></div><div class="city-list"> <ul> <li class="country">India</li><li data-value=="ATQ">Amritsar (ATQ)</li><li data-value=="DEL">Delhi (DEL)</li><li data-value=="COK">Kochi (COK)</li><li data-value=="BOM">Mumbai (BOM)</li><li data-value=="TRZ">Tiruchirappalli (TRZ)</li><li data-value=="TRV">Trivandrum (TRV)</li><li data-value=="VTZ">Visakhapatnam (VTZ)</li></ul></div><div class="city-list"><ul> <li class="country">Indonesia</li><li data-value=="DPS">Denpasar Bali (DPS)</li><li data-value=="CGK">Jakarta (CGK)</li><li data-value=="PKU">Pekan Baru (PKU)</li><li data-value=="SUB">Surabaya (SUB)</li></ul></div><div class="city-list"> <ul> <li class="country">Malaysia</li><li data-value=="IPH">Ipoh (IPH)</li><li data-value=="BKI">Kota Kinabalu (BKI)</li><li data-value=="KUL">Kuala Lumpur (KUL)</li><li data-value=="KCH">Kuching (KCH)</li><li data-value=="LGK">Langkawi (LGK)</li><li data-value=="PEN">Penang (PEN)</li></ul> <ul> <li class="country">Nepal</li><li data-value=="KTM">Kathmandu (KTM)</li></ul></div><div class="city-list"> <ul> <li class="country">Thailand</li><li data-value=="DMK">Bangkok (DMK)</li><li data-value=="CEI">Chiang Rai (CEI)</li><li data-value=="HDY">Hat Yai (HDY)</li><li data-value=="KBV">Krabi (KBV)</li><li data-value=="HKT">Phuket (HKT)</li><li data-value=="URT">Surat Thani (URT)</li><li data-value=="UBP">Ubon Ratchathani (UBP)</li></ul> <ul> <li class="country">Vietnam</li><li data-value=="SGN">Ho Chi Minh City (SGN)</li></ul></div>');
		}
		else if(selectedCity == "DMK")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"> <ul> <li class="country">Bangladesh</li><li data-value=="DAC">Dhaka (DAC)</li></ul> <ul> <li class="country">India</li><li data-value=="COK">Kochi (COK)</li><li data-value=="TRV">Trivandrum (TRV)</li><li data-value=="VTZ">Visakhapatnam (VTZ)</li></ul></div><div class="city-list"><ul> <li class="country">Malaysia</li><li data-value=="KUL">Kuala Lumpur (KUL)</li></ul><ul> <li class="country">Singapore</li><li data-value=="SIN">Singapore (SIN)</li></ul></div><div class="city-list"> <ul> <li class="country">Thailand</li><li data-value=="CNX">Chiang Mai (CNX)</li><li data-value=="CEI">Chiang Rai (CEI)</li><li data-value=="HDY">Hat Yai (HDY)</li><li data-value=="KBV">Krabi (KBV)</li><li data-value=="NST">Nakhon Si Thammarat (NST)</li></ul></div><div class="city-list"> <ul> <li class="country">Thailand</li><li data-value=="HKT">Phuket (HKT)</li><li data-value=="URT">Surat Thani (URT)</li><li data-value=="UBP">Ubon Ratchathani (UBP)</li><li data-value=="UTH">Udon Thani (UTH)</li></ul></div>');
		}
		else if(selectedCity == "CNX")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Thailand</li><li data-value=="DMK">Bangkok (DMK)</li></ul></div>');
		}
		else if(selectedCity == "CEI" || selectedCity == "KBV" || selectedCity == "HKT" || selectedCity == "URT" || selectedCity == "UBP")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Singapore</li><li data-value=="SIN">Singapore (SIN)</li></ul></div><div class="city-list"><ul><li class="country">Thailand</li><li data-value=="DMK">Bangkok (DMK)</li></ul></div>');
		}
		else if(selectedCity == "NST")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Thailand</li><li data-value=="DMK">Bangkok (DMK)</li></ul></div>');
		}
		else if(selectedCity == "BKK")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Indonesia</li><li data-value=="CGK">Jakarta (CGK)</li></ul></div>');
		}
		else if(selectedCity == "UTH")
		{
			$(listTarget+"List .list-container").html('<div class="city-list"><ul><li class="country">Thailand</li><li data-value=="DMK">Bangkok (DMK)</li><li data-value=="HDY">Hat Yai (HDY)</li></ul></div>');
		}
}