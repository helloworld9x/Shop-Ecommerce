	$(document).ready(function() {
		// Auto city remover
	$(window).load(function()
	{
		var cityCode = $("#depCity").val();
		var jrny = "arrCity";
		$.fn.autoCityRemover(cityCode, jrny);
	});
	$("#depCity").change(function()
	{
		var cityCode = $(this).val();
		var jrny = "arrCity";
		$.fn.autoCityRemover(cityCode, jrny);
		//alert("cityCode : "+cityCode)
	});
	
	
	
	$.fn.autoCityRemover = function(city_code, journey)
	{
		if(city_code == "AOR" || city_code == "KTE" || city_code == "KUA" || city_code == "TGG" || city_code == "BTH")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malayisa"><option value="SZB">Subang (SZB)</option></optgroup>');
		}
		else if(city_code == "KCH")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malaysia"><option value="KUL">Kuala Lumpur (KUL)</option><option value="LGK">Langkawi (LGK)</option><option value="PEN">Penang (PEN)</option></optgroup><optgroup label="India"><option value="BOM">Mumbai (BOM)</option><option value="COK">Kochi (COK)</option><option value="DEL">Delhi (DEL)</option><option value="TRZ">Tiruchirapally (TRZ)</option></optgroup><optgroup label="Bangladesh"><option value="CGP">Chittagong (CGP)</option><option value="DAC">Dhaka (DAC)</option></optgroup><optgroup label="Singapore"><option value="SIN">Singapore (SIN)</option></optgroup>');
		}
		else if(city_code == "BKI")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malaysia"><option value="KUL">Kuala Lumpur (KUL)</option><option value="LGK">Langkawi (LGK)</option><option value="PEN">Penang (PEN)</option>/optgroup><optgroup label="India"><option value="BOM">Mumbai (BOM)</option><option value="COK">Kochi (COK)</option><option value="DEL">Delhi (DEL)</option><option value="TRZ">Tiruchirapally (TRZ)</option></optgroup><optgroup label="Bangladesh"><option value="CGP">Chittagong (CGP)</option><option value="DAC">Dhaka (DAC)</option></optgroup><optgroup label="Singapore"><option value="SIN">Singapore (SIN)</option></optgroup>');
		}
		else if(city_code == "IPH")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malaysia"><option value="JHB">Johor Bahru (JHB)</option><option value="SZB">Subang (SZB)</option></optgroup><optgroup label="Indonesia"><option value="KNO">Kuala Namu, Medan (KNO)</option></optgroup>');
		}
		else if(city_code == "JHB")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malaysia"><option value="IPH">Ipoh (IPH)</option><option value="PEN">Penang (PEN)</option><option value="SZB">Subang (SZB)</option></optgroup><optgroup label="Indonesia"><option value="KNO">Kuala Namu, Medan (KNO)</option></optgroup><optgroup label="India"><option value="BOM">Mumbai (BOM)</option><option value="COK">Kochi (COK)</option><option value="DEL">Delhi (DEL)</option><option value="TRZ">Tiruchirapally (TRZ)</option></optgroup><optgroup label="Bangladesh"><option value="CGP">Chittagong (CGP)</option><option value="DAC">Dhaka (DAC)</option></optgroup>');
		}
		else if(city_code == "KBR")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malaysia"><option value="KUL">Kuala Lumpur (KUL)</option><option value="PEN">Penang (PEN)</option>/optgroup><optgroup label="Malayisa"><option value="SZB">Subang (SZB)</option></optgroup>');
		}
		else if(city_code == "KUL")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malaysia"><option value="BKI">Kota Kinabalu (BKI)</option><option value="KBR">Kota Bharu (KBR)</option><option value="KCH">Kuching (KCH)</option><option value="LGK">Langkawi (LGK)</option><option value="PEN">Penang (PEN)</option></optgroup><optgroup label="Indonesia"><option value="BDO">Bandung (BDO)</option><option value="DPS">Denpasar Bali (DPS)</option><option value="CGK">Jakarta (CGK)</option></optgroup><optgroup label="India"><option value="BOM">Mumbai (BOM)</option><option value="COK">Kochi (COK)</option><option value="DEL">Delhi (DEL)</option><option value="TRZ">Tiruchirapally (TRZ)</option></optgroup><optgroup label="Bangladesh"><option value="CGP">Chittagong (CGP)</option><option value="DAC">Dhaka (DAC)</option></optgroup><optgroup label="Singapore"><option value="SIN">Singapore (SIN)</option></optgroup><optgroup label="Thailand"><option value="DMK">Bangkok (DMK)</option></optgroup>');
		}
		else if(city_code == "LGK")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malaysia"><option value="BKI">Kota Kinabalu (BKI)</option><option value="KCH">Kuching (KCH)</option><option value="KUL">Kuala Lumpur (KUL)</option><option value="PEN">Penang (PEN)</option><option value="SZB">Subang (SZB)</option></optgroup><optgroup label="India"><option value="BOM">Mumbai (BOM)</option><option value="COK">Kochi (COK)</option><option value="DEL">Delhi (DEL)</option><option value="TRZ">Tiruchirapally (TRZ)</option></optgroup><optgroup label="Bangladesh"><option value="CGP">Chittagong (CGP)</option><option value="DAC">Dhaka (DAC)</option></optgroup><optgroup label="Singapore"><option value="SIN">Singapore (SIN)</option></optgroup>');
		}
		else if(city_code == "MKZ")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malayisa"><option value="PEN">Penang (PEN)</option></optgroup><optgroup label="Indonesia"><option value="PKU">Pekanbaru (PKU) </option></optgroup>');
		}
		else if(city_code == "PEN")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malaysia"><option value="BKI">Kota Kinabalu (BKI)</option><option value="JHB">Johor Bahru (JHB)</option><option value="KBR">Kota Bharu (KBR)</option><option value="KCH">Kuching (KCH)</option><option value="LGK">Langkawi (LGK)</option><option value="MKZ">Melaka (MKZ)</option><option value="SZB">Subang (SZB)</option></optgroup><optgroup label="India"><option value="BOM">Mumbai (BOM)</option><option value="COK">Kochi (COK)</option><option value="DEL">Delhi (DEL)</option><option value="TRZ">Tiruchirapally (TRZ)</option></optgroup><optgroup label="Bangladesh"><option value="CGP">Chittagong (CGP)</option><option value="DAC">Dhaka (DAC)</option></optgroup><optgroup label="Singapore"><option value="SIN">Singapore (SIN)</option></optgroup><optgroup label="Thailand"><option value="KBV">Krabi (KBV)</option></optgroup>');
		}
		else if(city_code == "SZB")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malaysia"><option value="AOR">Alor Setar (AOR)</option><option value="BKI">Kota Kinabalu (BKI)</option><option value="IPH">Ipoh (IPH)</option><option value="JHB">Johor Bahru (JHB)</option><option value="KBR">Kota Bharu (KBR)</option><option value="KCH">Kuching (KCH)</option><option value="KTE">Kerteh (KTE)</option><option value="KUA">Kuantan (KUA)</option><option value="LGK">Langkawi (LGK)</option><option value="MKZ">Melaka (MKZ)</option><option value="PEN">Penang (PEN)</option><option value="TGG">Kuala Terengganu (TGG)</option></optgroup><optgroup label="Indonesia"><option value="BTH">Batam (BTH)</option><option value="CGK">Jakarta (CGK)</option><option value="KNO">Kuala Namu, Medan (KNO)</option></optgroup><optgroup label="India"><option value="BOM">Mumbai (BOM)</option><option value="COK">Kochi (COK)</option><option value="DEL">Delhi (DEL)</option><option value="TRZ">Tiruchirapally (TRZ)</option></optgroup><optgroup label="Bangladesh"><option value="CGP">Chittagong (CGP)</option><option value="DAC">Dhaka (DAC)</option></optgroup></optgroup><optgroup label="Thailand"><option value="DMK">Bangkok (DMK)</option><option value="KBV">Krabi (KBV)</option></optgroup>');
		}
		else if(city_code == "BDO")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malayisa"><option value="KUL">Kuala Lumpur (KUL)</option></optgroup>');
		}
		else if(city_code == "DPS")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malayisa"><option value="KUL">Kuala Lumpur (KUL)</option></optgroup></optgroup><optgroup label="India"><option value="BOM">Mumbai (BOM)</option><option value="COK">Kochi (COK)</option><option value="DEL">Delhi (DEL)</option><option value="TRZ">Tiruchirapally (TRZ)</option></optgroup><optgroup label="Bangladesh"><option value="CGP">Chittagong (CGP)</option><option value="DAC">Dhaka (DAC)</option></optgroup>');
		}
		else if(city_code == "CGK")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malayisa"><option value="KUL">Kuala Lumpur (KUL)</option></optgroup></optgroup><optgroup label="India"><option value="BOM">Mumbai (BOM)</option><option value="COK">Kochi (COK)</option><option value="DEL">Delhi (DEL)</option><option value="TRZ">Tiruchirapally (TRZ)</option></optgroup><optgroup label="Bangladesh"><option value="CGP">Chittagong (CGP)</option><option value="DAC">Dhaka (DAC)</option></optgroup><optgroup label="Singapore"><option value="SIN">Singapore (SIN)</option></optgroup>');
		}
		else if(city_code == "KNO")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malaysia"><option value="IPH">Ipoh (IPH)</option><option value="JHB">Johor Bahru (JHB)</option><option value="SZB">Subang (SZB)</option></optgroup>');
		}
		else if(city_code == "PKU")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malayisa"><option value="MKZ">Melaka (MKZ)</option></optgroup>');
		}
		else if(city_code == "BOM")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malaysia"><option value="BKI">Kota Kinabalu (BKI)</option><option value="JHB">Johor Bahru (JHB)</option><option value="KCH">Kuching (KCH)</option><option value="KUL">Kuala Lumpur (KUL)</option><option value="LGK">Langkawi (LGK)</option><option value="PEN">Penang (PEN)</option></optgroup><optgroup label="Indonesia"><option value="DPS">Denpasar Bali (DPS)</option><option value="CGK">Jakarta (CGK)</option></optgroup>');
		}
		else if(city_code == "COK" || city_code == "CGP" || city_code == "DAC")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malaysia"><option value="BKI">Kota Kinabalu (BKI)</option><option value="JHB">Johor Bahru (JHB)</option><option value="KCH">Kuching (KCH)</option><option value="KUL">Kuala Lumpur (KUL)</option><option value="LGK">Langkawi (LGK)</option><option value="PEN">Penang (PEN)</option></optgroup><optgroup label="Indonesia"><option value="DPS">Denpasar Bali (DPS)</option><option value="CGK">Jakarta (CGK)</option></optgroup><optgroup label="Singapore"><option value="SIN">Singapore (SIN)</option></optgroup><optgroup label="Thailand"><option value="DMK">Bangkok (DMK)</option></optgroup>');
		}
		else if(city_code == "DEL" || city_code == "TRZ")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malaysia"><option value="BKI">Kota Kinabalu (BKI)</option><option value="JHB">Johor Bahru (JHB)</option><option value="KCH">Kuching (KCH)</option><option value="KUL">Kuala Lumpur (KUL)</option><option value="LGK">Langkawi (LGK)</option><option value="PEN">Penang (PEN)</option></optgroup><optgroup label="Indonesia"><option value="DPS">Denpasar Bali (DPS)</option><option value="CGK">Jakarta (CGK)</option></optgroup><optgroup label="Singapore"><option value="SIN">Singapore (SIN)</option></optgroup>');
		}
		else if(city_code == "DMK")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malayisa"><option value="KUL">Kuala Lumpur (KUL)</option></optgroup><optgroup label="India"><option value="COK">Kochi (COK)</option></optgroup><option value="SIN">Singapore (SIN)</option></optgroup>');
		}
		else if(city_code == "KBV")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malayisa"><option value="PEN">Penang (PEN)</option><option value="SZB">Subang (SZB)</option></optgroup>');
		}
		else if(city_code == "SIN")
		{
			$("#"+journey).html('<option value="0">Select City</option><optgroup label="Malaysia"><option value="BKI">Kota Kinabalu (BKI)</option><option value="KCH">Kuching (KCH)</option><option value="KUL">Kuala Lumpur (KUL)</option><option value="LGK">Langkawi (LGK)</option><option value="PEN">Penang (PEN)</option></optgroup><optgroup label="India"><option value="COK">Kochi (COK)</option><option value="DEL">Delhi (DEL)</option><option value="TRZ">Tiruchirapally (TRZ)</option></optgroup><optgroup label="Bangladesh"><option value="CGP">Chittagong (CGP)</option><option value="DAC">Dhaka (DAC)</option></optgroup><optgroup label="Indonesia"><option value="CGK">Jakarta (CGK)</option></optgroup><optgroup label="Singapore"><option value="SIN">Singapore (SIN)</option></optgroup><optgroup label="Thailand"><option value="DMK">Bangkok (DMK)</option></optgroup>');
		}
		
		$("#"+journey).select2();
		
	}
		
});
