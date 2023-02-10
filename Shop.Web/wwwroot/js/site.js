// Write your JavaScript code.

const headerElement = document.querySelector("nav");

window.onload = function () {
    var match = document.cookie.match(new RegExp('(^| )' + "Theme" + '=([^;]+)'));
    if (match && match[2] === "Dark") {
        setDarkTheme();
    }
}

function setDarkTheme() {
    headerElement.classList.add("theme-dark");
    saveThemeCookie("Dark", 1);
}

function removeDarkTheme() {
    headerElement.classList.remove("theme-dark");
    saveThemeCookie("Light", 1);
}

function saveThemeCookie(cookieValue, expDays) {
    const date = new Date();
    let cookieName = "Theme";
    date.setTime(date.getTime() + (expDays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + date.toUTCString();
    document.cookie = cookieName + "=" + cookieValue + ";" + expires + ";path=/";
}