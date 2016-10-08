$(document).ready(function(){
    $("#paypal_form").hide();

    $("#search-open").hide();

    $(".slideDown dt").click(function(){$(this).toggleClass("active").parent(".slideDown").find("dd").slideToggle()})
    $(".code a.code-icon").toggle(function(){$(this).find("i").text("-");Cufon.refresh();$(this).next("div.grabber").slideDown()},function(){$(this).find("i").text("+");Cufon.refresh();$(this).next("div.grabber").slideUp()})    
    
    $(".slideDown2 dt").click(function(){$(this).toggleClass("active").parent(".slideDown2").find("dd").slideToggle()})
    $(".code a.code-icon").toggle(function(){$(this).find("i").text("-");Cufon.refresh();$(this).next("div.grabber").slideDown()},function(){$(this).find("i").text("+");Cufon.refresh();$(this).next("div.grabber").slideUp()})  


	$("#search").click(function(){
		$(".search-open").fadeToggle(500);
		$(".user-open").fadeOut(500);
		$(".cart-open").fadeOut(500);
	});

	$("#user").click(function(){
		$(".user-open").fadeToggle(500);
		$(".search-open").fadeOut(500);
		$(".cart-open").fadeOut(500);
	});
	
	$("#cart").click(function(){
		$(".cart-open").fadeToggle(500);
		$(".search-open").fadeOut(500);
		$(".user-open").fadeOut(500);
	});


  $(document).on('submit','form#access-login',function(event){
      event.preventDefault();
      var fLog = $('form#access-login');
      var pos    =   $.ajax({
          type     : $(this).attr('method'),
          url      : $(this).attr('action'),
          dataType : 'json',
          data     : $(this).serialize(),
          beforeSend : function(){
              fLog.find('button').html('Login <i class="fa fa-refresh fa-spin"></i>');
          }
      }); 
      pos.done(function(response){
          if(response.status){
            fLog.fadeOut(function(){
              $('.user-open').html('<p>Halo '+response.message+' !</p><a href=""><button class="button-1">View Profile</button></a><a href="#" class="access-logout"><button class="button-1">Logout</button></a>')
            });
          }
          else{
            fLog.find('button').html('Login');
            fLog.find('span').html('<span>'+response.message+'</span>');
          }
      });
      pos.fail(function(xhr,response){
          fLog.find('button').html('Login');
          fLog.find('span').html('<span>Login failed, try again.</span>');
      });
  });

  $(document).on('click','a.access-logout',function(event){

      var update    =   $.ajax({
          type     : 'GET',
          url      : $('#base').text()+'access/logout',
          dataType : 'json'
      }); 
      update.done(function(response){
          if(response.status){
            $('.user-open').html(response.message);
          }
      });
      update.fail(function(xhr,response){
          alert(response);
      });
  });  

});


		
function showcredit(){
			
	jQuery('#credit_form').fadeIn(500);
	jQuery('#paypal_form').fadeOut(500);
}
function showpaypal(){
			
	jQuery('#paypal_form').fadeIn(500);
	jQuery('#credit_form').fadeOut(500);
}
 
 
function handleSearch() {    
    jQuery('#search').click(function () {
       jQuery('.search-open').fadeIn(500);
    }); 
}

function closeSearch() {    
    jQuery('#close-search').click(function () {
       jQuery('.search-open').fadeOut(500);
    }); 
}

function tab(tab) {
    document.getElementById('tab1').style.display = 'none';
    document.getElementById('tab2').style.display = 'none';
    document.getElementById('li_tab1').setAttribute("class", "");
    document.getElementById('li_tab2').setAttribute("class", "");
    document.getElementById(tab).style.display = 'block';
    document.getElementById('li_'+tab).setAttribute("class", "active");
}

function str_replace (search, replace, subject, count) {
  var i = 0,
    j = 0,
    temp = '',
    repl = '',
    sl = 0,
    fl = 0,
    f = [].concat(search),
    r = [].concat(replace),
    s = subject,
    ra = Object.prototype.toString.call(r) === '[object Array]',
    sa = Object.prototype.toString.call(s) === '[object Array]';
  s = [].concat(s);
  if (count) {
    this.window[count] = 0;
  }

  for (i = 0, sl = s.length; i < sl; i++) {
    if (s[i] === '') {
      continue;
    }
    for (j = 0, fl = f.length; j < fl; j++) {
      temp = s[i] + '';
      repl = ra ? (r[j] !== undefined ? r[j] : '') : r[0];
      s[i] = (temp).split(f[j]).join(repl);
      if (count && s[i] !== temp) {
        this.window[count] += (temp.length - s[i].length) / f[j].length;
      }
    }
  }
  return sa ? s : s[0];
}
