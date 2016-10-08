$(document).ready(function() {
	$('.product-grid-slider').slick({
		autoplay: true,
  		autoplaySpeed: 6000,
		infinite: true,
		slidesToShow: 3,
		slidesToScroll: 3,
		responsive: [
		    {
		      breakpoint: 980,
		      settings: {
		        slidesToShow: 2,
				slidesToScroll: 2,
		      }
		    },
		    {
		      breakpoint: 480,
		      settings: {
		        slidesToShow: 1,
				slidesToScroll: 1,
		      }
		    }
	    ]
	});
	$('.flight-travel-page').parents('.master-wrapper-content').removeClass('container');
	$('.flight-travel-page').parents('body').addClass('body-flight-travel');
});