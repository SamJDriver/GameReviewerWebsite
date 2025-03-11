import useSWR from "swr";
import './MyList.css';
import { fetcherToken } from "../../utils/Fetcher";
import useBaseUrlResolver from "../../utils/useBaseUrlResolver";
import { useToken } from "../../utils/useToken";
import { useState } from "react";
import { PlayRecordModal } from "../../components/PlayRecordModal/PlayRecordModal";
import { IPlayRecord } from "../../interfaces/IPlayRecord";
import { Link } from "react-router-dom";
    
const MyList = () => {
    const baseUrl = useBaseUrlResolver();
    const { token } = useToken();
    const {data: playRecords, error: playRecordsError, isLoading: playRecordsIsLoading } 
        = useSWR([`${baseUrl}/play-record`, token], ([url, token]) => fetcherToken(url, token));
    const [showModal, setShowModal] = useState(false);
    const [selectedIndex, setSelectedIndex] = useState<number>(0);
    const [playRecordsState, setPlayRecordsState] = useState([]);

    if (playRecordsError) {
        return <div>Failed to load + {playRecordsError.message}</div>
    }
    
    if (playRecordsIsLoading || !playRecords) {
      return <div>Loading...</div>
    }

    const handleOpenPlayRecordModal = () => {
        setShowModal(true);
      };
    
      const handleClosePlayRecordModal = () => {
        setShowModal(false);
      };

    return (
        <>
        <div className="grid-container">
            <div className="grid-header" style={{textAlign: 'left'}}>Title</div>
            <div className="grid-header">Score</div>
            <div className="grid-header">Hours</div>
            <div className="grid-header">Completed?</div>

            {playRecords.map((playRecord: any, index: number) =>  (
                <div onClick={() => {handleOpenPlayRecordModal(); setSelectedIndex(index);}} key={index} className="grid-row">
                    <div style={{textAlign: 'left', fontSize: '1em', overflow: 'hidden'}}>
                    <Link to={'/game/' + playRecord.gameId}>
                        <img style={{width: '4em', height: 'auto', borderRadius: '5px'}} src={playRecord.coverImageUrl} />
                    </Link>
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
        <PlayRecordModal
            gameId={playRecords[selectedIndex].gameId}
            show={showModal}
            playRecord={(playRecords[selectedIndex] as IPlayRecord)} 
            onHide={handleClosePlayRecordModal} />
        </>
    );
}

export default MyList;