import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import Maps from './views/Maps.vue'
import Login from './views/Login.vue'
import Register from './views/Register.vue'
import GameOver from './views/GameOver.vue'
import Finish from './views/Finish.vue'
import Game from './views/Game.vue'
import Leaderboard from './views/Leaderboard.vue'
import Leadermap from './views/Leadermap.vue'
import Friends from './views/Friends.vue'
import moment from 'moment'
import store from './store.js'

Vue.use(Router)

const router = new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/maps',
      name: 'maps',
      component: Maps,
      meta: {
        requiresAuth: true
      }
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
      component: Game,
      meta: {
        requiresAuth: true
      }
    },
    {
      path:'/leaderboard/:map',
      name: 'leaderboard',
      component: Leaderboard,
      meta: {
        requiresAuth: true
      }
    },
    {
      path:'/leaderboard',
      name: 'leadermap',
      component: Leadermap,
      meta: {
        requiresAuth: true
      }
    },
    {
      path:'/friends',
      name: 'friends',
      component: Friends,
      meta: {
        requiresAuth: true
      }
    }
  ]

})

router.beforeEach((to, from, next) => {
  // if(to.meta.requiresAuth && localStorage.authToken == null){
  //   return next('/login')
  // }

if(to.meta.requiresAuth){
  var expDateStorage =localStorage.expDate;
  var expDate = moment(expDateStorage);
  var now = moment();

  try{
    if(now.isBefore(expDate)){ 

      next();
      
    }else{
      return next('/login')
    }
  }catch(err){
    return next('/login')
  }
     
}
  next();
})

export default router;