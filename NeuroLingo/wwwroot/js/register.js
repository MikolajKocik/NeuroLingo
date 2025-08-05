document.addEventListener("DOMContentLoaded", () => {

    // register hint visible
    const passwordInput = document.getElementById("password");
    const hint = document.querySelector(".register-hint");
    const ruleIds = ["rule-length", "rule-uppercase", "rule-digit"];

    if (passwordInput && hint) {
        hint.classList.remove("visible");

        passwordInput.addEventListener("focus", () => {
            hint.classList.add("visible");
        });

        passwordInput.addEventListener("blur", () => {
            hint.classList.remove("visible");

            ruleIds.forEach(id => {
                const li = document.getElementById(id);
                if (!li) return;
                li.classList.remove("valid", "invalid");
                li.style.color = "";
            })
        });
    }

    // dynamic password check  
    if (passwordInput) {
        passwordInput.addEventListener("input", (e) => {
            const val = e.target.value;
            const setState = (id, isValid) => {
                    const li = document.getElementById(id);
                    if (li) {
                        li.classList.toggle("valid", isValid);
                        li.classList.toggle("invalid", !isValid);
                        li.style.color = isValid ? "green" : "red";
                    }                
            };    

            setState("rule-length", val.length >= 8);
            setState("rule-uppercase", /[A-Z]/.test(val));
            setState("rule-digit", /\d/.test(val));
        });
    }

    // AJAX
    const loginForm = document.getElementById("registerForm");
    if (loginForm) {

        loginForm.addEventListener("submit", function (event) {
            event.preventDefault();

            // get data from form
            const formData = new FormData(loginForm);
            const data = new URLSearchParams(formData).toString();

            // send AJAX

            fetch('/Auth/Register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'RequestVerificationToken': document.querySelector('[name="__RequestVerificationToken"]').value
                },
                body: data
            })
                .then(response => response.json())
                .then(result => {
                    if (result.IsSuccess) {
                        alert(result.Message);
                        window.location.href = '/Home';
                    } else {
                        const errors = result.Errors || [];
                        errors.forEach(error => {
                            alert(error);
                        });
                    }
                })
                .catch(error => {
                    alert('Error occured during login action');
                    console.error('Error', error);
                });
        });
    }
});
