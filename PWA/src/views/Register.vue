<template>
<div>
    <slideout-panel>
    </slideout-panel>
    <div class="c-login">
        <div class="c-language-icon">
            <div>
                <img v-on:click.prevent="showPanel" src="../assets/translation.png" alt="">
            </div>
        </div>
        <h1>GeoPoint</h1>
        <div class="c-login__form">
            <form @submit.prevent="userRegister">
                <input v-model="account.login" id="inpUsername" v-bind:placeholder="$t('USERNAME')" type="text"  autocomplete="off" required/>
                <input v-model="account.email" v-bind:placeholder="$t('E-MAIL')" tpye="e-mail" autocomplete="off" required/>
                <vue-password v-model="account.password1"
                                    classes="c-login__form__password"
                                    v-bind:placeholder="$t('PASSWORD')"
                                    :disableStrength="false"
                    >
                    </vue-password>
                <vue-password v-model="account.password2"
                                    classes="c-login__form__password"
                                    v-bind:placeholder="$t('CONFIRM-PASSWORD')"
                                    :disableStrength="true"
                    >
                    </vue-password>
                    <p class="c-login__form__error">{{ errorMessage }}</p>
                    <p class="c-login__form__error">{{ errorMessage2 }}</p>
                <button class="c-button-primary">{{ $t('SIGN-UP') }}</button>
            </form>
        </div>
        <p class="c-login__signup">{{ $t('HAVE-ACCOUNT') }} <router-link to="/login">{{ $t('LOG-IN') }}</router-link></p>
    </div>
</div>
    
</template>

<style lang="scss">
     @import './src/style/components/components.login.scss';

     button{
         display:block;
     }

     .VuePassword__Meter{
         margin-bottom:20px;
     }

     .VuePassword__Message{
         margin-bottom:30px;
     }
</style>

<script>
import VuePassword from 'vue-password'
import { VueSlideoutPanel } from 'vue2-slideout-panel';
import changelanguage from '../components/ChangeLanguage';
import { vueSlideoutPanelService } from 'vue2-slideout-panel';

export default {
    name:'register',
    components:{
        VuePassword,
        'slideout-panel': VueSlideoutPanel
    },
    data(){
        return{
            account:{
                login:'',
                password1:'',
                password2:'',
                email:''
            },
            errMess:''
        }
    },
    computed:{
        errorMessage(){
            return this.$store.getters.getErrorMessage
        },
        errorMessage2(){
            return this.errMess
        }
    },
    methods:{
        showPanel(){
            const panel1Handle = vueSlideoutPanelService.show({
            component : changelanguage,
            width: '350px',
            })
        },
        userRegister: function(){
            if(this.account.password1 == this.account.password2){
                this.errMess = ""
                this.$store.commit('resetValues');
                this.$store.commit('userRegister', this.account);
            } else{
                this.$store.commit('resetValues');
                this.errMess = "Passwords do not match"
            }
        }
    },
    created: function(){
        this.$store.commit('resetValues');
    }
}
</script>

