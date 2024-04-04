function areYouShure(email) {
    var elementId = 'areYouShure_' + email;
    document.getElementById(elementId).classList.add("showAreYouShureContainer");
}

function cancel(elementId) {
    document.getElementById(elementId).classList.remove("showAreYouShureContainer");
}


