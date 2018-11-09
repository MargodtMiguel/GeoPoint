<template>
    <div class="c-login">
        <h1>GeoPoint</h1>
        <div class="c-login__form">
            <form  @submit.prevent="userLogIn">
                <input v-model="account.login" id="inpUsername" placeholder="Username" type="text" autocomplete="off" required/>
                <!-- <input v-model="account.password" id="inpPassword" placeholder="Password" type="password" autocomplete="off" required/> -->
                    <vue-password v-model="account.password"
                                    classes="c-login__form__password"
                                    placeholder="Password"
                    >
                    </vue-password>
                <button type="submit" class="c-button-primary">LOG IN</button>
            </form>
        </div>

        <p class="c-login__signup">Need an account? <router-link to="/register">Sign up</router-link></p>
    </div>
</template>

<style lang="scss">
 @import './src/style/components/components.login.scss';
 button{
     display:block;
     margin:0 auto;
 }
 .c-login__togglePassword{
     float:right;
 }
</style>

<script>
import VuePassword from 'vue-password'

export default {
    name: 'login',
    components: {
        VuePassword
    },
    data(){
        return{
            account:{
                login:'',
                password:''
            }
        }
    },
    methods:{
        userLogIn: function(){
            this.$store.commit('userLogIn', this.account);
            
        }
    }
}
</script>
