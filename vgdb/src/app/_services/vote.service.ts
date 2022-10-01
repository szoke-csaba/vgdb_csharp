import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { VoteDto } from "../_models/voteDto.model";
import { EnvironmentUrlService } from "./environment-url.service";

const API_URL = 'api/vote/';

@Injectable({
  providedIn: 'root'
})

export class VoteService {
  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService) { }

  public vote(vote: VoteDto) {
    return this.http.post(this.createCompleteRoute(API_URL, this.envUrl.urlAddress), vote);
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
