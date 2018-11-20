<template>
  <div id="app">
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
 export default {
    name: 'App',
    components: {
      Container,
      Row
    },
    computed:{
      curUser(){
        return this.$store.getters.getSignalrCurUser;
      },
      connection(){
        return this.$store.getters.getSignalrConnection;
      }
    },
    watch:{
        curUser(value){
          console.log("user changed");
          this.startConnection(value);
        },
    },
    methods:{
      startConnection: function(value){

        console.log("started with user "+ value)
        console.log(this.connection);

        this.connection.start().then(function(){
        }).catch(function(err){
            return console.error(err.toString());
        });

        this.connection.on('Login', () => {
          this.connection.invoke("Login", value);
        });

        this.connection.on("ServerMessage", function (message) {        
          console.log(message)
        });

        this.connection.on("RecieveFriendRequest",function(message){
            alert(message);
        });
      }
    },
    created: function(){
        this.$store.commit('setConnection')
    },
  }
  
</script>

