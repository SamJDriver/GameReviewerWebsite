import React from "react";
import IImageScrollItem from "../../interfaces/IImageScrollItem"
import './FocusedImageList.css';

interface Props {
  itemList: IImageScrollItem[]
}

interface State {
  itemList: IImageScrollItem[]
}

class FocusedImageList extends React.Component<Props, State> {

  constructor(props: Props) {
    super(props);
    this.state = { itemList: props.itemList };
  }

  render(): React.ReactNode {
    return (
      <>
        <div className="focused-image-list--main-artwork-container">
          <img
            className="focused-image-list--main-artwork"
            src = {this.state.itemList.find( (i: IImageScrollItem) => i.focusedItemFlag === true )?.imageSourceUrl }
          />
        </div>

        <div className="focused-image-list--container">
          <ul className="list-group list-group-horizontal" >
            {this.state.itemList.map( (item: IImageScrollItem, index: number) => (
              <li key={index}>
                <button onClick={() => this.focusImageClickEvent(index, this.state.itemList.findIndex( (i: IImageScrollItem) => i.focusedItemFlag === true))}>
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
      </>
    );
  }

  focusImageClickEvent(newFocusedIndex: number, oldFocusedIndex: number): void {
    if (newFocusedIndex === oldFocusedIndex) {
      return;
    }

    let items: IImageScrollItem[] = this.state.itemList;
    items[newFocusedIndex].focusedItemFlag = true;
    items[oldFocusedIndex].focusedItemFlag = false;
    this.setState({itemList: items})

  }

}

// const FocusedImageList: React.FC<{itemList: IImageScrollItem[]}> = ({itemList}) =>
// {
//     return (
//       <>
//         <div className="focused-image-list--container">
//           <ul className="list-group list-group-horizontal" >
//             {itemList.map( (item: IImageScrollItem, index: number) => (
//               <li key={index}>
//                 <button onClick={() => FocusImageClickEvent(index, itemList.findIndex( (i: IImageScrollItem) => i.focusedItemFlag === true))}>
//                   <img 
//                     className= {item.focusedItemFlag === true ? 'focused-image-list--selected-artwork' : 'focused-image-list--unselected-artwork'}
//                     src={item.imageSourceUrl}
//                     alt={'img ' + index}
//                   />
//                 </button>
//               </li>
//             ))}
//           </ul>
//         </div>
//       </>
//     );
// }
//

export default FocusedImageList;
