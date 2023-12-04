import '../../styles/LobbyItem.css';
import React, {useState} from 'react';

function AllBookingsItem(props) {
    const [elements, setElements] = useState([]);
    const [inputDisable, setInputDisable] = useState(false);

    function clear()
    {
        setInputDisable(false);
        setElements([]);
    }

    return (
        <div className="lobby-item">
            <h1>Bookings</h1>
        <div>
        <table border="1">
        {
            elements.length !== 0 && (
                <>
                    <tr>
                        <th>id</th>
                        <th>clientId</th>
                        <th>clientName</th>
                        <th>tableId</th>
                        <th>restaurantName</th>
                        <th>bookingTime</th>
                    </tr>
                    {elements.map(item =>(
                        <tr key={item.id}>
                        <td>{item.id}</td> 
                        <td>{item.clientId}</td> 
                        <td>{item.clientName}</td> 
                        <th>{item.tableId}</th>
                        <th>{item.restaurantName}</th>
                        <th>{item.bookingTime}</th>
                        </tr>
                    ))}
                </>
            )
        }
        </table>
        </div>
            <input disabled={inputDisable} type="button" value="Get"
             onClick={() => props.onClick(elements, setElements, setInputDisable)}/>
            <input type="button" value='clear' onClick={clear}/>
        </div>
    );
}

export default AllBookingsItem;