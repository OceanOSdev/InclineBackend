
$(document).ready(function(){

  var affixOffset;


	$('.hamburger').click(function(){
		$('.hamburger, .mobile-nav').toggleClass('is-active');

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

    // if ($(window).width() < 768) {
    //   var sources = document.querySelectorAll('source');
    //   var video = document.querySelector('video');
    //   for(var i = 0; i<sources.length;i++) {
    //     sources[i].setAttribute('src', sources[i].getAttribute('data-src'));
    //   }
    //   video.load();
    // }



  });

  $('.mobile-nav').css({top: 59});
  // var windowScroll = $(document).scrollTop(),
  //     lockIconPos = $('.lock-icon').offset().top - 102;

  $(document).scroll(function(){
      if ( $(this).scrollTop() > affixOffset ){
        $('.secondary-header').addClass('fixed');
        $('.secondary-header').css('color', '#4A4A4A');
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
