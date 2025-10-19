
function CardV2({ itemImage, itemName, itemDescription, itemCost, itemId }) {
    return (
        <div class="card col-4 mb-2" style={{ width: 18 + 'rem'}}>
            <img class="card-img-top" src={itemImage} alt={"Image of " + itemName} />
            <div class="card-body">
                <h5 class="card-title">{itemName}</h5>
                <p class="card-text">{itemDescription}</p>
                <p class="card-text">{itemCost}</p>
                <a href="#" class="btn btn-primary">Go somewhere {itemId}</a>
            </div>
        </div>
    )
}

export default CardV2;