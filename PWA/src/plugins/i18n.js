import Vue from 'vue';
import VueI18n from 'vue-i18n';

import en from '../assets/translations/en.json'
import nl from '../assets/translations/nl.json'


Vue.use(VueI18n)
let languageFiles = {
    en : en,
    nl : nl
}
export const i18n = new VueI18n({
    locale:'nl',
    fallbackLocale: 'en',
    messages:languageFiles
})