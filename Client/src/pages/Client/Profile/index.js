import { useState, useEffect } from "react";
import { getUsers,getTour } from "../../../services/userService";
import axios from "axios";
function Profile() {
  const [users, setUsers] = useState([]);
  const [tours, setTours] = useState([]);

  useEffect(() => {
    const fetUsers = async () => {
      const result = await axios.get("https://localhost:7039/api/Route?page=1&size=10");
      setUsers(result.data);
     
    };
    fetUsers();
  },[]);
  console.log(users);
  return (
    <div>
      <h1>Profile</h1>
      <ul>
     
      </ul>
    </div>
  )
}

export default Profile;
