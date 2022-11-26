import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { User } from '../../_models/user/user.model';
import { AuthenticationService } from '../../_services/authentication.service';
import { UserService } from '../../_services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class UserProfileComponent implements OnInit {
  user!: User | null;

  constructor(private authService: AuthenticationService, private activatedRoute: ActivatedRoute, private userService: UserService, private titleService: Title) {}

  ngOnInit(): void {
    this.activatedRoute.params
      .subscribe(params => {
        let paramId = params['id'] ?? 0;

        if (paramId == 0) {
          this.getUser();

          return;
        }

        this.userService.get(paramId).subscribe((result: User) => {
          this.user = result;
          this.setTitle();
        }, error => console.error(error));
      });

    this.setTitle();
  }

  private getUser(): void {
    const user = this.authService.getUser();
    this.user = user ? user : null;
  }

  private setTitle(): void {
    const title = this.user ? `${this.user?.username}'s profile` : 'User not found';

    this.titleService.setTitle(`${title} | vgdb`);
  }
}
