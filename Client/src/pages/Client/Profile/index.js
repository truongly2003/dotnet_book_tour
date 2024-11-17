import { useState, useEffect } from "react";
import {
  GetProfileByUserId,
  UpdateProfileByUserId,
} from "../../../services/userService";
function Profile() {
  const [user, setUser] = useState({
    name: "",
    email: "",
    phone: "",
    address: "",
  });
  const [errors, setErrors] = useState({});
  const [reload, setReload] = useState(false);
  useEffect(() => {
    const fetchUserData = async () => {
      try {
        const response = await GetProfileByUserId(6);
        console.log(response);
        if (response) {
          setUser({
            name: response.name || "",
            email: response.email || "",
            phone: response.phone || "",
            address: response.address || "",
          });
        }
      } catch (error) {
        console.error("Error fetching user data:", error);
      }
    };

    fetchUserData();
  }, [reload]);
  const validate = () => {
    const newErrors = {};
    if (!user.name.trim()) {
      newErrors.name = "Họ tên không được để trống.";
    }
    if (!user.phone.trim()) {
      newErrors.phone = "Số điện thoại không được để trống.";
    } else if (!/^\d{10,11}$/.test(user.phone)) {
      newErrors.phone =
        "Số điện thoại không hợp lệ. Chỉ chấp nhận 10-11 chữ số.";
    }

    if (!user.email.trim()) {
      newErrors.email = "Email không được để trống.";
    } else if (!/\S+@\S+\.\S+/.test(user.email)) {
      newErrors.email = "Email không hợp lệ.";
    }
    if (!user.address.trim()) {
      newErrors.address = "Địa chỉ không được để trống.";
    }

    return newErrors;
  };

  const handleChange = (e) => {
    const { id, value } = e.target;
    setUser((prevUser) => ({
      ...prevUser,
      [id]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const validationErrors = validate();
    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
      return;
    }
    try {
      const response = await UpdateProfileByUserId(6, user);
      alert("Thông tin đã được cập nhật thành công!");
      setReload(true);
      setErrors({});
      console.log(response);
    } catch (error) {
      console.error("Error updating user data:", error);
      alert("Đã xảy ra lỗi khi cập nhật thông tin.");
    }
  };

  return (
    <div className="container">
      <h5 className="mt-2">Thông tin cá nhân</h5>
      <form onSubmit={handleSubmit}>
        <div className="row mb-3">
          <div className="col-md-6">
            <label htmlFor="name" className="form-label">
              Họ Tên<span className="text-danger">*</span>
            </label>
            <input
              type="text"
              className={`form-control ${errors.name ? "is-invalid" : ""}`}
              id="name"
              placeholder="Họ"
              value={user.name}
              onChange={handleChange}
            />
            {errors.name && (
              <div className="invalid-feedback">{errors.name}</div>
            )}
          </div>
          <div className="col-md-6">
            <label htmlFor="phone" className="form-label">
              Số điện thoại <span className="text-danger">*</span>
            </label>
            <input
              type="text"
              className={`form-control ${errors.phone ? "is-invalid" : ""}`}
              id="phone"
              placeholder="Số điện thoại"
              value={user.phone}
              onChange={handleChange}
            />
            {errors.phone && (
              <div className="invalid-feedback">{errors.phone}</div>
            )}
          </div>
        </div>
        <div className="row mb-3">
          <div className="col-md-12">
            <label htmlFor="email" className="form-label">
              Email <span className="text-danger">*</span>
            </label>
            <input
              type="email"
              className={`form-control ${errors.email ? "is-invalid" : ""}`}
              id="email"
              placeholder="Email"
              value={user.email}
              onChange={handleChange}
            />
            {errors.email && (
              <div className="invalid-feedback">{errors.email}</div>
            )}
          </div>
        </div>

        <div className="mb-3">
          <label htmlFor="address" className="form-label">
            Địa chỉ
          </label>
          <input
            type="text"
            className={`form-control ${errors.address ? "is-invalid" : ""}`}
            id="address"
            placeholder="Địa chỉ"
            value={user.address}
            onChange={handleChange}
          />
          {errors.address && (
            <div className="invalid-feedback">{errors.address}</div>
          )}
        </div>
        <div className="">
          <button type="submit" className="btn btn-primary mb-2">
            Lưu thông tin
          </button>
        </div>
      </form>
    </div>
  );
}

export default Profile;
