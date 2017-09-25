
import { AxiosInstance } from 'axios';
import Axios, { AxiosResponse } from 'axios';
import { ListItemModel } from '../models/models';

export default class ListItemServices {

    private apiUrl: string;
    private axios: AxiosInstance;

    constructor(apiUrl: string) {
        this.apiUrl = apiUrl;
        this.axios = Axios.create({
            baseURL: apiUrl
        });
    }

    Create(listId:number, listitem: ListItemModel) {
        return this.axios.post('lists/' + listId + '/listitems', listitem);
    }

    Read(listId:number, id: number) {
        return this.axios.get('lists/' + listId + '/listitems/' + id).then(response => response.data);
    }

    Update(listId:number, listitem: ListItemModel) {
        return this.axios.put('lists/' + listId + '/listitems/' + listitem.id, listitem);
    }

    Delete(listId:number, id: number) {
        return this.axios.delete('lists/' + listId + '/listitems/' + id);
    }
}
