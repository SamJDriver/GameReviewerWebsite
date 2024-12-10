import axios from "axios";

export const fetcher = async (url: string, token?: string | undefined | null) => {
    const { data } = await axios.get(url) ; 
    return data;
  };

export const fetcherToken = async (url: string, token?: string | undefined | unknown |null) => {
  if (!token) {
    return null;
  }
  
  const { data } = await axios.get(url, { headers: { Authorization: "Bearer " + token } });    
  return data;
};


