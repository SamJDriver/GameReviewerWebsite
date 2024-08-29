import { useEffect, useState } from "react";
import IApiResponse from "../interfaces/IApiResponse";

export function useApi<T>(url: string): IApiResponse<T>{
  const [data, setdata] = useState<T | null>(null);
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
