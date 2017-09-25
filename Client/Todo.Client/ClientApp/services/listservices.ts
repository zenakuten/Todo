
import { AxiosInstance } from 'axios';
import Axios, { AxiosResponse } from 'axios';
import { ListModel } from '../models/models';

export default class ListServices {

    private apiUrl: string;
    private axios: AxiosInstance;

    constructor(apiUrl: string) {
        this.apiUrl = apiUrl;
        this.axios = Axios.create({
            baseURL: apiUrl
        });
    }

    Create(list: ListModel) {
        return this.axios.post('lists', list);
    }

    Read(id: number) {
        return this.axios.get('lists/' + id).then(response => response.data);
    }

    Update(list: ListModel) {
        return this.axios.put('lists/' + list.id, list);
    }

    Delete(id: number) {
        return this.axios.delete('lists/' + id);
    }
}
