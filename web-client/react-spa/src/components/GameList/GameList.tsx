import { Link } from 'react-router-dom';
import IVanillaGame from '../../interfaces/IVanillaGame';
import './GameList.css';

interface IProps {
  items: IVanillaGame[] | null | undefined,
  heading: string
}

function GameList(props: IProps) {
  const items = props.items;
  const heading = props.heading;

  return (
    <>
      <div className='game-list--container'>
        <div className="list-group-heading game-list--header">{heading}</div>
        {(!items || items.length) === 0 && <p>No items found.</p>}

        <ul className="game-list--ul">
          {items!.map((item: any, index: number) => (
            <li key={index} className={'game-list--li'}>
              <div>
                <Link to={'game/' + item.id}>
                     <img 
                       className='game-list--image'
                       src={item.coverImageUrl}
                       alt={item.title}
                       height='100%'
                       width='100%'
                     />
                </Link>
                <div className="game-list--main-text-container"> 
                  <span className='game-list--main-text-span'>
                    {item.title}
                  </span>
                </div>
              </div>
            </li>
          ))}
        </ul>
      </div>
    </>
  );
}

export default GameList;
