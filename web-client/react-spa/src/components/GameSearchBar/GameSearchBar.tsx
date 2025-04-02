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
import IPaginator from "../../interfaces/IPaginator";
import IVanillaGame from "../../interfaces/IVanillaGame";

interface IReleaseYearRange {
  startYearLimit: string;
  endYearLimit: string;
}

interface IProps {
  onSearchResults?: (result: IPaginator<IVanillaGame> | null) => void;
  onIsCleanChange?: (isCleanFlag: boolean) => void;
  pageIndex?: number;
  pageSize?: number;
}

export const GameSearchBar = (props: IProps) => {
  const [pageIndex, setPageIndex] = useState(props.pageIndex || 0);
  const [pageSize, setPageSize] = useState(props.pageSize || 8);
  const [isCleanFlag, setIsCleanFlag] = useState(true);

  const { data: genres, error: genresError, isLoading: genresIsLoading } = useSWR<IGenre[]>(BASE_URL + '/lookup/genres', fetcher);
  const { data: releaseYearRange, error: releaseYearRangeError, isLoading: releaseYearRangeIsLoading } = useSWR<IReleaseYearRange>(BASE_URL + '/lookup/release-year-range', fetcher);
  const [searchParameters, setSearchParameters] = useState<ISearchGame>({
    searchTerm: null,
    selectedGenreIds: null,
    selectedStartReleaseDate: null,
    selectedEndReleaseDate: null
  });
  const latestSearchParameters = useRef(searchParameters);
  const initialRenderFlag = useRef(true);

  useEffect(() => {
    setPageIndex(props.pageIndex || 0);
  }, [props.pageIndex]);

  useEffect(() => {
    latestSearchParameters.current = searchParameters;
  }, [searchParameters]);


  useEffect(() => {
    if (initialRenderFlag.current) {
      initialRenderFlag.current = false;
      return;
    }

    if (!searchParameters.searchTerm && !searchParameters.selectedGenreIds && !searchParameters.selectedStartReleaseDate && !searchParameters.selectedEndReleaseDate) {
      props.onSearchResults?.(null);
      setIsCleanFlag(true);
      props.onIsCleanChange?.(true);
      return;
    }
    else {
      setIsCleanFlag(false);
      props.onIsCleanChange?.(false);
    }

    const params = new URLSearchParams();
    if (latestSearchParameters.current.searchTerm){
      params.append('searchTerm', latestSearchParameters.current.searchTerm);
    }

    if (latestSearchParameters.current.selectedGenreIds){
      latestSearchParameters.current.selectedGenreIds.forEach((genreId) => params.append('genreIds', genreId.toString()));
    }

    if (latestSearchParameters.current.selectedStartReleaseDate){
      params.append('startReleaseDate', latestSearchParameters.current.selectedStartReleaseDate.toISOString());
    }

    if (latestSearchParameters.current.selectedEndReleaseDate){
      params.append('endReleaseDate', latestSearchParameters.current.selectedEndReleaseDate.toISOString());
    }

    const timeoutId = setTimeout(() => {
      fetch(`${BASE_URL}/game/search/${pageIndex ?? 0}/${pageSize ?? 8}/?${params.toString()}`)
      .then(res => res.json())
      .then((data) => { props.onSearchResults?.(data); });
    }, 1000);

    return () => {
      clearTimeout(timeoutId);
    };
  },[searchParameters, pageIndex]);

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
          onItemSelected={(event) => setSearchParameters({...searchParameters, selectedGenreIds: event?.map((item: IDropdownItem) => item.id) ?? null })} 
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

