import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Game } from '../_models/game/game.model';
import { Pagination } from '../_models/pagination.model';
import { GameService } from '../_services/game.service';

@Component({
  selector: 'app-games',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
})

export class GameComponent implements OnInit {
  games?: Game[];
  loading: boolean = true;
  sort!: string;
  search: string = '';
  page!: number;
  pagination!: Pagination | null;

  constructor(private gameService: GameService, private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.activatedRoute.queryParams
      .subscribe(params => {
        let paramSearch = params['search'] ?? '';
        let paramSort = params['sort'] ?? '';
        let paramPage = params['page'] ?? 1;

        this.getAllGames(paramSort, paramSearch, paramPage, true);
      });
  }

  selectChanged(sort: string) {
    this.getAllGames(sort, this.search, this.page);
  }

  searchChanged(search: string) {
    this.getAllGames(this.sort, search, this.page);
  }

  pageChanged(page: number) {
    this.getAllGames(this.sort, this.search, page);
  }

  getAllGames(sort: string = "", search: string = "", page: number = 1, init: boolean = false) {
    this.loading = true;

    this.gameService.getAll(sort, search, page).subscribe(result => {
      this.loading = false;
      this.games = result.games;
      this.sort = result.sort == "" ? "recently" : result.sort;
      this.search = result.search;
      this.page = result.pagination.currentPage;
      this.pagination = result.pagination;

      if (!init) {
        this.router.navigate(
          [],
          {
            relativeTo: this.activatedRoute,
            queryParams: { sort: this.sort, search: this.search, page: this.page },
            queryParamsHandling: 'merge',
          });
      }
    }, error => console.error(error));
  }
}
