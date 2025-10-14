<template>
  <chambers-time-select-tabs
    :initial-tab="initialTab"
    :fair-use-unavailable="fairUseUnavailable"
    :fair-use-disabled="fairUseDisabled || hasExistingChambersRequest"
    hearing-type-name="chambers hearing"
  >
    >
    <template v-slot:regularBooking>
      <regular-booking
        :dates="availableRegularDates"
        :length="chambersLength"
        :initial-value="selectedRegularDate"
        hearing-type-name="chambers hearing"
      >
        <template v-slot:noDatesError>
          <div class="alert alert-danger" role="alert">
            <i class="fa fa-ban"></i>
            There are currently no chambers hearing dates available at
            {{ sessionInfoBookingLocationName }}. Try again at a later time as more dates become
            available in the system.
          </div>
        </template>
      </regular-booking>
    </template>

    <template v-slot:fairUseBooking>
      <fair-use-booking
        :dates="availableFairUseDates"
        :initial-value="selectedFairUseDateStrings"
        :max-selection-size="scMaxChambersDateSelections"
        hearing-type-name="chambers hearing"
      >
        <template v-slot:mobileTabDescription>
          Request up to {{ scMaxChambersDateSelectionsString }} dates for a chambers hearing
          starting in the upcoming release of dates.
        </template>

        <template v-slot:noDatesError>
          <div class="alert alert-danger" role="alert">
            <i class="fa fa-ban"></i>
            There are no dates set for the upcoming release. You can instantly book a chambers
            hearing date that is currently available in the system instead.
          </div>
        </template>

        <template v-slot:howItWorksDescription>
          <p class="mb-3">
            <strong>A chambers hearing date is not being booked at this stage.</strong> You are
            providing your availability for a chambers hearing to start on <strong>one</strong> (out
            of a maximum of {{ scMaxChambersDateSelectionsString }}) of your requested dates for
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
                Period to provide chambers date availability for
                <b>{{ bookingPeriodName }}</b> dates.
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
              <h4>If a chambers hearing date is set for your case</h4>
              <p>
                You must file your court documents according to the <em>Supreme Court Rules</em> for
                your particular application or petition. Please visit
                <a
                  target="_blank"
                  rel="noopener"
                  href="https://www.bccourts.ca/supreme_court/practice_and_procedure/acts_rules_and_forms/"
                >
                  Supreme Court Act, Rules and Forms</a
                >
                for more information.
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
          Request chambers hearing start dates for {{ sessionInfoBookingLocationName }} ({{
            chambersLength
          }}
          {{ chambersLength == 1 ? "day" : "days" }})
        </template>

        <template v-slot:dateSelectionHeader="{ maxSelectionSize }">
          <h6>Chambers dates for {{ bookingPeriodName }}</h6>
          <p class="mb-3">
            Request <b>up to {{ maxSelectionSize }} starting dates</b>. Some dates are not available
            due to statutory holidays or court closures.
          </p>
        </template>
      </fair-use-booking>
    </template>

    <template v-slot:fairUseTabDescription>
      Request up to {{ scMaxChambersDateSelectionsString }} dates for a chambers hearing starting in
      the upcoming release of dates.
    </template>

    <template v-slot:fairUseDisabledAlert v-if="hasExistingChambersRequest">
      A request for upcoming chambers hearing dates has already been submitted for this fair-use
      period.
    </template>

    <template v-slot:fairUseDisabledAlert v-if="!hasExistingChambersRequest">
      The {{ currentMonth }} booking period ended on {{ fairUseEndDate }}. The next booking period
      will open in {{ nextMonth }}.
    </template>

    <template v-slot:regularTabDescription>
      You can instantly book a chambers hearing date that is currently available in the system.
    </template>
  </chambers-time-select-tabs>
</template>

<script>
import ChambersTimeSelectTabs from "../_shared/Tabs";
import RegularBooking from "../_shared/RegularBooking";
import FairUseBooking from "../_shared/FairUseBooking";

export default {
  name: "LongChambersTimeSelect",
  components: {
    ChambersTimeSelectTabs,
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
    scMaxChambersDateSelections: {
      type: Number,
      required: false,
      default: 0,
    },
    scMaxChambersDateSelectionsString: {
      type: String,
      required: false,
      default: "",
    },
    selectedFairUseDateStrings: {
      type: Array,
      required: true,
    },
    selectedRegularDate: {
      type: String,
      required: true,
    },
    sessionInfoBookingLocationName: {
      type: String,
      required: true,
    },
    chambersLength: {
      type: Number,
      required: true,
    },
    hasExistingChambersRequest: {
      type: Boolean,
      required: true,
    },
  },
};
</script>
