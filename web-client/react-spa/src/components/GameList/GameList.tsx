import { Link } from 'react-router-dom';
import IVanillaGame from '../../interfaces/IVanillaGame';
import './GameList.css';
import { Button } from 'react-bootstrap';
import { SlArrowLeft, SlArrowRight } from 'react-icons/sl';
import IFriendPlayRecordGame from '../../interfaces/IFriendPlayRecordGame';

interface IProps {
  items: IVanillaGame[] | IFriendPlayRecordGame[] | null | undefined,
  heading: string,
  leftFunction: () => void,
  rightFunction: () => void
}

function GameList(props: IProps) {
  
  return (
    <>
      <div className='game-list--container'>
        <div className="list-group-heading game-list--header">
          {props.heading}
        </div>

        {(!props.items || props.items.length) === 0 && <p>No items found.</p>}

        <ul className="game-list--grid-container">
            <button onClick={props?.leftFunction} className='game-list--page-button'>
              <SlArrowLeft/>
            </button>
            {props.items!.map((item: any, index: number) =>  (
              <li className='game-list--grid-item' key={index}>
                <Link to={'game/' + (item.id ?? item.gameId)}>
                  <img className='game-list--img' src={item.coverImageUrl} alt={item.title} />
                </Link>
              </li>
            ))}
            <button onClick={props?.rightFunction} className='game-list--page-button'>
              <SlArrowRight/>
            </button>
        </ul>
      </div>
    </>
  );
}

export default GameList;
