<template>
<div>
  <slideout-panel>
  </slideout-panel>
<div class="c-home">
    <div class="c-language-icon">
      <div>
        <img v-on:click.prevent="showPanel" src="../assets/translation.png" alt="">
      </div>
    </div>
    <h1>GeoPoint</h1>
      <router-link to="/maps">
        <div class="c-button-primary">{{ $t('PLAY') }}</div>
      </router-link>

      <router-link to="/friends">
        <div class="c-button-primary">{{ $t('FRIENDS') }}</div>
      </router-link>

    <router-link to="/leaderboard">
      <div class="c-button-primary secundary">{{ $t('LEADERBOARDS') }}</div>
    </router-link>
    <div class="c-home__logout">
      <p @click="logOut()">{{ $t('LOG-OUT') }}</p>
    </div>

  </div>

</div>
  
</template>
<style lang="scss">
  .c-home__logout{
    width:100%;
    text-align:center;
    font-weight:600;
    cursor:pointer;
  }

  .c-home h1{
    padding-top:0px;
  }
</style>

<script>
// @ is an alias to /src
import { VueSlideoutPanel } from 'vue2-slideout-panel';
import changelanguage from '../components/ChangeLanguage';
import { vueSlideoutPanelService } from 'vue2-slideout-panel';

export default {
  
  name: 'home',
  components:{
    'slideout-panel': VueSlideoutPanel
  },
  methods:{
    logOut(){
      this.$store.commit('userLogOut');
      this.$router.replace('/login')
    },
    showPanel(){
      const panel1Handle = vueSlideoutPanelService.show({
      component : changelanguage,
      width: '350px',
      })
    },
  },
  created:function(){
    if(this.$store.getters.getSignalrConnection == ''){
      this.$store.commit('setConnection');
    }
  }
}
</script>
