$(document).ready(function () {

    //Pre-filling input field based on selection of court class on the Supreme Court side
    $('#courtClassDropdown').change(function() {
        $('#caseNumberInput').val($(this).val());
    });

    // hide the hearing type dropdown for CV-PTC
    $("input[name=HearingTypeId]").change(function () {
        var $courtClassDropdown = $("#courtClassDropdown");

        if ($(this).val() === "9543") {
            $("#courtClassLabel").hide();
            $courtClassDropdown.val('');
            $courtClassDropdown.hide();
            $courtClassDropdown.prop('required', false);
            $("#caseNumberInput").val('');
        } else {
            $("#courtClassLabel").show();
            $courtClassDropdown.show();
            $courtClassDropdown.prop('required', true);
        }
    });

});

// Called by Vue when a time-slot is selected
function validateCaseDate(containerId, bookingDate) {
    //set hidden fields based on the swiper selected dates
    $("#hidContainerId").val(containerId);
    $("#hidDate").val(bookingDate);

    //submit the form to go to the next page
    $("#btnSearch").trigger("click");
}
