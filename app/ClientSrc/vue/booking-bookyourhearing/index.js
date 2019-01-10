import Vue from "vue";
import AvailableTimes from "./availabletimes.vue";

import "es6-promise/auto"; // ES6 Promises Polyfill for IE11 support

Vue.config.devtools = false;
Vue.config.productionTip = false;

Vue.component("availabletimes", AvailableTimes);

// ReSharper disable once ConstructorCallNotUsed
new Vue({
    el: "#VueAvailableTimes"
});
