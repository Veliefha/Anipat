// api-service.js
fetch("/api/Service")
    .then(response => {
        if (!response.ok) throw new Error("Şəbəkə xətası!");
        return response.json();
    })
    .then(services => {
        const container = document.getElementById("services-container");
        if (!container) return;

        container.innerHTML = ""; // İçini tam təmizləyirik

        services.forEach(service => {
            // Sığorta: Həm "Name", həm "name" variantını yoxlayırıq
            const name = service.name || service.Name || "Adsız xidmət";
            const description = service.description || service.Description || "Təsvir yoxdur";
            const icon = service.icon || service.Icon || "service_icon_1.png";

            const div = document.createElement("div");
            div.className = "col-lg-4 col-md-6";
            div.innerHTML = `
                <div class="single_service">
                     <div class="service_thumb service_icon_bg_1 d-flex align-items-center justify-content-center">
                         <div class="service_icon">
                             <img src="/img/service/${icon}" alt="${name}">
                         </div>
                     </div>
                     <div class="service_content text-center">
                        <h3>${name}</h3>
                        <p>${description}</p>
                     </div>
                </div>
            `;
            container.appendChild(div);
        });
    })
    .catch(err => console.error("Xəta baş verdi:", err));