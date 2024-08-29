import { PropsWithChildren } from "react";
import Navbar from "react-bootstrap/Navbar";
import input from "react-bootstrap";
import { useIsAuthenticated, useMsal } from "@azure/msal-react";
import { SignInButton } from ".././SignInButton";
import { SignOutButton } from ".././SignOutButton";
import './PageLayout.css';
import { BsSearch } from "react-icons/bs";
 
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

                <div className="container-fluid collapse navbar-collapse">
                    <ul className="navbar-nav page-layout--search-container">

                        
                        <li className="nav-item page-layout--nav-buttons">
                            <div className="input-group mb-3">
                              <div className="input-group-prepend">
                                <span className="input-group-text">
                                    <BsSearch />
                                </span>
                              </div>
                              <input type="text" className="form-control" aria-label="Amount (to the nearest dollar)" />
                            </div>
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

                        {isAuthenticated ? 
                            <li className="nav-item page-layout--nav-buttons">
                                <a className="nav-link" href="#"><b>Profile</b></a>
                            </li>
                         : null}

                    </ul>
                </div>


                <div>
                  {isAuthenticated ? <SignOutButton /> : <SignInButton />}
                </div>
            </div>
        </Navbar>
        
        <div className="mainContent">{props.children}</div>
    </>
  );
};
