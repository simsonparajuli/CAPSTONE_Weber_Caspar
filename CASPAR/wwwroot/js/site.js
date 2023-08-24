// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const COOKIE_NAME = "DropDownsOpen";

function persistToNavBarDropDown(id) {
    // If the id already exists as a cookie remove it
    let cookie = getCookie(COOKIE_NAME);
    if (removePersistToNavBarDropDown(id)) {
        ;
    }
    else {
        // Get previous clicked dropdowns then add the new one to the list
        setCookie(COOKIE_NAME, id + "," + cookie);
    }
}

/** 
 * If a id already exists in the cookie remove it
 * @param {string} id the string we are removing
 * @returns {boolean} Returns true if a value is removed
 */
function removePersistToNavBarDropDown(id) {
    let cookie = getCookie(COOKIE_NAME);
    let ca = cookie.split(',');
    let startIndex = 0;
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        if (c.indexOf(id) == 0) {
            // Remove the cookie from the middle of the string  Instructor,Student,Staff => Instructor,Staff
            cookie = cookie.substring(0, startIndex) + cookie.substring(startIndex + c.length + 1);
            setCookie(COOKIE_NAME, cookie);
            return true;
        }
        startIndex += c.length + 1; // + 1 for the comma
    }
    return false;
}

/**
 * Delete all the persists on the nav bar
 */
function clearAllPersistToNavBarDropDown() {
    setCookie(COOKIE_NAME, '');
}

function loadNavBarDropDown() {
    let cookie = getCookie(COOKIE_NAME);
    let ca = cookie.split(',');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        if (c == "") {
            continue;
        }
        let trigger = document.querySelector(`a[href='#${c}']`);
        trigger.setAttribute('aria-expanded', 'true');

        let dropdown = document.getElementById(c);
        dropdown.classList.add('show');
        //dropdown.style.transitionDuration = "0s";
    //    let collapse = new bootstrap.Collapse(dropdown, { show: true });
    }
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function setCookie(cname, cvalue) {
    document.cookie = cname + "=" + cvalue + ";" + "path=/";
}



// Open the dropdowns on page load
window.onload = function () {
    loadNavBarDropDown();
};