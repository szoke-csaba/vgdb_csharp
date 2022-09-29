export interface VoteStats {
  numberOfVotesPerRating: { rating: number, numberOfVotes: number };
  averageRating: number;
  mostVotesForARating: number;
}
