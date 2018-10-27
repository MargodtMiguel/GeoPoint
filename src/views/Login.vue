<template>
    <div class="c-login">
        <h1>GeoPoint</h1>
        <div class="c-login__form">
            <input placeholder="Username" type="text" required/>
            <input placeholder="Password" type="password" required/>
        </div>
        <router-link to="/"><div class="c-button-primary">LOG IN</div></router-link>
        <p class="c-login__signup">Need an account? <router-link to="/register">Sign up</router-link></p>
    </div>
</template>

<style lang="scss">
 @import './src/style/components/components.login.scss';
</style>

<script>
export default {
    name: 'login',
    components: {
       
    }
}
</script>
