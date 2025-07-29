$(document).ready(function () {
    var input = document.querySelector("#phone");
    var iti = window.intlTelInput(input, {
        initialCountry: "in",
        separateDialCode: true,
    });

    $("form").on("submit", function (e) {
        let valid = true;
        $(".error-message").remove();

        const email = $("input[name='Email']").val();
        const password = $("#password").val();
        const confirmPassword = $("#confirmPassword").val();
        const contact = $("#phone").val();
        const file = $("input[name='ProfilePicture']").val();

        // Set country code
        const dialCode = iti.getSelectedCountryData().dialCode;
        $("#countryCode").val(dialCode);

        // Email
        if (!email.match(/^[^\s@]+@[^\s@]+\.[^\s@]+$/)) {
            valid = false;
            $("input[name='Email']").after("<div class='error-message'>Enter a valid email.</div>");
        }

        // Passwords
        if (!password) {
            valid = false;
            $("#password").after("<div class='error-message'>Password is required.</div>");
        }
        if (password !== confirmPassword) {
            valid = false;
            $("#confirmPassword").after("<div class='error-message'>Passwords do not match.</div>");
        }

        // Contact Number
        if (!contact.match(/^[0-9]{10}$/)) {
            valid = false;
            $("#phone").after("<div class='error-message'>Enter a valid 10-digit number.</div>");
        }

        // File
        if (file) {
            let ext = file.split('.').pop().toLowerCase();
            if ($.inArray(ext, ['png', 'jpg', 'jpeg']) === -1) {
                valid = false;
                $("input[name='ProfilePicture']").after("<div class='error-message'>Only .jpg, .jpeg, .png allowed.</div>");
            }
        }

        if (!valid) e.preventDefault();
    });
});
