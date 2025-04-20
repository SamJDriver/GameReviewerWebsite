import React, { useState } from 'react';
import axios, { AxiosError, AxiosResponse } from 'axios';

export const useDeleteRequest = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<any | null>(null);
  const [data, setData] = useState<any | null>(null);

  const deleteData = async (url: string, headers?: any) => {
    setIsLoading(true);

    try {
      const response: AxiosResponse = await (headers ? axios.delete(url, { headers: headers }) : axios.delete(url));
      setData(response.data);
    } catch (error: any) {
        setError(error);
    } finally {
      setIsLoading(false);
    }
  };

  return { isLoading, error, data, deleteData };
};