import React from 'react';

function UserRoleItem(props) {
    return (
        <div>
            <table border="1">
            {
                props.content.length !== 0 && (
                    <>
                    <tr>
                        <th>id</th>
                        <th>name</th>
                    </tr>
                    {props.content.map(item => (
                        <tr key={item.id}>
                        <td>{item.id}</td> 
                        <td>{item.name}</td> 
                        </tr>
                    ))}
                    </>
                )
            }
            </table>
        </div>
    );
}

export default UserRoleItem;