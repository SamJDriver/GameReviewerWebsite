import { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import './GenericModal.css';
import { usePostRequest } from '../../utils/usePostRequest';
import { useToken } from '../../utils/useToken';
import { BASE_URL } from "../../UrlProvider";
import { IPlayRecord_Create } from '../../interfaces/IPlayRecord';
import { Dropdown, Form } from 'react-bootstrap';

interface IGenericModalProps {
    show: boolean;
    onHide: () => void;
}


export const GenericModal = (props: IGenericModalProps) => {
    const { postData, isLoading, error } = usePostRequest();
    const { token } = useToken();

    const [ hoursPlayed, setHoursPlayed ] = useState<string | null>(null);
    const [ rating, setRating ] = useState<string | null>(null);
    const [ playDescription, setPlayDescription ] = useState<string | null>(null);


    const handleSubmit = async (args: IPlayRecord_Create) => {
        await postData(BASE_URL + '/play-record', args, { 'Authorization': 'Bearer ' + token });
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
                        value={hoursPlayed ?? ''}
                        onChange={(e) => { !isNaN(+e.target.value) ? setHoursPlayed(e.target.value) : setHoursPlayed(null); }}
                        style={{width: '8em', overflow: 'scroll'}}
                    />
                </div>
                <div>
                    <Form.Label>Score</Form.Label>
                    <Form.Control
                        value={rating ?? ''}
                        onChange={(e) => {
                            const val = +e.target.value
                            if (!isNaN(val)){
                                if (val > 100){
                                    setRating("100");
                                }
                                else if (val < 0){
                                    setRating("0");
                                }
                                else{
                                    setRating(val.toString());
                                }
                            }
                            else {
                                setRating(null);
                            }
                        }}
                    />
                </div>
                <div>
                    <Form.Label>Description</Form.Label>
                    <Form.Control as="textarea" rows={3} />
                </div>
            </div>

        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={props.onHide}>
            Cancel
          </Button>
          <Button variant="primary" onClick={props.onHide}>
            Submit
          </Button>
        </Modal.Footer>
      </Modal>
    </>
    );
}