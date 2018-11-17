<template>
    <div class="c-language">
        <div class="c-language__close">
            <div class="c-language__close__title">
                Change language
            </div>
            <div v-on:click.prevent="closePanel" class="c-language__close__button">
                <img src="../assets/cross.png" alt="">
            </div>
        </div>
         <select @change="changeLanguage ($event)" placeholder="Pick a language">
        <option value="" disabled selected>Pick a language</option>
          <option value="en">English</option>
          <option value="nl">Nederlands</option>
          </select> 
    </div>
</template>

<style lang="scss">
 @import './src/style/settings/settings.colors.scss';
 
    .c-language{
        background:$bg-color;
        height:100vh;
    }

    .c-language__close img{
        width:25px;
    }

    .c-language__close{
        width:100%;
        display:flex;
        justify-content: space-between;
    }

    .c-language__close__button{
        margin:20px;
    }
    
    .c-language__close__button:hover{
        cursor: pointer;
    }

    .c-language__close__title{
        margin-left:7%;
        font-size:1.5em;
        font-weight:600;
        line-height:70px;
    }

    .c-language select{
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

    .c-language option{
        background:$bg-color;
        color:$alpha-color;
        border:none;
    }

    .c-language option:hover{
        background:$bg-color-dark;
    }

    .c-language select:focus{
        outline:none;
    }
    
</style>

<script>
import { i18n } from'../plugins/i18n'

export default {
    name:'changelanguage',
    data(){
        return{

        };
    },
    props: {
        
    },
    methods:{
        closePanel() {
            this.$emit("closePanel", {});
        },
        changeLanguage(event){
            i18n.locale = event.target.value;
      }
    }
}
</script>


