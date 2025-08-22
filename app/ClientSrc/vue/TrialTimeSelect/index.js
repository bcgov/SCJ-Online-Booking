import { createApp } from "vue";
import TrialTimeSelectApp from "./TrialTimeSelectApp.vue";
import { extractProps } from "./helpers.js";

// Mount vue app if a container element exists
if (document.getElementById("VueTrialTimeSelect")) {
  const container = document.getElementById("VueTrialTimeSelect");

  // Extract data attributes from the server-rendered elements
  const tabsElement = container.querySelector("trial-time-select-tabs");

  // Find elements in template content for child component props
  let regularElement = null;
  let fairUseElement = null;

  const templates = container.querySelectorAll("template");
  templates.forEach((template) => {
    const content = template.content || template;
    if (!regularElement) {
      regularElement = content.querySelector("regular-booking");
    }
    if (!fairUseElement) {
      fairUseElement = content.querySelector("fair-use-booking");
    }
  });

  const tabsProps = extractProps(tabsElement);
  const regularProps = extractProps(regularElement);
  const fairUseProps = extractProps(fairUseElement);

  // Create the main app using the clean Vue component
  const app = createApp(TrialTimeSelectApp, {
    tabsProps,
    regularProps,
    fairUseProps,
  });

  // Enhanced error handling
  app.config.errorHandler = (err, vm, info) => {
    console.error("[Vue Error]", err);
    console.error("[Vue Error Info]", info);
  };

  app.config.warnHandler = (msg, vm, trace) => {
    console.warn("[Vue Warning]", msg);
  };

  // Clear the container and mount
  container.innerHTML = "";

  try {
    const mountedApp = app.mount("#VueTrialTimeSelect");
  } catch (error) {
    console.error("Failed to mount Vue app:", error);
  }
}
