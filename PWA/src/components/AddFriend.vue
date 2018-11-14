<template>
    <div class="c-addfriend">
        <div class="c-addfriend__close">
            <div class="c-addfriend__close__title">
                Add friend
            </div>
            <div v-on:click.prevent="closePanel" class="c-addfriend__close__button">
                <img src="../assets/cross.png" alt="">
            </div>
        </div>
        <input id="friendToAdd" placeholder="Enter friends username" type="text" autocomplete="off" required/>

        <button type="submit" class="c-button-primary">SEND INVITE</button>
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
    
</style>

<script>
export default {
    name:'addfriend',
    data(){
        return{

        };
    },
    methods:{
        closePanel() {
            this.$emit("closePanel", {});
        }
    }
}
</script>


