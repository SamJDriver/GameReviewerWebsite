
import { useState } from 'react';

function ListGroup({ items, heading, onSelectItem }) {

  console.log(items);

  // Hook
  const [selectedIndex, setSelectedIndex] = useState(-1);

  return (
    <>
      <h1>{heading}</h1>
      {items.length === 0 && <p>No items found.</p>}

      <ul className="list-group list-group-horizontal-xxl" style={{ height: '25em' }}>

        {items.map((item, index) => (
          <li key={item.id} className={selectedIndex === index ? 'gameListItem list-group-item active flex-fill' : 'gameListItem list-group-item flex-fill'} onClick={() => { setSelectedIndex(index); onSelectItem(item); }}>
            {
                   <img 
                   src={item.artwork[0].imageUrl}
                   alt={item.title}
                   />
            }
          </li>
        ))}

      </ul>
    </>
  );
}

export default ListGroup;
