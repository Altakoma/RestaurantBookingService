import '../../styles/LobbyItem.css';
import React, {useState} from 'react';

function RestaurantByIdItem(props) {
    const [elements, setElements] = useState([]);

    function clear()
    {
        setElements([]);
    }

    return (
        <div className="lobby-item">
            <h1>Restaurant</h1>
        <div>
        <table border="1">
        {
            elements.length !== 0 && (
                <>
                    <tr>
                        <th>id</th>
                        <th>name</th>
                    </tr>
                    {elements.map(item =>(
                        <tr key={item.id}>
                        <td>{item.id}</td>
                        <td>{item.name}</td>
                        </tr>
                    ))}
                </>
            )
        }
        </table>
        {
            elements.map(item =>
                item.readTableDTOs.map(subItem =>(
                <h4>id - {subItem.id} seats - {subItem.seatsCount}</h4>
            )))
        }
        </div>
            <input type='number' onChange={event => {props.onClick(event.target.value, setElements)}}/>
            <input type="button" value='clear' onClick={clear}/>
        </div>
    );
}

export default RestaurantByIdItem;