<template>
  <div id="app">
    <container class="o-container--full">
      <router-view/>
      <button @click.stop="sendFriendRequest">press here</button>
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
  import * as SignalR from '@aspnet/signalr'
 
  export default {
    name: 'App',
    components: {
      Container,
      Row
    },
    data(){
      return{
         connection: new SignalR.HubConnectionBuilder().withUrl("https://localhost:44363/friendRequest").build(),
        
      }
    },   
    computed:{
       curUser(){
         return this.$store.getters.getCurUser
       } 
    } ,
    watch:{
      curUser (value) {
        this.startRealTime(value)
      }
    },
    methods:{
      sendFriendRequest:function(){
        console.log(this.curUser)
         this.connection.invoke("sendFriendRequest", this.curUser, "MiguelMargodt").catch(function (err) {
            return console.error(err.toString());
            
        });
      },
    startRealTime: function(user){
      
      this.connection.start().catch(function(err){
        return console.log(err.toString());
      });
      this.connection.on("ReceiveMessage", function (user, message) {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = user + " says " + msg;
        console.log(encodedMsg);
      });
      connection.on('Login', () => {
        connection.invoke("Login", user);
      });
      let start = document.addEventListener("DOMContentLoaded",(event) => {
        console.log("DOM fully loaded and parsed");        
        
      });
    }
  }
}
  
</script>