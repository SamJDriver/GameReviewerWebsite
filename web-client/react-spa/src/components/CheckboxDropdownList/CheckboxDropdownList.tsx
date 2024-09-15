import Form from 'react-bootstrap/Form';
import Dropdown from 'react-bootstrap/Dropdown';
import { useState } from 'react';
import IDropdownItem from '../../interfaces/IDropdownItem';
import classnames from 'classnames';



interface IProps {
    className?: string;
    items: IDropdownItem[];
    heading: string;
    onItemSelected: (item: IDropdownItem[]) => void;
}

export const CheckboxDropdownList = (props: IProps) => {
    const [selectedItems, setSelectedItems] = useState<IDropdownItem[] | null>(null);

    const ItemCheckboxOnClick = (item: IDropdownItem) => {
      if (!selectedItems) {
        setSelectedItems([item]);
        props.onItemSelected([item]);
        return;
      }
    
      const copy = selectedItems.slice();
      const index: number = copy?.indexOf(item) ?? -1;
      
      if (index !== -1) {
        copy?.splice(index, 1);
      }
      else {
        copy.push(item);
      
      }

      setSelectedItems(copy);
      props.onItemSelected(copy);
    }
    
    const classes = classnames('input-group', props.className);

    return (
        <>
            <div className={classes} >
                <div className="input-group-text game-search--item-background-color">
                  <Dropdown>
                    <Dropdown.Toggle variant="dark" className="game-search--genre-toggle"  id="dropdown-basic"> {props.heading} </Dropdown.Toggle>
                    <Form>
                      <Dropdown.Menu>
                        {
                          props.items.map( (item: IDropdownItem, index: number) => {
                            if (!item) {
                              return <Dropdown.Item key={index}> Loading ... </Dropdown.Item>
                            }
                            return (
                              <Form.Check type='checkbox' key={'genre-check'+index} id={'genre-check-id'+index} className=" game-search--item-background-color" label={item.name} onClick={() => ItemCheckboxOnClick(item)}>
                              </Form.Check>
                            )
                          })
                        }
                      </Dropdown.Menu>
                    </Form>
                  </Dropdown>
                </div>
                <div id="genreTextInput" className="game-search--text-box game-search--item-background-color form-control">
                  {selectedItems?.map( (item: IDropdownItem, i: number) => <span className="game-search--genre-item mx-1" key={i} >{item.name}</span> )}
                </div>
            </div>
        </>
    )
}