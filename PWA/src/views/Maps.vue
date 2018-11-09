<template>
  <div>
    <MapPick routeVar="/game/"></MapPick>

    <div @click="getRandomMap()" class="c-button-primary">RANDOM MAP</div>
    <router-link to="/"><div class="c-button-primary secundary">BACK TO MENU</div></router-link>   
  
  </div>
</template>

<style lang="scss">
 @import './src/style/components/components.maps.scss';
</style>

<script>
  import MapPick from '../components/MapPick';
  var allmaps = ['Africa', 'Europe'];



  export default {
    name:'maps',
    components:{
      MapPick
    },
    methods:{
      getRandomMap: function(){
        var randomMap = allmaps[Math.floor(Math.random()*allmaps.length)];
        this.$router.push("/game/" + randomMap);
      }
    }
  }
</script>
