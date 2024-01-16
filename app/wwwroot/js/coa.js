$(document).ready(function () {

    var validCaseSelection = true;

    // Give active style class to checked radio button input
    var $radiobtns = $('input[type="radio"]');
    $radiobtns.click(function () {
        $radiobtns.each(function () {
            if (!$(this).parent().hasClass("disabled")) {
                $(this).parent().toggleClass("active", this.checked);
            }
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

        var hearingType = $('#IsAppealHearing:checked').val();
        if (hearingType === "true") {
            toggleAppeal();
        }

        if (hearingType === "false") {
            toggleCriminal();
        }
    });

    //Display last row of questions for Civil case if previous questions are answered Yes
    //And selection of related case files is valid
    var toggleCriminal = function () {
        var $Civil_FactumFiled = $('#Civil_FactumFiled input[type="radio"]:checked').val();
        if ($Civil_FactumFiled === "true" && validCaseSelection) {
            $("#AppealsAdditionalQs").show();
        } else {
            $("#AppealsAdditionalQs").hide();
        }
    };

    $('#Civil_FactumFiled input[type="radio"]').click(toggleCriminal);
    $('#DateIsAgreed input[type="radio"]').click(toggleCriminal);

    //Display last row of questions for Criminal case if previous questions are answered Yes
    //And selection of related case files is valid
    var toggleAppeal = function () {
        var $Appeal_FactumFiled = $('#Appeal_FactumFiled input[type="radio"]:checked').val();
        var $IsButtonDisabled = $('#Appeal_FactumFiled input[type="radio"]:checked').parent().hasClass('disabled');
        if (!$IsButtonDisabled) {
            if ($Appeal_FactumFiled === "true" && validCaseSelection) {
                $("#AppealAdditionalQs").css("display", "flex");
            } else {
                $("#AppealAdditionalQs").hide();
            } 
        }
    };

    $('#Appeal_FactumFiled input[type="radio"]').click(toggleAppeal);


    //Display Show Available Dates button when all fields are correctly selected
    //and display errors for required preliminary questions
    $('.preliminary_questions input[type="radio"], input[name="SelectedCases"]').change(function () {
        var hearingType = $('#IsAppealHearing:checked').val();
        var $radioBtnGroup = $(this).parent().parent();
        
        if ($radioBtnGroup.hasClass("preliminary_questions__radio")) {
            var $radioBtnValue = $(this).val();
            if ($radioBtnValue === "false" && !$(this).parent().hasClass('disabled')) {
                $radioBtnGroup.siblings(".alert--preliminary_question").show();
                $radioBtnGroup.siblings(".notice--preliminary_question").hide();
            } else if ($radioBtnValue === "true" && !$(this).parent().hasClass('disabled')) {
                $radioBtnGroup.siblings(".alert--preliminary_question").hide();
                $radioBtnGroup.siblings(".notice--preliminary_question").show();
            }
        }

        //hearing type is appeals
        if (hearingType === "true") {
            var $Appeal_FactumFiled = $('#Appeal_FactumFiled input[type="radio"]:checked').val();
            var $Appeal_IsFullDay = $('#Appeal_IsFullDay input[type="radio"]:checked').val();

            if ($Appeal_FactumFiled === "true" &&
                ($Appeal_IsFullDay === "true" || $Appeal_IsFullDay === "false") &&
                validCaseSelection) {
                $("#btnShowDates").css("display", "flex");
            } else {
                $("#btnShowDates").hide();
            }
        }

        //hearing type is chambers
        if (hearingType === "false") {
            var $Chambers_TimeRequired = $('#Chambers_TimeRequired input[type="radio"]:checked').val();
            
            if (($Chambers_TimeRequired === "true" || $Chambers_TimeRequired === "false") &&
                validCaseSelection) {
                $("#btnShowDates").css("display", "flex");
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
    
    $('.btn-radio--hearing-type').click(function () {
        if ($('.btn-radio--date-agreed.active input').val() == "true") {
            $('#btnNext').show();
        } else {
            $('#btnNext').hide();
        }
    });
    $('.btn-radio--date-agreed input').click(function () {
        if ($(this).val() == "true" && $('.btn-radio--hearing-type').hasClass('active')) {
            $('#btnNext').show();
        } else {
            $('#btnNext').hide();
        }
    });
});
