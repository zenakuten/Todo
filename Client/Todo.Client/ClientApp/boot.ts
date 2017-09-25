import './css/site.css';
import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';
import AppComponent from './components/app/app';
import  Services  from './services/services';
import UserServices  from './services/userservices';
import { UserModel } from './models/models';
import './filters/datetimefilter';
import 'awesome-bootstrap-checkbox';

Vue.use(VueRouter);

var services = new Services();

var vm : Vue;
var initRoute: VueRouter.NavigationGuard = (to, from, next) => {

    if (!to.meta.authenticated) {
        next();
        return;
    }
    
    services.UserServices.Read(localStorage.UserId)
        .then(data => {
            services.UserServices.CurrentUser = data;
            if (services.UserServices.CurrentUser != null && services.UserServices.CurrentUser.id > 0) {
                vm.$root.$emit('AuthenticationChange', true);
                next()
            } else {
                next({ path: '/login' });
            }
        })
        .catch(error => {
            delete localStorage.UserId;
            next({ path: '/login' });
        });
};

const routes = [
    { path: '/', component: require('./components/home/home.vue.html'), beforeEnter: initRoute, meta: { authenticated: true } },
    { path: '/todolist', component: require('./components/todolist/todolist.vue.html'), beforeEnter: initRoute, meta: { authenticated: true } },
    { path: '/listdetail/:id', component: require('./components/listdetail/listdetail.vue.html'), beforeEnter: initRoute, meta: { authenticated: true } },
    { path: '/login', component: require('./components/login/login.vue.html'), beforeEnter: initRoute },
    { path: '/logout', component: require('./components/logout/logout.vue.html'), beforeEnter: initRoute },
    { path: '/register', component: require('./components/register/register.vue.html'), beforeEnter: initRoute }
];

vm = new Vue({
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h(require('./components/app/app.vue.html')),    
});




