import '../../styles/LobbyItem.css';
import React, {useState} from 'react';
import UserItem from './UserItem';

function UserByIdItem(props) {
    const [elements, setElements] = useState([]);

    function clear()
    {
        setElements([]);
    }

    return (
        <div className="lobby-item">
            <h1>User by id</h1>
            <UserItem content={elements}/>
            {props.content && props.onClick && 
            (
            <>
                <input type='number' onChange={event => {props.onClick(event.target.value, setElements)}}/>
                <input type="button" value='clear' onClick={clear}/>
            </>
            )}
        </div>
    );
}

export default UserByIdItem;