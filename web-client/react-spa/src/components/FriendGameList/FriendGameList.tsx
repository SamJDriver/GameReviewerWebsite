import { useState, useEffect } from 'react';
import { useToken } from '../../utils/useToken';
import { format } from 'date-fns'
import { Link } from 'react-router-dom';
import { BASE_URL } from '../../UrlProvider';

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
    return <p>Loading...</p>;
  }

  const items = data.items;
  
  return (
    <>
      <div className="list-group-heading game-list--header">{heading}</div>
      {items.length === 0 && <p>No items found.</p>}

      <ul className="list-group list-group-horizontal" style={{ "paddingBottom": "50px" }}>
        {items.map((item: any) => (
          <li key={item.playRecordId} className={'game-list--item'}>
            <div>
              <Link to={'game/' + item.gameId}>
                <img 
                   className='game-list--cover'
                   src={item.coverImageUrl}
                   alt={item.title}
                   height='100%'
                   width='100%'
                />
              </Link>
                            
              <span className="game-list--main-text"> {item.title} </span>
              <br/>
              <p className="game-list--supplemental-text"> {item.reviewerName} • {item.rating}% <br/> {format(item.reviewDate, 'MM-dd-yyyy')} </p>

            </div>
          </li>
        ))}
      </ul>
    </>
  );
}

export default FriendGameList;
