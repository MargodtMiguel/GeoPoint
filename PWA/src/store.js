import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import moment from 'moment'
import router from './router.js'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    authToken:localStorage.authToken,
    expDate:localStorage.expDate,
    lastScore: 0,
    currentMap: '',
    startTime: '',
    endTime: '',
    topScores: [],
    errorMessage: '',
  },
  getters:{
    getLastScore: state => state.lastScore,
    getCurrentMap: state => state.currentMap,
    getDurationTime(state){
      return (state.endTime - state.startTime) / 1000
    },
    getTopScores: state => state.topScores,
    isLoggedIn(state){
      var expDateStorage = moment(localStorage.expDate);
      var now = moment(Date.now());
    
      try{
        if(now.isBefore(expDateStorage)){
          return true;
        }else{
          return false;
        }
      }catch(err){
        return false;
      }
    },
    getErrorMessage: state => state.errorMessage,
    getCurUser: state => state.curUser
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
      state.topScores = s
    },
    resetValues(state){
      //reset all values (in case of new game)
      state.lastScore = 0;
      state.currentMap = '';
      state.startTime = '';      
      state.endTime = '';
      state.topScores = [];
      state.errorMessage = '';
    },
    userLogIn(state, account){
      //clear local storage data's
      localStorage.clear();
      axios.post(`https://localhost:44363/api/Auth/Login`, {
        username: account.login,
        password: account.password
      })
      .then(response => {
        if(response.data.token != undefined){
          //set local storage data's
          localStorage.authToken = response.data.token;
          localStorage.curUser = account.login;
          localStorage.expDate =  moment(response.data.expiration).add(40, 'm').toDate();
          router.push('/')
        }else{
        }

      })
      .catch(e => {
        localStorage.clear();
        state.errorMessage = e.response.data;
      })
    },
    userRegister(state, account){
      //clear local storage data's
      localStorage.clear();

      //clear previous error message
      state.errorMessage = '';

      axios.post(`https://localhost:44363/api/Auth/Register`, {
        username: account.login,
        password: account.password1,
        email: account.email
      })
      .then(response => {
        if(response.data.token != undefined){
          //set local storage data's
          localStorage.authToken = response.data.token;
          localStorage.expDate =  moment(response.data.expiration).add(40, 'm').toDate();
          router.push('/')
        }else{
        }
      })
      .catch(e => {
        //clear local storage data's
      localStorage.clear();
      state.errorMessage = e.response.data;
      })
    },
    userLogOut(state){
      localStorage.clear();
    },
    addScore(state, score){
      let token = "Bearer " + localStorage.authToken;
      let data = JSON.stringify({
        value: score.value,
        area: score.area.toUpperCase(),
        timeSpan: score.timeSpan
    })
      axios.post('https://localhost:44363/api/Scores/addScore',data, {
        headers: {
          'Content-Type': 'application/json',
          'Authorization': token
        },

      })
      .then(response => {
        console.log("Score added")

      })
      .catch(e => {
        console.log("error " + e)
      })
    }    
  },
  actions: {
    fetchTopScoresByArea:({commit, state}, a)=>{
      let token = "Bearer " + localStorage.authToken;
      axios.get('https://localhost:44363/api/Scores/getTopScores',
        {
          headers: {'Authorization': token},
          params:{
            area: a,
            length: 10
          }
        }
      )
      .then(response => {
        console.log(response)
        commit('setTopScores', response.data);  
      })
    },
    searchUser:({commit, state}, val) =>{
      let token = "Bearer " + localStorage.authToken;
      axios.get('https://localhost:44363/api/Scores/getTopScores',
      {
        headers: {'Authorization': token},
        params:{
          area: a,
          length: 10
        }
      }
    )
    }
  }
})
