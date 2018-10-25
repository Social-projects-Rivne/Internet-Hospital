import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from '../../Services/authentication.service';
@Injectable()
export class ModeratorGuard implements CanActivate {

    constructor(private router: Router,private authenticationService: AuthenticationService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this.authenticationService.hasModeratorRole()) {            
            return true;
        }        
        return false;
    }
}