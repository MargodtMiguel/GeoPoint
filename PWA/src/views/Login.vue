<template>
    <div class="c-login">
        <h1>GeoPoint</h1>
        <div class="c-login__form">
            <form  @submit.prevent="userLogIn">
                <input v-model="account.login" id="inpUsername" placeholder="Username" type="text" autocomplete="off" required/>
                <input v-model="account.password" id="inpPassword" placeholder="Password" type="password" autocomplete="off" required/>
                <button type="submit" class="c-button-primary">LOG IN</button>
            </form>
        </div>
        <!-- <router-link to="/"> -->

        <!-- </router-link> -->
        <p class="c-login__signup">Need an account? <router-link to="/register">Sign up</router-link></p>
    </div>
</template>

<style lang="scss">
 @import './src/style/components/components.login.scss';
 button{
     display:block;
     margin:0 auto;
 }
</style>

<script>
export default {
    name: 'login',
    components: {
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
