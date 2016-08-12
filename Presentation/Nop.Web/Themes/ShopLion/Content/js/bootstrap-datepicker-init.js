$(document).ready(function() {
	Date.prototype.addDays = function(days)
	{
		var dat = new Date(this.valueOf());
		dat.setDate(dat.getDate() + days);
		return dat;
	}
	/*mobile Swipe Event */
	$("body").swiperight(function() {  
		console.log("swiperight")
		if($('.ui-datepicker').is(":visible")) {
	  		$(".ui-datepicker-prev").click();  
		}
	});  
   $("body").swipeleft(function() { 
   		if($('.ui-datepicker').is(":visible")) {
	  		$(".ui-datepicker-next").click();  
		}
	});
	/*mobile Swipe Event */	
	var winWid = $(window).width();
	viewportSize(winWid);
	$(window).resize(function(){
		winWid = $(window).width();
		viewportSize(winWid);
	});
	var date1Id = '#dpd1';
	var date2Id = '#dpd2';
	function viewportSize(winWid)
	{
		console.log(winWid)
		if(winWid > 500)	
		{
			
			dateQuery("#dpd1", "#dpd2",2, new Date());
			dateQuery("#dpd5", "#dpd6",2, new Date().addDays(1));
			dateQuery("#dpd3", "#dpd4",2, new Date().addDays(1));
			dateQuery("#dpd7", "#dpd8",2, new Date().addDays(1));	
			dateQuery("#dpd9", "#dpd10",2, new Date().addDays(1));	
			//dateQuery("#dpd7", "#dpd8",2, new Date();
		}
		else
		{
			
			dateQuery("#dpd1", "#dpd2",1, new Date());
			dateQuery("#dpd5", "#dpd6",1, new Date().addDays(1));
			dateQuery("#dpd3", "#dpd4",1, new Date().addDays(1));
			dateQuery("#dpd7", "#dpd8",1, new Date().addDays(1));	
			dateQuery("#dpd9", "#dpd10",2, new Date().addDays(1));
			//dateQuery("#dpd7", "#dpd8",1);
		}
		if(winWid < 485)
		{
			$("#dpd1,#dpd2,#dpd3,#dpd4,#dpd5,#dpd6,#dpd7,#dpd8,#dpd9,#dpd10").prop("readonly", true);
			$
		}
		else
		{
			$("#dpd1,#dpd2,#dpd3,#dpd4,#dpd5,#dpd6,#dpd7,#dpd8,#dpd9,#dpd10").prop("readonly", false);
		}
	}
	function dateQuery(startDateId, endDateId, noOfmonth, startDate)
	{
		$( startDateId ).datepicker("destroy");
		$( endDateId ).datepicker( "destroy" );
		var firstPickedDateReturn = false;
		$(startDateId).val(formatDate(startDate));
		$(endDateId).val(formatDate(startDate.addDays(1)));
		$( startDateId ).datepicker({
		  defaultDate: "+1w",
		  changeMonth: true,
		  numberOfMonths: noOfmonth,
		  minDate: startDate,
		  maxDate: new Date().addDays(332),
		  dateFormat: "dd/mm/yy",
		  prevText: '<span class="icon im-long-arrow-left">',
		  nextText: '<span class="icon im-long-arrow-right">',
		   onSelect: function(dateText, inst) { 
			  var pickedDate = $(this).datepicker( 'getDate' ); //the getDate method
			  $( endDateId ).datepicker( "option", "minDate", pickedDate.addDays(1));
		   },
		  onClose: function( selectedDate ) {
			 if(startDateId === "#dpd1")
			 {
				$( endDateId ).datepicker( "option", "minDate", (selectedDate == "") ? startDate : selectedDate);
			 }
			$( endDateId ).focus();
		  }
		});
		$( endDateId ).datepicker({
		  defaultDate: "+1w",
		  changeMonth: true,
		  numberOfMonths: noOfmonth,
		  dateFormat: "dd/mm/yy",
		  minDate: startDate,
		  maxDate: new Date().addDays(332),
		  prevText: '<span class="icon im-long-arrow-left">',
		  nextText: '<span class="icon im-long-arrow-right">',
		  onClose: function( selectedDate ) {
			  //if(firstPickedDateReturn)
				//$( startDateId ).datepicker( "option", "minDate", selectedDate );
		  }
		});
	}
	function formatDate(date)
	{
		var d = date;
		var curr_date = d.getDate();
		var curr_month = d.getMonth();
		curr_month++;
		var curr_year = d.getFullYear();
		if(curr_date < 10)
			curr_date = "0"+curr_date
		if(curr_month < 10)
			curr_month = "0"+curr_month
		return curr_date + "/" + curr_month + "/" + curr_year;
	}

});