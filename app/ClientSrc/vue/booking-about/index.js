import Vue from "vue";
import Sample from "./sample.vue";
import "es6-promise/auto"; // ES6 Promises Polyfill for IE11 support

Vue.config.devtools = false;
Vue.config.productionTip = false;

Vue.component("sample", Sample);

// ReSharper disable once ConstructorCallNotUsed
new Vue({
    el: "#VueExample"
});
