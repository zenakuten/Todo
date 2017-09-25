import { AxiosInstance } from 'axios';
import Axios, { AxiosResponse } from 'axios';
import { UserModel } from '../models/models';

export default class UserServices {

    private apiUrl: string;
    private axios: AxiosInstance;

    private static _currentUser: UserModel;
    get CurrentUser(): UserModel
    {
        return UserServices._currentUser;
    }
    set CurrentUser(model: UserModel) {
        UserServices._currentUser = model;
    }


    constructor(apiUrl: string) {
        this.apiUrl = apiUrl;
        this.axios = Axios.create({
            baseURL: apiUrl
        });
    }

    Create(user: UserModel) {
        return this.axios.post('users', user ); 
    }

    Read(id: number) {
        return this.axios.get('users/' + id).then(response => response.data);
    }

    Update(user: UserModel) {
        return this.axios.post('users/' + user.id, user);
    }

    Delete(id: number) {
        return this.axios.delete('users/' + id);
    }

    Login(username:string, password:string) {
        return this.axios.post('users/login', { username: username, password: password });
    }

    Lists(id: number) {
        return this.axios.get('users/' + id + '/lists');
    }
}
