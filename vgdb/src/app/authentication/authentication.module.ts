import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AnonymGuard } from './anonym.guard';
import { AuthGuard } from './auth.guard';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { RegisterUserComponent } from './register/register.component';

const childRoutes: Routes = [
  {
    path: 'register',
    component: RegisterUserComponent,
    title: 'Register | vgdb',
    canActivate: [AnonymGuard]
  },
  {
    path: 'login',
    component: LoginComponent,
    title: 'Login | vgdb',
    canActivate: [AnonymGuard]
  },
  {
    path: 'logout',
    component: LogoutComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  declarations: [
    RegisterUserComponent,
    LoginComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild(childRoutes)
  ],
  providers: [AnonymGuard, AuthGuard]
})

export class AuthenticationModule { }
