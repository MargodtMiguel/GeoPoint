import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    lastScore: 0,
    currentMap: '',
    startTime: '',
    endTime: '',
  },
  getters:{
    getLastScore: state => state.lastScore,
    getCurrentMap: state => state.currentMap,
    getDurationTime(state){
      return (state.endTime - state.startTime) / 1000
    }
  },
  mutations: {
    setLastScore(state, s){
      state.lastScore = s
    },
    setCurrentMap(state, m){
      state.currentMap = m
    },
    setStartTime(state){
      state.startTime = Date.now()
    },
    setEndTime(state){
      state.endTime = Date.now()
    },
    resetValues(state){
      state.lastScore = 0;
      state.currentMap = '';
      state.startTime = '';      
      state.endTime = '';
    }
    
  },
  actions: {

  }
})
