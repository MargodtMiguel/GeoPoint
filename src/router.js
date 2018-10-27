import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import Singleplayer from './views/Singleplayer.vue'
import Login from './views/Login.vue'
import Register from './views/Register.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/singleplayer',
      name: 'singleplayer',
      component: Singleplayer
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/register',
      name: 'register',
      component: Register
    }
  ]
})
