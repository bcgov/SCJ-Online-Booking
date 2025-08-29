import { createApp } from "vue";
import TrialTimeSelect from "./TrialTimeSelect.vue";

if (document.getElementById("VueTrialTimeSelect")) {
  const app = createApp({});
  app.component("TrialTimeSelect", TrialTimeSelect);
  app.mount("#VueTrialTimeSelect");
}
