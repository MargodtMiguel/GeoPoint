import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import Maps from './views/Maps.vue'
import Login from './views/Login.vue'
import Register from './views/Register.vue'
import GameOver from './views/GameOver.vue'
import Finish from './views/Finish.vue'
import Game from './views/Game.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/maps',
      name: 'maps',
      component: Maps
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
    },
    {
      path: '/gameover',
      name: 'gameover',
      component: GameOver
    },
    {
      path:'/finish',
      name: 'finish',
      component: Finish
    },
    {
      path:'/game/:map',
      name: 'game',
      component: Game
    }
  ]
})