<template>
    <div class="c-leaderboard">
        <h1>{{ mapRoute }}</h1>
        <div class="c-leaderboard__holder" v-if="topScores && topScores.length">
            <div class="c-leaderboard__toggle">
                <p>Top 10 scores</p>
                <div  v-if="friendsOnly" class="c-button-primary secundary toggle" @click="toggleFriends()">
                    {{ $t('FRIENDS-ONLY') }}
                </div>
                  <div v-else class="c-button-primary secundary toggle" @click="toggleFriends()">
                    {{ $t('GLOBAL') }}
                </div>   
            </div>
            <div class="c-leaderboard__header">
                <p>Position</p>
                <p>User</p>
                <p>Score</p>
                <p>Time</p>
            </div>
            <div class="c-leaderboard__grid" v-for="(score, index) in topScores" v-bind:key="score.id">
                <div class="position"><div class="test"><p>{{ index + 1}}</p></div></div>
                <div class="username"><p>{{score.user.userName}}</p></div>
                <div class="score-label"><p class="label__helper">Score</p></div>
                <div class="time-label"><p class="label__helper">{{ $t('TIME') }}</p></div>
                <div class="score"><div class="circle__helper">{{score.value}}</div></div>
                <div class="time"><div class="circle__helper"><p>{{ score.timeSpan }}s</p></div></div>
            </div>
            
    
        </div>
        <div v-else>
            <p class="c-leaderboard__no-scores">{{ $t('NO-SCORES-AVAILABLE') }}</p>
        </div>
       

        <div @click="otherMap()" class="c-button-primary secundary">{{ $t('OTHER-MAP') }}</div>
    </div>
</template>

<style lang="scss">
    @import './src/style/settings/settings.colors.scss';

    @import './src/style/components/components.leaderboard.scss';

    .c-leaderboard__no-scores{
        text-align:center;
    }

    </style>

<script>

  


export default {
    name: 'leaderboard',
    data(){
        return{
            friendsOnly : false,
        }
    },
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
        },
        toggleFriends: function(){
            if(this.friendsOnly){
                this.$store.dispatch('fetchTopScoresByArea', this.$route.params.map);
                this.friendsOnly = false;
            }else{
                this.$store.dispatch('fetchFriendTopScoresByArea', this.$route.params.map);
                this.friendsOnly = true;
            }
        }

    },
    created: function(){
        this.$store.dispatch('fetchTopScoresByArea', this.$route.params.map);
    }
}
</script>

