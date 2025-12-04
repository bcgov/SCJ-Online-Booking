$(document).ready(function () {
  var validCaseSelection = true;

  // scroll the last "section" into view when a form page loads
  if ($(".form-scroll-section").length) {
    $(".form-scroll-section").last()[0].scrollIntoView();
  }

  // prevent form submission until validation passes
  let allowSubmit = false;

  // allow form submission on the confirmation page
  if ($("#CaseConfirmForm").length > 0) {
    allowSubmit = true;
  }

  // prevent invalid/incomplete form submissions
  // (pressing "enter" on the keyboard, etc.)
  $("form").on("submit", () => allowSubmit);

  // prevent clicks on disabled radio button labels
  $("label.disabled, label.disabled input").on("click", () => false);

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
    if ($(this).is(":checked")) {
      if (!($checkboxes.filter('[data-main="True"]').attr("checked") == "checked")) {
        if ($(this).attr("data-main") == "True") {
          //If the main is not checked, and the current checked one is the main
          $(".alert--related-cases").hide();
          validCaseSelection = true;
          validateRequestForm();
        } else {
          //If the main is not checked, and the current checked one is another sub
          $(".alert--related-cases").show();
          validCaseSelection = false;
          validateRequestForm();
        }
      }

      this.setAttribute("checked", "checked");
      this.checked = true;
    } else {
      var $checked = $('.related-cases input[name="SelectedCases"]:checked');

      if ($checkboxes.filter('[data-main="True"]').attr("checked") == "checked") {
        if ($(this).attr("data-main") == "True" && $checked.length > 1) {
          //If the main is checked, and the current unchecked one is the main
          //AND there are more than 1 sub options that are checked
          $(".alert--related-cases").show();
          validCaseSelection = false;
          validateRequestForm();
        }
      } else {
        if ($checked.length == 1) {
          //If the main is not checked
          //AND there is only 1 other sub option that is checked
          $(".alert--related-cases").hide();
          validCaseSelection = true;
          validateRequestForm();
        }
      }

      this.setAttribute("checked", ""); // For IE
      this.removeAttribute("checked"); // For other browsers
      this.checked = false;
    }

    var isAppeal = $("#IsAppealHearing:checked").val();
    if (isAppeal === "true") {
      toggleAppeal();
    }
  });

  // Show the hidden "Next" button on form interaction
  $("#CaseNumber").on("input", function () {
    // skip if the input isn't more than the initial "CA"
    if ($(this).val().length <= 2) return;

    allowSubmit = true;

    $("#btnNext").show();
    $(".btn-restart-search").show();
  });

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
    var $IsCriminal = $("#CaseType").val() === "Criminal";
    var $IsButtonDisabled = $('#Appeal_FactumFiled input[type="radio"]:checked')
      .parent()
      .hasClass("disabled");
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
  $(
    '.preliminary_questions input[type="radio"], input[name="SelectedCases"], select[name="HearingTypeId"]',
  ).change(function () {
    const isAppeal = $('input[name="IsAppealHearing"]:checked').val();
    const isDateAgreed = $('input[name="DateIsAgreed"]:checked').val();
    const isFactumFiled = $('input[name="FactumFiled"]:checked').val();

    // show notices for "preliminary question" radio buttons
    if (!$("#Appeal_FactumFiled > label").hasClass("disabled")) {
      if (isFactumFiled === "false") {
        $(".alert--preliminary_question.factumFiled").show();
        $(".notice--preliminary_question.factumFiled").hide();
        console.log("factum b");
      } else if (isFactumFiled === "true") {
        console.log("factum a");
        $(".alert--preliminary_question.factumFiled").hide();
        $(".notice--preliminary_question.factumFiled").show();
      }
    }

    // show warning alert if the date hasn't been agreed upon
    const $dateAgreedAlerts = $(
      ".alert--preliminary_question.appeal, .alert--preliminary_question.chambers",
    ).hide();
    if (isDateAgreed === "false" && !$("#DateIsAgreed > label").hasClass("disabled")) {
      if (isAppeal === "true") {
        $dateAgreedAlerts.filter(".appeal").show();
      } else if (isAppeal === "false") {
        $dateAgreedAlerts.filter(".chambers").show();
      }
    }

    //hearing type is appeal
    if (isAppeal === "true") {
      var $Appeal_FactumFiled = $('#Appeal_FactumFiled input[type="radio"]:checked').val();
      var $IsCriminal = $("#CaseType").val() === "Criminal";
      var $CriminalHearingType = $("#HearingTypeId").val();
      var $CriminalHearingTypeSelected =
        $IsCriminal && $CriminalHearingType && $CriminalHearingType.length > 0;
      var $Appeal_IsFullDay = $('#Appeal_IsFullDay input[type="radio"]:checked').val();
      if (
        ($CriminalHearingTypeSelected || $Appeal_FactumFiled === "true") &&
        ($Appeal_IsFullDay === "true" || $Appeal_IsFullDay === "false") &&
        validCaseSelection
      ) {
        allowSubmit = true;
        $("#btnShowDates").css("display", "flex");
      } else {
        allowSubmit = false;
        $("#btnShowDates").hide();
      }
    }

    //hearing type is chambers
    if (isAppeal === "false" && $("#Chambers_IsHalfHour").length) {
      // validate at least one application type is selected
      const selectedApplicationTypes = $('input[name="SelectedApplicationTypes"]').length > 0;

      var $Chambers_IsHalfHour = $('#Chambers_IsHalfHour input[type="radio"]:checked').val();

      if (
        ($Chambers_IsHalfHour === "true" || $Chambers_IsHalfHour === "false") &&
        validCaseSelection &&
        selectedApplicationTypes
      ) {
        allowSubmit = true;
        $("#btnShowDates").css("display", "flex");
      } else {
        allowSubmit = false;
        $("#btnShowDates").hide();
      }
    }
  });

  //Display more available dates by groups of 2 months
  $("#btnShowMore").click(function () {
    $(".availableDates__month.hidden").slice(0, 2).removeClass("hidden");

    if (!$(".availableDates__month.hidden").length) {
      $("#btnShowMore").hide();
    }
  });

  /**
   * Validates the "Request" form and shows (or hides) the "Next" button.
   */
  function validateRequestForm() {
    let valid = true;

    // validate main + related file number selection
    // (calculated on file number checkbox change)
    if (!validCaseSelection) {
      valid = false;
    }

    // Appeal or Chambers
    if ($('input[name="IsAppealHearing"]:checked').length === 0) {
      valid = false;
    }

    // hearing date agreed
    if ($('input[name="DateIsAgreed"]:checked').length === 0) {
      valid = false;
    } else if ($('input[name="DateIsAgreed"]:checked').val() !== "true") {
      // for appeal hearings, date must be agreed upon
      const IsAppealHearingVal = $('input[name="IsAppealHearing"]:checked').val() === "true";
      if (IsAppealHearingVal) {
        valid = false;
      }
    }

    // show the "next" button if the form is valid
    allowSubmit = valid;
    $("#btnNext").toggle(valid);
  }

  // Re-validate form on input change
  $('input[name="IsAppealHearing"], input[name="DateIsAgreed"]').on("change", validateRequestForm);

  // show the "Confirm selection" button when a date is selected
  $('input[name="SelectedDate"]').change(function () {
    allowSubmit = true;
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

  $timelineRadios = $('input[name="IsHalfHour"]');
  $halfHourRadio = $('input[name="IsHalfHour"][value="true"]');
  $halfHourLabel = $halfHourRadio.parent("label");
  $fullHourLabel = $('input[name="IsHalfHour"][value="false"]').parent("label");
  $halfHourNotice = $(".notice-half-hour-required");

  if (hourNeeded) {
    // select "One Hour" radio button
    $timelineRadios.val(["false"]);
    $halfHourLabel.toggleClass("active", false);
    $fullHourLabel.toggleClass("active", true);
  }

  // disable "Half Hour" radio button
  $halfHourRadio.prop("disabled", hourNeeded);
  $halfHourLabel.toggleClass("disabled", hourNeeded);
  $halfHourNotice.toggle(hourNeeded);

  // trigger a radio button change handler to show/hide the "Show dates" button as needed
  $('input[name="IsHalfHour"]').eq(0).trigger("change");
}
