import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";

import "./styles.css";
import 'virtual:uno.css'
import 'vue-m-message/dist/style.css'

// await getCurrent().center();
createApp(App).use(router).mount("#app");

