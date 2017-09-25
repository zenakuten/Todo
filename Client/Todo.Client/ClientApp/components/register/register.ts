import Vue from 'vue';
import { Component, Inject, Model } from 'vue-property-decorator';

import { UserModel } from '../../models/models';
import Services from '../../services/services';
var $: any = require('jquery');

@Component
export default class RegisterComponent extends Vue {
    username: string = '';
    password: string = '';

    @Inject()
    services: Services;

    mounted() {
        $('#username').focus();
    }

    register() {
        var model = new UserModel();
        model.username = this.username;
        this.services.UserServices.Create(model)
            .then(data => {
                return data.data as UserModel
            })
            .then(data => {
                this.services.toastr.success('Registration successful');
                this.$router.replace('/');
            }).catch(err => 
                this.services.toastr.error('Username ' + this.username + ' is aleady registered!')            
            );
    }
}
