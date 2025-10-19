$(function () {
  // show additional questions for "Trial" booking type
  if ($("input[name=HearingTypeId]:checked").length) {
    showTrialFields();
  }

  //Pre-filling input field based on selection of court class on the Supreme Court side
  $("#courtClassDropdown").on("change", function () {
    var $caseNum = $("#caseNumberInput");
    // don't clear the case number if it already starts with the selected class prefix
    if ($caseNum.val().length === 0 || $caseNum.val().charAt(0) !== $(this).val()) {
      $caseNum.val($(this).val());
    }
  });

  // show/hide extra fields for specific booking types
  $("input[name=HearingTypeId]").on("change", function () {
    var $courtClassDropdown = $("#courtClassDropdown");

    if ($(this).val() === "9543") {
      $("#courtClassLabel").hide();
      $("#caseNumberTooltip").hide();
      $courtClassDropdown.val("");
      $courtClassDropdown.hide();
      $courtClassDropdown.prop("required", false);
      $("#caseNumberInput").val("");
    } else {
      $("#courtClassLabel").show();
      $("#caseNumberTooltip").show();
      $courtClassDropdown.show();
      $courtClassDropdown.prop("required", true);
    }

    // show additional questions for "Trial" booking type
    showTrialFields();
  });

  $("input[name=IsHomeRegistry]").on("change", showTrialFields);
  $("input[name=IsLocationChangeFiled]").on("change", showTrialFields);

  $("#dateBtn").on("click", function () {
    $("#datepicker").datepicker().focus();
  });
});

// Shows or hides the additional form fields for Trials
function showTrialFields() {
  const hearingTypeId = $("input[name=HearingTypeId]:checked").val();

  const trialSelected = hearingTypeId === "9001";
  const chambersSelected = hearingTypeId === "9012";

  $("#existing-trial-error").hide();
  $("#btnNext").show();

  if (trialSelected && checkExistingTrialBookings()) {
    $("#existing-trial-error").show();
    $("#btnNext").hide();
    $("#lotteryenabled-additional-fields").hide();
  } else {
    $("#trial-additional-fields").toggle(trialSelected);
    $("#lotteryenabled-additional-fields").toggle(trialSelected || chambersSelected);
  }

  $(".txtChambers").toggle(chambersSelected);
  $(".txtTrial").toggle(!chambersSelected);

  $("#chambers-additional-fields").toggle(chambersSelected);

  const notHomeRegistry = $("input[name=IsHomeRegistry]:checked").val() === "false";
  $("#different-place-of-trial").toggle(notHomeRegistry);

  const differentPlace = $("input[name=IsLocationChangeFiled]:checked").val() === "true";
  const notDifferentPlace = $("input[name=IsLocationChangeFiled]:checked").val() === "false";
  $("#trial-location").toggle(notHomeRegistry && differentPlace);
  $("#trial-location-warning").toggle(notHomeRegistry && notDifferentPlace);
}

// Shows an error if the case already has a future trial or a trial request
function checkExistingTrialBookings() {
  if ($("#FutureTrialBooked").val() === "True") {
    $("#reason-future-trial-booked").show();
    return true;
  }

  if ($("#HasExistingTrialRequest").val() === "True") {
    $("#reason-existing-trial-request").show();
    return true;
  }

  return false;
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
  let charCode = evt.which ? evt.which : event.keyCode;
  return !(charCode !== 46 && charCode !== 45 && charCode > 31 && (charCode < 48 || charCode > 57));
}
