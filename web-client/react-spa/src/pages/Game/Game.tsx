import { useParams } from "react-router";
import { useState, useEffect } from 'react';
import { BASE_URL } from "../../UrlProvider";
import HorizontalScrollingImageList from "../../components/HorizontalScrollingImageList/HorizontalScrollingImageList";
import IImageScrollItem from "../../interfaces/IImageScrollItem";
import './Game.css';

const Game = () => {
  const gameId = useParams().gameId;
  const [game, setGame] = useState<any>();

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
      <h1>{game.title}</h1>
      <HorizontalScrollingImageList initialItemList={gameArtworkItems} /> 
    </>
  )
}

export default Game;
