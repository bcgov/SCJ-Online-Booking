<template>
  <div class="trial-time-select-tabs">
    <ul class="p-0 m-0" v-if="!fairUseUnavailable">
      <li :class="['tab-li', { selected: tab === 'Fair-Use', disabled: fairUseDisabled }]">
        <label role="button" @click="checkFairUseDisabled">
          <div>
            <input type="radio" value="Fair-Use" v-model="tab" />
            <strong class="d-md-none">Provide availability</strong>
            <strong class="d-none d-md-block">Provide your availability for upcoming dates</strong>
          </div>
          <div v-if="!fairUseDisabled" class="d-none d-md-block">
            <slot name="fairUseTabDescription" />
          </div>
          <!-- show "fair use disabled" alert text inline on larger screens -->
          <div v-else class="d-none d-md-block"><slot name="fairUseDisabledAlert" /></div>
        </label>
      </li>

      <li :class="['tab-li', { selected: tab === 'Regular' }]">
        <label role="button">
          <div>
            <input type="radio" value="Regular" v-model="tab" />
            <strong class="d-md-none">Book now</strong>
            <strong class="d-none d-md-block"
              >Book currently available {{ hearingTypeName }} dates</strong
            >
          </div>
          <div class="d-none d-md-block">
            <slot name="regularTabDescription" />
          </div>
        </label>
      </li>
    </ul>

    <div class="bg-white">
      <input type="hidden" name="FormulaType" :value="tab" />

      <!-- show "fair use disabled" alert text in a floating banner on small screens -->
      <div class="alert sm-banner alert-warning m-0 d-md-none" v-if="showFairUseDisabledAlert">
        <i class="fa fa-exclamation-triangle" />

        <slot name="fairUseDisabledAlert" />

        <button
          type="button"
          class="close d-md-none"
          aria-label="Close"
          @click="showFairUseDisabledAlert = false"
        >
          <span aria-hidden="true">&times;</span>
        </button>
      </div>

      <slot v-if="tab === 'Fair-Use'" name="fairUseBooking" />
      <slot v-if="tab === 'Regular'" name="regularBooking" />
    </div>
  </div>
</template>

<script>
export default {
  name: "TrialTimeSelectTabs",

  props: {
    initialTab: {
      type: String,
      default: "Fair-Use",
    },

    fairUseDisabled: {
      type: Boolean,
      default: false,
    },

    fairUseUnavailable: {
      type: Boolean,
      default: false,
    },

    hearingTypeName: {
      type: String,
      default: "trial",
    },
  },

  data: () => ({
    tab: "Fair-Use",
    showFairUseDisabledAlert: false,
  }),

  // set initial value, if provided
  created() {
    if (this.fairUseDisabled) {
      this.tab = "Regular";
    } else if (this.initialTab) {
      this.tab = this.initialTab;
    }
  },

  watch: {
    // change the next button label based on the tab selected
    tab: function (value) {
      const nextButton = document.getElementById("btnNext");
      if (nextButton) {
        if (value == "Fair-Use" && !this.fairUseUnavailable && !this.fairUseDisabled) {
          nextButton.innerText = "Review your request";
        } else {
          if (this.hearingTypeName === "chambers") {
            nextButton.innerText = "Book chambers date";
          } else {
            nextButton.innerText = "Book trial date";
          }
        }
      }
    },
  },

  methods: {
    /**
     * Blocks click events if fair use booking is disabled,
     * and shows an alert notice.
     *
     * @param {Event} event - click event
     */
    checkFairUseDisabled(event) {
      if (this.fairUseDisabled) {
        event.preventDefault();

        this.showFairUseDisabledAlert = true;
      }
    },
  },
};
</script>

<style scoped lang="scss">
@use "../../sass/variables" as *;

ul {
  display: flex;

  li {
    margin: 0;
    padding: 0;
    list-style-type: none;
    user-select: none;
    flex: 1;

    label {
      margin: 0;
      white-space: normal;
      font-weight: normal;
    }

    input {
      display: none;
    }

    &.disabled {
      color: $grey-medium;
    }
  }
}

.tab-li label {
  white-space: normal !important;
}

// minimal tab styles on small screens
@media (max-width: ($breakpoint-md - 1px)) {
  ul {
    background: $white;
    justify-content: stretch;
    border-bottom: 1px solid $grey-medium;

    li {
      flex-basis: 50%;
      min-width: 10em;
      display: flex;
      justify-content: stretch;
      align-items: baseline;
      border-bottom: 3px solid transparent;
      // overlap the border-bottom of the UL
      margin-bottom: -2px;

      &.selected {
        border-bottom-color: $blue-light;
        color: $blue-light;
      }

      label {
        flex-grow: 1;
        text-align: center;
        padding: 0.75em 1em;
      }
    }
  }
}

// large tab styles on larger screens
@include breakpoint(md) {
  ul {
    color: $blue-light;
    border-bottom: 3px solid $blue-light;
    gap: 1em;

    li {
      background: $white;
      display: flex;
      border-bottom: none;
      border-radius: 14px 14px 0 0;

      &.selected {
        background: $blue-light;
        color: $white;
      }

      label {
        padding: 1em;
      }
    }
  }
}
</style>
