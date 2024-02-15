$(document).ready(function () {
    // show additional questions for "Trial" booking type
    if ($('input[name=HearingTypeId]:checked').length) {
        showTrialFields();
    }

    //Pre-filling input field based on selection of court class on the Supreme Court side
    $('#courtClassDropdown').change(function () {
        var $caseNum = $('#caseNumberInput');
        // don't clear the case number if it already starts with the selected class prefix
        if ($caseNum.val().length === 0 || $caseNum.val().charAt(0) !== $(this).val()) {
            $caseNum.val($(this).val());
        }
    });

    $('#CaseRegistryId').change(function () {
        if (!$('.showMsgClass').is(":hidden")) {
            $('.showMsgClass').prop("hidden", true);
        }

        if ($('.hideMsgClass').is(":hidden")) {
            $('.hideMsgClass').removeAttr('hidden');
        }
    });

    // show/hide extra fields for specific booking types
    $("input[name=HearingTypeId]").change(function () {
        var $courtClassDropdown = $("#courtClassDropdown");

        if ($(this).val() === "9543") {
            $("#courtClassLabel").hide();
            $("#caseNumberTooltip").hide();
            $courtClassDropdown.val('');
            $courtClassDropdown.hide();
            $courtClassDropdown.prop('required', false);
            $("#caseNumberInput").val('');
        } else {
            $("#courtClassLabel").show();
            $("#caseNumberTooltip").show();
            $courtClassDropdown.show();
            $courtClassDropdown.prop('required', true);
        }

        // show additional questions for "Trial" booking type
        showTrialFields();
    });

    $("input[name=IsHomeRegistry]").change(showTrialFields)
    $("input[name=IsDifferentPlaceOfTrial]").change(showTrialFields)

    $("#dateBtn").click(function () {
        $('#datepicker').datepicker().focus();
    });
});

// Shows or hides the additional form fields for Trials
function showTrialFields() {
    const trialSelected = $('input[name=HearingTypeId]:checked').val() === "99999";
    $('#trial-additional-fields').toggle(trialSelected);

    const notHomeRegistry = $('input[name=IsHomeRegistry]:checked').val() === 'false';
    $('#different-place-of-trial').toggle(notHomeRegistry);

    const differentPlace = $('input[name=IsDifferentPlaceOfTrial]:checked').val() === 'true';
    const notDifferentPlace = $('input[name=IsDifferentPlaceOfTrial]:checked').val() === 'false';
    $('#trial-location').toggle(notHomeRegistry && differentPlace);
    $('#trial-location-warning').toggle(notHomeRegistry && notDifferentPlace);
}

// Called by Vue when a time-slot is selected
function validateCaseDate(containerId, bookingDate) {
    //set hidden fields based on the swiper selected dates
    $("#hidContainerId").val(containerId);
    $("#hidDate").val(bookingDate);

    //submit the form to go to the next page
    $("#btnSearch").trigger("click");
}

function isValidKey(evt) {
    let charCode = (evt.which) ? evt.which : event.keyCode;
    return !(charCode !== 46 && charCode !== 45 &&
        charCode > 31 && (charCode < 48 || charCode > 57));
}
