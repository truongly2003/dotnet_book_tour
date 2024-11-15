import { useState, useEffect } from "react";
import { getUsers } from "../../../services/userService";
function Profile() {
  const [user, setUser] = useState({
    lastName: "",
    firstName: "",
    email: "",
    phone: "",
    address: "",
    gender: "",
    birthDate: "",
    company: "",
  });

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        const response = await getUsers();
        if (response && response.data) {
          // Cập nhật dữ liệu người dùng
          setUser({
            lastName: response.data.lastName || "",
            firstName: response.data.firstName || "",
            email: response.data.email || "",
            phone: response.data.phone || "",
            address: response.data.address || "",
            gender: response.data.gender || "",
            birthDate: response.data.birthDate || "",
            company: response.data.company || "",
          });
        }
      } catch (error) {
        console.error("Error fetching user data:", error);
      }
    };

    fetchUserData();
  }, []);

  const handleChange = (e) => {
    const { id, value } = e.target;
    setUser((prevUser) => ({
      ...prevUser,
      [id]: value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log("User data submitted:", user);
  };

  return (
    <div className="container">
      <h5 className="mt-2">Thông tin cá nhân</h5>
      <form onSubmit={handleSubmit}>
        <div className="row mb-3">
          <div className="col-md-6">
            <label htmlFor="lastName" className="form-label">
              Họ <span className="text-danger">*</span>
            </label>
            <input
              type="text"
              className="form-control"
              id="lastName"
              placeholder="Họ"
              value={user.lastName}
              onChange={handleChange}
            />
          </div>
          <div className="col-md-6">
            <label htmlFor="firstName" className="form-label">
              Tên
            </label>
            <input
              type="text"
              className="form-control"
              id="firstName"
              placeholder="Tên"
              value={user.firstName}
              onChange={handleChange}
            />
          </div>
        </div>
        <div className="row mb-3">
          <div className="col-md-6">
            <label htmlFor="email" className="form-label">
              Email <span className="text-danger">*</span>
            </label>
            <input
              type="email"
              className="form-control"
              id="email"
              placeholder="Email"
              value={user.email}
              onChange={handleChange}
            />
          </div>
          <div className="col-md-6">
            <label htmlFor="phone" className="form-label">
              Số điện thoại <span className="text-danger">*</span>
            </label>
            <input
              type="text"
              className="form-control"
              id="phone"
              placeholder="Số điện thoại"
              value={user.phone}
              onChange={handleChange}
            />
          </div>
        </div>
      
        <div className="mb-3">
          <label htmlFor="address" className="form-label">
            Địa chỉ
          </label>
          <input
            type="text"
            className="form-control"
            id="address"
            placeholder="Địa chỉ"
            value={user.address}
            onChange={handleChange}
          />
        </div>
        <button type="submit" className="btn btn-primary mb-2">
          Lưu thông tin
        </button>
      </form>
    </div>
  );
}

export default Profile;
