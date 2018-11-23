import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import moment from 'moment'
import router from './router.js'
import * as signalR from '@aspnet/signalr'

Vue.use(Vuex)

const store = new Vuex.Store({
  state: {
    authToken:localStorage.authToken,
    expDate:localStorage.expDate,
    lastScore: 0,
    currentMap: '',
    startTime: '',
    endTime: '',
    topScores: [],
    friends: [],
    errorMessage: '',
    signalrConnection: '',
    signalrCurUser: localStorage.signalrCurUser,
    foundUsers: '',
  },
  getters:{
    getLastScore: state => state.lastScore,
    getCurrentMap: state => state.currentMap,
    getDurationTime(state){
      return (state.endTime - state.startTime) / 1000
    },
    getTopScores: state => state.topScores,
    getFriends(state){
      var friends = [];
      for(var i = 0, l = state.friends.length; i<l; i++){
        // console.log(state.friends[i])
        if(state.friends[i].isPending != true){
          friends.push(state.friends[i])
        }
      }
      return friends;
    },
    getPendingFriends(state){
      var pendingfriends = [];
      for(var i = 0, l = state.friends.length; i<l; i++){
        // console.log(state.friends[i])
        if(state.friends[i].isPending == true){
          pendingfriends.push(state.friends[i])
        }
      }
      return pendingfriends;
    },
    isLoggedIn(state){
      var expDateStorage = moment(localStorage.expDate);
      var now = moment(Date.now());

      if(now.isBefore(expDateStorage)){ 
        return true;
      }else{
        return false;
      }
    },
    getErrorMessage: state => state.errorMessage,
    getSignalrConnection: state => state.signalrConnection,
    getSignalrCurUser: state => state.signalrCurUser,
    getUsersByUsername: state => state.foundUsers
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
    setFriends(state, f){
      state.friends = f
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
          state.signalrCurUser = account.login;
          localStorage.signalrCurUser = account.login;
          localStorage.expDate =  moment().add(40, 'm').format();
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
      state.friends=[];

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
          state.signalrCurUser = account.login;
          localStorage.signalrCurUser = account.login;
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
    },
    setConnection(state){
        state.signalrConnection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44363/friendRequest").build();
    },
    setFoundUsers(state,users){
      state.foundUsers = users
      //console.log("setFoundUsers "+ state.foundUsers) 
    } ,
    sendFriendRequest(state,friend){
    let token = "Bearer " + localStorage.authToken;
    let data = JSON.stringify({
      username: friend
    })
    axios.post('https://localhost:44363/api/Users/sendFriendRequest',data, {
      headers: {
        'Content-Type': 'application/json',
        'Authorization': token
      },

    })
    .then(response => {
      console.log("friend added")

    })
    .catch(e => {
      console.log("error " + e)
    })
    },
    userLogOut(state){
      localStorage.clear();
    },
    confirmFriendRequest(state, fun){
      let token = "Bearer " + localStorage.authToken;
      let data = JSON.stringify({
        username: fun
      })
      axios.post('https://localhost:44363/api/Users/confirmFriendRequest',data, {
        headers: {
          'Content-Type': 'application/json',
          'Authorization': token
        },

      })
      .then(response => {
        console.log("friend added")
      })
      .catch(e => {
        console.log("error " + e)
      })
    },
    declineFriendRequest(state, fun){
      let token = "Bearer " + localStorage.authToken;
      axios.delete('https://localhost:44363/api/Users/declineFriendrequest', {
        headers: {
          'Content-Type': 'application/json',
          'Authorization': token
        },
        data:{
          username:fun
        }

      })
      .then(response => {
        console.log("friend declined")
      })
      .catch(e => {
        console.log("error " + e)
      })
    },
    deleteFriend(state, fun){
      let token = "Bearer " + localStorage.authToken;
      axios.delete('https://localhost:44363/api/Users/removeFriend', {
        headers: {
          'Content-Type': 'application/json',
          'Authorization': token
        },
        data:{
          username:fun
        }

      })
      .then(response => {
        console.log("friend removed")
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
        commit('setTopScores', response.data);  
      })
    },
    searchUser:({commit}, val) =>{
      let token = "Bearer " + localStorage.authToken;
      axios.get('https://localhost:44363/api/Users/searhUser',
      {
        headers: {'Authorization': token},
        params:{
          Username: val
        }
      }
    ).then(response => {
      console.log(response)
      commit('setFoundUsers', response.data);  
    }).catch(err=>{
      console.log(err)

    })
    },
    fetchFriends:({commit, state})=>{
      let token = "Bearer " + localStorage.authToken;
      axios.get('https://localhost:44363/api/Users/getMyFriends',
        {
          headers: {'Authorization': token}
        }
      )
      .then(response => {
        commit('setFriends', response.data);  
      })
    },
  }
})

export default store;
