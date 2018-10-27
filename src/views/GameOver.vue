<template>
    <div class="c-gameover">
        <h1 class="c-gameover__title">GAME OVER</h1>
        <p class="c-gameover__score">YOU SCORED: {{ lastScore }}</p>
         <router-link to="/singleplayer">
        <div class="c-button-primary">TRY AGAIN</div>
         </router-link>
          <router-link to="/">
        <div class="c-button-primary">BACK TO MENU</div>
          </router-link>
    </div>
</template>

<style lang="scss">
    @import './src/style/settings/settings.colors.scss';
    @import './src/style/settings/settings.global.scss';
    .c-gameover__title{
        color: $beta-color;
    }

    .c-gameover__score{
        text-align:center;
        font-family: $font-stack;
        color:$text-color;
        font-size:1.8em;
        font-weight:600;
        margin-bottom:40px;
    }
</style>

<script>
export default {
    name:'gameover',
    computed:{
        lastScore(){
            return this.$store.getters.getLastScore
        }
    }
}
</script>
