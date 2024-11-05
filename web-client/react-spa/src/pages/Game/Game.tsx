import { useParams } from "react-router";
import { useState, useEffect } from 'react';
import { BASE_URL } from "../../UrlProvider";
import HorizontalScrollingImageList from "../../components/HorizontalScrollingImageList/HorizontalScrollingImageList";
import IImageScrollItem from "../../interfaces/IImageScrollItem";
import './Game.css';
import { Dropdown, DropdownButton, ListGroup } from "react-bootstrap";
import { fetcher } from "../../utils/Fetcher";
import useSWR from "swr";
import { usePostRequest } from "../../utils/usePostRequest";
import { IPlayRecord_Create } from "../../interfaces/IPlayRecord";
import { useToken } from "../../utils/useToken";

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

interface IGame_Get_ById {
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
  const { data: game, error: gameError, isLoading: gameIsLoading } = useSWR<IGame_Get_ById>(BASE_URL + '/game/' + gameId, fetcher);
  const { postData, isLoading, error } = usePostRequest();
  const { token } = useToken();

  if (!game) { return <div>Loading...</div> }


  const handleSubmit = async (args: IPlayRecord_Create) => {
    await postData(BASE_URL + '/play-record', args, { 'Authorization': 'Bearer ' + token });
  };


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
          <Dropdown.Item onClick={() => handleSubmit({GameId: game.id, CompletedFlag: true, HoursPlayed: 4, Rating: 98, PlayDescription: 'test'}) } className="game--list-status-button-text" eventKey="1">Played</Dropdown.Item>
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
