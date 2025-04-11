function setActiveTab(clickedLink) {
    const tabs = document.querySelectorAll('#loginTabs .nav-link');
    tabs.forEach(tab => tab.classList.remove('active'));
    clickedLink.classList.add('active');

    const tabText = clickedLink.innerText.trim();

    const loginForm = document.getElementById('loginForm');
    const cadastroForm = document.getElementById('cadastroForm');

    if (tabText === "Entrar") {
        loginForm.classList.remove('d-none');
        cadastroForm.classList.add('d-none');
    } else {
        loginForm.classList.add('d-none');
        cadastroForm.classList.remove('d-none');
    }
}

document.addEventListener("DOMContentLoaded", function () {
    const isLoginPage = window.location.pathname.toLowerCase().includes("/login");
    if (isLoginPage) {
        document.body.style.overflow = "hidden";
    }
});