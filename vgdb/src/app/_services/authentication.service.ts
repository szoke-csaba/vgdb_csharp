import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { UserForAuthenticationDto } from 'src/app/_models/user/userForAuthenticationDto.model';
import { AuthResponseDto } from '../_models/user/authResponseDto.model';
import { User } from '../_models/user/user.model';
import { UserForRegistrationDto } from '../_models/user/userForRegistrationDto.model';
import { StorageService } from '../_services/storage.service';
import { EnvironmentUrlService } from './environment-url.service';

const AUTH_URL = 'api/authenticate/';

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {
  private authChangeSub = new Subject<boolean>();
  public authChanged = this.authChangeSub.asObservable();
  public user: User | null = null;
  public token: string = "";
  public admin: boolean = false;

  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService, private storage: StorageService) {
    this.setUserDataFromStorage();
  }

  public registerUser = (body: UserForRegistrationDto) => {
    return this.http.post(this.createCompleteRoute(AUTH_URL + 'register', this.envUrl.urlAddress), body);
  }

  public loginUser = (body: UserForAuthenticationDto) => {
    return this.http.post<AuthResponseDto>(this.createCompleteRoute(AUTH_URL + 'login', this.envUrl.urlAddress), body);
  }

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }

  public isUserAuthenticated(): boolean {
    return this.user ? true : false;
  }

  public getUser(): User | null {
    return this.user;
  }

  public isAdmin(): boolean {
    return this.admin;
  }

  public getToken(): string {
    return this.token;
  }

  public setUserDataFromStorage(): void {
    this.user = this.storage.getUser();
    this.token = this.user ? this.user.token : "";
    this.admin = this.user ? this.user.roles.includes("Admin") : false;
  }

  public logout(): void {
    this.user = null;
    this.token = "";
    this.storage.logOut();
    this.sendAuthStateChangeNotification(false);
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
