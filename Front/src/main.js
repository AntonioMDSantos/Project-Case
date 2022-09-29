import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";
import VueResource from 'vue-resource';
import VueTheMask from "vue-the-mask";

import light from './styles/export_light';
import dark from './styles/export_dark';
import './styles/export.css';

import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";

Vue.use(Toast, {
  transition: "Vue-Toastification__bounce",
  maxToasts: 20,
  newestOnTop: true
});

Vue.use(VueResource);
Vue.use(VueTheMask);

Vue.prototype.$light_theme = light;
Vue.prototype.$dark_theme = dark;

Vue.config.productionTip = false;

Vue.$globalEvent = new Vue();

new Vue({
  router,
  store,
  vuetify,
  render: (h) => h(App),
}).$mount("#app");
