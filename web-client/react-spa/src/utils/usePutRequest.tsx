import React, { useState } from 'react';
import axios, { AxiosError, AxiosResponse } from 'axios';

export const usePutRequest = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<any | null>(null);
  const [data, setData] = useState<any | null>(null);

  const putData = async (url: string, payload: any, headers?: any) => {
    setIsLoading(true);

    try {
      const response: AxiosResponse = await (headers ? axios.put(url, payload, { headers: headers }) : axios.put(url, payload));
      setData(response.data);
    } catch (error: any) {
        setError(error);
    } finally {
      setIsLoading(false);
    }
  };

  return { isLoading, error, data, putData };
};