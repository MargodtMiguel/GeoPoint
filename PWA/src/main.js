import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import './registerServiceWorker'

Vue.config.productionTip = false

import * as Sentry from '@sentry/browser'

Sentry.init({
  dsn: 'https://10f52e9632f44558a1bb77ab3a53e49a@sentry.io/1320794',
  integrations: [new Sentry.Integrations.Vue({ Vue })]
})

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
