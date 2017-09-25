import UserServices from './userservices'
import ListServices from './listservices'
import ListItemServices from './listitemservices'
var Toastr: any = require('toastr');

export default class Services {

    public apiUrl: string = 'http://localhost:5001/api/';  //todo load from appsettings.json
    public ListServices: ListServices;
    public ListItemServices: ListItemServices;
    public UserServices: UserServices;
    public toastr: any;

    constructor() {
        this.ListServices = new ListServices(this.apiUrl);
        this.ListItemServices = new ListItemServices(this.apiUrl);
        this.UserServices = new UserServices(this.apiUrl);
        this.initToaster();
        this.toastr = Toastr;
    }

    private initToaster() {
        Toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-center",
            "preventDuplicates": true,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "2000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
    }
}

    
