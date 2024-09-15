import { BsSearch } from "react-icons/bs";
import { IGenre } from "../../interfaces/IGenre";
import { BASE_URL } from "../../UrlProvider";
import { useEffect, useRef, useState } from "react";
import 'react-date-range/dist/styles.css'; // main style file
import 'react-date-range/dist/theme/default.css'; // theme css file
import './GameSearch.css';
import useSWR from "swr";
import { fetcher } from "../../utils/Fetcher";
import ISearchGame from "../../interfaces/ISearchGame";
import { CheckboxDropdownList } from "../CheckboxDropdownList/CheckboxDropdownList";
import IDropdownItem from "../../interfaces/IDropdownItem";
import { CalenderDatePicker } from "../CalendarDatePicker/CalendarDatePicker";

interface IReleaseYearRange {
  startYearLimit: string;
  endYearLimit: string;
}

export const GameSearch = () => {
  const { data: genres, error: genresError, isLoading: genresIsLoading } = useSWR<IGenre[]>(BASE_URL + '/lookup/genres', fetcher);
  const { data: releaseYearRange, error: releaseYearRangeError, isLoading: releaseYearRangeIsLoading } = useSWR<IReleaseYearRange>(BASE_URL + '/lookup/release-year-range', fetcher);
  const [searchParameters, setSearchParameters] = useState<ISearchGame>({
    searchTerm: null,
    selectedGenreIds: null,
    selectedStartReleaseDate: null,
    selectedEndReleaseDate: null
  });
  const latestSearchParameters = useRef(searchParameters);

  useEffect(() => {
    latestSearchParameters.current = searchParameters;
  }, [searchParameters]);


  useEffect(() => { 
    const timeoutId = setTimeout(() => {
      console.log(latestSearchParameters.current);
    }, 2000);

    return () => {
      clearTimeout(timeoutId);
    };
  },[searchParameters]);

  if (genresError || releaseYearRangeError) {
    return <div>Failed to load + {genresError} {releaseYearRangeError}</div>
  }

  if (genresIsLoading || releaseYearRangeIsLoading) {
    return <div>Loading...</div>
  }

  if (!genres || !releaseYearRange) {
    return <div>Loading...</div>
  }



  return (
    <>
      <div className="game-search--container">
        <div className="input-group mx-4" >
          <div className="input-group-prepend input-group-text game-search--item-background-color">
            <BsSearch/>
          </div>
          <input 
            type="text" 
            onChange={(event) => setSearchParameters({...searchParameters, searchTerm: event.target.value})} 
            className="form-control game-search--item-background-color" 
            aria-label="game search" />
        </div>

        <CheckboxDropdownList
          onItemSelected={(event) => setSearchParameters({...searchParameters, selectedGenreIds: event.map((item: IDropdownItem) => item.id)})} 
          heading="Genres"
          items={genres} />

        <CalenderDatePicker
          onChange={(event) => setSearchParameters({...searchParameters, selectedStartReleaseDate: event})}
          minDate={new Date(`${releaseYearRange?.startYearLimit}-01-01`)}
          maxDate={new Date(`${releaseYearRange?.endYearLimit}-12-31`)}
          className="mx-4"
          placeholder="Start Release Date" />

        <CalenderDatePicker
          onChange={(event) => setSearchParameters({...searchParameters, selectedEndReleaseDate: event})}
          minDate={searchParameters.selectedStartReleaseDate ? searchParameters.selectedStartReleaseDate : new Date(`${releaseYearRange?.startYearLimit}-01-01`)}
          maxDate={new Date(`${releaseYearRange?.endYearLimit}-12-31`)}
          className="mx-4"
          placeholder="End Release Date" />
      </div>
    </>
  )
}

