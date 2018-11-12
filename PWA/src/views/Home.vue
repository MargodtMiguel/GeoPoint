<template>
  <div class="home">
    <h1>GeoPoint</h1>
      <router-link to="/maps">
        <div class="c-button-primary">PLAY</div>
      </router-link>

    <!-- <div class="c-button-primary">MULTIPLAYER</div> -->
    <router-link to="/leaderboard">
      <div class="c-button-primary secundary">LEADERBOARDS</div>
    </router-link>
  </div>
</template>

<script>
// @ is an alias to /src
export default {
  name: 'home'
}
</script>
