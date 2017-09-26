import Vue from 'vue';
import { Component, Inject } from 'vue-property-decorator';

import { UserModel, ListModel, ListItemModel } from '../../models/models';
import Services from '../../services/services';


@Component
export default class HomeComponent extends Vue {    
    lists:ListModel[] = [];
    user: UserModel = new UserModel();

    @Inject()
    services: Services;

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

    isComplete(list: ListModel) {
        var retval: boolean  = true;
        if(list.items.length == 0)
            return false;

        for(var listitem of list.items) {
           retval = retval && listitem.completed;
            }
        return retval;
    }

    passedDue(item:ListItemModel) {
        var d = new Date(item.deadline);
        var itemDate = new Date(d.getFullYear(), d.getMonth(), d.getDate());
        var t = new Date();
        var todayDate = new Date(t.getFullYear(),t.getMonth(),t.getDate());
        var retval = itemDate < todayDate;
        return retval && !item.completed;
        }

     toggleComplete(item : ListItemModel) {
        this.services.ListItemServices.Update(item.listId, item)
        .catch(error => {
            this.services.toastr.error('Something went wrong');
            });
    }
}
