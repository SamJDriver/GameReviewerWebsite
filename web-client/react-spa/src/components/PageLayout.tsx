import Navbar from "react-bootstrap/Navbar";
import { useIsAuthenticated } from "@azure/msal-react";
import { SignInButton } from "./SignInButton";
import { SignOutButton } from "./SignOutButton";
/**
 * Renders the navbar component with a sign-in or sign-out button depending on whether or not a user is authenticated
 * @param props
 */
export const PageLayout = (props) => {
  const isAuthenticated = useIsAuthenticated();

  return (
    <>
        <Navbar style={{ backgroundColor: "#4C566A", padding: "10px" }}>
            <div className="container-fluid">
                <a className="navbar-brand justify-content-start" href="/" style={{ backgroundColor: "#1E1E1E", padding: "10px", borderRadius: "5px" }}>
                  DGC
                </a>

                <div className="title container-fluid" >
                    <button type="button" className="btn topNavButtons">Reviews</button>
                    <button type="button" className="btn topNavButtons">My List</button>
                    <button type="button" className="btn topNavButtons">Social</button>
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
