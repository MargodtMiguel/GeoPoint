<template>
  <div>
    <MapPick routeVar="/game/"></MapPick>

    <div @click="getRandomMap()" class="c-button-primary secundary">RANDOM MAP</div>   
  
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
