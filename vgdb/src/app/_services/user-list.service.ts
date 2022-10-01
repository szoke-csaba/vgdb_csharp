import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UserListDto } from "../_models/userListDto.model";
import { EnvironmentUrlService } from "./environment-url.service";

const API_URL = 'api/userlist/';

@Injectable({
  providedIn: 'root'
})

export class UserListService {
  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService) { }

  public changeList(userList: UserListDto) {
    return this.http.post(this.createCompleteRoute(API_URL, this.envUrl.urlAddress), userList);
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
