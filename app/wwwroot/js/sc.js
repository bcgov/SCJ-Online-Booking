$(document).ready(function () {

    //Pre-filling input field based on selection of court class on the Supreme Court side
    $('#courtClassDropdown').change(function () {
        var $caseNum = $('#caseNumberInput');
        // don't clear the case number if it already starts with the selected class prefix
        if ($caseNum.val().length === 0 || $caseNum.val().charAt(0) !== $(this).val()) {
            $caseNum.val($(this).val());
        }
    });

    //$("input:radio[name=SelectedCaseId]").change(function () {
    //    //if ($('#ChooseACase').length) {
    //    //    $("#ChooseACase").hide();
    //    //}
    //});

    // hide the hearing type dropdown for CV-PTC
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
    });

    //$("#SearchForCase").click(function () {
    //    //if ($("input:radio").length) {
    //    //    $("input:radio").attr("checked", false);
    //    //}
    //    //if ($('#ChooseACase').length) {
    //    //    $("#ChooseACase").show();
    //    //}
    //});
});

// Called by Vue when a time-slot is selected
function validateCaseDate(containerId, bookingDate) {
    //set hidden fields based on the swiper selected dates
    $("#hidContainerId").val(containerId);
    $("#hidDate").val(bookingDate);

    //submit the form to go to the next page
    $("#btnSearch").trigger("click");
}
