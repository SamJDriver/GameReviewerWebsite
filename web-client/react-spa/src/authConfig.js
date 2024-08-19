/*
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License.
 */

import { LogLevel } from "@azure/msal-browser";


// Browser check variables
// If you support IE, our recommendation is that you sign-in using Redirect APIs
// If you as a developer are testing using Edge InPrivate mode, please add "isEdge" to the if check
const ua = window.navigator.userAgent;
const msie = ua.indexOf("MSIE ");
const msie11 = ua.indexOf("Trident/");
const msedge = ua.indexOf("Edge/");
const firefox = ua.indexOf("Firefox");
const isIE = msie > 0 || msie11 > 0;
const isEdge = msedge > 0;
const isFirefox = firefox > 0; // Only needed if you need to support the redirect flow in Firefox incognito

/**
 * Configuration object to be passed to MSAL instance on creation. 
 * For a full list of MSAL.js configuration parameters, visit:
 * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/configuration.md 
 */

export const b2cPolicies = {
    names: {
        signUpSignIn: "B2C_1_test"
        // editProfile: "B2C_1_ProfileEditPolicy"
    },
    authorities: {
        signUpSignIn: {
            authority: "https://dominiongamingcompany.b2clogin.com/dominiongamingcompany.onmicrosoft.com/B2C_1_test"
        }
        // editProfile: {
        //     authority: "https://msidlabb2c.b2clogin.com/msidlabb2c.onmicrosoft.com/B2C_1_ProfileEditPolicy"
        // }
    },
    authorityDomain: "dominiongamingcompany.b2clogin.com"
}

export const msalConfig = {
    auth: {
        clientId: "037043cf-7754-4522-9d3a-c94f5a2c41d6",
        authority: b2cPolicies.authorities.signUpSignIn.authority,
        knownAuthorities: [b2cPolicies.authorityDomain],

        // local
        redirectUri: "http://localhost:3000/",
        postLogoutRedirectUri: "http://localhost:3000/"

        // docker
        // redirectUri: "http://localhost:3001/",
        // postLogoutRedirectUri: "http://localhost:3001/"
    },
    cache: {
        cacheLocation: "localStorage",
        storeAuthStateInCookie: isIE || isEdge || isFirefox
    },
    system: {	
        loggerOptions: {	
            loggerCallback: (level, message, containsPii) => {	
                if (containsPii) {		
                    return;		
                }		
                switch (level) {
                    case LogLevel.Error:
                        console.error(message);
                        return;
                    case LogLevel.Info:
                        console.info(message);
                        return;
                    case LogLevel.Verbose:
                        console.debug(message);
                        return;
                    case LogLevel.Warning:
                        console.warn(message);
                        return;
                    default:
                        return;
                }	
            }	
        }	
    }
};

/**
 * Scopes you add here will be prompted for user consent during sign-in.
 * By default, MSAL.js will add OIDC scopes (openid, profile, email) to any login request.
 * For more information about OIDC scopes, visit: 
 * https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-permissions-and-consent#openid-connect-scopes
 */
export const loginRequest = {
    scopes: ["https://dominiongamingcompany.onmicrosoft.com/919d8d18-f64a-4a6a-8ee4-91b599eac5e2/gamereview-user"]
};

/**
 * Enter here the coordinates of your web API and scopes for access token request
 * The current application coordinates were pre-registered in a B2C tenant.
 */
export const apiConfig = {
    scopes: ["https://dominiongamingcompany.onmicrosoft.com/919d8d18-f64a-4a6a-8ee4-91b599eac5e2/gamereview-user"],

    // local
    uri: 'http://localhost:3000/api/users'

    //docker
    // uri: 'http://localhost:3001/api/users'
};

/**
 * Add here the scopes to request when obtaining an access token for MS Graph API. For more information, see:
 * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/resources-and-scopes.md
 */
export const graphConfig = {
    graphMeEndpoint: "https://graph.microsoft.com/v1.0/me",
};
