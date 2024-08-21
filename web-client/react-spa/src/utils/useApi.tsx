import { useEffect, useState } from "react";

export function useApi(url){
  const [data, setdata] = useState(null);
  const [loading, setloading] = useState(true);
  const [error, seterror] = useState("");

  useEffect(() => {
    fetch(url)
      .then((res) => res.json())
      .then((data) => {
        setdata(data);
        setloading(false);
        seterror(data.error);
      });
  }, [url]);

  return { data, loading, error };
};