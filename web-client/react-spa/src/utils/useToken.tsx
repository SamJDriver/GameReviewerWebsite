import { useEffect, useState } from "react";
import {  useIsAuthenticated, useMsal } from '@azure/msal-react';
import { loginRequest } from '../authConfig';

export function useToken() {
  const { instance, accounts } = useMsal();
  const [token, setToken] = useState<string | null>(null);

  const currentAccount = accounts[0];

  useEffect(() => {
      const accessTokenRequest = {
          ...loginRequest,
          account: currentAccount,
        };
        
        instance.acquireTokenSilent(accessTokenRequest).then((response) => {
          setToken(response.accessToken);
      });
      
  }, []);

  return { token: token };
}
