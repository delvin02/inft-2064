import Card from './CardV3';
import cardData from '../assets/itemData.json'
import React, { useState, useEffect } from 'react';

const CardList = () => {
    const [cardData, setCardData] = useState([]);
    const [query, setQuery] = useState('');

    useEffect(() => {
        fetch(`http://localhost:5211/api/ItemsWebAPI?searchText=${query}`).then(response => response.json()).then(data => setCardData(data)).catch(err => {
            console.log(err);
        })
    }, [query])

    function searchQuery() {
        const value = document.querySelector('[name="searchText"]').value;
        console.log(value);
        setQuery(value);
    }

    function handleSubmit(e) {
        e.preventDefault();

        const form = e.target;
        const formData = new FormData(form);
        console.log("Form Data: " + formData.get("searchText"));
        setQuery(formData.get("searchText"));
    }

    return (
        <div>
            <form method="post" onSubmit={handleSubmit} className="row justify-content-center mb-3">
                <div className="col-3">
                    <input type="text" className="form-control" name="searchText" placeholder="Type your query"/>
                    
                </div>
                <div className="col text-left">
                    <button type="button" className="btn btn-primary" onClick={searchQuery}>Search</button>
                </div>
            </form>
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
        </div>
    )
}

export default CardList;