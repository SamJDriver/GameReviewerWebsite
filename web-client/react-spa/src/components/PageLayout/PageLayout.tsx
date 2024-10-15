import { PropsWithChildren } from "react";
import Navbar from "react-bootstrap/Navbar";
import { useIsAuthenticated, useMsal } from "@azure/msal-react";
import { SignInButton } from ".././SignInButton";
import { SignOutButton } from ".././SignOutButton";
import './PageLayout.css';
import { NavLink } from "react-router-dom"; 

/**
 * Renders the navbar component with a sign-in or sign-out button depending on whether or not a user is authenticated
 * @param props
 */
export const PageLayout = (props: PropsWithChildren) => {
  const isAuthenticated = useIsAuthenticated();

  return (
    <>
        <Navbar style={{ backgroundColor: "#4C566A", padding: "10px" }}>
            <div className="container-fluid">

                <a className="navbar-brand justify-content-start page-layout--logo-text" href="/" >
                  DGC
                </a>

                    <ul className="navbar-nav page-layout--search-container">

                         <li className="nav-item page-layout--nav-buttons">
                            <NavLink className={({ isActive }) => "page-layout--nav-link nav-link" + (isActive ? " page-layout--nav-link-active" : "")} to={'search'}>
                                <b>Search</b>
                            </NavLink> 
                        </li>
                        
                        {
                            isAuthenticated ? 
                                <li className="nav-item page-layout--nav-buttons">
                                    <NavLink className={({ isActive }) => "page-layout--nav-link nav-link" + (isActive ? " page-layout--nav-link-active" : "")} to={'my-list'}>
                                        <b>My List</b>
                                    </NavLink>
                                </li>
                            :
                            null
                        }
                        
                        <li className="nav-item page-layout--nav-buttons">
                            <NavLink className={({ isActive }) => "page-layout--nav-link nav-link" + (isActive ? " page-layout--nav-link-active" : "")} to={'reviews'}>
                                <b>Reviews</b>
                            </NavLink> 
                        </li>

                        <li className="nav-item page-layout--nav-buttons">
                            <NavLink className={({ isActive }) => "page-layout--nav-link nav-link" + (isActive ? " page-layout--nav-link-active" : "")} to={'social'}>
                                <b>Social</b>
                            </NavLink> 
                        </li>

                    </ul>


                <div>
                  {isAuthenticated ? <SignOutButton /> : <SignInButton />}
                </div>
            </div>
        </Navbar>
        
        <div className="mainContent">{props.children}</div>
    </>
  );
};
