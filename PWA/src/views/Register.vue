<template>
    <div class="c-register">
        <h1>GeoPoint</h1>
        <div class="c-login__form">
            <form @submit.prevent="userRegister">
                <input v-model="account.login" id="inpUsername" placeholder="Username" type="text"  autocomplete="off" required/>
                <input v-model="account.email" placeholder="E-mail address" tpye="e-mail" autocomplete="off" required/>
                <vue-password v-model="account.password1"
                                    classes="c-login__form__password"
                                    placeholder="Password"
                                    :disableStrength="false"
                    >
                    </vue-password>
                <vue-password v-model="account.password2"
                                    classes="c-login__form__password"
                                    placeholder="Confirm password"
                                    :disableStrength="true"
                    >
                    </vue-password>
                    <p class="c-login__form__error">{{ errorMessage }}</p>
                    <p class="c-login__form__error">{{ errorMessage2 }}</p>
                <button class="c-button-primary">SIGN UP</button>
            </form>
        </div>
        <p class="c-login__signup">Already have an account? <router-link to="/login">Log in</router-link></p>
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

export default {
    name:'register',
    components:{
        VuePassword
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

