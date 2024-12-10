import { Link } from 'react-router-dom';
import IVanillaGame from '../../interfaces/IVanillaGame';
import './GameList.css';
import { Button } from 'react-bootstrap';
import { SlArrowLeft, SlArrowRight } from 'react-icons/sl';
import IFriendPlayRecordGame from '../../interfaces/IFriendPlayRecordGame';

interface IProps {
  items: IVanillaGame[] | IFriendPlayRecordGame[] | null | undefined,
  heading: string
}

function GameList(props: IProps) {
  const items = props.items;
  const heading = props.heading;
  
  return (
    <>
      <div className='game-list--container'>
        <div className="list-group-heading game-list--header">
          {heading}
        </div>

        {(!items || items.length) === 0 && <p>No items found.</p>}

        <ul className="game-list--grid-container">
            <button className='game-list--page-button'>
              <SlArrowLeft/>
            </button>
            {items!.map((item: any, index: number) =>  (
              <li className='game-list--grid-item' key={index}>
                <Link to={'game/' + (item.id ?? item.gameId)}>
                  <img className='game-list--img' src={item.coverImageUrl} />
                </Link>
              </li>
            ))}
            <button className='game-list--page-button'>
              <SlArrowRight/>
            </button>
        </ul>
      </div>
    </>
  );
}

export default GameList;
