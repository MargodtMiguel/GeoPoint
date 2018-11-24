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
 export default {
    name: 'App',
    components: {
      Container,
      Row
    },
    created: function(){
        if(localStorage.signalrCurUser != undefined){

            this.$store.commit('setConnection');
          }
    },
  }
  
</script>

