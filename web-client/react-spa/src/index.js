import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';

import { PublicClientApplication, EventType } from '@azure/msal-browser';
import { MsalProvider } from '@azure/msal-react';
import { msalConfig } from './authConfig';

// Bootstrap components
import 'bootstrap/dist/css/bootstrap.min.css';

export const msalInstance = new PublicClientApplication(msalConfig);

msalInstance.initialize().then(() => {
    // Default to using the first account if no account is active on page load
    if (!msalInstance.getActiveAccount() && msalInstance.getAllAccounts().length > 0) {
        console.log('set active acc ', msalInstance.getAllAccounts()[0]);

      // Account selection logic is app dependent. Adjust as needed for different use cases.
      msalInstance.setActiveAccount(msalInstance.getAllAccounts()[0]);
    }
  
    // Optional - This will update account state if a user signs in from another tab or window
    msalInstance.enableAccountStorageEvents();
  
    msalInstance.addEventCallback((event) => {
      if (event.eventType === EventType.LOGIN_SUCCESS
        ||
        event.eventType === EventType.ACQUIRE_TOKEN_SUCCESS
        ||
        event.eventType === EventType.SSO_SILENT_SUCCESS
      ) {
        const account = event.payload.account;
        msalInstance.setActiveAccount(account);
      }
    });
  
    const container = document.getElementById("root");
    const root = ReactDOM.createRoot(container);
  
/**
 * We recommend wrapping most or all of your components in the MsalProvider component. It's best to render the MsalProvider as close to the root as possible.
 */
    root.render(
        <React.StrictMode>
            <MsalProvider instance={msalInstance}>
                <App />
            </MsalProvider>
        </React.StrictMode>
    );
  });
  
// const root = ReactDOM.createRoot(document.getElementById('root'));
