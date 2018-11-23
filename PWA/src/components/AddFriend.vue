<template>
    <div class="c-addfriend">
        <div class="c-addfriend__close">
            <div class="c-addfriend__close__title">
                {{ this.aFriend }}
            </div>
            <div v-on:click.prevent="closePanel" class="c-addfriend__close__button">
                <img src="../assets/cross.png" alt="">
            </div>
        </div>
        <input v-model="friendToAdd" id="friendToAdd" v-bind:placeholder="this.eUsername" type="text" autocomplete="off" required/>
        <div v-for="(fr) in foundUsers" v-bind:key="fr">
            <p class="c-selectOption" @click="changeSelected(fr)" :id="fr">{{fr}}</p>
        </div>
        <button @click="sendFriendRequest" type="submit" class="c-button-primary c-button-adjust">{{ this.sInvite }}</button>
    </div>
</template>

<style lang="scss">
 @import './src/style/settings/settings.colors.scss';
    .c-addfriend{
        background:$bg-color;
        height:100vh;
    }

    .c-addfriend__close img{
        width:30px;
        margin:20px;
    }
    .c-addfriend__close{
        width:100%;
        display:flex;
        justify-content: space-between;
    }  
    .c-addfriend__close__button:hover{
        cursor: pointer;
    }
    .c-addfriend input{
        width:85%;
        border:0;
        border-bottom:1px $alpha-color solid;
        background:none;
        color:$alpha-color;
        font-size:1.3em;
        margin-top:40px;
        margin-bottom:40px;
        margin-left:7%;
    }
    .c-addfriend input:focus{
        outline:none;
    }   

    .c-addfriend__close__title{
        margin-left:7%;
        font-size:1.5em;
        font-weight:600;
        line-height:70px;
    }
    .c-button-adjust{
        margin-top: 25px;
    }
    .c-selectOption{
        height:30px;
        line-height: 30px;
        display: block;
        margin:0 auto;
        width: 280px;
        cursor: pointer;
    }
    .c-selected{
        color:$bg-color;
        background: $alpha-color;

    }
</style>

<script>
import store from '../store.js';
import { i18n } from'../plugins/i18n'

export default {
    name:'addfriend',
    data(){
        return{
            friendToAdd: '',
            foundUsers:'',
            selectedUser:'',
            aFriend:i18n.t('ADD-FRIEND'),
            eUsername:i18n.t('ENTER-USERNAME'),
            sInvite:i18n.t('SEND-INVITE'),
        };
    },
    computed:{
        connection(){
            return store.getters.getSignalrConnection;
        },
        curUser(){
                return store.getters.getSignalrCurUser;
        },
        users(){
            return store.getters.getUsersByUsername;
        }
    },
     watch: {
        friendToAdd: {
        handler: function(val) {
            if(val.length >= 3){
                this.searchFriends(val);
            }
            else{
                this.selectedUser = '';
                this.foundUsers = '';
            }
        },
        deep: true
        },         
    },
    methods:{
        closePanel() {
            this.$emit("closePanel", {});
        },
        searchFriends(val){
            store.dispatch('searchUser',val);
           
        },
        sendFriendRequest(){
            var selected = this.selectedUser;
            if(!selected == ''){
                store.commit('sendFriendRequest',this.selectedUser.toString());
                this.$emit("closePanel", {});
            }
            console.log(this.connection);
            // this.connection.invoke("sendFriendRequest",this.selectedUser.toString()).catch(function(err){
            //     console.error(err.toString());
            // });
        },
        changeSelected(fr){
            if(!(this.selectedUser == '')){
                document.querySelector("#"+this.selectedUser).classList.remove('c-selected');
            }          
            this.selectedUser = fr;
            this.friendToAdd = fr;
            document.querySelector("#"+fr).classList.add('c-selected');
            
        }
    },
    created(){
        store.watch((state) => state.foundUsers, (newval, oldval) => {
            this.foundUsers = newval;
        })
    }
    
}
</script>


