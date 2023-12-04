import '../../styles/LobbyItem.css';
import React, {useState} from 'react';
import UserItem from './UserItem';

function AllUsersItem(props) {
    const [elements, setElements] = useState([]);

    function clear()
    {
        setElements([]);
    }

    return (
        <div className="lobby-item">
            <h1>Users</h1>
            <UserItem content={elements}/>
            {props.content && props.onClick && 
            (
            <>
                <input type="button" value={props.content} onClick={() => props.onClick(elements, setElements)}/>
                <input type="button" value='clear' onClick={clear}/>
            </>
            )}
        </div>
    );
}

export default AllUsersItem;