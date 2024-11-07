import { useState, useEffect } from "react";
import { getUsers,getTour } from "../../../services/userService";
function Profile() {
  const [users, setUsers] = useState([]);
  const [tours, setTours] = useState([]);

  useEffect(() => {
    const fetUsers = async () => {
      const result = await getUsers();
      setUsers(result.result);
      // console.log(result.result);
    };
    fetUsers();
  },[]);
  
  return (
    <div>
      <h1>Profile</h1>
      <ul>
        {
          
          users.map((item,index)=>(
            <li key={index}>
              {item.username}
            </li>
          ))
        }
      </ul>
    </div>
  )
}

export default Profile;
