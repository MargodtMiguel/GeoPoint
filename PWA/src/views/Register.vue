<template>
    <div class="c-register">
        <h1>GeoPoint</h1>
        <div class="c-login__form">
            <form @submit.prevent="userRegister">
                <input v-model="account.login" id="inpUsername" placeholder="Username" type="text"  autocomplete="off" required/>
                <input placeholder="E-mail address" tpye="e-mail"  autocomplete="off"/>
                <input v-model="account.password1" id="inpPassword1" placeholder="Password" type="password"  autocomplete="off" required/>
                <input v-model="account.password2" id="inpPassword2" placeholder="Confirm password" type="password"  autocomplete="off" required/>
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
</style>

<script>
export default {
    name:'register',
    data(){
        return{
            account:{
                login:'',
                password1:'',
                password2:''
            }
        }
    },
    methods:{
        userRegister: function(){
            if(this.account.password1 == this.account.password2){
                this.$store.commit('userRegister', this.account);
            } else{
                console.log("passwords do not match omegalul")
            }
            
        }
    }
}
</script>

