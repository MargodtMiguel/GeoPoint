<template>
<div>
    <slideout-panel>

    </slideout-panel>

    <div class="c-friends">
        <div class="c-friends__header">
            <div class="c-friends__header__item">
                <h1>{{ $t('FRIENDS') }}</h1>
            </div>
            <div class="c-friends__header__item">
                <img v-on:click.prevent="showPanel" src="../assets/addFriend.png" alt="">
            </div>
        </div>
        <div v-if="PendingFriends && PendingFriends.length"> 
            <h2>{{ $t('PENDING') }}</h2>
            <div v-for="friend in PendingFriends" v-bind:key="friend.username">
                <div class="c-friends__list">
                    <div class="c-friends__list__item">
                        <p>{{friend.username}}</p>
                    </div>
                    <div class="c-friends__list__item">
                        <img src="../assets/checkmark.png" alt="" @click="confirmFriendRequest(friend.username)">
                        <img src="../assets/cross.png" alt="" @click="declineFriendRequest(friend.username)">
                    </div>
                </div>
            </div>
        </div>

        

        <h2>{{ $t('FRIENDLIST') }}</h2>
        <div  v-if="Friends && Friends.length">
            <div v-for="friend in Friends" v-bind:key="friend.username">
                <div class="c-friends__list">
                    <div class="c-friends__list__item">
                        <p>{{friend.username}}</p>
                    </div>
                    <div class="c-friends__list__item">
                        <img src="../assets/trash.png" alt="" @click="deleteFriend(friend.username)">
                    </div>
                </div>
            </div>
        </div>
        
        <div v-else>
            <p>{{ $t('NO-FRIENDS') }}</p>
        </div>

        <router-link to="/">    
            <div class="c-button-primary secundary">{{ $t('BACK-TO-MENU') }}</div>
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
    computed:{
        PendingFriends(){
            return this.$store.getters.getPendingFriends
        },
        Friends(){
            return this.$store.getters.getFriends
        }
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
        },
        confirmFriendRequest(fun){
            this.$store.commit('confirmFriendRequest', fun);
            setTimeout(() => this.$store.dispatch('fetchFriends'), 100);
        },
        declineFriendRequest(fun){
            this.$store.commit('declineFriendRequest', fun);
            setTimeout(() => this.$store.dispatch('fetchFriends'), 100);
        },
        deleteFriend(fun){
            this.$store.commit('deleteFriend', fun);
            setTimeout(() => this.$store.dispatch('fetchFriends'), 100);
        }
    },
    created: function(){
        this.$store.dispatch('fetchFriends');
    }
}
</script>

