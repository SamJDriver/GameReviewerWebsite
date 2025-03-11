import { useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import './PlayRecordModal.css';
import { usePostRequest } from '../../utils/usePostRequest';
import { useToken } from '../../utils/useToken';
import { BASE_URL } from "../../UrlProvider";
import { IPlayRecord, IPlayRecord_Create } from '../../interfaces/IPlayRecord';
import { Dropdown, Form } from 'react-bootstrap';
import useSWR from 'swr';
import { fetcher } from '../../utils/Fetcher';
import { usePutRequest } from '../../utils/usePutRequest';
import { useDeleteRequest } from '../../utils/useDeleteRequest';

interface IPlayRecordModalProps {
    gameId: number;
    show: boolean;
    playRecord?: IPlayRecord;
    onHide: () => void;
}

export const PlayRecordModal = (props: IPlayRecordModalProps) => {
    const { postData, isLoading: isPostLoading, error: postError } = usePostRequest();
    const { putData, isLoading: isPutLoading, error: putError } = usePutRequest();
    const { deleteData, isLoading: isDeleteLoading, error: deleteError } = useDeleteRequest();
    const { token } = useToken();
    const [ playRecord, setPlayRecord ] = useState<IPlayRecord_Create>({
        GameId: props.gameId,
        CompletedFlag: null,
        HoursPlayed: null,
        Rating: null,
        PlayDescription: null
    });

    const handleSubmit = async () => {
        if (props.playRecord) {
            await putData(BASE_URL + '/play-record/' + props.playRecord.id, playRecord, { 'Authorization': 'Bearer ' + token });   
        }
        else {
            await postData(BASE_URL + '/play-record',    playRecord, { 'Authorization': 'Bearer ' + token });
        }
    };

    const handleDelete = async () => {
        if (props.playRecord) {
            await await deleteData(BASE_URL + '/play-record/' + props.playRecord.id, { 'Authorization': 'Bearer ' + token });   
        }
    };

    useEffect(() => {
        if (props.playRecord) {
          setPlayRecord({
            ...playRecord,
            GameId: props.playRecord.gameId,
            CompletedFlag: props.playRecord.completedFlag,
            HoursPlayed: props.playRecord.hoursPlayed+'',
            Rating: props.playRecord.rating+'',
            PlayDescription: props.playRecord.playDescription
          });
        }
      }, [props.playRecord]); // Only run when playRecord prop changes
      
    return (
    <>
      <Modal show={props.show} onHide={props.onHide} size='lg'>
        <Modal.Header closeButton>
          <Modal.Title>
            
            Add to My List

            <img
              style={{width: '4em', height: 'auto', borderRadius: '5px'}}
              src={props.playRecord?.coverImageUrl}
              alt={props.playRecord?.gameTitle}
            />

          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
            <div style={{display: 'flex', flexDirection: 'column', gap: '1em'}}>
                <div>
                    <Form.Label>Have You completed this game?</Form.Label>
                    <Dropdown data-bs-theme="dark">
                        <Dropdown.Toggle variant="secondary" id="dropdown-basic">
                            {playRecord.CompletedFlag === null ? 'Select' : playRecord.CompletedFlag ? 'Yes' : 'No'}
                        </Dropdown.Toggle>

                        <Dropdown.Menu>
                          <Dropdown.Item onClick={() => setPlayRecord({...playRecord, CompletedFlag: true})}>Yes</Dropdown.Item>
                          <Dropdown.Item onClick={() => setPlayRecord({...playRecord, CompletedFlag: false})}> No</Dropdown.Item>
                        </Dropdown.Menu>                
                    </Dropdown>
                </div>

                <div>
                    <Form.Label>Hours Played</Form.Label>
                    <Form.Control
                        value={playRecord.HoursPlayed ?? ''}
                        onChange={(e) => { !isNaN(+e.target.value) ? setPlayRecord({...playRecord, HoursPlayed: e.target.value}) : setPlayRecord({...playRecord, HoursPlayed: null}); }}
                        style={{width: '8em', overflow: 'scroll'}}
                    />

                </div>

                <div>
                    <Form.Label>Score (0-100)</Form.Label>
                    <Form.Control
                        value={playRecord.Rating ?? ''}
                        onChange={(e) => {
                            const val = +e.target.value
                            if (!isNaN(val)){
                                if (val > 100){
                                    setPlayRecord({...playRecord, Rating: "100"});
                                }
                                else if (val < 0){
                                    setPlayRecord({...playRecord, Rating: "0"});
                                }
                                else{
                                    setPlayRecord({...playRecord, Rating: val.toString()});
                                }
                            }
                            else {
                                setPlayRecord({...playRecord, Rating: null});
                            }
                        }}
                    />
                </div>


                <div>
                    <Form.Label>Description</Form.Label>
                    <Form.Control 
                        as="textarea" 
                        rows={3} 
                        value={playRecord.PlayDescription ?? ''}
                        onChange={(e) => setPlayRecord({...playRecord, PlayDescription: e.target.value})} />
                </div>

            </div>

        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={props.onHide}>
            Cancel
          </Button>
          {props.playRecord && 
            <Button variant="danger" onClick={() => { handleDelete(); props.onHide()}}>
              Delete
            </Button>
          }
          <Button variant="primary" onClick={() => { handleSubmit(); props.onHide()}}>
            {props.playRecord ? 'Update' : 'Add'}
          </Button>
        </Modal.Footer>
      </Modal>
    </>
    );
}