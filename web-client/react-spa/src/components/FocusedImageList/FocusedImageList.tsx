import React, { useRef, useState } from "react";
import IImageScrollItem from "../../interfaces/IImageScrollItem";
import { SlArrowLeft } from "react-icons/sl";
import { SlArrowRight } from "react-icons/sl";
import './FocusedImageList.css';
import { Button } from "react-bootstrap";

interface Props {
  itemList: IImageScrollItem[]
}

const FocusedImageList  = (props: Props) => {
    const ulRef = useRef<HTMLUListElement>(null);
    const [itemList, setItemList] = useState<IImageScrollItem[]>(props.itemList);

    const leftScrollClick = (): void => {
      const itemListCopy = [...itemList];
      const focusedItemIndex: number = itemListCopy.findIndex( (i) => i.focusedItemFlag === true);
      const newFocusedItemIndex: number = focusedItemIndex - 1 >= 0 ? focusedItemIndex - 1 : itemListCopy.length - 1; 
  
      itemListCopy[newFocusedItemIndex].focusedItemFlag = true;
      itemListCopy[focusedItemIndex].focusedItemFlag = false;
  
      const ul = ulRef.current;
      const focusedItem = ul!.children[newFocusedItemIndex];
      focusedItem.scrollIntoView({block: 'nearest', inline: 'center'});
  
      // Shift focus to item to the left, meaning slide items to the right.
      // items.unshift.apply(items, items.splice(items.length-1, 1));
 
      setItemList(itemListCopy);
    };
  
    const rightScrollClick = (): void => {
      const itemListCopy = [...itemList];
      const focusedItemIndex: number = itemListCopy.findIndex( (i) => i.focusedItemFlag === true);
      const newFocusedItemIndex: number = focusedItemIndex + 1 >= itemListCopy.length ? 0 : focusedItemIndex + 1; 
  
      itemListCopy[newFocusedItemIndex].focusedItemFlag = true;
      itemListCopy[focusedItemIndex].focusedItemFlag = false;
  
      const ul = ulRef.current;
      const focusedItem = ul!.children[newFocusedItemIndex];
      focusedItem.scrollIntoView({block: 'nearest', inline: 'center'});
  
      // Shift focus to item to the right, meaning slide items to the left.
      // items.push.apply(items, items.splice(0, 1));
  
      setItemList(itemListCopy);
    };

    const focusImageClickEvent = (newFocusedIndex: number, oldFocusedIndex: number): void  => {
      if (newFocusedIndex === oldFocusedIndex) {
        return;
      }
  
      let items: IImageScrollItem[] = [...itemList];
      items[newFocusedIndex].focusedItemFlag = true;
      items[oldFocusedIndex].focusedItemFlag = false;
      setItemList(items);
    }


    return (
      <>
        <div className="focused-image-list--main-artwork-container">
          <img
            className="focused-image-list--main-artwork"
            src = {itemList.find( (i: IImageScrollItem) => i.focusedItemFlag === true )?.imageSourceUrl }
            alt = "main artwork"
          />
        </div>

        <div style={{display: 'flex'}}>
          <div>
            <Button onClick={() => leftScrollClick()} style={{height: '100%'}} variant="outline-secondary">
              <SlArrowLeft/>
            </Button>{' '}
          </div>
          <div className="focused-image-list--container">
          <ul ref={ulRef} style={{display: 'inline'}} className="list-group list-group-horizontal focused-image-list--ul" >
            {itemList.map( (item: IImageScrollItem, index: number) => (
              <li key={index} className="focused-image-list--li">
                <button className='focused-image-list--artwork-selection-button' onClick={() => focusImageClickEvent(index, itemList.findIndex( (i: IImageScrollItem) => i.focusedItemFlag === true))}>
                  <img 
                    className= {item.focusedItemFlag === true ? 'focused-image-list--selected-artwork' : 'focused-image-list--unselected-artwork'}
                    src={item.imageSourceUrl}
                    alt={'img ' + index}
                  />
                </button>
              </li>
            ))}
          </ul>
          </div>

          <div>
            <Button onClick={() => rightScrollClick()} style={{height: '100%'}} variant="outline-secondary">
              <SlArrowRight/>
            </Button>{' '}
          </div>
        </div>
      </>
    );
  }

export default FocusedImageList;
