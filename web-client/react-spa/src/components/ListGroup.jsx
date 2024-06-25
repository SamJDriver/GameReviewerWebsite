
import { useState } from 'react';

function ListGroup({ items, heading, onSelectItem }) {

  console.log(items);

  // Hook
  const [selectedIndex, setSelectedIndex] = useState(-1);

  return (
    <>
      <div className="list-group-heading">{heading}</div>
      {items.length === 0 && <p>No items found.</p>}

      <ul className="list-group list-group-horizontal" style={{ "padding-bottom": "50px" }}>

        {items.map((item, index) => (
          <li key={item.id} className={selectedIndex === index ? 'gameListItem active flex-fill' : 'gameListItem flex-fill'} onClick={() => { setSelectedIndex(index); }}>
            <div>
                   <img 
                   className='gameCover'
                   src={item.cover[0].imageUrl}
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

export default ListGroup;
