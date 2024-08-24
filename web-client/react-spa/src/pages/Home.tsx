import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import { useApi } from "../utils/useApi";
import Alert from "../components/Alert";
import ListGroup from "../components/ListGroup/ListGroup";
import FriendGameList from "../components/FriendGameList/FriendGameList";
import { ProfileContent } from "../pages/ProfileContent";
import { PageLayout } from "../components/PageLayout";

const BASE_URL = 'https://localhost:7272/api';

const Home = () => {
    // const [friendGames, setFriendGames] = useApi(BASE_URL, 'games/friend/0/10') 
    const { data, loading, error } = useApi(BASE_URL + '/game/0/10') 

    if (loading){
        return <div>Loading...</div>
    }

    if(error){
        return(<div>error:&nbsp;{error} </div>)
    }

    return (
      <>
        <PageLayout>
          <div className="App">
              <AuthenticatedTemplate>
                  <ListGroup items={ data.data } heading="Popular" />
                  <FriendGameList heading="Recently played by friends" />
                  <ProfileContent />
              </AuthenticatedTemplate>

              <UnauthenticatedTemplate>
                  <Alert>Please sign-in to see your profile information.</Alert>
                  <ListGroup items={ data.data } heading="Popular" />
              </UnauthenticatedTemplate>
          </div>
        </PageLayout>
      </>
    );
};

export default Home;
