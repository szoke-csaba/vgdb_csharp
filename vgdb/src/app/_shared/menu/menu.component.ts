import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/user/user.model';
import { AuthenticationService } from '../../_services/authentication.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})

export class MenuComponent implements OnInit {
  user!: User | null;

  constructor(private authService: AuthenticationService) {
    this.getUser();
  }

  ngOnInit(): void {
    this.authService.authChanged.subscribe({
      next: () => {
        this.getUser();
      }
    })
  }

  private getUser(): void {
    const user = this.authService.getUser();
    this.user = user ? user : null;
  }
}
