<template>
    <div class="c-gameover">
        <h1 class="c-gameover__title">GAME OVER</h1>
        <p class="c-gameover__score">{{ $t('YOU-SCORED') }}: {{ lastScore }}</p>
         <router-link to="/maps">
        <div class="c-button-primary">{{ $t('TRY-AGAIN') }}</div>
         </router-link>
          <router-link to="/">
        <div class="c-button-primary secundary">{{ $t('BACK-TO-MENU') }}</div>
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
    data(){
        return{
            score:{
                value: this.$store.getters.getLastScore,
                area: this.$store.getters.getCurrentMap,
                timeSpan: this.$store.getters.getDurationTime
            }
        }
    },
    computed:{
        lastScore(){
            return this.$store.getters.getLastScore
        }
    },
    created: function(){
        this.$store.commit('addScore', this.score);
    }
}
</script>
