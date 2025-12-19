import { createApp } from "vue";
import HearingTimeSelect from "./HearingTimeSelect.vue";

if (document.getElementById("VueHearingTimeSelect")) {
  const app = createApp({});
  app.component("HearingTimeSelect", HearingTimeSelect);
  app.mount("#VueHearingTimeSelect");
}
