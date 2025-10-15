<template>
  <div class="trial-time-select-regular-booking content-pad">
    <div class="d-md-none mb-3">
      You can instantly book a {{ hearingTypeName }} date that is currently available in the system.
    </div>

    <h3 class="mt-0 mb-5" v-if="hearingTypeName === 'trial'">
      Choose {{ hearingTypeName }} start date (trial length: {{ lengthDisplay }})
    </h3>

    <h3 class="mt-0 mb-5" v-if="hearingTypeName !== 'trial'">
      Choose {{ hearingTypeName }} start date ({{ lengthDisplay }})
    </h3>

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
          name="SelectedRegularDate"
          class="d-none"
          type="radio"
          :value="date.isoDate"
          v-model="selected"
        />
        <span class="font-weight-normal">{{ date.dayOfWeek }}</span>
        <strong>&nbsp;{{ date.formattedDate }}</strong>
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

    length: {
      type: Number,
      default: 0,
    },

    initialValue: {
      type: String,
      default: "",
    },

    hearingTypeName: {
      type: String,
      default: "trial",
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

    lengthDisplay() {
      const days = this.length === 1 ? "day" : "days";

      return `${this.length} ${days}`;
    },
  },
};
</script>

<style scoped lang="scss">
@use "../../sass/variables" as *;

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
