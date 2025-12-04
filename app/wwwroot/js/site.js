// setup Bootstrap 4 Forms Validation

(function () {
  "use strict";
  window.addEventListener(
    "load",
    function () {
      // Fetch all the forms we want to apply custom Bootstrap validation styles to
      var forms = document.getElementsByClassName("needs-validation");
      // Loop over them and prevent submission
      var validation = Array.prototype.filter.call(forms, function (form) {
        form.addEventListener(
          "submit",
          function (event) {
            if (form.checkValidity() === false) {
              event.preventDefault();
              event.stopPropagation();
              // cancel the spinner
              window.setTimeout(function () {
                $("div#progress-overlay").hide();
              }, 100);
            }
            form.classList.add("was-validated");
          },
          false
        );
      });
    },
    false,
  );
})();

$(document).ready(function () {
  // Init Tooltips
  $('[data-toggle="tooltip"]').tooltip();

  //caches a jQuery object containing the header element
  var header = $("#scj-header-nav");
  $(window).scroll(function () {
    var scroll = $(window).scrollTop();
    if (scroll >= 85) {
      header.addClass("affixed");
    } else {
      header.removeClass("affixed");
    }
  });

  // phone number inputs - form field data tidy up
  $("input.phone-input").change(function () {
    var phone = this.value.replace(/[^0-9]/g, "");

    if (phone.indexOf("1") === 0) {
      phone = phone.substr(1);
    }

    if (phone.length === 10) {
      this.value = phone.replace(/(\d{3})(\d{3})(\d{4})/, "$1-$2-$3");
    }
  });

  // Set up the progress spinner overlay
  $("body").on(
    "click",
    "input.progress-spinner, a.progress-spinner, button.progress-spinner",
    function () {
      $("div#progress-overlay").show();
      $("div#progress-overlay-spinner").spin();
    },
  );

  // kills the spinner when the back button is pressed
  window.onunload = function () {};
});

// logout
function submitLogoutForm(e) {
  e.preventDefault();
  document.getElementById("logout_form").submit();
}
