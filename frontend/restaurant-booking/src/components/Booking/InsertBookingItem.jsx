import '../../styles/LobbyItem.css';
import React, {useState} from 'react';

function InsertBookingItem(props) {
    const [element, setElement] = useState({});

    return (
        <div className="lobby-item">
            <h1>Book table</h1>
            <input type='number' placeholder='tableId' onChange= {
                (event) => { 
                element.tableId = event.target.value;
                return setElement(element);}}/>
            <input type='date' min="2022-01-01" max="2024-11-25" onChange= {
                (event) => {
                element.date = event.target.value;
                return setElement(element);}}/>
            <input type='time' onChange= {
                (event) => { 
                element.time = event.target.value;
                return setElement(element);}}/>
            <input type='button' value="Insert" onClick={() => props.onClick(element)}/>
        </div>
    );
}

export default InsertBookingItem;