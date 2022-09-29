export interface Pagination {
  currentPage: number;
  totalPages: number;
  pageSize: number;
  totalCount: number;
  pageItemsFrom: number;
  pageItemsTo: number;
  hasPrevious: boolean;
  hasNext: boolean;
  previousPage: number;
  nextPage: number;
}
