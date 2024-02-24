<template>
  <div class="trial-time-select-fair-use-booking">
    <div class="dates-info content-pad mb-3">
      <div
        @click="showInfo = !showInfo"
        class="d-flex justify-content-between align-items-center mb-5 expand-header"
      >
        <h3 class="m-0">How This Process Works</h3>
        <i
          class="fas expand-chevron-icon"
          :class="showInfo ? 'fa-chevron-up' : 'fa-chevron-down'"
        />
      </div>

      <p class="mb-3">
        <strong>A trial date is not being booked at this stage.</strong> You are providing your
        availability for a trial to start on one (out of a maximum of five) of your requested dates
        18 months in advance.
      </p>

      <p class="mb-3">
        <strong>The time of your submission has no bearing on the result of your request.</strong>
      </p>

      <div v-if="showInfo" class="expand-content">
        <div class="step">
          <span class="number">1</span>
          <div class="description">
            <h4>[Start date] [Start time]</h4>
            <p>Period to provide trial availability opens.</p>
          </div>
        </div>

        <div class="step">
          <span class="number">2</span>
          <div class="description">
            <h4>[End date] [End time]</h4>
            <p>
              Period to provide trial date availability closes. The system will begin to set trial
              dates.
            </p>
          </div>
        </div>

        <div class="step">
          <span class="number">3</span>
          <div class="description">
            <h4>[Result date]</h4>
            <p>You will receive an email with the results of your request.</p>
          </div>
        </div>

        <div class="step">
          <span class="number">4</span>
          <div class="description">
            <h4>[Notice of Trial date]</h4>
            <p>
              If a trial date is set for your case, you must file your Notice of Trial by [Notice of
              Trial date] to secure the trial date.
            </p>
          </div>
        </div>

        <p class="mb-3">
          To learn more about this process, please visit
          <a target="_blank" href="https://www.bccourts.ca/">bccourts.ca</a> or call Supreme Court
          Scheduling at 604-660-2853.
        </p>
      </div>

      <a
        @click.prevent="showInfo = !showInfo"
        href="#"
        class="expand-info align-items-baseline m-0 d-md-none"
      >
        See key dates <i class="fas" :class="showInfo ? 'fa-chevron-up' : 'fa-chevron-down'" />
      </a>
    </div>

    <div class="content-pad">
      <h3 class="mt-0 mb-3">Choose trial start date (Trial length {{ trialLength }} days)</h3>
    </div>

    <div class="column-container">
      <div class="dates-intro content-pad select-dates-intro">
        <h6>[January 2024]</h6>
        <p class="mb-3">
          Choose up to 5 starting dates. Some dates are not available due to statutory holidays or
          court closures.
        </p>
        <div
          class="list-info-header d-flex justify-content-between align-items-center mb-4 d-md-none"
        >
          <h6 class="text-secondary">[0]/5 selected</h6>
          <a class="scroll-link" @click.prevent href="#"
            >See my choices <i class="fas fa-long-arrow-alt-down"
          /></a>
        </div>
      </div>

      <div class="mb-5 dates-list content-pad select-dates">
        <label
          class="label-button date-button"
          :class="dateClasses(date)"
          role="button"
          v-for="date in visibleDates"
          :key="date.isoDate"
        >
          <input class="d-none" type="checkbox" :value="date.isoDate" v-model="selected" />
          <span class="font-weight-normal">{{ date.dayOfWeek }}</span>
          <strong>{{ date.formattedDate }}</strong>
        </label>
      </div>

      <div class="dates-intro content-pad selected-dates-intro">
        <h6>Your Availability</h6>
        <p class="mb-3">You can reorder dates to indicate your preference.</p>
        <div class="list-info-header d-flex justify-content-between align-items-center mb-4">
          <h6 class="text-secondary">[0]/5 selected</h6>
          <a class="scroll-link d-md-none" @click.prevent href="#"
            >Select more dates <i class="fas fa-long-arrow-alt-up"
          /></a>
        </div>
      </div>

      <div class="mb-5 dates-list content-pad selected-dates">
        <div class="draggable-dates">
          <div
            class="date-wrap date-button d-flex justify-content-stretch align-items-center"
            v-for="date in formattedSelection"
            :key="date.isoDate"
          >
            <button class="btn-icon grab-button" type="button">
              <i class="fas fa-grip-horizontal" />
            </button>
            <label class="label-button selected m-0">
              <span class="font-weight-normal">{{ date.dayOfWeek }}</span>
              <strong>{{ date.formattedDate }}</strong>
            </label>
            <button @click="unselect(date.isoDate)" class="btn-icon delete-button" type="button">
              <i class="fas fa-trash" />
            </button>
          </div>
          <div class="alert alert-warning" v-if="formattedSelection.length === 0">
            <i class="fa fa-exclamation-triangle" />
            Choose at least one starting date for your trial.
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { formatDate } from "./helpers.js";

export default {
  name: "FairUseBooking",

  data: () => ({
    // show or hide extra "dates-info" text
    showInfo: false,

    selected: [],
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
  },

  computed: {
    formattedDates() {
      return this.dates.map(formatDate);
    },

    formattedSelection() {
      return this.selected.map(formatDate);
    },

    visibleDates() {
      return this.formattedDates.slice(0, 8).filter((date) => {
        return !this.selected.includes(date.isoDate);
      }); // temporary
    },
  },

  // expand "Key Dates" info box by default on larger screens
  mounted() {
    if (window.innerWidth >= 768) {
      this.showInfo = true;
    }
  },

  methods: {
    dateClasses(date) {
      return {
        selected: this.selected.includes(date.isoDate),
      };
    },

    /**
     * Removes a date from the selection.
     *
     * @param {string} isoDate - date string to remove
     */
    unselect(isoDate) {
      this.selected = this.selected.filter((date) => date !== isoDate);
    },
  },
};
</script>

<style scoped lang="scss">
@import "../../sass/variables";

.column-container {
  display: grid;
  grid-template-areas:
    "select-dates-intro"
    "select-dates"
    "selected-dates-intro"
    "selected-dates";

  .select-dates-intro {
    grid-area: select-dates-intro;
  }

  .select-dates {
    grid-area: select-dates;
  }

  .selected-dates-intro {
    grid-area: selected-dates-intro;
  }

  .selected-dates {
    grid-area: selected-dates;
  }

  // show lists side-by-side on wider screens
  @include breakpoint(md) {
    grid-template-columns: 45% 1fr;
    grid-template-areas:
      "select-dates-intro selected-dates-intro"
      "select-dates selected-dates";
  }
}

// expandable info panel
.dates-info {
  background: $blue-active-lighter;

  .expand-header {
    cursor: pointer;
  }

  .expand-content {
    .step {
      position: relative;

      &:not(:last-of-type) {
        border-left: 1px solid $grey-medium;
      }

      padding-left: 1.8em;
      padding-bottom: 1em;
      margin-left: 1em;

      // circled number icon
      .number {
        $icon-size: 2em;
        $font-size: 1.2em;

        font-size: $font-size;
        position: absolute;
        top: (1em - $font-size) * 0.5;
        left: -$icon-size * 0.5;
        display: flex;
        justify-content: center;
        align-items: center;
        background-color: $white;
        color: $blue-light;
        border: 1px solid $blue-light;
        border-radius: 100%;
        height: $icon-size;
        width: $icon-size;
        font-weight: 700;
      }
    }
  }
}

// underline icons in links
a > i {
  text-decoration: underline;
}

.date-button {
  margin: 0 0 16px;
}

.grab-button {
  cursor: grab;
}

.dates-list {
  .date-wrap {
    gap: 0.5em;

    button {
      width: 40px;
      height: 40px;
    }

    .label-button {
      flex-grow: 1;
    }
  }
}
</style>
