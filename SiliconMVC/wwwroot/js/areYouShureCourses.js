function areYouShureCourses(id) {
    console.log("inne")
    var elementId = 'areYouShureCourses_' + id;
    document.getElementById(elementId).classList.add("showAreYouShureContainer");
}

function cancelCourses(elementId) {
    document.getElementById(elementId).classList.remove("showAreYouShureContainer");
}