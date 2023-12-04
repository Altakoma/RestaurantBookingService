import '../../styles/LobbyItem.css';
import React, {useState} from 'react';
import UserRoleItem from './UserRoleItem';

function AllUserRoleItem(props) {
    const [elements, setElements] = useState([]);

    function clear()
    {
        setElements([]);
    }

    return (
        <div className="lobby-item">
            <h1>User roles</h1>
            <UserRoleItem content={elements}/>
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

export default AllUserRoleItem;