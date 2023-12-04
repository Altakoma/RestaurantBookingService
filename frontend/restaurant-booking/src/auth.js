import { refresh } from "./user";

export function postAuthData(url, data, handleData, handleException) {  
    fetch(url, {
      method: 'POST',
      headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
      },
      credentials: "include",
      body: data
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
    })
    .then(responseData => {
        handleData(responseData);
    })
    .catch((e) => {
        handleException();
    });
}