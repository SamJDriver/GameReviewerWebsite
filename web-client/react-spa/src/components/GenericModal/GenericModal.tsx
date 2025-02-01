import { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import './GenericModal.css';
import { usePostRequest } from '../../utils/usePostRequest';
import { useToken } from '../../utils/useToken';
import { BASE_URL } from "../../UrlProvider";
import { IPlayRecord, IPlayRecord_Create } from '../../interfaces/IPlayRecord';
import { Dropdown, Form } from 'react-bootstrap';
import useSWR from 'swr';
import { fetcher } from '../../utils/Fetcher';

interface IGenericModalProps {
    gameId: number;
    show: boolean;
    onHide: () => void;
}

export const GenericModal = (props: IGenericModalProps) => {
    const { postData, isLoading, error } = usePostRequest();
    const { token } = useToken();


    const [ playRecord, setPlayRecord ] = useState<IPlayRecord_Create>({
        GameId: props.gameId,
        CompletedFlag: null,
        HoursPlayed: null,
        Rating: null,
        PlayDescription: null
    });

    const handleSubmit = async () => {

        await postData(BASE_URL + '/play-record', playRecord, { 'Authorization': 'Bearer ' + token });
    };
      
    return (
    <>
      <Modal show={props.show} onHide={props.onHide} size='lg'>
        <Modal.Header closeButton>
          <Modal.Title>Add to My List</Modal.Title>
        </Modal.Header>
        <Modal.Body>
            <div style={{display: 'flex', flexDirection: 'column'}}>
                <div>
                    <Dropdown>
                        <Dropdown.Toggle variant="success" id="dropdown-basic">
                          Completed?
                        </Dropdown.Toggle>

                        <Dropdown.Menu>
                          <Dropdown.Item href="#/action-1">Yes</Dropdown.Item>
                          <Dropdown.Item href="#/action-2"> No</Dropdown.Item>
                        </Dropdown.Menu>                
                    </Dropdown>
                    <Form.Label>Hours Played</Form.Label>
                    <Form.Control
                        value={playRecord.HoursPlayed ?? ''}
                        onChange={(e) => { !isNaN(+e.target.value) ? setPlayRecord({...playRecord, HoursPlayed: e.target.value}) : setPlayRecord({...playRecord, HoursPlayed: null}); }}
                        style={{width: '8em', overflow: 'scroll'}}
                    />
                    
                    <Form.Label>Score</Form.Label>
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
          <Button variant="primary" onClick={() => { handleSubmit(); props.onHide()}}>
            Submit
          </Button>
        </Modal.Footer>
      </Modal>
    </>
    );
}