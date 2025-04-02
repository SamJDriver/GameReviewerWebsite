import { useState } from "react";
import { GameSearchBar } from "../../components/GameSearchBar/GameSearchBar";
import IPaginator from "../../interfaces/IPaginator";
import IVanillaGame from "../../interfaces/IVanillaGame";
import { Spinner } from "react-bootstrap";
import './SearchGames.css';
import { Link } from "react-router-dom";

const SearchGames = () => {
    const [searchResults, setSearchResults] = useState<IPaginator<IVanillaGame> | null>(null);
    const [pageIndex, setPageIndex] = useState(0);
    const [isCleanFlag, setIsCleanFlag] = useState(true);

    const handleSearchResults = (searchResults: IPaginator<IVanillaGame> | null) => {
        setSearchResults(searchResults);
    }

    const handleCleanFlagChange = (isCleanFlag: boolean) => {
        setIsCleanFlag(isCleanFlag);
    }

    console.log("Search Results: ", searchResults);

    return (
        <>
            <GameSearchBar pageSize={15} onIsCleanChange={handleCleanFlagChange} onSearchResults={handleSearchResults} />

            {
                isCleanFlag
                ? <div> Search Something! </div>
                : searchResults && searchResults.items && searchResults.items.length > 0
                ?

                <div className="grid-container">
                    <div className="grid-header" style={{textAlign: 'left'}}>Title</div>
                    <div className="grid-header">Release Date</div>
                    <div className="grid-header">Platform</div>
                    <div className="grid-header">Genre</div>

                    {searchResults.items.map((item: any, index: number) =>  (
                        <div className="grid-row" key={index}>
                            <div style={{textAlign: 'left', fontSize: '1em', overflow: 'hidden'}}>
                            <Link to={'/game/' + item.id}>
                                <img style={{width: '4em', height: 'auto', borderRadius: '5px'}} src={item.coverImageUrl} />
                            </Link>
                            <span style={{marginLeft: '1em'}}>{item.title}</span>
                            </div>
                            <div className="grid-item">{item.releaseDate}</div>
                            <div className="grid-item">{item.completed}</div>
                        </div>
                    ))}

                </div>
                // ? <GameList items={ searchResults.items } heading="Search Results" />
                : <Spinner animation="border" role="status">
                    <span className="visually-hidden">Loading...</span>
                  </Spinner>

                  

            

            }
        </>
    );
}

export default SearchGames;