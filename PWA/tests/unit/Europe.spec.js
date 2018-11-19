import { expect } from 'chai'
import { shallowMount } from '@vue/test-utils'
import Vue from 'vue'
import Europe from '@/components/Europe.vue'

describe('Europe.vue',()=>{
    it('increments count when correct country was clicked',() =>{
        const Constructor = Vue.extend(Europe)
        const vm = new Constructor().$mount()
        vm.incrementScore()
        expect(vm.score).to.equal(1);
    })
})

