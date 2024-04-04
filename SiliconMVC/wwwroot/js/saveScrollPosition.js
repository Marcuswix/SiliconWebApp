window.onbeforeunload = function () {
    sessionStorage.setItem("scrollPosition", window.scrollY);
};

document.addEventListener("DOMContentLoaded", function () {
    var scrollPosition = sessionStorage.getItem("scrollPosition");

/*    document.body.style.display = "none"*/


    if (scrollPosition !== null) {

        setTimeout(function () {
            window.scrollTo({
                top: parseInt(scrollPosition),
                behavior: "instant",
            });

            sessionStorage.removeItem("scrollPosition");
        }, 100);

        document.body.style.display = "block"

    }
});

