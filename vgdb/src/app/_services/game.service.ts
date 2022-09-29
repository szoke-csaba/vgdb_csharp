import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { GameAll } from "../_models/game/gameAll.model";
import { GameSingle } from "../_models/game/gameSingle.model";
import { EnvironmentUrlService } from "./environment-url.service";

const MESSAGE_URL = 'api/game/';

@Injectable({
  providedIn: 'root'
})

export class GameService {
  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService) { }

  public getAll(sort: string = "", search: string = "", page: number = 1) {
    let params: HttpParams = new HttpParams();

    if (sort != "") {
      params = params.append('sort', sort);
    }
    if (search != "") {
      params = params.append('search', search);
    }
    if (page != 1) {
      params = params.append('page', page);
    }

    return this.http.get<GameAll>(this.createCompleteRoute(MESSAGE_URL, this.envUrl.urlAddress), { params });
  }

  public get(id: number) {
    return this.http.get<GameSingle>(this.createCompleteRoute(MESSAGE_URL + id, this.envUrl.urlAddress));
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
