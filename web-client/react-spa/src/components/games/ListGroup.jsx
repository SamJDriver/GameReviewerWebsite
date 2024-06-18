
import { useState } from 'react';

function ListGroup() {
  let items = ["New York", "San Francisco", "Los Angeles", "Chicago", "Dallas"];

  // Hook
  const [selectedIndex, setSelectedIndex] = useState(-1);

  return (
    <>
      <h1>DGC User</h1>
      {items.length === 0 && <p>No items found.</p>}
      <ul className="list-group">
        {items.map((item, index) => (
          <li key={item} className={selectedIndex === index ? 'list-group-item active' : 'list-group-item'} onClick={() => {setSelectedIndex(index)}}>
            {item}
          </li>
        ))}
      </ul>
    </>
  );
}

export default ListGroup;
