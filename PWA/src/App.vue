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
    },

    methods:{
     
    },
    created: function(){
        this.$store.commit('setConnection')
    },
  }
  
</script>

