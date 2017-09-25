import Vue from 'vue';
import { Component, Inject, Model } from 'vue-property-decorator';
import { UserModel } from '../../models/models';
import Services from '../../services/services';
var $: any = require('jquery');

@Component
export default class LoginComponent extends Vue {
    username: string = '';
    password: string = '';

    @Inject()
    services: Services;

    mounted() {
        $('#username').focus();
    }
       
    login() {
        this.services.UserServices.Login(this.username, this.password)
            .then(data => {
                return data.data as UserModel
            })
            .then(user => {
                this.services.UserServices.CurrentUser = user;
                localStorage.UserId = user.id;
                this.services.toastr.success('Login successful');
                this.$root.$emit('AuthenticationChange', true);
                this.$router.replace('/');
            }).catch(e => {
                this.services.toastr.error('Login failed', 'Error');
            });
    }
}
