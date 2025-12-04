import { createApp } from "vue";
import LongChambersTimeSelect from "./LongChambersTimeSelect.vue";

if (document.getElementById("VueLongChambersTimeSelect")) {
  const app = createApp({});
  app.component("LongChambersTimeSelect", LongChambersTimeSelect);
  app.mount("#VueLongChambersTimeSelect");
}
