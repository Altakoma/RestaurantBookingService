import { get } from "./user.js";

export function getAllRestaurants(elements, setElements)
{
    const url = 'http://localhost:8074/api/catalog/restaurant';
    const handleData = (data, elements, setElements) => putAll(data, elements, setElements);
    const handleError = () => console.log("Error");
  
    get(url, handleData, handleError, elements, setElements);
}

export function getRestaurantById(id, setElements)
{
  if(id === '')
  {
    setElements([]);
    return;
  }

  const url = "http://localhost:8074/api/booking/restaurant/".concat(id);
  const handleData = (data, id, setElements) => putOne(data, setElements);
  const handleError = () => setElements([]);

  get(url, handleData, handleError, id, setElements);
}

function putOne(data, setElements)
{
  setElements([data]);
}

function putAll(data, elements, setElements)
{
  setElements([...data]);
}