import { useEffect, useState } from "react";
import {  useIsAuthenticated, useMsal } from '@azure/msal-react';
import { loginRequest } from '../authConfig';
import { InteractionRequiredAuthError } from "@azure/msal-browser";

export function useToken() {
  const { instance, accounts } = useMsal();
  const [token, setToken] = useState<string | null>(null);

  const currentAccount = accounts[0];

  useEffect(() => {
      const accessTokenRequest = {
          ...loginRequest,
          account: currentAccount,
        };
        
        
          // instance.acquireTokenSilent(accessTokenRequest).then((response) => {
          //   setToken(response.accessToken);
          // });

          instance.acquireTokenSilent(accessTokenRequest)
          .then(function (accessTokenResponse) {
            // Acquire token silent success
            let accessToken = accessTokenResponse.accessToken;
            setToken(accessToken);
          })
          .catch(function (error) {
            //Acquire token silent failure, and send an interactive request
            // console.log(error);
            if (error instanceof InteractionRequiredAuthError) {
              instance.logoutRedirect();
              // instance.acquireTokenRedirect(accessTokenRequest);
            }
          });
  }, []);

  return { token: token };
}
