import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Game } from '../../_models/game/game.model';
import { User } from '../../_models/user/user.model';
import { UserListDto } from '../../_models/userListDto.model';
import { VoteDto } from '../../_models/voteDto.model';
import { AuthenticationService } from '../../_services/authentication.service';
import { GameService } from '../../_services/game.service';
import { ToastService } from '../../_services/toast.service';
import { UserListService } from '../../_services/user-list.service';
import { VoteService } from '../../_services/vote.service';

@Component({
  selector: 'app-game-single',
  templateUrl: './game-single.component.html',
  styleUrls: ['./game-single.component.css'],
})

export class GameSingleComponent implements OnInit {
  game?: Game | null = null;
  loading: boolean = true;
  userRating?: string ;
  userListType?: string;
  user: User | null = null;

  constructor(private gameService: GameService, private activatedRoute: ActivatedRoute, private authService: AuthenticationService,
    private voteService: VoteService, private userListService: UserListService, private toastService: ToastService) {
    this.getUser();
  }

  ngOnInit() {
    this.activatedRoute.params
      .subscribe(params => {
        let paramId = params['id'] ?? 0;

        this.gameService.get(paramId).subscribe(result => {
          this.loading = false;
          this.game = result.game;
          this.userRating = result.userRating;
          this.userListType = result.userListType;
        }, error => console.error(error));
      });
  }

  voteChanged(event: any) {
    const voteDto: VoteDto = {
      gameId: this.game?.id ?? 0,
      rating: event.target.value
    };

    this.voteService.vote(voteDto).subscribe(() => {
      this.userRating = event.target.value;
      event.target.blur();
      this.toastService.success('Vote saved.');

    }, error => this.toastService.error(error.error.message));
  }

  listChanged(event: any) {
    const userListDto: UserListDto = {
      gameId: this.game?.id ?? 0,
      listType: event.target.value
    };

    this.userListService.changeList(userListDto).subscribe(() => {
      this.userListType = event.target.value;
      event.target.blur();
      this.toastService.success('List changed.');

    }, error => this.toastService.error(error.error.message));
  }

  private getUser(): void {
    const user = this.authService.getUser();
    this.user = user ? user : null;
  }
}
