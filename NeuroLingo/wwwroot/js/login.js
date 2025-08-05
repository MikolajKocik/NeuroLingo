document.addEventListener("DOMContentLoaded", () => {

    // AJAX
    const loginForm = document.getElementById("loginForm");
    if (loginForm) {

        loginForm.addEventListener("submit", function (event) {
            event.preventDefault();

            // get data from form
            const formData = new FormData(loginForm);
            const data = new URLSearchParams(formData).toString();

            // send AJAX

            fetch('/Auth/Login', {
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
