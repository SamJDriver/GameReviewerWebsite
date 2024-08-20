
import { useState } from 'react';

function ListGroup({ items, heading, onSelectItem }) {
  // Hook
  const [selectedIndex, setSelectedIndex] = useState(-1);

  return (
    <>
      <div className="list-group-heading gameListHeader">{heading}</div>
      {items.length === 0 && <p>No items found.</p>}

      <ul className="list-group list-group-horizontal" style={{ "paddingBottom": "50px" }}>

        {items.map((item, index) => (
          <li key={item.id} className={'gameListItem'} onClick={() => { setSelectedIndex(index); }}>
            <div>
                   <img 
                   className='gameCover'
                   src={item.cover[0].imageUrl}
                   alt={item.title}
                   height='100%'
                   width='100%'
                   />
                  <span className="gameListMainText"> {item.title} </span>
            </div>
          </li>
        ))}

      </ul>
    </>
  );
}

export default ListGroup;
