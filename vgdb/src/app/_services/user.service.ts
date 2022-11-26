import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { User } from "../_models/user/user.model";
import { EnvironmentUrlService } from "./environment-url.service";

const API_URL = 'api/user/';

@Injectable({
  providedIn: 'root'
})

export class UserService {
  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService) { }

  public get(id: string) {
    return this.http.get<User>(this.createCompleteRoute(API_URL + id, this.envUrl.urlAddress));
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
