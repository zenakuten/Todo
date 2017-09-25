import Vue from 'vue';
import { Component, Inject, Model, Provide } from 'vue-property-decorator';

import { UserModel } from '../../models/models';
import Services from '../../services/services';
import  vm  from '../../boot';


@Component
export default class NavmenuComponent extends Vue {

    authenticated: boolean = false;

    @Inject()
    services: Services;


    created() {
        this.$root.$on('AuthenticationChange', (isAuth:boolean) => { 
            this.authenticated = isAuth 
        });
    }
}