import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from '../../Services/authentication.service';
@Injectable()
export class PatientGuard implements CanActivate {

    constructor(private router: Router,private authenticationService: AuthenticationService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        
        if (this.authenticationService.hasPatientRole()) {  
            if(this.authenticationService.isApprovedPatient())
                return true;            
        }        
        return false;
    }
}