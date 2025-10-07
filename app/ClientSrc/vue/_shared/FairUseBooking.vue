<template>
  <div class="trial-time-select-fair-use-booking">
    <div class="d-md-none mb-3 content-pad">
      <!-- Tab description text shown on small screens -->
      <slot name="mobileTabDescription" />
    </div>

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

      <slot name="howItWorksDescription"></slot>

      <div v-if="showInfo" class="expand-content"><slot name="datesInfo" /></div>

      <a
        @click.prevent="showInfo = !showInfo"
        href="#"
        class="expand-info align-items-baseline m-0 d-md-none"
      >
        See key dates <i class="fas" :class="showInfo ? 'fa-chevron-up' : 'fa-chevron-down'" />
      </a>
    </div>

    <div class="content-pad">
      <h3 class="mt-0 mb-3"><slot name="dateSelectionSectionHeader" /></h3>

      <slot v-if="dates.length === 0" name="noDatesError" />
    </div>

    <div class="column-container" v-if="dates.length > 0">
      <div class="dates-intro content-pad select-dates-intro">
        <slot name="dateSelectionHeader" :max-selection-size="maxSelectionSize" />

        <div
          ref="availableDates"
          class="list-info-header d-flex justify-content-between align-items-center mb-4 d-md-none"
        >
          <h6 v-show="dates.length >= maxSelectionSize" class="text-secondary">
            {{ selected.length }}/{{ maxSelectionSize }} selected
          </h6>

          <a class="scroll-link" @click.prevent="scrollTo('selectedDates')" href="#"
            >See my choices <i class="fas fa-long-arrow-alt-down"
          /></a>
        </div>

        <div class="alert sm-banner alert-warning m-0" v-if="showSelectionSizeAlert">
          <i class="fa fa-exclamation-triangle" />

          Remove a chosen date before adding another.

          <button
            type="button"
            class="close d-md-none"
            aria-label="Close"
            @click="showSelectionSizeAlert = false"
          >
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
      </div>

      <div class="mb-5 dates-list content-pad select-dates">
        <div
          class="label-button date-button text-center"
          :class="dateClasses(date)"
          role="button"
          v-for="date in visibleDates"
          :key="date.isoDate"
          @click="select(date.isoDate)"
        >
          <span class="font-weight-normal">{{ date.dayOfWeek }}</span>
          <strong>&nbsp;{{ date.formattedDate }}</strong>
        </div>
      </div>

      <div class="dates-intro content-pad selected-dates-intro">
        <h6>Your Availability</h6>
        <p class="mb-3">Reorder dates using drag and drop to indicate your preference.</p>
        <div
          ref="selectedDates"
          class="list-info-header d-flex justify-content-between align-items-center mb-4"
        >
          <h6 v-show="dates.length > 0" class="text-secondary">
            {{ selected.length }}/{{ maxSelectionSize }} selected
          </h6>

          <a class="scroll-link d-md-none" @click.prevent="scrollTo('availableDates')" href="#"
            >Select more dates <i class="fas fa-long-arrow-alt-up"
          /></a>
        </div>
      </div>

      <div class="mb-5 dates-list content-pad selected-dates">
        <div class="draggable-dates">
          <draggable
            v-model="selected"
            handle=".grab-button"
            direction="vertical"
            item-key="isoDate"
          >
            <template #item="{ element: isoDate }">
              <div
                class="date-wrap date-button d-flex justify-content-stretch align-items-center"
                :key="isoDate"
              >
                <div class="btn-icon grab-button d-flex justify-content-center align-items-center">
                  <i class="fas fa-grip-horizontal" />
                </div>
                <div class="label-button selected text-center m-0">
                  <input type="hidden" :value="isoDate" name="SelectedFairUseDates" />
                  <span class="font-weight-normal">{{ formatIsoDate(isoDate).dayOfWeek }}</span>
                  <strong>&nbsp;{{ formatIsoDate(isoDate).formattedDate }}</strong>
                </div>
                <button @click="unselect(isoDate)" class="btn-icon delete-button" type="button">
                  <i class="fas fa-trash" />
                </button>
              </div>
            </template>
          </draggable>

          <div class="alert alert-warning" v-if="formattedSelection.length === 0">
            <i class="fa fa-exclamation-triangle" />
            Choose at least one starting date for your {{ hearingTypeName }}.
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import draggable from "vuedraggable";

import { formatDate } from "./helpers.js";

export default {
  name: "FairUseBooking",

  components: {
    draggable,
  },

  data: () => ({
    // show or hide extra "dates-info" text
    showInfo: false,

    selected: [],
    // show or hide "selection full" alert
    showSelectionSizeAlert: false,
  }),

  props: {
    dates: {
      type: Array,
      default: () => [],
    },

    initialValue: {
      type: Array,
      default: () => [],
    },

    maxSelectionSize: {
      type: Number,
      default: 5,
    },

    hearingTypeName: {
      type: String,
      default: "trial",
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

  // set initial value, if provided
  created() {
    if (this.initialValue.length > 0) {
      this.selected = this.initialValue.filter((date) => this.dates.includes(date));
    }
  },

  // expand "Key Dates" info box by default on larger screens
  mounted() {
    if (window.innerWidth >= 768) {
      this.showInfo = true;
    }
  },

  methods: {
    /**
     * Returns an object with CSS classes for a given date.
     * The 'selected' class is applied if the date is included in the selected dates.
     *
     * @param {Object} date - The date object containing an isoDate property.
     */
    dateClasses(date) {
      return {
        selected: this.selected.includes(date.isoDate),
      };
    },

    /**
     * Formats an ISO date string using the formatDate utility.
     *
     * @param {string} isoDate - The ISO date string to format.
     */
    formatIsoDate(isoDate) {
      return formatDate(isoDate);
    },

    /**
     * Adds a date to the selection.
     *
     * @param {string} isoDate - date string to remove
     */
    select(isoDate) {
      // prevent adding duplicates
      if (this.selected.includes(isoDate)) return;

      // prevent adding more than maxSelectionSize
      if (this.selected.length >= this.maxSelectionSize) {
        // show the "selection full" alert
        this.showSelectionSizeAlert = true;

        return;
      }

      this.selected.push(isoDate);
    },

    /**
     * Removes a date from the selection.
     *
     * @param {string} isoDate - date string to remove
     */
    unselect(isoDate) {
      this.selected = this.selected.filter((date) => date !== isoDate);

      // hide the "selection full" alert
      this.showSelectionSizeAlert = false;
    },

    /**
     * Scrolls a ref element into view.
     *
     * @param {string} refName - name in this component's $refs
     */
    scrollTo(refName) {
      this.$refs[refName].scrollIntoView({ behavior: "smooth" });
    },
  },
};
</script>

<style scoped lang="scss">
@use "../../sass/variables" as *;

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
    gap: 0.25em;
    background: $white;

    &.sortable-ghost {
      opacity: 0.5;
    }

    &.sortable-drag {
      border-radius: 8px;
      padding: 0.25em 0;
    }

    .btn-icon {
      height: 40px;
      flex: 0 0 40px;
      color: $blue-dark;
    }

    .label-button {
      flex-grow: 1;
    }
  }
}
</style>
<style lang="scss">
.trial-time-select-fair-use-booking {
  .expand-content {
    .step {
      position: relative;
      &:not(:last-of-type) {
        border-left: 1px solid #c8e0f2;
      }
      padding-left: 1.8em;
      padding-bottom: 1em;
      margin-left: 1em;

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
        background-color: #fff;
        color: #0a5288;
        border: 1px solid #0a5288;
        border-radius: 100%;
        height: $icon-size;
        width: $icon-size;
        font-weight: 700;
      }
    }
  }
}
</style>
