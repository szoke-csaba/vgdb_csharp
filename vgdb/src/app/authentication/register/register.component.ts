import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PasswordConfirmationValidatorService } from './../../_custom-validators/password-confirmation-validator.service';
import { UserForRegistrationDto } from './../../_models/user/userForRegistrationDto.model';
import { AuthenticationService } from './../../_services/authentication.service';

@Component({
  selector: 'app-register-user',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterUserComponent implements OnInit {
  registerForm!: FormGroup;
  errorMessage: string = "";
  errorMessages!: any[];
  showError: boolean = false;
  loading: boolean = false;

  constructor(private authService: AuthenticationService, private passConfValidator: PasswordConfirmationValidatorService, private router: Router) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      username: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      confirm: new FormControl('', [Validators.required])
    });

    this.registerForm.get('confirm')?.setValidators([
      Validators.required,
      this.passConfValidator.validateConfirmPassword(this.registerForm.get('password') as FormGroup)
    ]);
  }

  public validateControl = (controlName: string) => {
    return this.registerForm.get(controlName)?.invalid && this.registerForm.get(controlName)?.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.registerForm.get(controlName)?.hasError(errorName)
  }

  public registerUser = (registerFormValue: any) => {
    this.loading = true;
    this.showError = false;
    const formValues = { ...registerFormValue };

    const user: UserForRegistrationDto = {
      username: formValues.username,
      email: formValues.email,
      password: formValues.password,
      passwordConfirm: formValues.confirm
    };

    this.authService.registerUser(user)
      .subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(["/authentication/login"]);
        },
        error: (err: any) => {
          this.loading = false;
          this.showError = true;
          this.errorMessage = err.error.message;
          this.errorMessage = typeof err.error.message === 'string' ? err.error.message : "";
          this.errorMessages = typeof err.error.message !== 'string' ? err.error.message : null;
        }
      })
  }
}
