<template>
  <div class="trial-time-select-tabs">
    <ul class="content-pad m-0">
      <li :class="{ selected: tab === 'fairUseBooking' }">
        <label role="button">
          <div>
            <input type="radio" value="fairUseBooking" v-model="tab" />
            <strong>Provide your availability for upcoming dates</strong>
          </div>
          <div>Choose up to five dates for a trial starting in the upcoming release of dates</div>
        </label>
      </li>

      <li :class="{ selected: tab === 'regularBooking' }">
        <label role="button">
          <div>
            <input type="radio" value="regularBooking" v-model="tab" />
            <strong>Book currently available trial dates</strong>
          </div>
          <div>You can instantly book a trial date that is currently available in the system.</div>
        </label>
      </li>
    </ul>

    <input type="hidden" name="BookingFormula" :value="tab" />

    <slot v-if="tab === 'fairUseBooking'" name="fairUseBooking" />
    <slot v-if="tab === 'regularBooking'" name="regularBooking" />
  </div>
</template>

<script>
export default {
  name: "TrialTimeSelectTabs",

  props: {
    initialTab: {
      type: String,
      default: "fairUseBooking",
    },
  },

  data: () => ({
    tab: "fairUseBooking",
  }),

  // set initial value, if provided
  created() {
    if (this.initialTab) {
      this.tab = this.initialTab;
    }
  },
};
</script>

<style scoped lang="scss">
@import "../../sass/variables";

.trial-time-select-tabs {
  background: $white;
}

ul {
  li {
    margin: 0;
    padding: 0;
    list-style-type: none;
    user-select: none;

    label {
      margin: 0 0 1em;
      white-space: initial;
      font-weight: normal;
    }
  }
}

// use tab styles on larger screens
@include breakpoint(md) {
  ul {
    display: flex;
    gap: 1em;
    color: $blue-light;

    li {
      display: flex;
      border: 1px solid $blue-light;
      border-radius: 14px;

      &.selected {
        background: $blue-light;
        color: $white;
      }

      label {
        margin: 0;
        padding: 1em;
      }

      input {
        display: none;
      }
    }
  }
}
</style>
