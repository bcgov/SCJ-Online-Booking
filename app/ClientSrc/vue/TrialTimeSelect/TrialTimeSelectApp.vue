<template>
  <trial-time-select-tabs v-bind="tabsProps">
    <template #regularBooking>
      <regular-booking v-bind="regularProps">
        <template #noDatesError>
          <div class="alert alert-danger" role="alert">
            <i class="fa fa-ban"></i>
            There are currently no trial dates available. Try again at a later time as more dates
            become available in the system.
          </div>
        </template>
      </regular-booking>
    </template>

    <template #fairUseBooking>
      <fair-use-booking v-bind="fairUseProps">
        <!-- Other nested templates would need to be extracted from server content -->
      </fair-use-booking>
    </template>

    <template #fairUseTabDescription>
      Request up to five dates for a trial starting in the upcoming release of dates.
    </template>

    <template #fairUseDisabledAlert>
      The booking period has ended. The next booking period will open soon.
    </template>

    <template #regularTabDescription>
      You can instantly book a trial date that is currently available in the system.
    </template>
  </trial-time-select-tabs>
</template>

<script>
import Tabs from "./Tabs.vue";
import RegularBooking from "./RegularBooking.vue";
import FairUseBooking from "./FairUseBooking.vue";

export default {
  name: "TrialTimeSelectApp",

  components: {
    "trial-time-select-tabs": Tabs,
    "regular-booking": RegularBooking,
    "fair-use-booking": FairUseBooking,
  },

  props: {
    tabsProps: {
      type: Object,
      default: () => ({}),
    },
    regularProps: {
      type: Object,
      default: () => ({}),
    },
    fairUseProps: {
      type: Object,
      default: () => ({}),
    },
  },
};
</script>
