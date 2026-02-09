fetch("/api/service")
    .then(res => res.json())
    .then(data => {
        const container = document.getElementById("services-container");
        container.innerHTML = "";

        data.forEach(service => {
            container.innerHTML += `
                <div class="col-lg-4 col-md-6">
                    <div class="single_service">
                        <div class="service_thumb" style="
                            background-image: url('/img/service/service_icon_bg_1.png');
                            background-position: center;
                            background-repeat: no-repeat;
                            background-size: cover;
                            display: flex;
                            justify-content: center;
                            align-items: center;
                        ">
                            <img src="/img/service/${service.icon}" alt="" style="max-width:50%; height:auto;">
                        </div>
                        <div class="service_content text-center">
                            <h3>${service.name}</h3>
                            <p>${service.description}</p>
                        </div>
                    </div>
                </div>
            `;
        });
    })
    .catch(err => console.log(err));

fetch("/api/team")
    .then(res => res.json())
    .then(data => {
        const container = document.getElementById("team-container");
        container.innerHTML = ""; 

        data.forEach(team => {  
            container.innerHTML += `
                <div class="col-lg-4 col-md-6">
                    <div class="single_team">
                        <div class="thumb">
                            <img src="/img/team/${team.image}" alt="${team.name}">
                        </div>
                        <div class="member_name text-center">
                            <div class="mamber_inner">
                                <h4>${team.name}</h4>
                                <p>${team.position}</p>
                            </div>
                        </div>
                    </div>
                </div>
            `;
        });
    })
    .catch(err => console.log(err));
fetch("/api/feedback")
    .then(res => res.json())
    .then(data => {
        const container = document.getElementById("feedback-container");

       
        container.innerHTML = `
            <div class="textmonial_active owl-carousel"></div>
        `;

        const carousel = container.querySelector(".owl-carousel");

        
        data.forEach(feedback => {
            carousel.innerHTML += `
                <div class="testmonial_wrap">
                    <div class="single_testmonial d-flex align-items-center">
                        <div class="test_thumb">
                            <img src="/img/testmonial/${feedback.image}" alt="${feedback.author}">
                        </div>
                        <div class="test_content">
                            <h4>${feedback.author}</h4>
                            <span>${feedback.position}</span>
                            <p>${feedback.text}</p>
                        </div>
                    </div>
                </div>
            `;
        });

        
        $('.textmonial_active').owlCarousel({
            items: 1,
            loop: true,
            autoplay: true,
            nav: false,
            dots: true,
            autoplayTimeout: 5000,
        });
    })
    .catch(err => console.log(err));






(function ($) {
"use strict";

$(window).on('scroll', function () {
	var scroll = $(window).scrollTop();
	if (scroll < 400) {
    $("#sticky-header").removeClass("sticky");
    $('#back-top').fadeIn(500);
	} else {
    $("#sticky-header").addClass("sticky");
    $('#back-top').fadeIn(500);
	}
});


$(document).ready(function(){


var menu = $('ul#navigation');
if(menu.length){
	menu.slicknav({
		prependTo: ".mobile_menu",
		closedSymbol: '+',
		openedSymbol:'-'
	});
};
// blog-menu
  // $('ul#blog-menu').slicknav({
  //   prependTo: ".blog_menu"
  // });

// review-active
$('.slider_active').owlCarousel({
  loop:true,
  margin:0,
  items:1,
  autoplay:true,
  nav:false,
  dots:true,
  autoplayHoverPause: true,
  autoplaySpeed: 800,
  responsive:{
      0:{
          items:1
      },
      767:{
          items:1
      },
      992:{
          items:1
      }
  }
});
// review-active
$('.textmonial_active').owlCarousel({
  loop:true,
  margin:100,
  items:1,
  autoplay:true,
  navText:['<i class="Flaticon flaticon-left"></i>','<i class="Flaticon flaticon-right"></i>'],
  nav:true,
  dots:false,
  autoplayHoverPause: true,
  autoplaySpeed: 800,
  responsive:{
      0:{
          items:1,
          nav:false,
      },
      767:{
          items:1,
          nav:true,
      },
      992:{
          items:1
      }
  }
});

// about_active
$('.about_active').owlCarousel({
  loop:true,
  margin:0,
  items:1,
  autoplay:true,
  navText:['<i class="ti-angle-left"></i>','<i class="ti-angle-right"></i>'],
  nav:true,
  dots:false,
  autoplayHoverPause: true,
  autoplaySpeed: 800,
  responsive:{
      0:{
          items:1,
          nav:false,
      },
      767:{
          items:1,
          nav:false,
      },
      992:{
          items:1
      }
  }
});

// review-active
$('.testmonial_active').owlCarousel({
  loop:true,
  margin:0,
items:1,
autoplay:true,
navText:['<i class="ti-angle-left"></i>','<i class="ti-angle-right"></i>'],
  nav:true,
dots:false,
autoplayHoverPause: true,
autoplaySpeed: 800,
  responsive:{
      0:{
          items:1,
          dots:false,
          nav:false,
      },
      767:{
          items:1,
          dots:false,
          nav:false,
      },
      992:{
          items:1,
          nav:false
      },
      1200:{
          items:1,
          nav:false
      },
      1500:{
          items:1
      }
  }
});

// for filter
  // init Isotope
  var $grid = $('.grid').isotope({
    itemSelector: '.grid-item',
    percentPosition: true,
    masonry: {
      // use outer width of grid-sizer for columnWidth
      columnWidth: 1
    }
  });

  // filter items on button click
  $('.portfolio-menu').on('click', 'button', function () {
    var filterValue = $(this).attr('data-filter');
    $grid.isotope({ filter: filterValue });
  });

  //for menu active class
  $('.portfolio-menu button').on('click', function (event) {
    $(this).siblings('.active').removeClass('active');
    $(this).addClass('active');
    event.preventDefault();
	});
  
  // wow js
  new WOW().init();

  // counter 
  $('.counter').counterUp({
    delay: 10,
    time: 10000
  });

/* magnificPopup img view */
$('.popup-image').magnificPopup({
	type: 'image',
	gallery: {
	  enabled: true
	}
});

/* magnificPopup img view */
$('.img-pop-up').magnificPopup({
	type: 'image',
	gallery: {
	  enabled: true
	}
});

/* magnificPopup video view */
$('.popup-video').magnificPopup({
	type: 'iframe'
});


  // scrollIt for smoth scroll
  $.scrollIt({
    upKey: 38,             // key code to navigate to the next section
    downKey: 40,           // key code to navigate to the previous section
    easing: 'linear',      // the easing function for animation
    scrollTime: 600,       // how long (in ms) the animation takes
    activeClass: 'active', // class given to the active nav element
    onPageChange: null,    // function(pageIndex) that is called when page is changed
    topOffset: 0           // offste (in px) for fixed top navigation
  });

  // scrollup bottom to top
  $.scrollUp({
    scrollName: 'scrollUp', // Element ID
    topDistance: '4500', // Distance from top before showing element (px)
    topSpeed: 300, // Speed back to top (ms)
    animation: 'fade', // Fade, slide, none
    animationInSpeed: 200, // Animation in speed (ms)
    animationOutSpeed: 200, // Animation out speed (ms)
    scrollText: '<i class="fa fa-angle-double-up"></i>', // Text for element
    activeOverlay: false, // Set CSS color to display scrollUp active point, e.g '#00FFFF'
  });


  // blog-page

  //brand-active
$('.brand-active').owlCarousel({
  loop:true,
  margin:30,
items:1,
autoplay:true,
  nav:false,
dots:false,
autoplayHoverPause: true,
autoplaySpeed: 800,
  responsive:{
      0:{
          items:1,
          nav:false

      },
      767:{
          items:4
      },
      992:{
          items:7
      }
  }
});

// blog-dtails-page

  //project-active
$('.project-active').owlCarousel({
  loop:true,
  margin:30,
items:1,
// autoplay:true,
navText:['<i class="Flaticon flaticon-left-arrow"></i>','<i class="Flaticon flaticon-right-arrow"></i>'],
nav:true,
dots:false,
// autoplayHoverPause: true,
// autoplaySpeed: 800,
  responsive:{
      0:{
          items:1,
          nav:false

      },
      767:{
          items:1,
          nav:false
      },
      992:{
          items:2,
          nav:false
      },
      1200:{
          items:1,
      },
      1501:{
          items:2,
      }
  }
});

if (document.getElementById('default-select')) {
  $('select').niceSelect();
}

  //about-pro-active
$('.details_active').owlCarousel({
  loop:true,
  margin:0,
items:1,
// autoplay:true,
navText:['<i class="ti-angle-left"></i>','<i class="ti-angle-right"></i>'],
nav:true,
dots:false,
// autoplayHoverPause: true,
// autoplaySpeed: 800,
  responsive:{
      0:{
          items:1,
          nav:false

      },
      767:{
          items:1,
          nav:false
      },
      992:{
          items:1,
          nav:false
      },
      1200:{
          items:1,
      }
  }
});

});

// resitration_Form
$(document).ready(function() {
	$('.popup-with-form').magnificPopup({
		type: 'inline',
		preloader: false,
		focus: '#name',

		// When elemened is focused, some mobile browsers in some cases zoom in
		// It looks not nice, so we disable it:
		callbacks: {
			beforeOpen: function() {
				if($(window).width() < 700) {
					this.st.focus = false;
				} else {
					this.st.focus = '#name';
				}
			}
		}
	});
});



//------- Mailchimp js --------//  
function mailChimp() {
  $('#mc_embed_signup').find('form').ajaxChimp();
}
mailChimp();



    // Search Toggle
    $("#search_input_box").hide();
    $("#search").on("click", function () {
        $("#search_input_box").slideToggle();
        $("#search_input").focus();
    });
    $("#close_search").on("click", function () {
        $('#search_input_box').slideUp(500);
    });
    // Search Toggle
    $("#search_input_box").hide();
    $("#search_1").on("click", function () {
        $("#search_input_box").slideToggle();
        $("#search_input").focus();
    });

})(jQuery);	