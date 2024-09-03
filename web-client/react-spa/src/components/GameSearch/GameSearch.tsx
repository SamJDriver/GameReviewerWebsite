import { BsSearch } from "react-icons/bs";
import { IGenre } from "../../interfaces/IGenre";
import { BASE_URL } from "../../UrlProvider";
import { useEffect, useState } from "react";
import Dropdown from 'react-bootstrap/Dropdown';
import Form from 'react-bootstrap/Form';
import './GameSearch.css';

export const GameSearch = () => {
  const [genres, setGenres] = useState<IGenre[]>();
  const [selectedGenres, setSelectedGenres] = useState<string[] | null>(null);
  const [releaseYears, setReleaseYears] = useState<number[] | null>(null);


  useEffect(() => {
      fetch(BASE_URL + '/lookup/genres')
          .then(response => response.json())
          .then(data => setGenres(data));
  }, []);

    useEffect(() => {
      fetch(BASE_URL + '/lookup/release-years')
          .then(response => response.json())
          .then(data => setReleaseYears(data));
  }, []);


  if (!genres || !releaseYears) { 
    return <div> Loading...</div>
  }

  const GenreCheckboxOnClick = (genreName: string) => {
    if (!genreName) {
      console.log('reeererere');
      return;
    }

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

  
  return (
    <>    
        <div className="search-filter--container">
          <div className="search-filter--text-box-container input-group text-secondary mx-4" >
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

          <div className="search-filter--text-box-container input-group mx-4" >
            <div className="input-group-text search-filter--item-background-color">
              <Dropdown>
                <Dropdown.Toggle variant="dark" className="search-filter--genre-toggle"  id="dropdown-basic"> Release Year </Dropdown.Toggle>
                <Form>
                  <Dropdown.Menu>
                  {
                    releaseYears.map( (releaseYear: number, index: number) => { 
                      if (!releaseYear) {
                        return <Dropdown.Item key={index}> Loading ... </Dropdown.Item>
                      }
                      return (<div key={index}>
                        <Form.Check type='checkbox' className="search-filter--item-background-color" label={releaseYear} />                    
                        </div>)                  
                    })
                  }
                  </Dropdown.Menu>
                </Form>
              </Dropdown>
            </div>
            <div id="genreTextInput" className="search-filter--item-background-color form-control">
            </div>
          </div>
        </div>
    </>
  )
}                    

