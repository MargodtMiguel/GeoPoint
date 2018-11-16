<template>
  <div class="c-home">
    <h1>GeoPoint</h1>
      <router-link to="/maps">
        <div class="c-button-primary">PLAY</div>
      </router-link>

      <router-link to="/friends">
        <div class="c-button-primary">FRIENDS</div>
      </router-link>

    <router-link to="/leaderboard">
      <div class="c-button-primary secundary">LEADERBOARDS</div>
    </router-link>
    <div class="c-home__logout">
      <p @click="logOut()">Log out</p>
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
</style>

<script>
// @ is an alias to /src

export default {
  name: 'home',
  methods:{
    logOut(){
      this.$store.commit('userLogOut');
      this.$router.replace('/login')
    }
  }
}
</script>
