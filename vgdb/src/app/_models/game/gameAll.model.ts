import { Pagination } from "../pagination.model";
import { Game } from "./game.model";

export interface GameAll {
  games: Game[];
  sort: string;
  search: string;
  pagination: Pagination;
}
