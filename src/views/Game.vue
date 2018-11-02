<template>
  <div>
      <component :is="mapComponent"></component>
  </div>
</template>

<style lang="scss">
 @import './src/style/components/components.game.scss';
</style>

<script>
//all map components
	import Africa from '../components/Africa';
	import Europe from '../components/Europe';

  export default {
    name: 'game',
    components: {
			Africa,
			Europe
    },
    computed:{
      //get the correct map from the router parameter
      mapComponent(){
        return this.$route.params.map
      }
    },
    created: function(){
      //make sure all values are resetted
      this.$store.commit('resetValues');
      //set the current map (to show on the finish page)
      this.$store.commit('setCurrentMap', this.mapComponent);
      //start the timer (duration time is used in scoreboard and on finish page)
      this.$store.commit('setStartTime');
    }
  }
</script>
