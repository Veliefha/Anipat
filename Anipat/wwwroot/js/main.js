(function ($) {
    "use strict";

    // 1. ÇIXIŞ FUNKSİYASI
    window.logOut = function () {
        localStorage.clear();
        window.location.href = "index.html"; // Səhifəni yönləndir və yenilə
    };

    // 2. AUTH RENDER (Adı və Düymələri göstərmək)
    function renderAuth() {
        const name = localStorage.getItem('userName');
        const role = localStorage.getItem('userRole');

        // Bütün #auth-box elementlərini tapırıq (normal və mobil menyu daxil)
        // Slicknav kopyalayanda ID-ləri də kopyaladığı üçün querySelectorAll lazımdır
        const authBoxes = document.querySelectorAll('#auth-box');

        authBoxes.forEach(box => {
            if (name) {
                // Giriş edilibsə
                box.innerHTML = `
                    <div class="auth-wrapper" style="display: flex; align-items: center; gap: 10px; padding: 5px 10px;">
                        <span style="color:#d32f2f; font-weight:bold; white-space: nowrap;">🐾 ${name}</span>
                        <button onclick="logOut()" style="background: #f4f4f4; color: #d32f2f; border: 1px solid #ddd; padding: 4px 10px; border-radius: 6px; cursor: pointer; font-size: 12px; font-weight: 600;">Çıxış</button>
                    </div>
                `;
            } else {
                // Giriş edilməyibsə
                box.innerHTML = `
                    <div class="auth-wrapper" style="display: flex; align-items: center; gap: 10px; padding: 5px 10px;">
                        <a href="login.html" style="font-weight:600; color: #222 !important;">Giriş</a>
                        <a href="register.html" style="background: #d32f2f; color: white !important; padding: 8px 18px; border-radius: 20px; font-weight: 600; text-decoration: none; font-size: 13px;">Qeydiyyat</a>
                    </div>
                `;
            }
        });
    }

    // 3. DATA FETCH (Xidmətlər və Komanda)
    const loadData = () => {
        const serviceContainer = document.getElementById("services-container");
        if (serviceContainer) {
            fetch("/api/service")
                .then(res => res.json())
                .then(data => {
                    serviceContainer.innerHTML = data.map(s => `
                        <div class="col-lg-4 col-md-6">
                            <div class="single_service text-center">
                                <h3>${s.name}</h3>
                                <p>${s.description}</p>
                            </div>
                        </div>`).join('');
                }).catch(err => console.log(err));
        }

        const teamContainer = document.getElementById("team-container");
        if (teamContainer) {
            fetch("/api/team")
                .then(res => res.json())
                .then(data => {
                    teamContainer.innerHTML = data.map(t => `
                        <div class="col-lg-4 col-md-6">
                            <div class="single_team text-center">
                                <h4>${t.name}</h4>
                                <p>${t.position}</p>
                            </div>
                        </div>`).join('');
                }).catch(err => console.log(err));
        }
    };

    // 4. INITIALIZE
    $(document).ready(function () {
        // Menyu aktivləşdir
        var menu = $('ul#navigation');
        if (menu.length) {
            menu.slicknav({
                prependTo: ".mobile_menu",
                closedSymbol: '+',
                openedSymbol: '-'
            });
        }

        // Həm dərhal, həm də SlickNav-ın kopyalaması bitəndən sonra render et
        renderAuth();
        loadData();

        // 500ms və 1500ms sonra təkrar yoxla (Mobil menyu gecikməsi üçün)
        setTimeout(renderAuth, 500);
        setTimeout(renderAuth, 1500);
    });

    $(window).on('scroll', function () {
        if ($(window).scrollTop() < 400) { $("#sticky-header").removeClass("sticky"); }
        else { $("#sticky-header").addClass("sticky"); }
    });

})(jQuery);