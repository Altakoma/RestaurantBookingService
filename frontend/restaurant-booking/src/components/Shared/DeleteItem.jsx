import '../../styles/LobbyItem.css';
import React, {useState} from 'react';

function DeleteItem(props) {
    const [elementId, setElementId] = useState(0);
    
    return (
        <div className="lobby-item">
            <h1>Delete user by id</h1>
            {props.onClick && 
            (
            <>
                <input type='number' onChange={event => {setElementId(event.target.value)}}/>
                <input type="button" value='delete' onClick={() => props.onClick(elementId)}/>
            </>
            )}
        </div>
    );
}

export default DeleteItem;