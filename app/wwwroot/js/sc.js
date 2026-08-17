$(function () {
  // initialize additional fields for the selected booking type
  if ($("input[name=HearingTypeId]:checked").length) {
    updateBookingTypeFields();
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

    // update additional fields for the selected booking type
    updateBookingTypeFields();
  });

  $("input[name=IsHomeRegistry]").on("change", updateBookingTypeFields);
  $("input[name=IsLocationChangeFiled]").on("change", updateBookingTypeFields);

  $("#dateBtn").on("click", function () {
    $("#datepicker").datepicker().focus();
  });
});

// Updates additional fields for the selected booking type and trial location answers
function updateBookingTypeFields() {
  const hearingTypeId = $("input[name=HearingTypeId]:checked").val();

  const trialSelected = hearingTypeId === "9001";
  const chambersSelected = hearingTypeId === "9012";
  const cpcSelected = hearingTypeId === "9089";
  const jccSelected = hearingTypeId === "9005";
  const tmcSelected = hearingTypeId === "9090";

  $("#existing-trial-error").hide();
  $("#existing-CPC-error").hide();
  $("#existing-JCC-error").hide();
  $("#existing-TMC-error").hide();
  $("#btnNext").show();

  if (trialSelected && checkExistingTrialBookings()) {
    $("#existing-trial-error").show();
    $("#btnNext").hide();
    $("#lotteryenabled-additional-fields").hide();
  } else {
    $("#trial-additional-fields").toggle(trialSelected);
    $("#lotteryenabled-additional-fields").toggle(trialSelected || chambersSelected);
  }

  if (cpcSelected && $("#FutureCPCBooked").val() === "True") {
    $("#existing-CPC-error").show();
    $("#btnNext").hide();
  }

  if (jccSelected && $("#FutureJCCBooked").val() === "True") {
    $("#existing-JCC-error").show();
    $("#btnNext").hide();
  }
  if (tmcSelected && $("#FutureTMCBooked").val() === "True") {
    $("#existing-TMC-error").show();
    $("#btnNext").hide();
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
