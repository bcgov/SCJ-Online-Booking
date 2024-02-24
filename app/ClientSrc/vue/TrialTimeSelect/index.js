import Vue from "vue";
import Tabs from "./Tabs";
import RegularBooking from "./InstantBook";
import ChooseAvailability from "./ChooseAvailability";

import "es6-promise/auto"; // ES6 Promises Polyfill for IE11 support

Vue.config.devtools = false;
Vue.config.productionTip = false;

Vue.component("TrialTimeSelectTabs", Tabs);
Vue.component("RegularBooking", RegularBooking);
Vue.component("ChooseAvailability", ChooseAvailability);

// Mount vue app if a container element exists
if ($("#VueTrialTimeSelect").length) {
    new Vue({
        el: "#VueTrialTimeSelect",
    });
}
