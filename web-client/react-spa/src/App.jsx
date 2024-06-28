import React, { useState, useEffect } from 'react';
import Axios, { AxiosInstance, AxiosResponse } from "axios"; // AxiosRequestConfig

import { PageLayout } from './components/PageLayout';
import { loginRequest, msalConfig } from './authConfig';
import { callMsGraph } from './graph';
import { ProfileData } from './components/ProfileData';

import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from '@azure/msal-react';
import './App.css';
import Button from 'react-bootstrap/Button';

import 'bootstrap/dist/css/bootstrap.css';
import ListGroup from './components/ListGroup';
import Alert from './components/Alert';

/**
 * Renders information about the signed-in user or a button to retrieve data about the user
 */

export const GetToken = () => {
    const { instance, accounts } = useMsal();
    const [state, setState] = useState();

    const currentAccount = accounts[0];

    const accessTokenRequest = {
        ...loginRequest,
        account: currentAccount,
      };
        instance.acquireTokenSilent(accessTokenRequest).then((response) => {
          setState(response);
      });
        
    return state?.accessToken;
}

const ProfileContent = () => {
    const { instance, accounts } = useMsal();
    const [graphData, setGraphData] = useState(null);

    function RequestProfileData() {
        // Silently acquires an access token which is then attached to a request for MS Graph data
        instance
            .acquireTokenSilent({
                ...loginRequest,
                account: accounts[0],
            })
            .then((response) => {
                callMsGraph(response.accessToken).then((response) => setGraphData(response));
            });
    }

    return (
        <>
            <h5 className="profileContent">Welcome {accounts[0].name}</h5>
            {graphData ? (
                <ProfileData graphData={graphData} />
            ) : (
                <Button variant="secondary" onClick={RequestProfileData}>
                    Request Profile
                </Button>
            )}
        </>
    );
};

export const useData = (url) => {
    const [state, setState] = useState();
  
    useEffect(() => {
      const dataFetch = async () => {
        const data = await (await fetch(url)).json();
  
        setState(data);
      };
  
      dataFetch();
    }, [url]);

    return { data: state };
  };

/**
 * If a user is authenticated the ProfileContent component above is rendered. Otherwise a message indicating a user is not authenticated is rendered.
 */
const MainContent = () => {

    // const token = GetToken();

    // const headers = new Headers();
    // headers.append("Authorization", `Bearer ${token}`);

    fetch('https://localhost:7272/api/user', { 
        method: 'get', 
        headers: new Headers({
            'Authorization': 'Bearer '+ ''
        }), 
    });    

    const { data } = useData('https://localhost:7272/api/game/0/10');
    if (!data) return 'loading';

    return (
        <div className="App">
            <AuthenticatedTemplate>
                <ListGroup items={ data.data } heading="Popular" />
                <ListGroup items={ data.data } heading="New Reviews From Friends" />
                <ProfileContent />
            </AuthenticatedTemplate>

            <UnauthenticatedTemplate>
                <Alert>Please sign-in to see your profile information.</Alert>
                <ListGroup items={ data.data } heading="Popular" />
            </UnauthenticatedTemplate>
        </div>
    );
};
export default function App() {

    return (
        <div style={{ backgroundColor: "#2A3440", height: "100vh" }}>
            <PageLayout>
                <MainContent>
                </MainContent>
            </PageLayout>
        </div>
    );
}
