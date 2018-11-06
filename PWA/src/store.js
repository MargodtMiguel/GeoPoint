import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    authToken:'',
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
    },
    userLogIn(state, account){
      axios.post(`https://localhost:44363/api/Auth/Login`, {
        userName: account.login,
        password: account.password
      })
      .then(response => {
        if(response.data.token != undefined){
          this.authToken = response.data.token;
          console.log(response.data.token)
        }else{
          console.log(response)
        }

      })
    },
    userRegister(state, account){
      axios.post(`https://localhost:44363/api/Auth/Register`, {
        userName: account.login,
        password: account.password1
      })
      .then(response => {
        if(response.data.token != undefined){
          this.authToken = response.data.token
        }else{
          console.log(response)
        }
      })
    }
  },
  actions: {
    
  }
})
