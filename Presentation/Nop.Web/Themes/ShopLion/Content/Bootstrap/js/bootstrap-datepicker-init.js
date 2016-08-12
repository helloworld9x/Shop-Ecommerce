$(document).ready(function() {
	    var date1Id = '#dpd1';
		var date2Id = '#dpd2';	
		$(window).load(function(){
			dateQuery("#dpd1", "#dpd2");
			dateQuery("#dpd3", "#dpd4");	
			dateQuery("#dpd5", "#dpd6");
			dateQuery("#dpd7", "#dpd8");
			
		});
		$(".cal").focus(function(){
			var curId = $(this).attr("id");
			if(curId == "dpd1")
				dateQuery("#dpd1", "#dpd2");	
			else if(curId == "dpd3")
				dateQuery("#dpd3", "#dpd4");	
			else  if(curId == "dpd5")
				dateQuery("#dpd5", "#dpd6");
			else  if(curId == "dpd7")
				dateQuery("#dpd7", "#dpd8");	
		});
		function dateQuery(date1, date2)
		{
			//alert(date1+" : "+date2)
			var nowTemp = new Date();
			var now = new Date(nowTemp.getFullYear(), nowTemp.getMonth(), nowTemp.getDate(), 0, 0, 0, 0);
			var checkin = $(date1).datepicker({
			onRender: function(date) {
			return date.valueOf() < now.valueOf() ? 'disabled' : '';
			}
			}).on('changeDate', function(ev) {
			if (ev.date.valueOf() > checkout.date.valueOf()) {
			var newDate = new Date(ev.date)
			newDate.setDate(newDate.getDate()); //newDate.setDate(newDate.getDate() + 1);
			checkout.setValue(newDate);
			}
			checkin.hide();
			$(date2)[0].focus();
			}).data('datepicker');
			var checkout = $(date2).datepicker({
			onRender: function(date) {
			return date.valueOf() < checkin.date.valueOf() ? 'disabled' : ''; //<=
			}
			}).on('changeDate', function(ev) {
			checkout.hide();
			}).data('datepicker');
		}
		
	});