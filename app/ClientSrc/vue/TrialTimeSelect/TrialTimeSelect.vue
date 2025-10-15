<template>
  <trial-time-select-tabs
    :initial-tab="initialTab"
    :fair-use-unavailable="fairUseUnavailable"
    :fair-use-disabled="fairUseDisabled"
  >
    <template v-slot:regularBooking>
      <regular-booking
        :dates="availableRegularDates"
        :length="trialLength"
        :initial-value="selectedRegularTrialDate"
      >
        <template v-slot:noDatesError>
          <div class="alert alert-danger" role="alert">
            <i class="fa fa-ban"></i>
            There are currently no trial dates available at {{ sessionInfoBookingLocationName }}.
            Try again at a later time as more dates become available in the system.
          </div>
        </template>
      </regular-booking>
    </template>

    <template v-slot:fairUseBooking>
      <fair-use-booking :dates="availableFairUseDates" :initial-value="selectedFairUseDateStrings">
        <template v-slot:mobileTabDescription>
          Request up to {{ scMaxTrialDateSelectionsString }} dates for a trial starting in the
          upcoming release of dates.
        </template>

        <template v-slot:noDatesError>
          <div class="alert alert-danger" role="alert">
            <i class="fa fa-ban"></i>
            There are no dates set for the upcoming release. You can instantly book a trial date
            that is currently available in the system instead.
          </div>
        </template>

        <template v-slot:howItWorksDescription>
          <p class="mb-3">
            <strong>A trial date is not being booked at this stage.</strong> You are providing your
            availability for a trial to start on <strong>one</strong> (out of a maximum of
            {{ scMaxTrialDateSelectionsString }}) of your requested dates for
            <b>{{ bookingPeriodName }}</b
            >.
          </p>
          <p class="mb-3">
            <strong
              >The time of your submission has no bearing on the results of your request.</strong
            >
          </p>
        </template>

        <template v-slot:datesInfo>
          <div class="step">
            <span class="number">1</span>
            <div class="description">
              <h4>
                <span class="d-sm-inline-block">{{ fairUseStartDate }} {{ fairUseStartTime }}</span>
                to
                <span class="d-sm-inline-block">{{ fairUseEndDate }} {{ fairUseEndTime }}</span>
              </h4>
              <p>
                Period to provide trial date availability for <b>{{ bookingPeriodName }}</b> dates.
              </p>
            </div>
          </div>
          <div class="step">
            <span class="number">2</span>
            <div class="description">
              <h4>{{ resultContactDate }}</h4>
              <p>
                You will receive an email with the results of your request no later than this date.
              </p>
            </div>
          </div>
          <div class="step">
            <span class="number">3</span>
            <div class="description">
              <h4>If a trial date is set for your case</h4>
              <p>
                You must file your Notice of Trial within 30 days of receiving the confirmation
                email in order to confirm the trial date.
              </p>
            </div>
          </div>
          <p class="mb-3">
            To learn more about this process, please visit
            <a target="_blank" href="https://www.bccourts.ca/supreme_court/scheduling/"
              >bccourts.ca</a
            >
            <i class="fa fa-external-link-alt"></i>.
          </p>
        </template>

        <template v-slot:dateSelectionSectionHeader>
          Request trial start dates for {{ sessionInfoBookingLocationName }} (trial length:
          {{ trialLength }} {{ trialLength == 1 ? "day" : "days" }})
        </template>

        <template v-slot:dateSelectionHeader="{ maxSelectionSize }">
          <h6>Trial dates for {{ bookingPeriodName }}</h6>
          <p class="mb-3">
            Request <b>up to {{ maxSelectionSize }} starting dates</b>. Some dates are not available
            due to statutory holidays or court closures.
          </p>
        </template>
      </fair-use-booking>
    </template>

    <template v-slot:fairUseTabDescription>
      Request up to {{ scMaxTrialDateSelectionsString }} dates for a trial starting in the upcoming
      release of dates.
    </template>

    <template v-slot:fairUseDisabledAlert>
      The {{ currentMonth }} booking period ended on {{ fairUseEndDate }}. The next booking period
      will open in {{ nextMonth }}.
    </template>

    <template v-slot:regularTabDescription>
      You can instantly book a trial date that is currently available in the system.
    </template>
  </trial-time-select-tabs>
</template>

<script>
import TrialTimeSelectTabs from "../_shared/Tabs";
import RegularBooking from "../_shared/RegularBooking";
import FairUseBooking from "../_shared/FairUseBooking";

export default {
  name: "TrialTimeSelect",
  components: {
    TrialTimeSelectTabs,
    RegularBooking,
    FairUseBooking,
  },
  props: {
    availableFairUseDates: {
      type: Array,
      required: true,
    },
    availableRegularDates: {
      type: Array,
      required: true,
    },
    bookingPeriodName: {
      type: String,
      required: true,
    },
    currentMonth: {
      type: String,
      required: true,
    },
    fairUseDisabled: {
      type: Boolean,
      required: true,
    },
    fairUseEndDate: {
      type: String,
      required: true,
    },
    fairUseEndTime: {
      type: String,
      required: true,
    },
    fairUseStartDate: {
      type: String,
      required: true,
    },
    fairUseStartTime: {
      type: String,
      required: true,
    },
    fairUseUnavailable: {
      type: Boolean,
      required: true,
    },
    initialTab: {
      type: String,
      required: true,
    },
    nextMonth: {
      type: String,
      required: true,
    },
    resultContactDate: {
      type: String,
      required: true,
    },
    scMaxTrialDateSelections: {
      type: Number,
      required: false,
      default: 0,
    },
    scMaxTrialDateSelectionsString: {
      type: String,
      required: false,
      default: "",
    },
    selectedFairUseDateStrings: {
      type: Array,
      required: true,
    },
    selectedRegularTrialDate: {
      type: String,
      required: true,
    },
    sessionInfoBookingLocationName: {
      type: String,
      required: true,
    },
    trialLength: {
      type: Number,
      required: true,
    },
  },
};
</script>
