const toggleButton = document.getElementById("hamburger");
const menu = document.getElementById("menu-mobil");
const main = document.getElementById("main");
const footer = document.getElementById("footer");
const contactForm = document.getElementById("contact-us");
const closeContactForm = document.getElementById("x-mark-contact");
const applicationForm = document.getElementById("application");
const header = document.getElementById("header");
const subscribeForm = document.getElementById("subscribeForm");
var scrollPosition;

// En "eventlyssnare" för klickhändelser på hela dokumentet
document.addEventListener('click', (event) => {
    const target = event.target;

    var currentPage = window.location.pathname

    if (target !== menu && target !== toggleButton) {
        if (menu.classList.contains('open')) {
            menu.classList.remove('open');
            toggleButton.classList.remove('fa-xmark');
            toggleButton.classList.add('fa-bars');
            main.classList.remove('overlay')
        }
    }

    if (currentPage === "/contact")
    {
        header.classList.add("headerGrayColor");
    }
    else
    {
        header.classList.remove("headerGrayColor");
    }
});

toggleButton.addEventListener("click", () => {
    menu.classList.toggle('open');

    if (menu.classList.contains('open')) {
        toggleButton.classList.remove('fa-bars');
        toggleButton.classList.add('fa-xmark');
        main.classList.add('overlay');
        footer.classList.add('overlay');
    }
    else {
        toggleButton.classList.remove('fa-xmark');
        toggleButton.classList.add('fa-bars');
        main.classList.remove('overlay')
        footer.classList.remove('overlay')
    }
});


function saveScrollPosition() {
    scrollPosition = window.pageYOffset || document.documentElement.scrollTop;
}

window.onload = function () {
    window.scrollTo(0, scrollPosition);
};

function showHideMessage()
{
    if (contactForm.classList.contains("hideForm")) {
        contactForm.classList.remove("hideForm")
        applicationForm.classList.add("hideForm")

    }
    else {
        contactForm.classList.add("hideForm")
    }
}

function showHideApplication() {
    if (applicationForm.classList.contains("hideForm"))
    {
        applicationForm.classList.remove("hideForm")
        contactForm.classList.add("hideForm")
    }
    else 
    {
        applicationForm.classList.add("hideForm")
    }
}

function hideMessage() {
    contactForm.classList.add("hideForm")
    applicationForm.classList.add("hideForm")
}

const messageApplication = document.getElementById("ThanksforyourMessage");

function onsubmitMessage() {

    setTimeout(function () {
        messageApplication.classList.remove("success");
        messageApplication.classList.add("hide");
    }, 3000);
}
const messageMessage = document.getElementById("thanksforyourApplication");
function onsubmitApplication() {

    setTimeout(function () {
        messageMessage.classList.remove("success");
        messageMessage.classList.add("hide");
    }, 3000);
}


document.addEventListener("DOMContentLoaded", function () {
    var links = document.querySelectorAll("#AccountMenu a");

    links.forEach(function (link) {
        if (link.href === window.location.href) {
            link.classList.add("active");
        }
    });
});
