import IImageScrollItem from "../../interfaces/IImageScrollItem"
import './FocusedImageList.css';

const FocusedImageList: React.FC<{itemList: IImageScrollItem[]}> = ({itemList}) =>
{
  console.log(itemList);
    return (
      <>
        <div className="focused-image-list--container">
          <ul className="list-group list-group-horizontal" >
            {itemList.map( (item: IImageScrollItem, index: number) => (
              <li key={index}>
                <img 
                  className= {item.focusedItemFlag === true ? 'focused-image-list--selected-artwork' : 'focused-image-list--unselected-artwork'}
                  src={item.imageSourceUrl}
                  alt={'img ' + index}
                />
              </li>
            ))}
          </ul>
        </div>
      </>
    );
}

export default FocusedImageList;
