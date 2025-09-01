import Vue from "vue";
import TrialTimeSelect from "./TrialTimeSelect.vue";

import "es6-promise/auto"; // ES6 Promises Polyfill for IE11 support

Vue.config.devtools = false;
Vue.config.productionTip = false;

Vue.component("TrialTimeSelect", TrialTimeSelect);

// Mount vue app if a container element exists
if ($("#VueTrialTimeSelect").length) {
  new Vue({
    el: "#VueTrialTimeSelect",
  });
}
