// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write here your JavaScript code.

function listenerToggleShowHidePassword(event){
    event.preventDefault();

    const toggle = event.target;
    const togglePassword = document.querySelector("#toggle-password");
    const toggleConfirmPassword = document.querySelector("#toggle-confirm-password");
    let password = null;

    switch (toggle) {
    case togglePassword:
        password = document.querySelector("#password");
        break;
    case toggleConfirmPassword:
        password = document.querySelector("#confirm-password");
        break;
    }

    if (password) {
        // Toggle the type attribute using getAttribute() method
        const type = password.getAttribute("type") === "password"
            ? "text"
            : "password";
        password.setAttribute("type", type);
    }

    // Toggle the eye and bi-eye icon
    toggle.classList.toggle("bi-eye");
};