export default interface IApiResponse<T> {
  data: T | null,
  loading: boolean,
  error: string
}
