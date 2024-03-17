import { get, refresh } from "./user.js";
import { getCookie } from './components/Login/Login';
import * as signalR from '@microsoft/signalr';

export function getAllBookings(elements, setElements, setInputDisable)
{
    setInputDisable(true);
    const url = 'http://localhost:8074/api/booking/booking';
    const handleData = (data, elements, setElements) => putAll(data, elements, setElements);
    const handleError = () => console.log("Error");
  
    get(url, handleData, handleError, elements, setElements);

    connectToSignalR(elements, setElements, setInputDisable);
}

function connectToSignalR(elements, setElements, setInputDisable)
{
  try
  {
    const connection = new signalR.HubConnectionBuilder()  
    .withUrl("http://localhost:8072/bookinghub")  
    .build();  

    connection.on("GetBookingMessageAsync", (messageType, bookingMessage) => {
        if(messageType === "Insert")
        {
            let item = JSON.parse(bookingMessage);

            let booking = {
                id: item.Id,
                clientId: item.ClientId,
                clientName: item.ClientName,
                tableId: item.TableId,
                restaurantName: item.RestaurantName,
                bookingTime: item.BookingTime,
            };
            
            if(!elements.includes(booking))
            {
                setElements(prevElements => {
                    return [...prevElements, booking];
                });
            }
        }
    });  
    
    connection.start();
  }
  catch
  {
    setInputDisable(false);
  }
}

export function post(url, data) {  
  fetch(url, {
    method: 'POST',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + getCookie('Authorization'), 
    },
    body: data,
    credentials: "include"
  })
  .then(response => {
    if(response.status === 401 || response.status === 403)
    {
      refresh();
      return;
    }
    if(!response.ok)
    {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    return response.json();
  });
}

export function postBooking(booking)
{
  let dateTime = booking.date + 'T' + booking.time + ":00Z";

  let insertBookingDTO = {
    tableId: booking.tableId,
    bookingTime: dateTime
  }

  const jsonData = JSON.stringify(insertBookingDTO);
  const url = 'http://localhost:8074/api/booking/booking';

  post(url, jsonData);
}

function putAll(data, elements, setElements)
{
  setElements([...data]);
}