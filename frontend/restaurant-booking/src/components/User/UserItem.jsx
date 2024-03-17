import React from 'react';

function UserItem(props) {
    return (
        <div>
            <table border="1">
            {
                props.content.length !== 0 && (
                    <>
                    <tr>
                        <th>id</th>
                        <th>name</th>
                        <th>role</th>
                        <th>login</th>
                    </tr>
                    {props.content.map(item =>(
                        <tr key={item.id}>
                        <td>{item.id}</td> 
                        <td>{item.name}</td> 
                        <td>{item.userRoleName}</td> 
                        <td>{item.login}</td> 
                        </tr>
                    ))}
                    </>
                )
            }
            </table>
        </div>
    );
}

export default UserItem;