<template>
  <div id="app">
    <notifications group="foo" />
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
</style>

<script>
  import Container from './components/Container';
  import Row from './components/Row';
  import stroe from './store.js'
import store from './store.js';
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
              // console.log(friend + "has send you a friend request!");
              alert(friend + "has send you a friend request!");
              }
            });
        }
    }
 }
  
</script>

