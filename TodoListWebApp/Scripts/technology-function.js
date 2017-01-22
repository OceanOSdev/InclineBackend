$(document).ready(function(){

  var affixOffset;
  var mobileNavOffset;

  $('.hamburger').click(function(){
    $('.hamburger, .mobile-nav, .filter-effect').toggleClass('is-active');

  });//target-burger-click

  $(window).resize(function() {

    var scrollTop = $(window).scrollTop();
    var elementOffset = $('#smallnav').offset().top - 59;

    affixOffset = (elementOffset - scrollTop);

    if ($(window).width() < 434) {
      mobileNavOffset = 84;
    }
    else {
      mobileNavOffset = 43;
    }

  });

  $('.mobile-nav').css({top: 59});



  $(document).scroll(function(){
      if ( $(this).scrollTop() > affixOffset ){
        $('.secondary-header').addClass('fixed');
        $('.secondary-header').css('color', '#4A4A4A');
        $('.mobile-nav').css({top: 59 + mobileNavOffset});

      }
      else {
        $('.secondary-header').removeClass('fixed');
        $('.secondary-header').css('color', '#FFFFFF');
        $('.mobile-nav').css({top: 59});
      }

  });

  $(window).resize(); //on page load

  $(function(){
    $('#Container').mixItUp();
  });


});//doc-rdy
