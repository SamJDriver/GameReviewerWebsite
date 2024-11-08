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
    = useSWR([`${baseUrl}/play-record`, token], ([url, token]) => fetcher(url, token));

    if (playRecordsError) {
        return <div>Failed to load + {playRecordsError.message}</div>
    }
    
    if (playRecordsIsLoading || !playRecords) {
      return <div>Loading...</div>
    }

    return (
        <>
        <div className="grid-container">
            <div className="grid-header" style={{textAlign: 'left'}}>Title</div>
            <div className="grid-header">Score</div>
            <div className="grid-header">Hours</div>
            <div className="grid-header">Completed?</div>

            {playRecords.map((playRecord: any, index: number) =>  (
                <div key={index} className="grid-row">
                    <div style={{textAlign: 'left', fontSize: '1em', overflow: 'hidden'}}>
                        <img style={{width: '4em', height: 'auto', borderRadius: '5px'}} src={playRecord.coverImageUrl} />
                        <span style={{marginLeft: '1em'}}>{playRecord.gameTitle}</span>
                    </div>
                    <div className="grid-item">
                        <span>{playRecord.rating}</span>
                    </div>
                    <div className="grid-item">{playRecord.hoursPlayed}</div>
                    <div className="grid-item">{playRecord.completedFlag ? 'Yes' : 'No'}</div>
                </div>
            ))}
        </div>
        </>
    );
}

export default MyList;