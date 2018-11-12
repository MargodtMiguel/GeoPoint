import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import moment from 'moment'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    authToken:localStorage.authToken,
    expDate:localStorage.expDate,
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
    getTopScores: state => state.topScores,
    isLoggedIn(state){
      var expDateStorage = localStorage.expDate;
      var expDate = new Date(expDateStorage);
      var now = moment(Date.now());
      console.log(expDateStorage);
      console.log(now);
      if(now != undefined && expDateStorage != undefined){
        if(now.isValid() && expDateStorage.isValid()){
          if(now.isBefore(expDateStorage)){
            console.log("true")
            return true;
          }else{
            console.log("false")
            return false;
          }
        }else{
          return false;
        }
      }else{
        return false;
      }
      
     
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
          console.log( moment(response.data.expiration).add(5, 'm').toDate())
          localStorage.expDate =  moment(response.data.expiration).add(5, 'm').toDate();
          console.log("userLogIn expDate"  + localStorage.expDate)
        }else{
          console.log(response)
        }

      })
      .catch(e => {
        localStorage.clear();
      })
    },
    userRegister(state, account){
      //clear local storage data's
      localStorage.clear();
      axios.post(`https://localhost:44363/api/Auth/Register`, {
        username: account.login,
        password: account.password1,
        email: account.email
      })
      .then(response => {
        if(response.data.token != undefined){
          //set local storage data's
          localStorage.authToken = response.data.token;
          console.log( moment(response.data.expiration).add(5, 'm').toDate())
          localStorage.expDate =  moment(response.data.expiration).add(5, 'm').toDate();
          console.log(localStorage.expDate)
        }else{
          console.log(response)
        }
      })
      .catch(e => {
        //clear local storage data's
      localStorage.clear();
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
            length: 20
          }
        }
      )
      .then(response => {
        commit('setTopScores', response.data);  
      })
    }
  }
})
