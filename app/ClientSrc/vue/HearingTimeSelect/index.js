import Vue from "vue";
import HearingTimeSelect from "./HearingTimeSelect.vue";

import "es6-promise/auto"; // ES6 Promises Polyfill for IE11 support

Vue.config.devtools = false;
Vue.config.productionTip = false;

Vue.component("HearingTimeSelect", HearingTimeSelect);

if ($("#VueHearingTimeSelect").length) {
  let vue = new Vue({
    el: "#VueHearingTimeSelect",
  });
}
