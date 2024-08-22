import { useState, useEffect } from 'react';
import { useApi } from '../utils/useApi';
import { useToken } from '../utils/useToken';
import { format } from 'date-fns'
import { Link, useNavigate } from 'react-router-dom';

const BASE_URL = 'https://localhost:7272/api';

function FriendGameList({ heading }) {
  // Hook
  const [selectedPlayRecordId, setSelectedPlayRecordId] = useState(-1);
  const [data, setData] = useState(null);
  const { token } = useToken();
  const navigate = useNavigate();

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

  const items = data.data;
  return (
    <>
      <div className="list-group-heading gameListHeader">{heading}</div>
      {items.length === 0 && <p>No items found.</p>}

      <ul className="list-group list-group-horizontal" style={{ "paddingBottom": "50px" }}>
        {items.map((item) => (
          <li key={item.playRecordId} className={'gameListItem'} onClick={() => { setSelectedPlayRecordId(item.playRecordId) }}>
            <div>
              <Link to={'game/' + item.gameId}>
                <img 
                   className='gameCover'
                   src={item.coverImageUrl}
                   alt={item.title}
                   height='100%'
                   width='100%'
                />
              </Link>


                            
              <span className="gameListMainText"> {item.title} </span>
              <br/>
              <p className="gameListSupplementalText"> {item.reviewerName} {item.rating}% <br/> {format(item.reviewDate, 'MM-dd-yyyy')} </p>
            </div>
          </li>
        ))}
      </ul>
    </>
  );
}

export default FriendGameList;
