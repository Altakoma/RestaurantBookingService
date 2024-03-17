import '../../styles/Welcome.css';
import '../../styles/Lobby.css';
import LobbyHeader from './LobbyHeader';
import AllUsersItem from '../User/AllUsersItem';
import AllRestaurantsItem from '../Restaurant/AllRestaurantsItem';
import { getAll, getById, getAllRoles,
         getRoleById,
         deleteById,
         updateById,
         getRoleOfUser
        } from '../../user.js';
import UserByIdItem from '../User/UserByIdItem';
import UserRoleByIdItem from '../UserRole/UserRoleByIdItem';
import UpdateUserByIdItem from '../User/UpdateUserByIdItem';
import AllUserRoleItem from '../UserRole/AllUserRoleItem';
import DeleteItem from '../Shared/DeleteItem';
import { getAllRestaurants, getRestaurantById } from '../../restaurant';
import RestaurantByIdItem from '../Restaurant/RestaurantByIdItem';
import AllBookingsItem from '../Booking/AllBookingsItem';
import { getAllBookings, postBooking } from '../../booking';
import React, {useState, useEffect} from 'react';
import { getCookie } from '../Login/Login';
import InsertBookingItem from '../Booking/InsertBookingItem';

function Lobby() {
  const [role, setRole] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      const id = getCookie('UserId');
      const userRole = await getRoleOfUser(id);
      setRole(userRole);
    };

    fetchData();
  }, []);

  return (
    <div className='lobby-container'>
      <LobbyHeader />
      <div className="body-container">
      { role === 'Admin' && 
        <div className='container-collector'>
          <AllUsersItem content="Get" onClick={getAll}/>
          <UserByIdItem content="Get" onClick={getById}/>
          <UpdateUserByIdItem content="Get" onClick={updateById}/>
          <DeleteItem onClick={deleteById}/>
        </div>
      }
        
      { role === 'Admin' && 
        <div className='container-collector'>
          <AllUserRoleItem content="Get" onClick={getAllRoles}/>
          <UserRoleByIdItem content="Get" onClick={getRoleById}/>
        </div>
    }

        <div className='container-collector'>
          <AllRestaurantsItem onClick={getAllRestaurants}/>
          <RestaurantByIdItem onClick={getRestaurantById}/>
        </div>

        <div className='container-collector'>
        {
          role === 'Admin' && <AllBookingsItem onClick={getAllBookings}/>
        }
          <InsertBookingItem onClick={postBooking}/>
        </div>
      </div>
      <div className="footer-container">
      </div>
    </div>
  );
}

export default Lobby;