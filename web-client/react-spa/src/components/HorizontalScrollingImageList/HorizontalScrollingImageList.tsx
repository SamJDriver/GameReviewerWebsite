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
      <div className="horizontal-scrolling-image-list--container">
        <FocusedImageList itemList={this.state.itemList}/>
      </div>
    )
  }
}

export default HorizontalScrollingImageList;
