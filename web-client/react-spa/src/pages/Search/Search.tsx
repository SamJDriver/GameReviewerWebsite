import { BsSearch } from "react-icons/bs";
import { PageLayout } from "../../components/PageLayout/PageLayout";
import { IGenre } from "../../interfaces/IGenre";
import { BASE_URL } from "../../UrlProvider";
import { useEffect, useState } from "react";
import Dropdown from 'react-bootstrap/Dropdown';
import Form from 'react-bootstrap/Form';
import './Search.css';
import { FormCheckType } from "react-bootstrap/esm/FormCheck";

export const Search = () => {
  const [genres, setGenres] = useState<IGenre[]>();

  useEffect(() => {
      fetch(BASE_URL + '/lookup/genres')
          .then(response => response.json())
          .then(data => setGenres(data));
  }, []);

  if (!genres) { 
    return <div> Loading...</div>
  }

  return (
    <>    
      <PageLayout>    
        <div className="search--filter-container">

          <div className="input-group mb-3 mx-3" >
            <div className="input-group-prepend input-group-text">
              <BsSearch/>
            </div>
            <input type="text" className="form-control" aria-label="game search" />
          </div>


          <Dropdown>
            <Dropdown.Toggle variant="secondary" id="dropdown-basic">
              Genres
            </Dropdown.Toggle>

            <Form>
            <Dropdown.Menu>
              {
                genres.map( (g: IGenre, index: number) => { 
                  if (!g) {
                    return <Dropdown.Item key={index}> Loading ... </Dropdown.Item>
                  }
                  return (<div key={index}>
                           <Form.Check type='checkbox' id={'checkbox'+index} label={g.name} />                    
                         </div>)                  
              })
            }
            </Dropdown.Menu>
                        </Form>

          </Dropdown>

          <div className="input-group mb-3 mx-3" >
            <input type="search" placeholder="Genre" className="form-control" aria-label="genre search" />
          </div>


        </div>
      </PageLayout>
    </>
  )
}                    

