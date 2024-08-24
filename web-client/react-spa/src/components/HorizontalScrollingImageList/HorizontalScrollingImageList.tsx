import React from "react";
import IImageScrollItem from "../../interfaces/IImageScrollItem";
import './HorizontalScrollingImageList.css';
import FocusedImageList from "../FocusedImageList/FocusedImageList";

interface Props {
  initialItemList: IImageScrollItem[]
}

interface State {
  itemList: IImageScrollItem[]
}

class HorizontalScrollingImageList extends React.Component<Props, State> {

  constructor(props: Props) {
    super(props);
    this.state = { itemList: props.initialItemList };
  }

  render(): React.ReactNode {
    return (
      <>
        <div className="horizontal-scrolling-image-list--main-artwork-container">
          <img
            className="horizontal-scrolling-image-list--main-artwork"
            src = {this.state.itemList.find( (i: IImageScrollItem) => i.focusedItemFlag === true )?.imageSourceUrl }
          />
        </div>

        <FocusedImageList itemList={this.state.itemList}/>
        <button onClick={() => this.leftScrollClick(this.state.itemList)}>left</button>
        <button onClick={() => this.rightScrollClick(this.state.itemList)}>right</button>
      </>
    )
  }

  leftScrollClick(items: IImageScrollItem[]): void {
    const focusedItemIndex: number = items.findIndex( (i) => i.focusedItemFlag === true);
    const newFocusedItemIndex: number = focusedItemIndex - 1 >= 0 ? focusedItemIndex - 1 : items.length - 1; 

    items[newFocusedItemIndex].focusedItemFlag = true;
    items[focusedItemIndex].focusedItemFlag = false;

    // Shift focus to item to the left, meaning slide items to the right.
    items.unshift.apply(items, items.splice(items.length-1, 1));

    this.setState({itemList: items});
  }

  rightScrollClick(items: IImageScrollItem[]): void {
    const focusedItemIndex: number = items.findIndex( (i) => i.focusedItemFlag === true);
    const newFocusedItemIndex: number = focusedItemIndex + 1 >= items.length ? 0 : focusedItemIndex + 1; 

    items[newFocusedItemIndex].focusedItemFlag = true;
    items[focusedItemIndex].focusedItemFlag = false;

    // Shift focus to item to the right, meaning slide items to the left.
    items.push.apply(items, items.splice(0, 1));

    this.setState({itemList: items});
  }
}

export default HorizontalScrollingImageList;
