import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import './registerServiceWorker'
import * as SignalR from '@aspnet/signalr'
import {i18n } from'./plugins/i18n'
import Notifications from 'vue-notification'

import * as Sentry from '@sentry/browser'


Sentry.init({
  dsn: 'https://10f52e9632f44558a1bb77ab3a53e49a@sentry.io/1320794',
  integrations: [new Sentry.Integrations.Vue({ Vue })]
})


new Vue({
  i18n,
  router,
  Notifications,
  store,
  render: h => h(App)
}).$mount('#app')

Vue.use(Notifications)