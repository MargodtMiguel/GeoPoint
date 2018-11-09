import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    authToken:localStorage.getItem('auth-token'),
    lastScore: 0,
    currentMap: '',
    startTime: '',
    endTime: '',
    topScores: []
  },
  getters:{
    getLastScore: state => state.lastScore,
    getCurrentMap: state => state.currentMap,
    getDurationTime(state){
      return (state.endTime - state.startTime) / 1000
    },
    getTopScores: state => state.topScores
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
    setTopScores(state, s){
      console.log("setter:")
      console.log(s)
      state.topScores = s
      console.log("state topscores:")
      console.log(state.topScores)
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
          // state.authToken = response.data.token;
          localStorage.setItem('auth-token', response.data.token);
          console.log(state.authToken)
        }else{
          console.log(response)
        }

      })
      .catch(e => {
        localStorage.removeItem('auth-token');
      })
    },
    userRegister(state, account){
      axios.post(`https://localhost:44363/api/Auth/Register`, {
        userName: account.login,
        password: account.password1
      })
      .then(response => {
        if(response.data.token != undefined){
          // state.authToken = response.data.token
          localStorage.setItem('auth-token', response.data.token);
        }else{
          console.log(response)
        }
      })
      .catch(e => {
        localStorage.removeItem('auth-token');
      })
    }    
  },
  actions: {
    fetchTopScoresByArea:({commit, state})=>{
      let token = "Bearer " + state.authToken;
      console.log(token)
      axios.get('https://localhost:44363/api/Scores/getTopScores',
        {
          headers: {'Authorization': token},
          params:{
            area: 'EU',
            length: 20
          }
        }
      )
      .then(response => {
        console.log(response)
        commit('setTopScores', response.data);  
      })
    }
  }
})
