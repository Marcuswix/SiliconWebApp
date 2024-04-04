document.addEventListener("DOMContentLoaded", function () {
    let switchButton = document.querySelector('#switch-mode');

    switchButton.addEventListener('change', function () {

        let theme = this.checked ? "dark" : "light"

        // An AJAX call that changes the theme of the page without reloading
        fetch(`/SiteSettings/ChangeTheme?theme=${theme}`)
            .then(res => {
                if (res.ok) {
                    window.location.reload();
                }
                else {
                    console.log("Something went wrong");
                }
            })
    })
})
