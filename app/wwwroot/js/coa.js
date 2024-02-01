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

        var isAppeal = $('#IsAppealHearing:checked').val();
        if (isAppeal === "true") {
            toggleAppeal();
        }
    });

    // Show the hidden "Next" button on form interaction
    $('#CaseNumber').on('input', function () {
        // skip if the input isn't more than the initial "CA"
        if ($(this).val().length <= 2) return;

        $('#btnNext').show();
        $('.btn-restart-search').show();
    })

    //Display last row of questions for Civil case if previous questions are answered Yes
    //And selection of related case files is valid
    var toggleCriminal = function () {
        var $Appeal_FactumFiled = $('#Appeal_FactumFiled input[type="radio"]:checked').val();
        if ($Appeal_FactumFiled === "true" && validCaseSelection) {
            $("#AppealAdditionalQs").show();
        } else {
            $("#AppealAdditionalQs").hide();
        }
    };

    $('#Appeal_FactumFiled input[type="radio"]').click(toggleCriminal);

    //Display last row of questions for Criminal case if previous questions are answered Yes
    //And selection of related case files is valid
    var toggleAppeal = function () {
        var $Appeal_FactumFiled = $('#Appeal_FactumFiled input[type="radio"]:checked').val();
        var $IsCriminal = $('#CaseType').val() === "Criminal";
        var $IsButtonDisabled = $('#Appeal_FactumFiled input[type="radio"]:checked').parent().hasClass('disabled');
        if ($IsCriminal) {
            $("#AppealAdditionalQs").css("display", "flex");
        } else if (!$IsButtonDisabled) {
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
    $('.preliminary_questions input[type="radio"], input[name="SelectedCases"], select[name="HearingTypeId"]').change(function () {
        var isAppeal = $('#IsAppealHearing:checked').val();
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

        //hearing type is appeal
        if (isAppeal === "true") {
            var $Appeal_FactumFiled = $('#Appeal_FactumFiled input[type="radio"]:checked').val();
            var $IsCriminal = $('#CaseType').val() === "Criminal";
            var $CriminalHearingType = $("#HearingTypeId").val();
            var $CriminalHearingTypeSelected = $IsCriminal && $CriminalHearingType && $CriminalHearingType.length > 0;
            var $Appeal_IsFullDay = $('#Appeal_IsFullDay input[type="radio"]:checked').val();
            if (($CriminalHearingTypeSelected || $Appeal_FactumFiled === "true") &&
                ($Appeal_IsFullDay === "true" || $Appeal_IsFullDay === "false") &&
                validCaseSelection) {
                $("#btnShowDates").css("display", "flex");
            } else {
                $("#btnShowDates").hide();
            }
        }

        //hearing type is chambers
        if (isAppeal === "false") {
            // validate at least one application type is selected
            const selectedApplicationTypes = $('input[name="SelectedApplicationTypes"]').length > 0;

            var $Chambers_IsHalfHour = $('#Chambers_IsHalfHour input[type="radio"]:checked').val();

            if (
                ($Chambers_IsHalfHour === "true" || $Chambers_IsHalfHour === "false") &&
                validCaseSelection &&
                selectedApplicationTypes
            ) {
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

    // show the "Confirm selection" button when a date is selected
    $('input[name="SelectedDate"]').change(function () {
        $("#btnSelectDate").show();
    });
});

/**
 * Handle "Application types" selection change.
 *
 * @param {Array} selection - Selected application types
 */
function onApplicationTypeChange(selection) {
    // if any of the selected application types require a full hour,
    // disable the half hour duration button
    const hourNeeded = selection.some(({ timeline }) => timeline === "One Hour");

    $halfHourRadio = $('input[name="IsHalfHour"][value="true"]');
    $halfHourLabel = $halfHourRadio.parent("label");
    $halfHourNotice = $('.notice-half-hour-required');

    if (hourNeeded) {
        // deselect radio button
        $halfHourRadio.prop("checked", false);
        $halfHourLabel.toggleClass("active", false);
    }

    // disable radio button
    $halfHourRadio.prop("disabled", hourNeeded);
    $halfHourLabel.toggleClass("disabled", hourNeeded);
    $halfHourNotice.toggle(hourNeeded);

    // trigger a radio button change handler to show/hide the "Show dates" button as needed
    $('input[type="radio"]').eq(0).trigger("change");
};
