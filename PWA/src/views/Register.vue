<template>
    <div class="c-register">
        <h1>GeoPoint</h1>
        <div class="c-login__form">
            <input placeholder="Username" type="text" required/>
            <input placeholder="E-mail address" tpye="e-mail" required/>
            <input placeholder="Password" type="password" required/>
            <input placeholder="Confirm password" type="password" required/>
        </div>
        <router-link to="/"><div class="c-button-primary">SIGN UP</div></router-link>
        <p class="c-login__signup">Already have an account? <router-link to="/login">Log in</router-link></p>
    </div>
</template>

<style lang="scss">
     @import './src/style/components/components.login.scss';
</style>

<script>
export default {
    name:'register'
}
</script>

