import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import Alert from "../../components/Alert";
import GameList from "../../components/GameList/GameList";
import { BASE_URL } from "../../UrlProvider";
import IPaginator from "../../interfaces/IPaginator";
import IVanillaGame from "../../interfaces/IVanillaGame";
import { GameSearch } from "../../components/GameSearch/GameSearch";
import { useState } from "react";
import "./Home.css";
import { useToken } from "../../utils/useToken";
import { fetcher, fetcherToken } from "../../utils/Fetcher";
import useSWR from "swr";
import useBaseUrlResolver from "../../utils/useBaseUrlResolver";
import { Spinner } from "react-bootstrap";
import IFriendPlayRecordGame from "../../interfaces/IFriendPlayRecordGame";

const Home = () => {
  const [searchResults, setSearchResults] = useState<IPaginator<IVanillaGame> | null>(null);
  const baseUrl = useBaseUrlResolver();
  const { token } = useToken();
  const {data: friendGames, error: friendGamesError, isLoading: friendGamesIsLoading } = useSWR<IPaginator<IFriendPlayRecordGame>>([`${baseUrl}/game/friend/0/8`, token], ([url, token]) => fetcherToken(url, token));
  const {data: popularGames, error: popularGamesError, isLoading: popularGamesIsLoading } =  useSWR<IPaginator<IVanillaGame>>(BASE_URL + '/game/0/8', fetcher);

  if (popularGamesIsLoading){
    return <Spinner animation="border" role="status">
      <span className="visually-hidden">Loading...</span>
    </Spinner>
  }

  const handleSearchResults = (searchResults: IPaginator<IVanillaGame> | null) => {
    setSearchResults(searchResults);
  }

  const homePageContents = () => {
    return (
      <>
        <GameSearch onSearchResults={handleSearchResults} />
        {
          searchResults
          ? <GameList items={ searchResults.items } heading="Search Results" />
          : 
            !popularGamesIsLoading 
            ? <GameList items={ popularGames!.items } heading="Popular" />
            : <Spinner animation="border" role="status">
                <span className="visually-hidden">Loading...</span>
              </Spinner>
        }
      </>
    )
  }

    return (
      <>
          <div className="home--container">
              <AuthenticatedTemplate>
                {homePageContents()}
                {
                  friendGames && friendGames.items
                  ? <GameList items={ friendGames.items } heading="From Friends" />
                  : <Spinner animation="border" role="status">
                      <span className="visually-hidden">Loading...</span>
                    </Spinner>
                }
                {/* <FriendGameList heading="From Friends" /> */}
              </AuthenticatedTemplate>

              <UnauthenticatedTemplate>
                <Alert>Please sign-in to see your profile information.</Alert>
                {homePageContents()}
              </UnauthenticatedTemplate>
          </div>
      </>
    );
};

export default Home;
