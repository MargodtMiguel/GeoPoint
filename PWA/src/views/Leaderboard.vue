<template>
    <div class="c-leaderboard">
        <h1>{{ mapRoute }}</h1>
        <p>Top 20 best scores</p>
        <p></p>
        <table>
            <tr>
                <th class="c-leaderboard__placement"></th>
                <th>Username</th>
                <th>Score</th>
                <th>Time</th>
            </tr>
            <tr>
                <td class="c-leaderboard__placement">1</td>
                <td>Miguel</td>
                <td>44</td>
                <td>78.845</td>
            </tr>
            <tr>
                <td class="c-leaderboard__placement">2</td>
                <td>RuunTheBruuneBeer</td>
                <td>2</td>
                <td>412.237</td>
            </tr>
            <tr>
                <td class="c-leaderboard__placement">3</td>
                <td>BramAkaBrokke</td>
                <td>15</td>
                <td>12.47</td>
            </tr>
        </table>

        <router-link to="/"><div class="c-button-primary secundary">BACK TO MENU</div></router-link>
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
      }
    }
}
</script>

