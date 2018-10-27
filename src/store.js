import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    lastScore: 0
  },
  getters:{
    getLastScore: state => state.lastScore
  },
  mutations: {
    setLastScore(state, s){
      state.lastScore = s
    }
  },
  actions: {

  }
})
