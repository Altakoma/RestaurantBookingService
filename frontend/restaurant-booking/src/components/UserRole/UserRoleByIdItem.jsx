import '../../styles/LobbyItem.css';
import React, {useState} from 'react';
import UserRoleItem from './UserRoleItem';

function UserRoleByIdItem(props) {
    const [elements, setElements] = useState([]);

    function clear()
    {
        setElements([]);
    }

    return (
        <div className="lobby-item">
            <h1>User role by id</h1>
            <UserRoleItem content={elements}/>
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

export default UserRoleByIdItem;