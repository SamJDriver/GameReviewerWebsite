import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import { useApi } from "../../utils/useApi";
import Alert from "../../components/Alert";
import GameList from "../../components/GameList/GameList";
import FriendGameList from "../../components/FriendGameList/FriendGameList";
import { PageLayout } from "../../components/PageLayout/PageLayout";
import { BASE_URL } from "../../UrlProvider";
import IPaginator from "../../interfaces/IPaginator";
import IVanillaGame from "../../interfaces/IVanillaGame";
import IApiResponse from "../../interfaces/IApiResponse";
import { GameSearch } from "../../components/GameSearch/GameSearch";

const Home = () => {
    const paginatedGamesResponse: IApiResponse<IPaginator<IVanillaGame>> = useApi<IPaginator<IVanillaGame>>(BASE_URL + '/game/0/10');

    if (paginatedGamesResponse.loading || !paginatedGamesResponse.data){
        return <div>Loading...</div>
    }

    if(paginatedGamesResponse.error){
        return(<div>error:&nbsp;{paginatedGamesResponse.error} </div>)
    }

    return (
      <>
        <PageLayout>
          <div className="App">
              <AuthenticatedTemplate>
                <GameSearch />
                <GameList items={ paginatedGamesResponse.data.items } heading="Popular" />
                <FriendGameList heading="From Friends" />
              </AuthenticatedTemplate>

              <UnauthenticatedTemplate>
                  <Alert>Please sign-in to see your profile information.</Alert>
                  <GameList items={ paginatedGamesResponse.data.items }  heading="Popular" />
              </UnauthenticatedTemplate>
          </div>
        </PageLayout>
      </>
    );
};

export default Home;
