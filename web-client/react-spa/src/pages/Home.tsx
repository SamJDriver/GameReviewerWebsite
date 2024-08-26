import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import { useApi } from "../utils/useApi";
import Alert from "../components/Alert";
import ListGroup from "../components/ListGroup/ListGroup";
import FriendGameList from "../components/FriendGameList/FriendGameList";
import { ProfileContent } from "../pages/ProfileContent";
import { PageLayout } from "../components/PageLayout";
import { BASE_URL } from "../UrlProvider";
import IPaginator from "../interfaces/IPaginator";
import IVanillaGame from "../interfaces/IVanillaGame";

const Home = () => {
    // const [friendGames, setFriendGames] = useApi(BASE_URL, 'games/friend/0/10') 
    const paginatedGamesResponse = useApi<IPaginator<IVanillaGame>>(BASE_URL + '/game/0/10') 

    if (paginatedGamesResponse.loading || !paginatedGamesResponse.data){
        return <div>Loading...</div>
    }

    if(paginatedGamesResponse.error){
        return(<div>error:&nbsp;{paginatedGamesResponse.error} </div>)
    }

    console.log(paginatedGamesResponse);

    return (
      <>
        <PageLayout>
          <div className="App">
              <AuthenticatedTemplate>
                  <ListGroup items={ paginatedGamesResponse.data.items } heading="Popular" />
                  <FriendGameList heading="Recently played by friends" />
                  <ProfileContent />
              </AuthenticatedTemplate>

              <UnauthenticatedTemplate>
                  <Alert>Please sign-in to see your profile information.</Alert>
                  <ListGroup items={ paginatedGamesResponse.data.items } heading="Popular" />
              </UnauthenticatedTemplate>
          </div>
        </PageLayout>
      </>
    );
};

export default Home;
