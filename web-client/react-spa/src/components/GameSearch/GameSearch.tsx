import { BsSearch } from "react-icons/bs";
import { BsCalendarEvent } from "react-icons/bs";
import { IGenre } from "../../interfaces/IGenre";
import { BASE_URL } from "../../UrlProvider";
import { useState } from "react";
import Dropdown from 'react-bootstrap/Dropdown';
import Form from 'react-bootstrap/Form';
import 'react-date-range/dist/styles.css'; // main style file
import 'react-date-range/dist/theme/default.css'; // theme css file
import './GameSearch.css';
import { Calendar } from "react-date-range";
import useSWR from "swr";
import { fetcher } from "../../utils/Fetcher";

interface IReleaseYearRange {
  startYearLimit: string;
  endYearLimit: string;
}

export const GameSearch = () => {
  const { data: genres, error: genresError, isLoading: genresIsLoading } = useSWR<IGenre[]>(BASE_URL + '/lookup/genres', fetcher);
  const { data: releaseYearRange, error: releaseYearRangeError, isLoading: releaseYearRangeIsLoading } = useSWR<IReleaseYearRange>(BASE_URL + '/lookup/release-year-range', fetcher);

  const [startReleaseYearDatePickerOpen, setStartReleaseYearDatePickerOpen] = useState<boolean>(false);
  const [endReleaseYearDatePickerOpen, setEndReleaseYearDatePickerOpen] = useState<boolean>(false);

  const [selectedGenres, setSelectedGenres] = useState<string[] | null>(null);
  const [selectedStartReleaseDate, setSelectedStartReleaseDate] = useState<Date | null>(null);
  const [selectedEndReleaseDate, setSelectedEndReleaseDate] = useState<Date | null>(null);

  if (genresError || releaseYearRangeError) {
    return <div>Failed to load + {genresError} {releaseYearRangeError}</div>
  }

  if (genresIsLoading || releaseYearRangeIsLoading) {
    return <div>Loading...</div>
  }

  if (!genres || !releaseYearRange) {
    return <div>Loading...</div>
  }

  const GenreCheckboxOnClick = (genreName: string) => {
    if (!selectedGenres) {
      setSelectedGenres([genreName]);
      return;
    }

    const copy = selectedGenres.slice();
    const index: number = copy?.indexOf(genreName) ?? -1;
    if (index !== -1) {
      copy?.splice(index, 1);
    }
    else {
      copy.push(genreName);
    }
    setSelectedGenres(copy);
  }

  document.body.addEventListener('click', function(event: MouseEvent) {
    if (!(event.target as Element).closest('.game-search--date-picker')) {
      const calendarElement = document.getElementById('startReleaseDateCalendar') as HTMLElement | null;
      const clickedElementId = (event.target as Element).id;
      if (calendarElement && clickedElementId !== 'openStartReleaseDateCalendar' && clickedElementId !== 'startReleaseDateCalendar' && clickedElementId !=='startCalendarIcon') {
        setStartReleaseYearDatePickerOpen(false);
      } 
    }
  });

  document.body.addEventListener('click', function(event: MouseEvent) {
    if (!(event.target as Element).closest('.game-search--date-picker')) {
      const calendarElement = document.getElementById('endReleaseDateCalendar') as HTMLElement | null;
      const clickedElementId = (event.target as Element).id;
      if (calendarElement && clickedElementId !== 'openEndReleaseDateCalendar' && clickedElementId !== 'endReleaseDateCalendar' && clickedElementId !=='endCalendarIcon') {
        setEndReleaseYearDatePickerOpen(false);
      } 
    }
  });

  return (
    <>
        <div className="game-search--container">
          <div className="input-group mx-4" >
            <div className="input-group-prepend input-group-text game-search--item-background-color">
              <BsSearch/>
            </div>
            <input className="form-control game-search--item-background-color" aria-label="game search" />
          </div>

          <div className="input-group mx-4" >
            <div className="input-group-text game-search--item-background-color">
              <Dropdown>
                <Dropdown.Toggle variant="dark" className="game-search--genre-toggle"  id="dropdown-basic"> Genres </Dropdown.Toggle>
                <Form>
                  <Dropdown.Menu>
                    {
                      genres?.map( (g: IGenre, index: number) => {
                        if (!g) {
                          return <Dropdown.Item key={index}> Loading ... </Dropdown.Item>
                        }
                        return (
                          <Form.Check type='checkbox' key={'genre-check'+index} id={'genre-check-id'+index} className=" game-search--item-background-color" label={g.name} onClick={() => GenreCheckboxOnClick(g.name)}>
                          </Form.Check>
                        )
                      })
                    }
                  </Dropdown.Menu>
                </Form>
              </Dropdown>
            </div>
            <div id="genreTextInput" className="game-search--text-box game-search--item-background-color form-control">
              {selectedGenres?.map( (g: string, i: number) => <span className="game-search--genre-item mx-1" key={i} >{g}</span> )}
            </div>
          </div>

          <div className="input-group mx-4" >
            <div onClick={() => setStartReleaseYearDatePickerOpen(!startReleaseYearDatePickerOpen)} id="openStartReleaseDateCalendar" className="input-group-prepend input-group-text game-search--item-background-color">
              <BsCalendarEvent id="startCalendarIcon"/>
            </div>
            <input placeholder={selectedStartReleaseDate ? selectedStartReleaseDate.toLocaleDateString() : "Start Release Date"} className="form-control game-search--item-background-color" aria-label="search start date" />
            <div id="startReleaseDateCalendar" className="input-group-text" style={{backgroundColor: "rgb(26, 28, 30)", borderStyle: "none"}}>
              <Calendar
                minDate={new Date(`${releaseYearRange?.startYearLimit}-01-01`)} 
                maxDate={new Date(`${releaseYearRange?.endYearLimit}-12-31`)} 
                className={startReleaseYearDatePickerOpen ? "game-search--date-picker" : "game-search--date-picker-hide"} 
                onChange={ (event) => { setSelectedStartReleaseDate(event); setStartReleaseYearDatePickerOpen(false);}} /> 
            </div>
          </div>

          <div className="input-group mx-4">
            <div onClick={() => setEndReleaseYearDatePickerOpen(!endReleaseYearDatePickerOpen)} id="openEndReleaseDateCalendar" className="input-group-prepend input-group-text game-search--item-background-color">
              <BsCalendarEvent id="endCalendarIcon"/>
            </div>
            <input placeholder={selectedEndReleaseDate ? selectedEndReleaseDate.toLocaleDateString() : "End Release Date"} className="form-control game-search--item-background-color" aria-label="search end date" />
            <div id="endReleaseDateCalendar" className="input-group-text" style={{backgroundColor: "rgb(26, 28, 30)", borderStyle: "none"}}>
              <Calendar
                minDate={new Date(`${releaseYearRange?.startYearLimit}-01-01`)} 
                maxDate={new Date(`${releaseYearRange?.endYearLimit}-12-31`)} 
                className={endReleaseYearDatePickerOpen ? "game-search--date-picker" : "game-search--date-picker-hide"}
                onChange={ (event) => { setSelectedEndReleaseDate(event); setEndReleaseYearDatePickerOpen(false); }}/> 
            </div>
          </div>
        </div>
    </>
  )
}

