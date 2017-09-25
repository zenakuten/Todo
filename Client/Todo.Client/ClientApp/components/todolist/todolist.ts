import Vue from 'vue';
var $:any = require('jquery');

import { Component, Inject } from 'vue-property-decorator';
import Services from '../../services/services';
import { UserModel } from '../../models/models';
import { ListModel } from '../../models/models';

@Component
export default class TodoListComponent extends Vue {
    lists: ListModel[] = [];

    @Inject()
    services: Services;

    user: UserModel = new UserModel();

    mounted() {
        this.user = this.services.UserServices.CurrentUser;
        this.loadLists();

    }

    loadLists() {
        this.services.UserServices.Lists(this.user.id)
            .then(data => data.data as Promise<ListModel[]>)
            .then(data => {
                this.lists = data;
            })
            .catch(error => {
                this.services.toastr.error('Something went wrong');
            });
    }

    newList() {
        var model = new ListModel();
        model.name = '';
        model.userId = this.user.id;
        this.services.ListServices.Create(model)
            .then(result => result.data as Promise<ListModel>)
            .then(data => {
                this.lists.push(data);
                setTimeout(function() {
                $('#list' + data.id).focus();
                    }, 100);
            })
            .catch(error => {
                this.services.toastr.error('Something went wrong');
            });
    }
    saveList(list:ListModel) {
        this.services.ListServices.Update(list)
            .then(result => result.data as Promise<ListModel>)
            .then(data => {
                this.loadLists();
            })
            .catch(error => {
                this.services.toastr.error('Something went wrong');
            });
        }

    deleteList(list:ListModel) {
        this.services.ListServices.Delete(list.id)
            .then( result => result.data as boolean)
            .then( deleted => {
                this.loadLists();
                })
            .catch(error => {
                this.services.toastr.error('Something went wrong');
                });
    }
}
