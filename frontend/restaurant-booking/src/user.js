import { getCookie, handleLogin } from './components/Login/Login';

export function get(url, handleData, handleException, elements, setElements) {  
    fetch(url, {
      method: 'GET',
      headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + getCookie('Authorization'), 
      },
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
    })
    .then(responseData => {
        handleData(responseData, elements, setElements);
    })
    .catch((e) => {
        handleException();
    });
}

export function getUser(url) {  
  return fetch(url, {
    method: 'GET',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + getCookie('Authorization'), 
    },
    credentials: "include"
  })
  .then(response => {
    if(response.status === 401 || response.status === 403)
    {
      return '';
    }
    return response.json();
  });
}

export function refresh()
{
  const url = 'http://localhost:8074/api/identity/auth/refresh';

  fetch(url, {
  method: 'POST',
  headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
  },
  credentials: "include"
  })
  .then(response => {
      if(!response.ok)
      {
          throw new Error(`HTTP error! Status: ${response.status}`);
      }
      return response.json()
  })
  .then(responseData => {
      handleLogin(responseData);
  })
}

export function updateItem(url, data) {  
  const jsonData = JSON.stringify(data);
  fetch(url, {
    method: 'PUT',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + getCookie('Authorization'), 
    },
    credentials: "include",
    body: jsonData
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
      return response.json()
  });
}

export function deleteItem(url) {  
  fetch(url, {
    method: 'DELETE',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + getCookie('Authorization'), 
    },
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
      return response.json()
  });
}

export function getAll(elements, setElements)
{
  const url = 'http://localhost:8074/api/identity/user';
  const handleData = (data, elements, setElements) => putAll(data, elements, setElements);
  const handleError = () => console.log("Error");

  get(url, handleData, handleError, elements, setElements);
}

export function getAllRoles(elements, setElements)
{
  const url = 'http://localhost:8074/api/identity/userrole';
  const handleData = (data, elements, setElements) => putAll(data, elements, setElements);
  const handleError = () => console.log("Error");

  get(url, handleData, handleError, elements, setElements);
}

export function getById(id, setElements)
{
  if(id === '')
  {
    setElements([]);
    return;
  }

  const url = "http://localhost:8074/api/identity/user/".concat(id);
  const handleData = (data, id, setElements) => putOne(data, setElements);
  const handleError = () => setElements([]);

  get(url, handleData, handleError, id, setElements);
}

export async function getRoleOfUser(id)
{
  const url = "http://localhost:8074/api/identity/user/".concat(id);

  const result = await getUser(url);
  return result.userRoleName;
}

export function getRoleById(id, setElements)
{
  if(id === '')
  {
    setElements([]);
    return;
  }

  const url = "http://localhost:8074/api/identity/userrole/".concat(id);
  const handleData = (data, id, setElements) => putOne(data, setElements);
  const handleError = () => setElements([]);

  get(url, handleData, handleError, id, setElements);
}

export function updateById(data, id)
{
  const url = "http://localhost:8074/api/identity/user/".concat(id);

  updateItem(url, data);
}

export function deleteById(id)
{
  const url = "http://localhost:8074/api/identity/user/".concat(id);

  deleteItem(url);
}

export function putOne(data, setElements)
{
  setElements([data]);
}

export function putAll(data, elements, setElements)
{
  setElements([...data]);
}