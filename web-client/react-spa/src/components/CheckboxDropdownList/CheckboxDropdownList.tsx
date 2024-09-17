import Form from 'react-bootstrap/Form';
import Dropdown from 'react-bootstrap/Dropdown';
import { useState } from 'react';
import IDropdownItem from '../../interfaces/IDropdownItem';
import classnames from 'classnames';
import { MdClear } from 'react-icons/md';

interface IProps {
    className?: string;
    items: IDropdownItem[];
    heading: string;
    onItemSelected: (item: IDropdownItem[] | null) => void;
}

export const CheckboxDropdownList = (props: IProps) => {
    const [selectedItems, setSelectedItems] = useState<IDropdownItem[] | null>([]);
    const [displayItems, setDisplayItems] = useState<string[] | null>(null);

    const ItemCheckboxOnClick = (item: IDropdownItem) => {
      if (!selectedItems || selectedItems?.length === 0) {
        setSelectedItems([item]);
        setDisplayItems([item.name]);
        props.onItemSelected([item]);
        return;
      }
    
      const copy = selectedItems.slice();
      const index: number = copy?.indexOf(item) ?? -1;
      
      if (index !== -1) {

        copy?.splice(index, 1);
        const itemsFromCopy = copy.map((item: IDropdownItem) => item.name).slice(0, 1);
        if (copy.length > 1){
          itemsFromCopy.push("+ " + (copy.length - 1));
        }
        setDisplayItems(itemsFromCopy);
      }
      else {
        copy.push(item);
        if (copy.length > 1)
          {
            const itemsFromMap = copy.map((item: IDropdownItem) => item.name).slice(0, 1);
            itemsFromMap.push("+ " + (copy.length - 1));
            setDisplayItems(itemsFromMap);
          }
      }
      
      setSelectedItems(copy);
      props.onItemSelected(copy.length === 0 ? null : copy);
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
                              <Form.Check 
                                checked={selectedItems?.indexOf(item) !== -1}
                                type='checkbox' key={'genre-check'+index}
                                id={'genre-check-id'+index}
                                className=" game-search--item-background-color"
                                label={item.name} 
                                onChange={() => { ItemCheckboxOnClick(item); }}>
                              </Form.Check>
                            )
                          })
                        }
                      </Dropdown.Menu>
                    </Form>
                  </Dropdown>
                </div>
                <div id="genreTextInput" className="game-search--text-box game-search--item-background-color form-control">
                  {displayItems?.map( (item: string, i: number) => <span className="game-search--genre-item mx-1" key={i} >{item}</span> )}
                </div>
                <div onClick={() => { setSelectedItems([]); setDisplayItems(null); props.onItemSelected(null); } } className="input-group-append input-group-text game-search--item-background-color">
                    <MdClear/>
                </div>
            </div>
        </>
    )
}