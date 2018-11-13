<template>
    <div class="c-leaderboard">
        <h1>{{ mapRoute }}</h1>
        <div class="c-leaderboard__holder" v-if="topScores && topScores.length">
            <div class="c-leaderboard__toggle">
                <p>Top 20 best scores</p>
                <div class="c-button-primary secundary toggle">
                    Friends only
                </div>     
            </div>
            <div class="c-leaderboard__header">
                <p>Position</p>
                <p>User</p>
                <p>Score</p>
                <p>Time</p>
            </div>
            <div class="grid-container" v-for="(score, index) in topScores" v-bind:key="score.id">
                <div class="position"><p>{{ index + 1}}</p></div>
                <div class="username"><p>{{score.user.userName}}</p></div>
                <div class="Score"><p class="resp">Score</p><p>{{ score.value }}</p></div>
                <div class="Time"><p class="resp">Time</p><p>{{ score.timeSpan }}</p></div>
            </div>
    
        </div>
        <div v-else>
            <p>There are no scores available for this map</p>
        </div>
       

        <div @click="otherMap()" class="c-button-primary secundary">PICK OTHER MAP</div>
    </div>
</template>

<style lang="scss">
    @import './src/style/settings/settings.colors.scss';

    @import './src/style/components/components.leaderboard.scss';
    </style>

<script>

  


export default {
    name: 'leaderboard',
    computed:{
        mapRoute(){
            return this.$route.params.map
        },
        topScores(){
            return this.$store.getters.getTopScores
        }
    },
    methods:{
        otherMap: function(){
            this.$store.commit('resetValues');
            this.$router.push('/leaderboard');
        }
    },
    created: function(){
        this.$store.dispatch('fetchTopScoresByArea', this.$route.params.map);   
    }
}
</script>

