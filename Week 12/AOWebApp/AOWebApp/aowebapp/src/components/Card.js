
export default function Card(props) {
    return (
        <div class="card col-4 mb-2" style={{ width: 18 + 'rem'}}>
            <img class="card-img-top" src={props.itemImage} alt={"Image of " + props.itemName} />
            <div class="card-body">
                <h5 class="card-title">{props.itemName}</h5>
                <p class="card-text">{props.itemDescription}</p>
                <p class="card-text">{props.itemCost}</p>
                <a href="#" class="btn btn-primary">Go somewhere {props.itemId}</a>
            </div>
        </div>
    )
}