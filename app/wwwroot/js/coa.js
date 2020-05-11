$(document).ready(function () {

    var validCaseSelection = true;

    // Give active style class to checked radio button input
    var $radiobtns = $('input[type="radio"]');
    $radiobtns.click(function () {
        $radiobtns.each(function () {
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
                    $(".alert--related-cases").hide();
                    validCaseSelection = true;
                }

                else {
                    //If the main is not checked, and the current checked one is another sub
                    $(".alert--related-cases").show();
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
                    $(".alert--related-cases").show();
                    validCaseSelection = false;
                }
            }

            else {
                if ($checked.length == 1) {
                    //If the main is not checked
                    //AND there is only 1 other sub option that is checked
                    $(".alert--related-cases").hide();
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
    var toggleCivil = function () {
        var $Civil_CertificateOfReadiness = $('#Civil_CertificateOfReadiness input[type="radio"]:checked').val();
        var $Civil_DateIsAgreed = $('#Civil_DateIsAgreed input[type="radio"]:checked').val();
        if ($Civil_CertificateOfReadiness === "true" && $Civil_DateIsAgreed === "true" && validCaseSelection) {
            $("#CivilAdditionalQs").show();
        } else {
            $("#CivilAdditionalQs").hide();
        }
    };

    $('#Civil_CertificateOfReadiness input[type="radio"]').click(toggleCivil);
    $('#Civil_DateIsAgreed input[type="radio"]').click(toggleCivil);

    //Display last row of questions for Criminal case if previous questions are answered Yes
    //And selection of related case files is valid
    var toggleCriminal = function () {
        var $Criminal_DateIsAgreed = $('#Criminal_DateIsAgreed input[type="radio"]:checked').val();
        if ($Criminal_DateIsAgreed === "true" && validCaseSelection) {
            $("#CriminalAdditionalQs").css("display", "flex");
        } else {
            $("#CriminalAdditionalQs").hide();
        }
    };

    $('#Criminal_DateIsAgreed input[type="radio"]').click(toggleCriminal);


    //Display Show Available Dates button when all fields are correctly selected
    //and display errors for required preliminary questions
    $('.preliminary_questions input[type="radio"], .preliminary_questions select, input[name="SelectedCases"]').change(function () {
        var caseType = $("#CaseType").val().toLowerCase();
        var $radioBtnGroup = $(this).parent().parent();

        if ($radioBtnGroup.hasClass("preliminary_questions__radio")) {
            var $radioBtnValue = $(this).val();

            if ($radioBtnValue === "false") {
                $radioBtnGroup.siblings(".alert--preliminary_question").show();
                $radioBtnGroup.siblings(".notice--preliminary_question").hide();
            } else if ($radioBtnValue === "true") {
                $radioBtnGroup.siblings(".alert--preliminary_question").hide();
                $radioBtnGroup.siblings(".notice--preliminary_question").show();
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
                $("#btnShowDates").show();
            } else {
                $("#btnShowDates").hide();
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
                $("#btnShowDates").show();
            } else {
                $("#btnShowDates").hide();
            }
        }
    });

    //Display more available dates by groups of 2 months
    $("#btnShowMore").click(function () {
        $(".availableDates__month.hidden").slice(0, 2).removeClass("hidden");

        if (!($(".availableDates__month.hidden").length)) {
            $("#btnShowMore").hide();
        }
    });


    //Submitting selected date for Coa
    $("a.availableDate").click(function () {
        $("input#SelectedDate").val($(this).data("date"));
        $("#CaseSearchForm").submit();
    });

});
