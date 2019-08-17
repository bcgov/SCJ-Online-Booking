// Write your JavaScript code.
$(document).ready(function() {

    //Init Tooltips
    $('[data-toggle="tooltip"]').tooltip();

    window.addEventListener("load",
        function() {
            // Fetch all the forms we want to apply custom Bootstrap validation styles to
            var forms = document.getElementsByClassName("needs-validation");
            // Loop over them and prevent submission
            var validation = Array.prototype.filter.call(forms,
                function(form) {
                    form.addEventListener("submit",
                        function(event) {
                            if (form.checkValidity() === false) {
                                event.preventDefault();
                                event.stopPropagation();
                            }
                            form.classList.add("was-validated");
                        },
                        false);
                });
        },
        false);

    //caches a jQuery object containing the header element
    var header = $("#scj-header-nav");
    $(window).scroll(function() {
        var scroll = $(window).scrollTop();
        if (scroll >= 85) {
            header.addClass("affixed");
        } else {
            header.removeClass("affixed");
        }
    });

    // Homepage panels
    $("#btnSupreme").click(function(e) {
        e.preventDefault();
        $("#pnlSupreme").toggle();

        // scroll down to the bceid button
        if ($("#pnlSupreme").css("display") !== "none") {
            var panel = $("div[id='pnlSupreme']");
            $("html,body").animate({ scrollTop: panel.offset().top }, "fast");
        }
    });
});

function validateCaseDate(containerId, bookingDate) {
    //set hidden fields based on the swiper selected dates
    $("#hidContainerId").val(containerId);
    $("#hidDate").val(bookingDate);

    //simulate form submit
    $("#btnSearch").trigger("click");
}

$('body').on('click', 'input.progress-spinner, a.progress-spinner, button.progress-spinner', function () {
    $('div#progress-overlay').show();
    $('div#progress-overlay-spinner').spin();
});

// kills the spinner when the back button is pressed 
window.onunload = function () { };
