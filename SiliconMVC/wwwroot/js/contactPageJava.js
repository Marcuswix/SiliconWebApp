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
const messageMessage = document.getElementById("messageMessage");

function onsubmitMessage(event)
{
    try
    {
        var hasError = true;

            if (messageName.classList.contains("field-validation-valid") && messageEmail.classList.contains("field-validation-valid") && messageMessage.classList.contains("field-validation-valid"))
            {
                console.log("InneInneInneInne");
                console.log("Felaktigt span-element:", span.innerText);
                hasError = false;
                return;
            }

        console.log(hasError);

        if (hasError) {
            event.preventDefault();
            document.getElementById("fillOutForm").classList.remove("hideSpan");
        }
        else {
            console.log("inneInne");
        }
    }
    catch {
        console.log(Error.Error)
    }
}

