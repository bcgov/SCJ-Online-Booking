import { createApp } from "vue";
import ApplicationTypeSelect from "./ApplicationTypeSelect";

const el = document.getElementById("VueApplicationTypeSelect");
if (el) {
  createApp(ApplicationTypeSelect).mount(el);
}
