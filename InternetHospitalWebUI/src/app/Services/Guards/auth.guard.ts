import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { SIGN_IN } from '../../config';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (localStorage.getItem('currentUser')) {            
            return true;
        }        
        this.router.navigate([SIGN_IN], { queryParams: { returnUrl: state.url }});        
        return false;
    }
}