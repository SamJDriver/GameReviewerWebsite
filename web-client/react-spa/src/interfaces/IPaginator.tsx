export default interface IPaginator<T> {
  items: T[],
  pageIndex: number,
  pageSize: number,
  totalRowCount: number
}
