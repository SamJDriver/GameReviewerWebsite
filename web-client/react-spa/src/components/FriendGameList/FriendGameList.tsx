import { useState, useEffect } from 'react';
import { useToken } from '../../utils/useToken';
import { format } from 'date-fns'
import { Link } from 'react-router-dom';
import { BASE_URL } from '../../UrlProvider';
import '../GameList/GameList.css';
import '../FriendGameList/FriendGameList.css';
import { Spinner } from 'react-bootstrap';

interface IProps {
  heading: string
}

function FriendGameList(props: IProps) {
  const [data, setData] = useState<any | null>(null);
  const { token } = useToken();
  const heading = props.heading;

  useEffect(() => {
    if (token)
      {
        const headers = { 'Authorization': 'Bearer ' + token };
        fetch(BASE_URL + '/game/friend/0/10', { headers })
            .then(response => response.json())
            .then(data => setData(data));
      }
  }, [token]);

  if (!data) {
    return (
      <Spinner animation="border" role="status">
        <span className="visually-hidden">Loading...</span>
      </Spinner>
    );
  }

  const items = data.items;
  
  return (
    <>
      <div className='game-list--container'>
        <div className="list-group-heading game-list--header">{heading}</div>
        {items.length === 0 && <p>No items found.</p>}

        <ul className="game-list--ul">
          {items.map((item: any) => (
            <li key={item.playRecordId} className="game-list--li">
              <div>
                <Link to={'game/' + item.gameId}>
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
                    <br/>
                    <span className='friend-game-list--supplemental-text'> {item.reviewerName} • {item.rating}% <br/> {format(item.reviewDate, 'MM-dd-yyyy')} </span>
                  </div>
              </div>
            </li>
          ))}
        </ul>
      </div>
    </>
  );
}

export default FriendGameList;
