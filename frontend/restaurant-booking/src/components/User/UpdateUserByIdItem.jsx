import '../../styles/LobbyItem.css';
import React, {useState} from 'react';

function UpdateUserByIdItem(props) {
    const [element, setElement] = useState({});
    const [elementId, setElementId] = useState(0);

    return (
        <div className="lobby-item">
            <h1>Update by id</h1>
            <input type='number' placeholder='id' onChange= {
                (event) => { 
                return setElementId(event.target.value);}}/>
            <input type='number' placeholder='userRoleId' onChange= {
                (event) => { 
                element.userRoleId = event.target.value;
                return setElement(element);}}/>
            <input type='text' placeholder='name' onChange= {
                (event) => { 
                element.name = event.target.value;
                return setElement(element);}}/>
            <input type='text' placeholder='login' onChange= {
                (event) => { 
                element.login = event.target.value;
                return setElement(element);}}/>
            <input type='text' placeholder='password' onChange= {
                (event) => { 
                element.password = event.target.value;
                return setElement(element);}}/>
            <input type='button' value="Update" onClick={() => props.onClick(element, elementId)}/>
        </div>
    );
}

export default UpdateUserByIdItem;