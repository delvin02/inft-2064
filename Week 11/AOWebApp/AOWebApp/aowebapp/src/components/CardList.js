import Card from './CardV3';
import cardData from '../assets/itemData.json'

const CardList = () => {

    console.log('card data:', cardData);

    return (
        <div className="row gap-3">
            {cardData.map((obj) => (
                <Card
                    key={obj.itemId}
                    itemId={obj.itemId}
                    itemName={obj.itemName}
                    itemCost={obj.itemCost}
                    itemDescription={obj.itemDescription}
                    itemImage={obj.itemImage}
                />
            ))}
        </div>
    )
}

export default CardList;