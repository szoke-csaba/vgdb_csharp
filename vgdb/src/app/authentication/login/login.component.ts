import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthResponseDto } from '../../_models/user/authResponseDto.model';
import { UserForAuthenticationDto } from '../../_models/user/userForAuthenticationDto.model';
import { AuthenticationService } from '../../_services/authentication.service';
import { StorageService } from '../../_services/storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  errorMessage: string = "";
  showError: boolean = false;
  loading: boolean = false;

  constructor(private authService: AuthenticationService, private router: Router, private storage: StorageService) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      usernameOrEmail: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required])
    })
  }

  validateControl = (controlName: string) => {
    return this.loginForm.get(controlName)?.invalid && this.loginForm.get(controlName)?.touched
  }

  hasError = (controlName: string, errorName: string) => {
    return this.loginForm.get(controlName)?.hasError(errorName)
  }

  loginUser = (loginFormValue: any) => {
    this.loading = true;
    this.showError = false;
    const login = { ...loginFormValue };

    const userForAuth: UserForAuthenticationDto = {
      usernameOrEmail: login.usernameOrEmail,
      password: login.password
    }

    this.authService.loginUser(userForAuth)
      .subscribe({
        next: (res: AuthResponseDto) => {
          this.loading = false;
          this.storage.saveUserData(res);
          this.authService.setUserDataFromStorage();
          this.authService.sendAuthStateChangeNotification(true);
          this.router.navigate(['/']);
        },
        error: (err: HttpErrorResponse) => {
          this.loading = false;
          this.showError = true;
          this.errorMessage = err.message;
        }
      })
  }
}
