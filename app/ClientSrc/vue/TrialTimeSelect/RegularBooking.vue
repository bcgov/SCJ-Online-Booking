<template>
  <div class="trial-time-select-regular-booking content-pad">
    <h3 class="mt-0 mb-5">Choose trial start date (Trial length {{ trialLength }} days)</h3>

    <div class="mb-5">
      <label
        class="label-button"
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
