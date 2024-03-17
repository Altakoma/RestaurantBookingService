import '../../styles/LobbyItem.css';
import React, {useState} from 'react';

function AllRestaurantsItem(props) {
    const [elements, setElements] = useState([]);

    function clear()
    {
        setElements([]);
    }

    return (
        <div className="lobby-item">
            <h1>Restaurants</h1>
        <div>
        <table border="1">
        {
            elements.length !== 0 && (
                <>
                    <tr>
                        <th>id</th>
                        <th>name</th>
                        <th>city</th>
                        <th>street</th>
                        <th>house</th>
                    </tr>
                    {elements.map(item =>(
                        <tr key={item.id}>
                        <td>{item.id}</td> 
                        <td>{item.name}</td> 
                        <th>{item.city}</th>
                        <th>{item.street}</th>
                        <th>{item.house}</th>
                        </tr>
                    ))}
                </>
            )
        }
        </table>
        </div>
            <input type="button" value="Get" onClick={() => props.onClick(elements, setElements)}/>
            <input type="button" value='clear' onClick={clear}/>
        </div>
    );
}

export default AllRestaurantsItem;