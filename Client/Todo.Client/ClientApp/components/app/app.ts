import Vue from 'vue';
import { Component, Provide} from 'vue-property-decorator';

import Services from '../../services/services';
import { UserModel } from '../../models/models';
import Menu from '../navmenu/navmenu';

@Component({
    components: {
        MenuComponent: require('../navmenu/navmenu.vue.html'),
        LoginComponent: require('../login/login.vue.html'),
        RegisterComponent: require('../register/register.vue.html')
    }
})

export default class AppComponent extends Vue {

    @Provide()
    services: Services = new Services();
}
