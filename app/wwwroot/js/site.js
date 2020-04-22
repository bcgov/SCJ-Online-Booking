// setup Bootstrap 4 Forms Validation

(function() {
    "use strict";
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
                                // cancel the spinner
                                window.setTimeout(function() {
                                        $("div#progress-overlay").hide();
                                    },
                                    100);
                            }
                            form.classList.add("was-validated");
                        },
                        false);
                });
        },
        false);
})();

var validCaseSelection = true;

$(document).ready(function () {

    // Init Tooltips
    $('[data-toggle="tooltip"]').tooltip();


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


    // phone number inputs - form field data tidy up
    $("input.phone-input").change(function() {
        var phone = this.value.replace(/[^0-9]/g, "");

        if (phone.indexOf("1") === 0) {
            phone = phone.substr(1);
        }

        if (phone.length === 10) {
            this.value = phone.replace(/(\d{3})(\d{3})(\d{4})/, "$1-$2-$3");
        }
    });


    // Set up the progress spinner overlay
    $("body").on("click",
        "input.progress-spinner, a.progress-spinner, button.progress-spinner",
        function() {
            $("div#progress-overlay").show();
            $("div#progress-overlay-spinner").spin();
        });

    // kills the spinner when the back button is pressed 
    window.onunload = function() {};

    // Give active style class to checked radio button input
    var $radiobtns = $('input[type="radio"]');
    $radiobtns.click(function() {
        $radiobtns.each(function() {
            $(this).parent().toggleClass("active", this.checked);
        });
    });


    //Set checked attribute dynamically from clicks and
    //Display error message on relate case file selection
    var $checkboxes = $('.related-cases input[name="SelectedCases"]');
    $checkboxes.click(function () {
        if ($(this).is(':checked')) {

            if (!($checkboxes.filter('[data-main="True"]').attr("checked") == "checked")) {
                if ($(this).attr("data-main") == "True") {
                    //If the main is not checked, and the current checked one is the main
                    $(".alert--related-cases").css("display", "none");
                    validCaseSelection = true;
                }

                else {
                    //If the main is not checked, and the current checked one is another sub
                    $(".alert--related-cases").css("display", "block");
                    validCaseSelection = false;
                }
            }

            this.setAttribute("checked", "checked");
            this.checked = true;
        }

        else {
            var $checked = $('.related-cases input[name="SelectedCases"]:checked');

            if ($checkboxes.filter('[data-main="True"]').attr("checked") == "checked") {
                if ($(this).attr("data-main") == "True" && $checked.length > 1) {
                    //If the main is checked, and the current unchecked one is the main
                    //AND there are more than 1 sub options that are checked
                    $(".alert--related-cases").css("display", "block");
                    validCaseSelection = false;
                }
            }

            else {
                if ($checked.length == 1) {
                    //If the main is not checked
                    //AND there is only 1 other sub option that is checked
                    $(".alert--related-cases").css("display", "none");
                    validCaseSelection = true;
                }
            }

            this.setAttribute("checked", ""); // For IE
            this.removeAttribute("checked"); // For other browsers
            this.checked = false;
        }

        var caseType = $("#CaseType").val().toLowerCase();
        if (caseType === "civil") {
            toggleCivil();
        }

        if (caseType === "criminal") {
            toggleCriminal();
        }
    });


    //Display last row of questions for Civil case if previous questions are answered Yes
    //And selection of related case files is valid
    var toggleCivil = function() {
        var $Civil_CertificateOfReadiness = $('#Civil_CertificateOfReadiness input[type="radio"]:checked').val();
        var $Civil_DateIsAgreed = $('#Civil_DateIsAgreed input[type="radio"]:checked').val();
        if ($Civil_CertificateOfReadiness === "true" && $Civil_DateIsAgreed === "true" && validCaseSelection) {
            $("#CivilAdditionalQs").css("display", "block");
        } else {
            $("#CivilAdditionalQs").css("display", "none");
        }
    };

    $('#Civil_CertificateOfReadiness input[type="radio"]').click(toggleCivil);
    $('#Civil_DateIsAgreed input[type="radio"]').click(toggleCivil);

    //Display last row of questions for Criminal case if previous questions are answered Yes
    //And selection of related case files is valid
    var toggleCriminal = function() {
        var $Criminal_DateIsAgreed = $('#Criminal_DateIsAgreed input[type="radio"]:checked').val();
        if ($Criminal_DateIsAgreed === "true" && validCaseSelection) {
            $("#CriminalAdditionalQs").css("display", "flex");
        } else {
            $("#CriminalAdditionalQs").css("display", "none");
        }
    };

    $('#Criminal_DateIsAgreed input[type="radio"]').click(toggleCriminal);


    //Display Show Available Dates button when all fields are correctly selected
    //and display errors for required preliminary questions
    $('.preliminary_questions input[type="radio"], .preliminary_questions select, input[name="SelectedCases"]').change(function() {
        var caseType = $("#CaseType").val().toLowerCase();
        var $radioBtnGroup = $(this).parent().parent();

        if ($radioBtnGroup.hasClass("preliminary_questions__radio")) {
            var $radioBtnValue = $(this).val();

            if ($radioBtnValue === "false") {
                $radioBtnGroup.siblings(".alert--preliminary_question").css("display", "block");
                $radioBtnGroup.siblings(".notice--preliminary_question").css("display", "none");
            } else if ($radioBtnValue === "true") {
                $radioBtnGroup.siblings(".alert--preliminary_question").css("display", "none");
                $radioBtnGroup.siblings(".notice--preliminary_question").css("display", "block");
            }
        }

        if (caseType === "civil") {
            var $Civil_CertificateOfReadiness = $('#Civil_CertificateOfReadiness input[type="radio"]:checked').val();
            var $Civil_DateIsAgreed = $('#Civil_DateIsAgreed input[type="radio"]:checked').val();
            var $Civil_IsFullDay = $('#Civil_IsFullDay input[type="radio"]:checked').val();

            if ($Civil_CertificateOfReadiness === "true" &&
                $Civil_DateIsAgreed === "true" &&
                ($Civil_IsFullDay === "true" || $Civil_IsFullDay === "false") &&
                validCaseSelection) {
                $("#btnShowDates").css("display", "block");
            } else {
                $("#btnShowDates").css("display", "none");
            }
        }

        if (caseType === "criminal") {
            var $Criminal_DateIsAgreed =
                $('#Criminal_DateIsAgreed input[type="radio"]:checked').val();
            var $Criminal_IsFullDay = $('#Criminal_IsFullDay input[type="radio"]:checked').val();

            var $Criminal_HearingTypeId = $("select#HearingTypeId").val();

            if ($Criminal_DateIsAgreed === "true" &&
                ($Criminal_IsFullDay === "true" || $Criminal_IsFullDay === "false") &&
                $Criminal_HearingTypeId !== "" &&
                validCaseSelection) {
                $("#btnShowDates").css("display", "block");
            } else {
                $("#btnShowDates").css("display", "none");
            }
        }
    });

    //Display more available dates by groups of 2 months
    $("#btnShowMore").click(function() {
        $(".availableDates__month.hidden").slice(0, 2).removeClass("hidden");

        if (!($(".availableDates__month.hidden").length)) {
            $("#btnShowMore").css("display", "none");
        }
    });


    //Submitting selected date for Coa
    $("a.availableDate").click(function() {
        $("input#SelectedDate").val($(this).data("date"));
        $("#CaseSearchForm").submit();
    });

});


// Called by Vue when a timeslot is selected
function validateCaseDate(containerId, bookingDate) {
    //set hidden fields based on the swiper selected dates
    $("#hidContainerId").val(containerId);
    $("#hidDate").val(bookingDate);

    //submit the form to go to the next page
    $("#btnSearch").trigger("click");
}


//Pre-filling input field based on selection of court class on the Supreme Court side
var caseNumberInput = document.getElementById('caseNumberInput');
var courtClassDropdown = document.getElementById('courtClassDropdown');

courtClassDropdown.onchange = function () {
    caseNumberInput.value = this.value;
}
