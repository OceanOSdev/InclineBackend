$(document).ready(function(){

  var affixOffset;


	$('.hamburger').click(function(){
		$('.hamburger, .mobile-nav, .filter-effect').toggleClass('is-active');

	});//target-burger-click

  $(function() {
      $(".secondary-link").on("click", function(e) {
          $("a.active").removeClass("active");
          $(this).addClass("active");
          e.preventDefault();
      });
  });

  $(window).resize(function() {

    var scrollTop = $(window).scrollTop();
    var elementOffset = $('#smallnav').offset().top - 59;

    affixOffset = (elementOffset - scrollTop);



  });

  $('.mobile-nav').css({top: 59});
  // var windowScroll = $(document).scrollTop(),
  //     lockIconPos = $('.lock-icon').offset().top - 102;

  $(document).scroll(function(){
      if ( $(this).scrollTop() > affixOffset ){
        $('.secondary-header').addClass('fixed');
        $('.secondary-header').css('color', '#4A4A4A');
        $('.mobile-nav').css({top: 102});

      }
      else {
        $('.secondary-header').removeClass('fixed');
        $('.mobile-nav').css({top: 59});
        $('.secondary-header').css('color', '#FFFFFF');
      }

  });






  $(window).resize(); //on page load

  $('html').smoothScroll(600);



});//doc-rdy
