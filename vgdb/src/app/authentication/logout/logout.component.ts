import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../_services/authentication.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})

export class LogoutComponent {
  constructor(private authService: AuthenticationService, private router: Router) {
    this.authService.logout();
    this.router.navigate(["/"]);
  }
}
