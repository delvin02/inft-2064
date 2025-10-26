import { useState, useEffect } from "react";
import { Link, useParams } from "react-router-dom";

const CardDetail = ({ }) => {
    let params = useParams();

    const [cardData, setCardData] = useState({});
    const [itemId, setItemId] = useState(params.itemId);

    useEffect(() => {
        console.log("use effect");

        fetch(`http://localhost:5211/api/ItemsWebAPI/${itemId}`).then(response => response.json()).then(data => setCardData(data)).catch(err => { console.log(err)});

    }, [itemId])

    return (
        <div class="card col-4 mb-2" style={{ width: 18 + 'rem' }}>
            <img class="card-img-top" src={cardData.itemImage} alt={"Image of " + cardData.itemName} />
            <div class="card-body">
                <h5 class="card-title">{cardData.itemName}</h5>
                <p class="card-text">{cardData.itemDescription}</p>
                <p class="card-text">{cardData.itemDescription}</p>
                <p class="card-text">{cardData.itemCost}</p>
                <Link to="/Products" className="btn btn-primary">Back to Products</Link>
            </div>
        </div>
    )
}

export default CardDetail;