<template>
    <div>
        <MapPick routeVar="/leaderboard/"></MapPick>
        <router-link to="/"><div class="c-button-primary secundary">{{ $t('BACK-TO-MENU') }}</div></router-link>
    </div>
</template>

<style lang="scss">

</style>

<script>
import MapPick from '../components/MapPick';
export default {
    
    name:'Leadermap',
    components:{
        MapPick
    }
}
</script>


