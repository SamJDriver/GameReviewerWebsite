import React from "react";
import { useParams } from "react-router";
import { useState, useEffect } from 'react';
import { BASE_URL } from "../UrlProvider";

const Game = () => {
  const gameId = useParams().gameId;
  const [game, setGame] = useState<any>();

  useEffect(() => {
      fetch(BASE_URL + '/game/' + gameId)
          .then(response => response.json())
          .then(data => setGame(data));
  }, [gameId]);

  if (!game) { return <div>Loading...</div> }

  console.log(game);

  return (
    <>
      <div>
        <h1>{game.title}</h1>
      </div>

      <div className="gamePageBanner">
        <img 
          className='gamePageCoverImage'
          src={game.coverImageUrl}
          alt={game.title}
        />

        <ul className="list-group list-group-horizontal" >
          {game.artworkUrls.map((artworkUrl: any, index: number) => 

          (
            <li key={index}>
              <div>
                  <img 
                     className='gamePageSupplementalArtwork'
                     src={artworkUrl}
                     alt={'img ' + index}
                  />
             </div>
            </li>
          )

          )}
        </ul>
      </div>
  </>

 
  )
}

export default Game;
