import React, { useState, useEffect } from 'react';
import { Routes, Route, useNavigate } from "react-router-dom";
import Axios, { AxiosInstance, AxiosResponse } from "axios"; // AxiosRequestConfig
import { EventType } from "@azure/msal-browser";

import { PageLayout } from './components/PageLayout';
import { loginRequest, msalConfig, b2cPolicies } from './authConfig';
import { callMsGraph } from './graph';
import { ProfileData } from './components/ProfileData';

import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from '@azure/msal-react';
import './App.css';
import Button from 'react-bootstrap/Button';

import 'bootstrap/dist/css/bootstrap.css';
import ListGroup from './components/ListGroup';
import Alert from './components/Alert';
import { callApi } from './utils/ApiCall';
import { useData } from './utils/useData';

//test

/**
 * Renders information about the signed-in user or a button to retrieve data about the user
 */
const BASE_URL = 'https://localhost:7272/api';

export const GetToken = () => {
    const { instance, accounts } = useMsal();
    const [state, setState] = useState();

    const currentAccount = accounts[0];

    useEffect(() => {
        const accessTokenRequest = {
            ...loginRequest,
            account: currentAccount,
          };

        instance.acquireTokenSilent(accessTokenRequest).then((response) => {
            setState(response);
        });
    }, []);
    
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

// export const useData = (url) => {
//     const [state, setState] = useState();
//   
//     useEffect(() => {
//       const dataFetch = async () => {
//         const data = await (await fetch(url)).json();
//   
//         setState(data);
//       };
//   
//       dataFetch();
//     }, [url]);
//
//     return { data: state };
//   };

/**
 * If a user is authenticated the ProfileContent component above is rendered. Otherwise a message indicating a user is not authenticated is rendered.
 */
const MainContent = () => {
    const [friendGames, setFriendGames] = useData(BASE_URL, 'games/friend/0/10') 
    const [popularGames, setPopularGames] = useData(BASE_URL, 'games/0/10')
     
    // DOCKER
    // const { data } = useData('https://localhost:7272/api/game/0/10');
    // if (!data) return 'loading';

    return (
        <div className="App">
            <AuthenticatedTemplate>
                <ListGroup items={ popularGames } heading="Popular" />
                <ListGroup items={ friendGames } heading="New Reviews From Friends" />
                <ProfileContent />
            </AuthenticatedTemplate>

            <UnauthenticatedTemplate>
                <Alert>Please sign-in to see your profile information.</Alert>
                <ListGroup items={ popularGames } heading="Popular" />
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
