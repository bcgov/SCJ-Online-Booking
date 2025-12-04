import { createApp } from "vue";
import ApplicationTypeSelect from "./ApplicationTypeSelect";

// Mount vue app if a container element exists
if (document.getElementById("VueApplicationTypeSelect")) {
  const app = createApp({
    methods: {
      // bring functions (from /js/coa.js) into the vue app scope
      onApplicationTypeChange,
    },
  });
  app.component("ApplicationTypeSelect", ApplicationTypeSelect);
  app.mount("#VueApplicationTypeSelect");
}
