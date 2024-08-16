
import React, { useState } from 'react';

export default function useData(url){
  return (
  const [results, setResults] = useState([]);

  useEffect(() => {
    async function getData() {
      const response = await fetch(url);
      const data = await response.json();
      setResults(data);
    }

    getData();
    console.log("results ", results);
  }, [])
)
}
