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
            <form  @submit.prevent="userLogIn">
                <input v-model="account.login" id="inpUsername" v-bind:placeholder="$t('USERNAME')" type="text" autocomplete="off" required/>
                    <vue-password v-model="account.password"
                                    classes="c-login__form__password"
                                    v-bind:placeholder="$t('PASSWORD')"
                                    :disableStrength="true"
                    >
                    </vue-password>
                    <p class="c-login__form__error">{{ errorMessage }}</p>
                <button type="submit" class="c-button-primary">{{ $t('LOG-IN') }}</button>
            </form>
        </div>

        <p class="c-login__signup">{{ $t('NEED-ACCOUNT') }} <router-link to="/register">{{ $t('SIGN-UP') }}</router-link></p>
    </div>
    </div>
    
</template>

<style lang="scss">
@import "./src/style/components/components.login.scss";
button {
  display: block;
  margin: 0 auto;
  margin-top: 40px;
}

.VuePassword__Input {
  margin-bottom: 20px;
}
</style>

<script>
import VuePassword from "vue-password";
import { VueSlideoutPanel } from 'vue2-slideout-panel';
import changelanguage from '../components/ChangeLanguage';
import { vueSlideoutPanelService } from 'vue2-slideout-panel';

export default {
  name: "login",
  components: {
    VuePassword,
    'slideout-panel': VueSlideoutPanel
  },
  data() {
    return {
      account: {
        login: "",
        password: ""
      },
      isLoggedIn: false
    };
  },
  beforeCreate: function() {
    this.isLoggedIn = this.$store.getters.isLoggedIn;
  },
  computed: {
    errorMessage() {
      return this.$store.getters.getErrorMessage;
    }
  },
  methods: {
    showPanel(){
        const panel1Handle = vueSlideoutPanelService.show({
        component : changelanguage,
        width: '350px',
        })
    },
    userLogIn: function() {
      this.$store.commit("userLogIn", this.account);
    }
  },
  created: function() {
    this.$store.commit("resetValues");
  }
};
</script>
