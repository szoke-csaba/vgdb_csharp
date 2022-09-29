import { Game } from "./game.model";
import { VoteStats } from "./voteStats.model";

export interface GameSingle {
  game: Game;
  userRating: string;
  userListType: string;
  voteStats: VoteStats;
}
