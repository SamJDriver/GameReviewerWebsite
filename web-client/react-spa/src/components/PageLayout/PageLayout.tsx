import { PropsWithChildren } from "react";
import Navbar from "react-bootstrap/Navbar";
import input from "react-bootstrap";
import { useIsAuthenticated, useMsal } from "@azure/msal-react";
import { SignInButton } from ".././SignInButton";
import { SignOutButton } from ".././SignOutButton";
import './PageLayout.css';
import { BsSearch } from "react-icons/bs";
import { Link } from "react-router-dom";
 
/**
 * Renders the navbar component with a sign-in or sign-out button depending on whether or not a user is authenticated
 * @param props
 */
export const PageLayout = (props: PropsWithChildren) => {
  const isAuthenticated = useIsAuthenticated();
  const { instance, accounts } = useMsal();

  return (
    <>
        <Navbar style={{ backgroundColor: "#4C566A", padding: "10px" }}>
            <div className="container-fluid">

                <a className="navbar-brand justify-content-start page-layout--logo-text" href="/" >
                  DGC
                </a>

                    <ul className="navbar-nav page-layout--search-container">

                         <li className="nav-item page-layout--nav-buttons">
                            <Link className="nav-link" to={'search'}>
                                <b>Search</b>
                            </Link> 
                        </li>
                      
                        <li className="nav-item page-layout--nav-buttons">
                            <a className="nav-link" href="#"><b>My List</b></a>
                        </li>

                        <li className="nav-item page-layout--nav-buttons">
                            <a className="nav-link" href="#"><b>Reviews</b></a>
                        </li>

                        <li className="nav-item page-layout--nav-buttons">
                            <a className="nav-link" href="#"><b>Social</b></a>
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
