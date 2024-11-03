import { useParams } from "react-router";
import { useState, useEffect } from 'react';
import { BASE_URL } from "../../UrlProvider";
import HorizontalScrollingImageList from "../../components/HorizontalScrollingImageList/HorizontalScrollingImageList";
import IImageScrollItem from "../../interfaces/IImageScrollItem";
import './Game.css';
import { Dropdown, DropdownButton, ListGroup } from "react-bootstrap";

interface Company {
  companyId: number,
  companyName: string,
  companyImageFilePath: string,
  developerFlag: boolean,
  publisherFlag: boolean,
  portingFlag: boolean,
  supportingFlag: boolean
}

interface Genre {
  id: number,
  name: string,
  code: number,
  description: string,
}

interface Platform {
  id: number,
  name: string,
  releaseDate: Date,
  imageFilePath: string
}

interface Game_Get_ById {
  artworkUrls: string[],
  childGameIds: number[],
  companies: Company[],
  coverImageUrl: string,
  description: string,
  genres: Genre[],
  id: number,
  parentId: number,
  platforms: Platform[],
  releaseDate: Date,
  title: string
}

const Game = () => {
  const gameId = useParams().gameId;
  const [game, setGame] = useState<Game_Get_ById>();

  useEffect(() => {
      fetch(BASE_URL + '/game/' + gameId)
          .then(response => response.json())
          .then(data => setGame(data));
  }, [gameId]);

  if (!game) { return <div>Loading...</div> }

  let gameArtworkItems: IImageScrollItem[] = game.artworkUrls.map( (imageUrl: string) => { return { imageSourceUrl: imageUrl, focusedItemFlag: false }});

  if (gameArtworkItems)
  {
    gameArtworkItems.splice(0, 0, {imageSourceUrl: game.coverImageUrl, focusedItemFlag: true });
    gameArtworkItems.join();
  }

  return (
    <>
    <div className="game--container">
      <div style={{display: 'inline-flex'}}>
        <span style={{fontSize: 'xx-large'}}>{game.title}&nbsp;&nbsp;</span>
        {/* <div style={{flex: 1, backgroundColor: 'green'}}>test</div> */}
        <DropdownButton title="Add to My List" variant="secondary" style={{marginTop: '8px'}}>
          <Dropdown.Item  className="game--list-status-button-text" eventKey="1">Played</Dropdown.Item>
          <Dropdown.Item  className="game--list-status-button-text" eventKey="2">In Progress</Dropdown.Item>
          <Dropdown.Item  className="game--list-status-button-text" eventKey="3">Want to Play</Dropdown.Item>
        </DropdownButton>
      </div>
      <hr/>      
      <div className="game--cover-image-and-description-container">
        <HorizontalScrollingImageList initialItemList={gameArtworkItems} /> 
      </div>
      <div className="game--text-container">
        <span>{game.description}</span>
        <br/>
        <p><b>Genre(s):&nbsp;</b>{game.genres.map((g: Genre) => g.name).join(', ')}</p>
        <br/>
        <p><b>Platform(s):&nbsp;</b>{game.platforms.map((c: Platform) => c.name).join(', ')}</p>
        <br/>
        <p><b>Companies:&nbsp;</b>{game.companies.map((c: Company) => c.companyName).join(', ')}</p>
      </div>
    </div>
    </>
  )
}

export default Game;
