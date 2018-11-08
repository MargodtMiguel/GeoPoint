<template>
    <div class="c-maps">
    <h1>GeoPoint</h1>
    <p>PICK A MAP</p>
    <div class="c-maps__maps">
      <div @click="pickMap('Europe')" class="c-maps__maps__map">
          <img src="../assets/maps/europe.svg" />
          <p>Europe</p>
      </div>
      <div @click="pickMap('Africa')" class="c-maps__maps__map">
          <img src="../assets/maps/africa.svg"/>
          <p>Africa</p>
      </div>
    </div>
    </div>
</template>

<style lang="scss">
@import './src/style/settings/settings.colors.scss';

</style>

<script>
export default {
    name: 'MapPick',
    props: ['routeVar'],
    methods:{
      pickMap: function(map){
        this.$router.push(this.routeVar + map);
      }
    }
}
</script>

