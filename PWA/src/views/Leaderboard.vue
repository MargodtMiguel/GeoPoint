<template>
    <div class="c-leaderboard">
        <h1>{{ mapRoute }}</h1>
        <div v-if="topScores && topScores.length">
            <p>Top 20 best scores</p>
                    
            <table>
                <thead>
                    <th class="c-leaderboard__placement"></th>
                    <th>Username</th>
                    <th>Score</th>
                    <th>Time</th>
                </thead>
                <tbody v-for="(score, index) in topScores" v-bind:key="score.id">
                    <tr>
                        <td class="c-leaderboard__placement">{{ index + 1}}</td>
                        <td>{{score.user.userName}}</td>
                        <td>{{ score.value }}</td>
                        <td>{{ score.timeSpan }}</td>
                    </tr>

                </tbody>
            </table>
        </div>
        <div v-else>
            <p>There are no scores available for this map</p>
        </div>
       

        <div @click="otherMap()" class="c-button-primary secundary">PICK OTHER MAP</div>
    </div>
</template>

<style lang="scss">
    @import './src/style/settings/settings.colors.scss';

    .c-leaderboard h1{
        color:$alpha-color;
        padding-bottom:20px;
    }

    .c-leaderboard p{
        text-align:center;
        width:100%;
        padding-bottom:20px;
        font-size:1.4em;
    }

    .c-leaderboard table{
        width:70%;
        min-width:370px;
        margin:0 auto;
        font-size:1.2em;
        overflow-x:auto;
        // border: 1px solid white;
    }

    .c-leaderboard th{
        text-align: left;
        font-weight:600;
        padding-bottom:5px;
    }

    .c-leaderboard__placement{
        padding-right:5px;
    }

    .c-leaderboard td{
        font-weight:100;
        height:15px;
    }

    .c-leaderboard .c-button-primary{
        margin-top:50px;
    }
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

