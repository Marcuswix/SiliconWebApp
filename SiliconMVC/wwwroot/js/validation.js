function formErrorHandler(targetElement, validationResult)
{
    var termsspan = document.getElementById("termsspan");
    let spanElement = document.querySelector(`[data-valmsg-for="${targetElement.name}"]`);

    if (validationResult === true && targetElement.type === 'checkbox')
    {
        spanElement.classList.remove('field-validation-error');
        spanElement.classList.add('field-validation-valid');
        spanElement.classList.remove('checkmark');
        document.getElementById("termsspan").classList.remove("checkmark")
        spanElement.innerHTML = "";
    }
    if (validationResult === true) {
        targetElement.classList.remove('input-validation-error');
        spanElement.classList.add("checkmark")
        spanElement.classList.remove('field-validation-error');
        spanElement.classList.add('field-validation-valid');
        spanElement.innerHTML = "";
    } else {
        targetElement.classList.add('input-validation-error');
        spanElement.classList.add('field-validation-error');
        spanElement.classList.remove("checkmark")
        spanElement.classList.remove('field-validation-valid');
        spanElement.innerHTML = targetElement.dataset.valRequired;
    }

    termsspan.classList.remove('checkmark')
}

function subscribeFormErrorHandler(targetElement, validationResult)
{
    let spanElement = document.getElementById("AtLeastOneOptionSelected")

    let subscribeForm = document.getElementById("subscribeForm")

    let checkboxes = Array.from(subscribeForm.querySelectorAll('input[type="checkbox"]'));

    let atLeastOneChecked = checkboxes.some(checkbox => checkbox.checked);

    if (atLeastOneChecked === true) {

        spanElement.classList.remove('field-validation-error')
        spanElement.classList.add('field-validation-valid');
        spanElement.innerHTML = "";
    }
    else {
        spanElement.classList.add('field-validation-error')
        spanElement.classList.remove('field-validation-valid');
        spanElement.innerHTML = "At least one options is required";
    }
}

function emailValidator(targetElement) {
    const emailRegex = /^[^\s@]{2,}@[^@\s]{2,}\.[^\s@]{2,}$/;
    formErrorHandler(targetElement, emailRegex.test(targetElement.value));
}

function passwordValidator(targetElement) {

    var confirmPasswordInput = document.getElementById('confirmPassword-signUp');

    if (confirmPasswordInput)
    {
        confirmPasswordInput.addEventListener('keyup', (e) => {
        confirmPasswordValidator(e.target);
        });
    }
    const passwordRegex = /^(?=.*[A-Z])(?=.*[\W_])(?=.{8,})/;
    formErrorHandler(targetElement, passwordRegex.test(targetElement.value));
}

function confirmPasswordValidator(targetElement) {
    const confirmPasswordInput = document.getElementById("confirmPassword-signUp");
    const passwordInput = document.getElementById("password-signUp");

    if (confirmPasswordInput.value === passwordInput.value) {
        formErrorHandler(confirmPasswordInput, true);
    } else {
        formErrorHandler(confirmPasswordInput, false);
    }
}

function textValidator(targetElement, minLength = 2) {
    formErrorHandler(targetElement, targetElement.value.length >= minLength);
}

function checkboxValidator(targetElement) {


    if (targetElement.name === "StartUps" || targetElement.name === "Newsletter" || targetElement.name === "AdvertisingUpdates" || targetElement.name === "WeekInReview" || targetElement.name === "EventUpdates" || targetElement.name === "Podcasts")
    {
        if (targetElement.checked) {
            return subscribeFormErrorHandler(targetElement, true)
        }
        else {
            return subscribeFormErrorHandler(targetElement, false)
        }
    }
    if (targetElement.checked === true) {

        return formErrorHandler(targetElement, true)
    }
    else {
        return formErrorHandler(targetElement, false)
    }

    document.getElementById("termsspan").classList.remove("checkmark")
}

let forms = document.querySelectorAll('form');

forms.forEach(form => {
    let inputs = form.querySelectorAll('input, textarea');

    inputs.forEach(input => {
        if (input.dataset.val === 'true')
        {
            if (input.type === "checkbox")
            {
                input.addEventListener('change', (e) => {
                checkboxValidator(e.target)
                });
            }
            if (input.name === "textarea")
            {
                input.addEventListener('keyup', (e) => {
                    textValidator(e.target);
                });
            }
            else
            {
                input.addEventListener('keyup', (e) => {
                    switch (e.target.type) {
                        case 'text':
                            textValidator(e.target);
                            break;
                        case 'email':
                            emailValidator(e.target);
                            break;
                        case 'password':
                            passwordValidator(e.target);
                            break;
                        default:
                            textValidator(e.target);
                            break;
                    }
                });
            }
        }
    });
})







