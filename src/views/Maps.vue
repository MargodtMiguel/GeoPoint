<template>
  <div class="c-maps">
    <h1>GeoPoint</h1>
    <p>PICK A MAP</p>
    <div class="c-maps__maps">
      <div class="c-maps__maps__map">
        <router-link to="/game/Europe">
          <img src="../assets/maps/europe.svg" />
          <p>Europe</p>
        </router-link>
      </div>
      <div class="c-maps__maps__map">
        <router-link to="/game/Africa">
          <img src="../assets/maps/africa.svg"/>
          <p>Africa</p>
        </router-link>
      </div>

    </div>
    <div @click="getRandomMap()" class="c-button-primary secundary">RANDOM MAP</div>   
  
  </div>
</template>

<style lang="scss">
 @import './src/style/settings/settings.colors.scss';
  @import './src/style/settings/settings.global.scss';
  .c-maps h1{
    padding-bottom:20px;
  }

  .c-maps p{
    text-align:center;
    font-weight:600;
    font-size:1.5em;
    margin-bottom:40px;
  }

  .c-maps__maps{
    display:flex;
    flex-wrap:wrap;
    justify-content: space-around;
    align-items: flex-start;
    margin-bottom:50px;
  }

  .c-maps__maps__map{
    width:230px;
    max-height:200px;
    margin:10px;
    margin-bottom:40px;
    transition: all .2s ease-in-out;
  }

    .c-maps__maps__map img{
    height:200px;
  }

  .c-maps__maps__map:hover{
    cursor: pointer;
    transform: scale(1.1);
  }
</style>

<script>

  var allmaps = ['Africa', 'Europe'];

  export default {
    name:'maps',
    methods:{
      getRandomMap: function(){
        var randomMap = allmaps[Math.floor(Math.random()*allmaps.length)];
        this.$router.push(randomMap);
      }
    }
  }
</script>
