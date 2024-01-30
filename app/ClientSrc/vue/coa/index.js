import Vue from "vue";
import ApplicationTypeSelect from "./ApplicationTypeSelect";

import "es6-promise/auto"; // ES6 Promises Polyfill for IE11 support

Vue.config.devtools = false;
Vue.config.productionTip = false;

Vue.component("ApplicationTypeSelect", ApplicationTypeSelect);

// Mount vue app if a container element exists
if ($('#VueApplicationTypeSelect').length) {
    new Vue({
        el: "#VueApplicationTypeSelect"
    });
}
