import Vue from 'vue';
import { Component, Inject, Model, Prop } from 'vue-property-decorator';
import { UserModel, ListModel, ListItemModel } from '../../models/models';
import Services from '../../services/services';
var Datepicker:any = require( 'vuejs-datepicker');
var $:any = require('jquery');


@Component({
    components: {
        datepicker : Datepicker
        }
    })
export default class ListDetailComponent extends Vue {

    list: ListModel = new ListModel();
    user: UserModel = new UserModel();

    @Inject()
    services: Services;

    mounted() {
        this.user = this.services.UserServices.CurrentUser;
        this.services.ListServices.Read(Number(this.$route.params.id))
        .then(list => {
            this.list = list;
            })
        .catch(error => {
            this.services.toastr.error('Something went wrong');
            });

    }

    newTodo() {
        var model:ListItemModel = new ListItemModel();
        model.listId = this.list.id;
        this.services.ListItemServices.Create(this.list.id, model)
        .then(response => response.data as ListItemModel)
        .then(listitem => {
                this.list.items.push(listitem);
                //this.readList(this.list.id);
                setTimeout(function() {
                    $('#todovalue' + listitem.id).focus();
                    }, 50);
                })
        .catch(error => {
            this.services.toastr.error('Something went wrong');
            });
    }

    todoChanged(item : ListItemModel) {
        this.services.ListServices.Update(this.list)
        .catch(error => {
            this.services.toastr.error('Something went wrong');
            });
    }

    toggleComplete(item : ListItemModel) {
        this.services.ListServices.Update(this.list)
        .catch(error => {
            this.services.toastr.error('Something went wrong');
            });
    }

    deleteTodo(item : ListItemModel) {
        this.services.ListItemServices.Delete(this.list.id, item.id)
            .then( result => result.data as boolean)
            .then( deleted => {
                this.list.items.splice(this.list.items.indexOf(item), 1);
                //this.readList(this.list.id);
            })
        .catch(error => {
            this.services.toastr.error('Something went wrong');
            });
    }

    readList(id:number) {
        this.services.ListServices.Read(id)
        .then(list => {
            this.list = list;
            })
        .catch(error => {
            this.services.toastr.error('Something went wrong');
            });
        }

    passedDue(item:ListItemModel) {
        var d = new Date(item.deadline);
        var itemDate = new Date(d.getFullYear(), d.getMonth(), d.getDate());
        var t = new Date();
        var todayDate = new Date(t.getFullYear(),t.getMonth(),t.getDate());
        var retval = itemDate < todayDate;
        return retval && !item.completed;
        }
}
