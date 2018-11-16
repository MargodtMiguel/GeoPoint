<template>
<div>
    <slideout-panel>

    </slideout-panel>

    <div class="c-friends">
        <div class="c-friends__header">
            <div class="c-friends__header__item">
                <h1>Friends</h1>
            </div>
            <div class="c-friends__header__item">
                <img v-on:click.prevent="showPanel" src="../assets/addFriend.png" alt="">
            </div>
        </div>

        <h2>Pending</h2>
        <div class="c-friends__list">
            <div class="c-friends__list__item">
                <p>Username</p>
            </div>
            <div class="c-friends__list__item">
                <img src="../assets/checkmark.png" alt="">
                <img src="../assets/cross.png" alt="">
            </div>
        </div>
        <div class="c-friends__list">
            <div class="c-friends__list__item">
                <p>Username</p>
            </div>
            <div class="c-friends__list__item">
                <img src="../assets/checkmark.png" alt="">
                <img src="../assets/cross.png" alt="">
            </div>
        </div>

        <h2>Friendlist</h2>
        <div class="c-friends__list">
            <div class="c-friends__list__item">
                <p>Username</p>
            </div>
            <div class="c-friends__list__item">
                <img src="../assets/trash.png" alt="">
            </div>
        </div>
        <div class="c-friends__list">
            <div class="c-friends__list__item">
                <p>Username</p>
            </div>
            <div class="c-friends__list__item">
                <img src="../assets/trash.png" alt="">
            </div>
        </div>

        <router-link to="/">    
            <div class="c-button-primary secundary">BACK TO MENU    </div>
        </router-link>
    </div>
</div>
</template>

<style lang="scss">

    .c-friends{
        width:80%;
        margin:auto;
    }
    .c-friends__header{
        display:flex;
        justify-content: space-between;
        padding:40px 0px;
    }

    .c-friends__header__item h1{
        padding:0;
        font-size:3.2em;
    }

    .c-friends__header__item img{
        margin-top:6px;
        width:40px;
        transition: transform .2s ease-in-out;
    }

    .c-friends__header__item img:hover{
        cursor: pointer;
        transform: scale(1.2); 
    }

    .c-friends h2{
        font-size:2em;
        font-weight:600;
        margin:10px 0px;
    }

    .c-friends__list{
        display:flex;
        justify-content: space-between;
    }

    .c-friends__list__item{
        margin:3px 0px;
    }

    .c-friends__list__item img{
        width:24px;
        margin:0px 5px;
        transition: transform .2s ease-in-out;
    }

    .c-friends__list__item img:hover{
        cursor: pointer;
        transform: scale(1.2);
    }

    .c-friends__list__item p{
        font-size:1.3em;
    }

    .c-friends .secundary{
        margin-top:20px;
    }

    @media only screen and (max-width:600px){
    .c-friends{
        width: 95%;
    }
}
</style>

<script>
import { VueSlideoutPanel } from 'vue2-slideout-panel';
import addfriend from '../components/AddFriend';
import { vueSlideoutPanelService } from 'vue2-slideout-panel';

export default {
    name:'friends',
    components:{
        'slideout-panel': VueSlideoutPanel
    },
    methods:{
        showPanel(){
            const panel1Handle = vueSlideoutPanelService.show({
            component : addfriend,
            width: '350px',
            props: {
                //any data you want passed to your component
            }
        })

        }
    }
}
</script>

