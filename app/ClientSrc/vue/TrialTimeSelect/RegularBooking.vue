<template>
  <div class="trial-time-select-regular-booking content-pad">
    <div class="d-md-none mb-3">
      You can instantly book a trial date that is currently available in the system.
    </div>

    <h3 class="mt-0 mb-5">Choose trial start date (Trial length: {{ trialLengthDisplay }})</h3>

    <div class="mb-5">
      <slot v-if="dates.length === 0" name="noDatesError" />

      <label
        class="label-button text-center"
        :class="{ selected: selected === date.isoDate }"
        role="button"
        v-for="date in visibleDates"
        :key="date.isoDate"
      >
        <input
          name="SelectedRegularTrialDate"
          class="d-none"
          type="radio"
          :value="date.isoDate"
          v-model="selected"
        />
        <span class="font-weight-normal">{{ date.dayOfWeek }}</span>
        <strong>{{ date.formattedDate }}</strong>
      </label>
    </div>

    <div>
      <button
        class="btn btn-outline-primary btn-show-more"
        @click="numShowing += 10"
        v-if="visibleDates.length < dates.length"
        type="button"
      >
        Show More Dates
      </button>
    </div>
  </div>
</template>

<script>
import { formatDate } from "./helpers.js";

export default {
  name: "RegularBooking",

  data: () => ({
    // number of visible dates from the full list
    numShowing: 10,

    selected: null,
  }),

  props: {
    dates: {
      type: Array,
      default: () => [],
    },

    trialLength: {
      type: Number,
      default: 0,
    },

    initialValue: {
      type: String,
      default: "",
    },
  },

  // set initial value, if provided
  created() {
    if (this.dates.includes(this.initialValue)) {
      this.selected = this.initialValue;
    }
  },

  computed: {
    formattedDates() {
      return this.dates.map(formatDate);
    },

    visibleDates() {
      return this.formattedDates.slice(0, this.numShowing);
    },

    trialLengthDisplay() {
      const days = this.trialLength === 1 ? "day" : "days";

      return `${this.trialLength} ${days}`;
    },
  },
};
</script>

<style scoped lang="scss">
@import "../../sass/variables";

.btn-show-more {
  margin: 0 auto;
}

// limit button width on larger screens
@include breakpoint(md) {
  .label-button {
    max-width: 350px;
  }

  .btn-show-more {
    margin: 0;
  }
}
</style>
