import { Injectable, Injector } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpHeaderResponse, HttpSentEvent, HttpProgressEvent, HttpResponse, HttpUserEvent} from '@angular/common/http';
import { Observable, BehaviorSubject, throwError} from 'rxjs';
import { AuthenticationService } from '../Services/authentication.service';
import { catchError, switchMap, filter, take, finalize } from 'rxjs/operators'
import { ICurrentUser } from '../Models/CurrentUser'
@Injectable()
export class InterceptorService implements HttpInterceptor {

    constructor(private authenticationService: AuthenticationService) {}

    // method for interception all requests
    intercept(request: HttpRequest<any>, next: HttpHandler) : Observable<HttpSentEvent | HttpHeaderResponse | HttpProgressEvent | HttpResponse<any> | HttpUserEvent<any> | any> {
        return next.handle(this.addTokenToRequest(request, this.authenticationService.getAuthToken()))
         .pipe(catchError(err => {
            if (err.status === 401) {              
                return this.handle401Error(request, next);               
            }

            // we can show this error in alert services. 
            const error = err.error.message || err.statusText;           
            return throwError(error);
        }))
    }

    //set an access token in a request header
    private addTokenToRequest(request: HttpRequest<any>, token: string) : HttpRequest<any> {
            return request.clone({ setHeaders: { Authorization: `Bearer ${token}`}});
    }

    isRefreshingToken: boolean = false;
    tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);

    // if we get 401 error send a refresh token to renew an access
    private handle401Error(request: HttpRequest<any>, next: HttpHandler) {

            if(!this.isRefreshingToken) {
                this.isRefreshingToken = true;
                this.tokenSubject.next(null);
     
                return this.authenticationService.refreshToken()
                    .pipe(
                        switchMap((user: ICurrentUser) => { 
                                if(user) {
                                    this.tokenSubject.next(user.access_token);;
                                    localStorage.setItem('currentUser', JSON.stringify(user));
                                    return next.handle(this.addTokenToRequest(request, user.access_token));
                                }    
                                return <any>this.authenticationService.logout();
                        }),
                        catchError(err => {
                            return <any>this.authenticationService.logout();
                        }),
                        finalize(() => {
                            this.isRefreshingToken = false;
                        })
                    );
            } else {
                this.isRefreshingToken = false;
                
                return this.tokenSubject
                    .pipe(filter(token => token != null),
                    take(1),
                    switchMap(token => {
                        return next.handle(this.addTokenToRequest(request, token));
                    }));
            }
        }
}