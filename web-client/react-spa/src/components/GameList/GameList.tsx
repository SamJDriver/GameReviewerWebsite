import { Link } from 'react-router-dom';
import IVanillaGame from '../../interfaces/IVanillaGame';

interface IProps {
  items: IVanillaGame[],
  heading: string
}

function GameList(props: IProps) {
  const items = props.items;
  const heading = props.heading;

  return (
    <>
      <div className="list-group-heading game-list--header">{heading}</div>
      {items.length === 0 && <p>No items found.</p>}

      <ul className="list-group list-group-horizontal" style={{ "paddingBottom": "50px" }}>

        {items.map((item: any, index: number) => (
          <li key={index} className={'game-list--item'}>
            <div>
              <Link to={'game/' + item.id}>
                   <img 
                   className='game-list--cover'
                   src={item.coverImageUrl}
                   alt={item.title}
                   height='100%'
                   width='100%'
                   />
              </Link>
                  <span className="game-list--main-text"> {item.title} </span>
            </div>
          </li>
        ))}

      </ul>
    </>
  );
}

export default GameList;
