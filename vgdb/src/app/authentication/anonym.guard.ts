import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthenticationService } from '../_services/authentication.service';

@Injectable({
  providedIn: 'root'
})

export class AnonymGuard implements CanActivate {
  constructor(private authService: AuthenticationService, private router: Router) { };

  canActivate(): boolean {
    if (this.authService.isUserAuthenticated()) {
      this.router.navigate(['/']);
    }

    return true;
  }
}
