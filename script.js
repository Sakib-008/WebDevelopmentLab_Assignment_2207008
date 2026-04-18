(function () {
    /*
     * Updates footer text so the current year is always correct. It is replaced dynamically.
     */
    function updateFooterYear() {
        var footerText = document.querySelector('.site-footer p');
        if (!footerText) {
            return;
        }

        footerText.textContent = footerText.textContent.replace(/\b\d{4}\b/, String(new Date().getFullYear()));
    }

    /*
     * Adds validation for the registration form.
     * 1) Email must use the kuet.ac.bd domain.
     * 2) Password must be at least 8 characters.
     * 3) At least one area of interest must be selected.
     */
    function setupRegisterValidation() {
        var registerForm = document.querySelector('form[action="login.html"]');
        if (!registerForm) {
            return;
        }

        registerForm.addEventListener('submit', function (event) {
            var emailInput = document.getElementById('student-email');
            var passwordInput = document.getElementById('password');
            var interestInputs = document.querySelectorAll('input[name="interest"]');

            var emailValue = emailInput ? emailInput.value.trim().toLowerCase() : '';
            var validDomain = /@kuet\.ac\.bd$/;
            if (!validDomain.test(emailValue)) {
                event.preventDefault();
                alert('Please use your KUET email address (example: name@kuet.ac.bd).');
                if (emailInput) {
                    emailInput.focus();
                }
                return;
            }

            if (!passwordInput || passwordInput.value.length < 8) {
                event.preventDefault();
                alert('Password must be at least 8 characters long.');
                if (passwordInput) {
                    passwordInput.focus();
                }
                return;
            }

            var hasSelectedInterest = Array.prototype.some.call(interestInputs, function (input) {
                return input.checked;
            });

            if (!hasSelectedInterest) {
                event.preventDefault();
                alert('Please select at least one area of interest.');
            }
        });
    }

    /**
     * Adds validation for the login form.
     */
    function setupLoginValidation() {
        var loginForm = document.querySelector('form[action="index.html"]');
        if (!loginForm) {
            return;
        }

        loginForm.addEventListener('submit', function (event) {
            var emailInput = document.getElementById('login-email');
            var emailValue = emailInput ? emailInput.value.trim().toLowerCase() : '';

            if (!/@kuet\.ac\.bd$/.test(emailValue)) {
                event.preventDefault();
                alert('Login requires a KUET email address (name@kuet.ac.bd).');
                if (emailInput) {
                    emailInput.focus();
                }
            }
        });
    }

    // Run setup after the DOM is ready.
    document.addEventListener('DOMContentLoaded', function () {
        updateFooterYear();
        setupRegisterValidation();
        setupLoginValidation();
    });
})();
