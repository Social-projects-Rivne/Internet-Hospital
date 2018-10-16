import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Router} from '@angular/router';
import { ICurrentUser } from '../Models/CurrentUser'
import { HOST_URL } from '../../app/config';

@Injectable()
export class  AuthenticationService {
    constructor(private http: HttpClient, private router: Router) { }
    url = HOST_URL;
    //get an access token
    login(username: string, password: string):Observable<ICurrentUser> {
        return this.http.post<ICurrentUser>(this.url+`/api/Signin`, { username: username, password: password })
            .pipe(map(user => {
                if (user && user.access_token) {
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }
                return user;
            }));
    }

    //refresh access token 
    refreshToken() : Observable<ICurrentUser> {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        let token = currentUser.refresh_token;
      
     
        return this.http.post<ICurrentUser>(this.url+"/api/Signin/refresh", { refresh: token })
          .pipe(
            map(user => {     
                if (user && user.access_token) {
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }     
              return <ICurrentUser>user;
          }));
    }

    //get a token of logged user
    getAuthToken() : string {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if(currentUser != null) {
            return currentUser.access_token;
        }
        return '';
    }

    //check if user is logged
    isauthorised():boolean{
        if (localStorage.getItem('currentUser')) {
            return true;
        }
        return false;
    }

    // remove user from local storage to log user out
    logout() {
        localStorage.removeItem('currentUser');
    }
}