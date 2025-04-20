export default interface ISearchGame {
    searchTerm: string | null,
    selectedGenreIds: number[] | null,
    selectedStartReleaseDate: Date | null,
    selectedEndReleaseDate: Date | null
}