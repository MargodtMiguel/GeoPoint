import { expect } from 'chai'
import Vue from 'vue'
import Europe from '@/components/Europe.vue'

describe('Europe.vue',()=>{
    it('score should be 0 at start',() =>{
        const Constructor = Vue.extend(Europe)
        const vm = new Constructor().$mount()
        expect(vm.score).to.equal(0);
    })

    it('increments count when correct country was clicked',() =>{
        const Constructor = Vue.extend(Europe)
        const vm = new Constructor().$mount()
        vm.incrementScore()
        expect(vm.score).to.equal(1);
    })

    it('should be able to return a country to pick',() =>{
        const Constructor = Vue.extend(Europe)
        const vm = new Constructor().$mount()
        var randomCountry = vm.pickRandomCountry({random1:'Country1'})
        expect(randomCountry).to.equal('random1');
    })

    it('should show a correct country from the start',() =>{
        const Constructor = Vue.extend(Europe)
        const vm = new Constructor().$mount()
        expect(vm.corrCountry).not.to.equal('');
    })
})

