(function ($) {
    "use strict";

    // 1. API-dən Servisləri Çəkmək
    async function fetchServices() {
        try {
            const response = await fetch('/api/Service');
            const services = await response.json();
            const container = document.getElementById('services-container');

            if (!container) return;
            container.innerHTML = ""; // Köhnə statik məzmunu silirik

            services.forEach(item => {
                // Sığorta: Backend-dən gələn datanın key-lərini yoxla
                const title = item.title || item.Title || "No Title";
                const desc = item.description || item.Description || "";
                const icon = item.icon || item.Icon || "service_icon_1.png";

                container.innerHTML += `
                    <div class="col-lg-4 col-md-6">
                        <div class="single_service">
                             <div class="service_thumb service_icon_bg_1 d-flex align-items-center justify-content-center">
                                 <div class="service_icon">
                                     <img src="/img/service/${icon}" alt="">
                                 </div>
                             </div>
                             <div class="service_content text-center">
                                <h3>${title}</h3>
                                <p>${desc}</p>
                             </div>
                        </div>
                    </div>`;
            });
        } catch (error) {
            console.error("Servislər yüklənərkən xəta:", error);
        }
    }

    // 2. Carousel və Digər Pluginləri İşə Salmaq
    $(document).ready(function () {

        // Öncə datanı çəkirik
        fetchServices();

        // Mobil Menyu (SlickNav)
        var menu = $('ul#navigation');
        if (menu.length) {
            menu.slicknav({
                prependTo: ".mobile_menu",
                closedSymbol: '+',
                openedSymbol: '-'
            });
        }

        // Testimonial Slayderi (Dinamik data istifadə edəcəksənsə bura diqqət)
        $('.textmonial_active').owlCarousel({
            loop: true,
            margin: 0,
            items: 1,
            autoplay: true,
            navText: ['<i class="ti-angle-left"></i>', '<i class="ti-angle-right"></i>'],
            nav: true,
            dots: false,
            autoplayHoverPause: true,
            autoplaySpeed: 800,
            responsive: {
                0: { items: 1, nav: false },
                767: { items: 1, nav: false },
                992: { items: 1 }
            }
        });

        // Sticky Header
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

    });

})(jQuery);