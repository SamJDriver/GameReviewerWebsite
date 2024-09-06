import { BsSearch } from "react-icons/bs";
import { BsCalendarEvent } from "react-icons/bs";
import { IGenre } from "../../interfaces/IGenre";
import { BASE_URL } from "../../UrlProvider";
import { useEffect, useState } from "react";
import Dropdown from 'react-bootstrap/Dropdown';
import Form from 'react-bootstrap/Form';
import 'react-date-range/dist/styles.css'; // main style file
import 'react-date-range/dist/theme/default.css'; // theme css file
import './GameSearch.css';
import { Calendar } from "react-date-range";

interface IReleaseYearRange {
  startYear: string;
  endYear: string;
}

export const GameSearch = () => {
  const [genres, setGenres] = useState<IGenre[]>();
  const [releaseYearRange, setReleaseYearRange] = useState<IReleaseYearRange | null>(null);
  const [startReleaseYearDatePickerOpen, setStartReleaseYearDatePickerOpen] = useState<boolean>(false);
  const [endReleaseYearDatePickerOpen, setEndReleaseYearDatePickerOpen] = useState<boolean>(false);

  const [selectedGenres, setSelectedGenres] = useState<string[] | null>(null);
  const [selectedMinReleaseDate, setSelectedMinReleaseDate] = useState<Date | null>(null);
  const [selectedMaxReleaseDate, setSelectedMaxReleaseDate] = useState<Date | null>(null);

  useEffect(() => {
      fetch(BASE_URL + '/lookup/genres')
          .then(response => response.json())
          .then(data => setGenres(data));
      fetch(BASE_URL + '/lookup/release-year-range')
          .then(response => response.json())
          .then(data => setReleaseYearRange(data));
  }, []);

  if (!genres || !releaseYearRange) {
    return <div> Loading...</div>
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
    if (!(event.target as Element).closest('.search-filter--date-picker')) {
      const calendarElement = document.getElementById('startReleaseDateCalendar') as HTMLElement | null;
      const clickedElementId = (event.target as Element).id;
      if (calendarElement && clickedElementId !== 'openStartReleaseDateCalendar' && clickedElementId !== 'startReleaseDateCalendar' && clickedElementId !=='startCalendarIcon') {
        setStartReleaseYearDatePickerOpen(false);
      } 
    }
  });

  document.body.addEventListener('click', function(event: MouseEvent) {
    if (!(event.target as Element).closest('.search-filter--date-picker')) {
      const calendarElement = document.getElementById('endReleaseDateCalendar') as HTMLElement | null;
      const clickedElementId = (event.target as Element).id;
      if (calendarElement && clickedElementId !== 'openEndReleaseDateCalendar' && clickedElementId !== 'endReleaseDateCalendar' && clickedElementId !=='endCalendarIcon') {
        setEndReleaseYearDatePickerOpen(false);
      } 
    }
  });


  return (
    <>
        <div className="search-filter--container">
          <div className="input-group mx-4" >
            <div className="input-group-prepend input-group-text search-filter--item-background-color">
              <BsSearch/>
            </div>
            <input className="form-control search-filter--item-background-color" aria-label="game search" />
          </div>

          <div className="input-group mx-4" >
            <div className="input-group-text search-filter--item-background-color">
              <Dropdown>
                <Dropdown.Toggle variant="dark" className="search-filter--genre-toggle"  id="dropdown-basic"> Genres </Dropdown.Toggle>
                <Form>
                  <Dropdown.Menu>
                    {
                      genres.map( (g: IGenre, index: number) => {
                        if (!g) {
                          return <Dropdown.Item key={index}> Loading ... </Dropdown.Item>
                        }
                        return (
                          <Form.Check type='checkbox' key={'genre-check'+index} id={'genre-check-id'+index} className=" search-filter--item-background-color" label={g.name} onClick={() => GenreCheckboxOnClick(g.name)}>
                          </Form.Check>
                        )
                      })
                    }
                  </Dropdown.Menu>
                </Form>
              </Dropdown>
            </div>
            <div id="genreTextInput" className="search-filter--text-box search-filter--item-background-color form-control">
              {selectedGenres?.map( (g: string, i: number) => <span className="search-filter--genre-item mx-1" key={i} >{g}</span> )}
            </div>
          </div>

          <div className="input-group mx-4" >
            <div onClick={() => setStartReleaseYearDatePickerOpen(!startReleaseYearDatePickerOpen)} id="openStartReleaseDateCalendar" className="input-group-prepend input-group-text search-filter--item-background-color">
              <BsCalendarEvent id="startCalendarIcon"/>
            </div>
            <input placeholder="Start Release Date" className="form-control search-filter--item-background-color" aria-label="search start date" />
            <div id="startReleaseDateCalendar" className="input-group-text" style={{backgroundColor: "rgb(26, 28, 30)", borderStyle: "none"}}>
              <Calendar
                minDate={new Date(`${releaseYearRange.startYear}-01-01`)} 
                maxDate={new Date(`${releaseYearRange.endYear}-12-31`)} 
                className={startReleaseYearDatePickerOpen ? "search-filter--date-picker" : "search-filter--date-picker-hide"} 
                onChange={ (event) => console.log(event)} /> 
            </div>
          </div>

          <div className="input-group mx-4">
            <div onClick={() => setEndReleaseYearDatePickerOpen(!endReleaseYearDatePickerOpen)} id="openEndReleaseDateCalendar" className="input-group-prepend input-group-text search-filter--item-background-color">
              <BsCalendarEvent id="endCalendarIcon"/>
            </div>
            <input placeholder="End Release Date" className="form-control search-filter--item-background-color" aria-label="search end date" />
            <div id="endReleaseDateCalendar" className="input-group-text" style={{backgroundColor: "rgb(26, 28, 30)", borderStyle: "none"}}>
              <Calendar
                minDate={new Date(`${releaseYearRange.startYear}-01-01`)} 
                maxDate={new Date(`${releaseYearRange.endYear}-12-31`)} 
                className={endReleaseYearDatePickerOpen ? "search-filter--date-picker" : "search-filter--date-picker-hide"} 
                onChange={ (event) => console.log(event)} /> 
            </div>
          </div>
        </div>
    </>
  )
}

