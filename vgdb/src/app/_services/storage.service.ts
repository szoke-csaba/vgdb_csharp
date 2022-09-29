import { Injectable } from '@angular/core';
import { User } from '../_models/user/user.model';

const USER_DATA_KEY = 'userData';

@Injectable({
  providedIn: 'root'
})

export class StorageService {
  constructor() { }

  public saveUserData(data: any): void {
    const item = {
      user: data.user,
      expiry: new Date(data.expiration),
    }

    localStorage.setItem(USER_DATA_KEY, JSON.stringify(item));
  }

  public getUser(): User | null {
    const userDataStr = localStorage.getItem(USER_DATA_KEY) ?? '';

    if (!userDataStr) {
      return null;
    }

    const userData = JSON.parse(userDataStr);

    if (new Date() > new Date(userData.expiry)) {
      localStorage.removeItem(USER_DATA_KEY);

      return null;
    }

    return new User(userData.user.id, userData.user.token, userData.user.username, userData.user.email, userData.user.roles);
  }

  public logOut(): void {
    localStorage.removeItem(USER_DATA_KEY);
  }
}
