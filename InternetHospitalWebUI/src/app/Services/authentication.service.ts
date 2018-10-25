import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable,BehaviorSubject } from 'rxjs';
import { Router} from '@angular/router';
import { ICurrentUser } from '../Models/CurrentUser'
import { HOST_URL } from '../../app/config';
import { JwtHelperService } from '@auth0/angular-jwt';

const PATIENT:string = 'Patient';
const DOCTOR:string = 'Doctor';
const MODERATOR:string = 'Moderator';
const ADMIN:string = 'Admin';

@Injectable()
export class  AuthenticationService  {

    constructor(private http: HttpClient, private router: Router) {   }

    url = HOST_URL;
    jwtHelper = new JwtHelperService();   

    //get an access token
    login(username: string, password: string):Observable<ICurrentUser> {
        return this.http.post<ICurrentUser>(this.url+`/api/Signin`, { username: username, password: password })
            .pipe(map(user => {
                if (user && user.access_token) {
                    //save tokens into local storage
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    //set flag that a user is logged in
                    this.isLoginSubject.next(true);
                    //set user role depending on the token claims
                    this.setUserRole()                   
                }
                return user;
            }));
    }

    private setUserRole()
    {
        if(this.hasPatientRole()){
            this.isPatientSubject.next(true);   
        }
        else if(this.hasDoctorRole()){
            this.isDoctorSubject.next(true);   
        }
        else if(this.hasModeratorRole())
        {
            this.isModeratorSubject.next(true);
        }
        else if(this.hasAdminRole()){
            this.isAdminSubject.next(true);
        }
        else{
            this.logout();
        }
    }

    //check if user is logged
    private isLoginSubject = new BehaviorSubject<boolean>(this.hasAccessToken());
    isLoggedIn() : Observable<boolean> {
        return this.isLoginSubject.asObservable();
    }
    hasAccessToken():boolean{
        if (localStorage.getItem('currentUser')) {           
            return true;
        }
        return false;
    }

    //check if user is a patient
    private isPatientSubject =new BehaviorSubject<boolean>(this.hasPatientRole());
    isPatient():Observable<boolean>{
        return this.isPatientSubject.asObservable();
    }
    hasPatientRole():boolean{
        if (this.getUserRole()===PATIENT) {           
            return true;
        }
        return false;
    }

    //check if user is a doctor
    private isDoctorSubject =new BehaviorSubject<boolean>(this.hasDoctorRole());
    isDoctor():Observable<boolean>{
        return this.isDoctorSubject.asObservable();
    }
    hasDoctorRole():boolean{
        if (this.getUserRole()===DOCTOR) {           
            return true;
        }
        return false;
    }

    //check if user is a moderator
    private isModeratorSubject =new BehaviorSubject<boolean>(this.hasModeratorRole());
    isModerator():Observable<boolean>{
        return this.isModeratorSubject.asObservable();
    }
    hasModeratorRole():boolean{
        if (this.getUserRole()===MODERATOR) {           
            return true;
        }
        return false;
    } 

    //check if user is an admin
    private isAdminSubject =new BehaviorSubject<boolean>(this.hasAdminRole());
    isAdmin():Observable<boolean>{
        return this.isAdminSubject.asObservable();
    }
    hasAdminRole():boolean{
        if (this.getUserRole()===ADMIN) {           
            return true;
        }
        return false;
    }

    //return if user is approved doctor
    isApprovedPatient():boolean{
        if (localStorage.getItem('currentUser')){
            var tokenPayload = this.jwtHelper.decodeToken(JSON.parse(localStorage.getItem('currentUser')).access_token);
            if(tokenPayload["ApprovedPatient"]!==undefined)
                return true;
        }
        return false; 
    }

    //return if user is approved doctor
    isApprovedDoctor():boolean{
        if (localStorage.getItem('currentUser')){
            var tokenPayload = this.jwtHelper.decodeToken(JSON.parse(localStorage.getItem('currentUser')).access_token);
            if(tokenPayload["ApprovedDoctor"]!==undefined)
                return true;
        }
        return false;
    }

    //return user role
    private getUserRole():string{
        if (localStorage.getItem('currentUser')){
            var tokenPayload = this.jwtHelper.decodeToken(JSON.parse(localStorage.getItem('currentUser')).access_token);
            return tokenPayload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];  
        }
    }

    //refresh access token 
    refreshToken() : Observable<ICurrentUser> {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        let token = currentUser.refresh_token;          
        return this.http.post<ICurrentUser>(this.url+"/api/Signin/refresh",{ RefreshToken: token } )
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

    // log out user
    logout() {
        //clear localStorage
        localStorage.removeItem('currentUser');
        //set all flags about user status to false
        this.removeAllAuthorizeFlags();
        //redirect to sign in page     
        this.router.navigate(['sign-in']);
    }
    private removeAllAuthorizeFlags()
    {
        this.isLoginSubject.next(false); 
        this.isPatientSubject.next(false);
        this.isDoctorSubject.next(false);
        this.isModeratorSubject.next(false);
        this.isAdminSubject.next(false);
    }
}