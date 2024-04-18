function showHideMessage() {
    if (contactForm.classList.contains("hideForm")) {
        contactForm.classList.remove("hideForm")
        applicationForm.classList.add("hideForm")

    }
    else {
        contactForm.classList.add("hideForm")
    }
}

function showHideApplication() {
    if (applicationForm.classList.contains("hideForm")) {
        applicationForm.classList.remove("hideForm")
        contactForm.classList.add("hideForm")
    }
    else {
        applicationForm.classList.add("hideForm")
    }
}

function hideMessage() {
    contactForm.classList.add("hideForm")
    applicationForm.classList.add("hideForm")
}

const messageName = document.getElementById("messageName");
const messageEmail = document.getElementById("messageEmail");
const messageMessage = document.getElementById("messageMessage").classList.add("field-validation-error");
const filloutForm = document.getElementById("fillOutForm");

function onsubmitMessage(event)
{
    try
    {
        var hasError = true;

        if (messageName.classList.contains("field-validation-valid") && messageEmail.classList.contains("field-validation-valid") && !messageMessage.classList.includes("field-validation-error") && messageMessage.classList.contains("field-validation-valid"))
            {
                console.log("InneInneInneInne");
                console.log("Felaktigt span-element:", span.innerText);
                hasError = false;
                return;
            }

        console.log(hasError);

        if (hasError) {
            event.preventDefault();
            filloutForm.classList.remove("hideSpan");
        }
        else {
            console.log("inneInne");
        }
    }
    catch {
        console.log(Error.Error)
    }
}

const applicationName = document.getElementById("applicationName");
const applicationEmail = document.getElementById("applicationEmail");
const applicationMessage = document.getElementById("applicationMessage").classList.add("field-validation-error");
const applicationfilloutForm = document.getElementById("applicationfillOutForm");


function onsubmitApplication(event) {
    try {
        var hasError = true;

        if (applicationName.classList.contains("field-validation-valid") && applicationEmail.classList.contains("field-validation-valid") && !applicationMessage.classList.includes("field-validation-error") && applicationMessage.classList.contains("field-validation-valid"))
        {
            console.log("InneInneInneInne");
            console.log("Felaktigt span-element:", span.innerText);
            hasError = false;
            return;
        }

        console.log(hasError);

        if (hasError) {
            event.preventDefault();
            applicationfilloutForm.classList.remove("hideSpan");
        }
        else {
            console.log("inneInne");
        }
    }
    catch {
        console.log(Error.Error)
    }
}

