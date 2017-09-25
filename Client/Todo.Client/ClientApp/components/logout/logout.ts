import Vue from 'vue';
import { Component, Inject, Model } from 'vue-property-decorator';
import { UserModel } from '../../models/models';
import Services from '../../services/services';


@Component
export default class LogoutComponent extends Vue {

    @Inject()
    services: Services;

    mounted() {
        this.services.UserServices.CurrentUser = new UserModel();
        delete localStorage.UserId;
        this.$root.$emit('AuthenticationChange', false);
        this.$router.replace('/');
    }
}
