<template>
    <div class="c-finish">
        <h1>Success!</h1>
        <p>You finished the map <span class="c-finish__map">{{ currentMap }}</span></p>
        <div class="c-finish__results">
            <p>Score: {{ lastScore }}</p>
            <p>Duration: {{ durationTime }} seconds</p>
        </div>
        <router-link to="/maps">
        <div class="c-button-primary">PLAY AGAIN</div>
         </router-link>
          <router-link to="/">
        <div class="c-button-primary secundary">BACK TO MENU</div>
          </router-link>
   
  
    </div>
</template>

<style lang="scss">
 @import './src/style/settings/settings.colors.scss';
    .c-finish p{
        text-align:center;
        font-size:1.9em;
        margin-bottom:30px;
    }

    .c-finish h1{
        padding-bottom:15px;
    }

    .c-finish__map{
        color:$alpha-color;
    }

    .c-finish__results{
        width:270px;
        margin:0 auto;
    }

    .c-finish__results p{
        text-align:left;
        font-size:1.5em;
        margin-bottom:10px;
    }

    .c-finish__results p:last-child{
        margin-bottom:50px;
    }
</style>

<script>
export default {
    name:'finish',
    computed:{
        lastScore(){
            return this.$store.getters.getLastScore
        },
        durationTime(){
            return this.$store.getters.getDurationTime
        },
        currentMap(){
            return this.$store.getters.getCurrentMap
        }
    },
    data(){
        return{
            score:{
                value: this.$store.getters.getLastScore,
                area: this.$store.getters.getCurrentMap,
                timeSpan: this.$store.getters.getDurationTime
            }
        }
    },
    created: function(){
        if(this.lastScore == 0){
            this.$router.push('gameover');
        }else{
            this.$store.commit('addScore', this.score);
        }
    }
}
</script>
