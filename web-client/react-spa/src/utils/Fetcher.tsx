import axios from "axios";

export const fetcher = async (url: string, token?: string | undefined | null) => {
    const { data } = 
      token ? 
      await axios.get(url, { headers: { Authorization: "Bearer " + token } })
      :
      await axios.get(url) ;
      
    return data;
  };


