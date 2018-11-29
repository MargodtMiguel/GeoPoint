<template>
  <div id="app">
    <notifications group="friendrequest" classes="c-notification"/>
    <container class="o-container--full">
      <router-view/>
    </container>
  </div>
</template>

<style lang="scss">
 @import 'style/base';

 html, body{
   background-color:$bg-color-dark
 }

 .c-notification {
  // Style of the notification itself 
  background-color:$alpha-color-dark !important;
  margin:5px !important;
 
  .notification-title {
    font-size:1.4em !important;
    padding:20px !important;
    padding-bottom:10px !important;
  }
 
  .notification-content {
    font-size:1.1em !important;
    padding:20px !important;
    padding-top:0px !important;
  }
}
</style>

<script>
  import Container from './components/Container';
  import Row from './components/Row';
  import store from './store.js';
  import Vue           from 'vue'
  import Notifications from 'vue-notification'
 

Vue.use(Notifications)

 export default {
    name: 'App',
    components: {
      Container,
      Row
    },
    created: function(){

        if(localStorage.signalrCurUser != undefined){
            store.commit('setConnection');
            store.getters.getSignalrConnection.on("RecieveFriendRequest",function(friend,username){
              if(store.getters.getSignalrCurUser == username){
                        Vue.notify({
                        group: 'friendrequest',
                        title: 'New friend request!',
                        text: friend + " wants to become your friend."
                      })
              }
            });
        }
    }
 }
  
</script>

