document.addEventListener("DOMContentLoaded", () => {
    const passwordInput = document.getElementById("password");
    if (!passwordInput) return;

    passwordInput.addEventListener("input", (e) => {
        const val = e.target.value;
        const setState = (id, isValid) => {
            const li = document.getElementById(id);
            li.classList.toggle("valid", isValid);
            li.classList.toggle("invalid", !isValid);
            li.style.color = isValid ? "green" : "red";
        };

        setState("rule-length", val.length >= 8);
        setState("rule-uppercase", /[A-Z]/.test(val));
        setState("rule-digit", /\d/.test(val));
    });
});
