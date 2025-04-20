import axios from "axios";

export const fetcher = async (url: string, token?: string | undefined | null): Promise<any> => {
    const { data } = await axios.get(url) ; 
    return data ? data : null;
  };

export const fetcherToken = async (url: string, token?: string | undefined | unknown |null) => {
  if (!token) {
    return null;
  }
  
  const { data } = await axios.get(url, { headers: { Authorization: "Bearer " + token } });    
  return data;
};


