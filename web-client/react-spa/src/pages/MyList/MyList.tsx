import useSWR from "swr";
import { IPlayRecord } from "../../interfaces/IPlayRecord";
import './MyList.css';
import { fetcher } from "../../utils/Fetcher";
import useBaseUrlResolver from "../../utils/useBaseUrlResolver";
import { useToken } from "../../utils/useToken";
import { ListGroup } from "react-bootstrap";
    
const MyList = () => {
    const baseUrl = useBaseUrlResolver();
    const { token } = useToken();
    const {data: playRecords, error: playRecordsError, isLoading: playRecordsIsLoading }
    = useSWR([`${baseUrl}/play-record`, token], ([url, token]) => fetcher(url, token))

    // if (playRecordsError) {
    //     return <div>Failed to load + {playRecordsError}</div>
    //   }
    
    //   if (playRecordsIsLoading || !playRecords) {
    //     return <div>Loading...</div>
    //   }

    return (
        <>

            <div>My List</div>
        </>
    );
}

export default MyList;