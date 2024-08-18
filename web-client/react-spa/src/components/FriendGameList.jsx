import { useState, useEffect } from 'react';
import { useApi } from '../utils/useApi';
import { useToken } from '../utils/useToken';

const BASE_URL = 'https://localhost:7272/api';

function FriendGameList({ heading }) {
  // Hook
  const [selectedIndex, setSelectedIndex] = useState(-1);
  const [data, setData] = useState(null);
  const { token } = useToken(null);

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
      <div className="list-group-heading">{heading}</div>
      {items.length === 0 && <p>No items found.</p>}

      <ul className="list-group list-group-horizontal" style={{ "paddingBottom": "50px" }}>
        {items.map((item, index) => (
          <li key={item.id} className={'gameListItem'} onClick={() => { setSelectedIndex(index); }}>
            <div>
                   <img 
                   className='gameCover'
                   src={item.coverImageUrl}
                   alt={item.title}
                   height='100%'
                   width='100%'
                   />
                  <p style={{ height: "50%", width: "100%", textAlign: "center" }}>{item.title}</p>
            </div>
          </li>
        ))}
      </ul>
    </>
  );
}

export default FriendGameList;
