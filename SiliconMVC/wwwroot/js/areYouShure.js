function areYouShure(email) {
    var elementId = 'areYouShure_' + email;
    document.getElementById(elementId).classList.add("showAreYouShureContainer");
}

function cancel(elementId) {
    document.getElementById(elementId).classList.remove("showAreYouShureContainer");
}

function areYouShureDeleteAccount() {
    var check = document.getElementById("flexCheckDefault");
    var spanElement = document.querySelector("#deleteTermsAndConditions"); 

    if (check.checked) {
        document.getElementById("areYouShureDeleteAccount").classList.add("showAreYouShureContainer");
        spanElement.textContent = "";
        spanElement.classList.remove('field-validation-error');
    } else {
        spanElement.textContent = "You must confirm the delete terms & conditions.";
        spanElement.classList.add('field-validation-error');
    }
}

// Lägg till händelsehanterare för ändring av kryssrutan
var check = document.getElementById("flexCheckDefault");
var spanElement = document.querySelector("#deleteTermsAndConditions");

check.addEventListener("change", function () {
    if (check.checked) {
        spanElement.textContent = "";
        spanElement.classList.remove('field-validation-error');
    } else {
        spanElement.textContent = "You must confirm the delete terms & conditions.";
        spanElement.classList.add('field-validation-error');
    }
});


function cancelDeleteAccount() {
    document.getElementById("areYouShureDeleteAccount").classList.remove("showAreYouShureContainer");

}


