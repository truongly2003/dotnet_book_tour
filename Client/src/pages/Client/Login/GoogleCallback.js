import React, { useEffect } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { handleOAuthCallback } from "../../../services/authenticationService";
import { decodeToken } from "../../../services/authenticationService";

const GoogleCallback = ({ provider }) => {
  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    const code = params.get("code");

    if (code) {
      console.log(`Google OAuth2 code received:`, code);

      // Gọi hàm service để xử lý callback
      handleOAuthCallback("google", code)
        .then((response) => {
          console.log("Response from backend:", response);

          // Lấy token từ response
          const token = response.token;
          const user_name = response.name;
          const email = response.email;

          // Lưu token và thông tin người dùng vào localStorage
          localStorage.setItem("token", token);
          localStorage.setItem("username", user_name);
          localStorage.setItem("email", email);

          // Decode token để lấy role
          decodeToken(token)
            .then((decodedData) => {
              const role = decodedData.result?.role_name;
              console.log("Decoded data:", decodedData.result);

              localStorage.setItem("userId", decodedData.result.user_id);
              if (role === "ROLE_CUSTOMER") {
                navigate("/"); // Điều hướng nếu là khách hàng
              } else {
                console.error("Unhandled role:", role);
              }
            })
            .catch((decodeError) => {
              console.error("Error decoding token:", decodeError);
            });
        })
        .catch((error) => {
          console.error(`${provider} login callback error:`, error);
        });
    }
  }, [location, navigate, provider]);

  return <div>Loading...</div>; // Hiển thị khi đang chờ phản hồi
};

export default GoogleCallback;
