import { useParams } from "react-router";
import { useState, useEffect, useRef } from 'react';
import { BASE_URL } from "../../UrlProvider";
import HorizontalScrollingImageList from "../../components/HorizontalScrollingImageList/HorizontalScrollingImageList";
import IImageScrollItem from "../../interfaces/IImageScrollItem";
import './Game.css';
import { Button, Dropdown, DropdownButton, ListGroup } from "react-bootstrap";
import { fetcher } from "../../utils/Fetcher";
import useSWR from "swr";
import { usePostRequest } from "../../utils/usePostRequest";
import { IPlayRecord, IPlayRecord_Create } from "../../interfaces/IPlayRecord";
import { PlayRecordModal } from "../../components/PlayRecordModal/PlayRecordModal";
import { useMsal } from "@azure/msal-react";

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
  const { instance } = useMsal();
  const { data: existingPlayRecord, error: existingPlayRecordError, isLoading: existingPlayRecordIsLoading } = useSWR<IPlayRecord[]>(BASE_URL + '/play-record/?gameId=' + gameId + '&userId=' + instance.getActiveAccount()?.localAccountId, fetcher);
  const [showModal, setShowModal] = useState(false);

  if (!game) { return <div>Loading...</div> }

  const handleOpenPlayRecordModal = () => {
    setShowModal(true);
  };

  const handleClosePlayRecordModal = () => {
    setShowModal(false);
  };

  console.log('in gamer', existingPlayRecord);

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
        <span style={{fontSize: 'xx-large'}}>{game.title}</span>
        
        {existingPlayRecord ?
          <>
            <Button variant="secondary" onClick={handleOpenPlayRecordModal}>
              Edit Play Record
            </Button>  
            <PlayRecordModal
              gameId={game.id}
              show={showModal}
              playRecord={(existingPlayRecord as IPlayRecord[])[0]} 
              onHide={handleClosePlayRecordModal} />
          </>
              :
              <>
              <Button variant="secondary" onClick={handleOpenPlayRecordModal}>
                Add to My List
              </Button>
                  <PlayRecordModal
                    gameId={game.id}
                    show={showModal}
                    onHide={handleClosePlayRecordModal} />
            </>
        }


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
