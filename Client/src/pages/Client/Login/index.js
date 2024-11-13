import {
    Box,
    Button,
    Card,
    CardContent,
    Divider,
    TextField,
    Typography,
    Snackbar,
    Alert,
    InputAdornment,
    IconButton,
  } from "@mui/material";
  import { useEffect, useState } from "react";
  import { useNavigate } from "react-router-dom";
  import Visibility from "@mui/icons-material/Visibility";
  import VisibilityOff from "@mui/icons-material/VisibilityOff";
  import { FaFacebook, FaGoogle } from "react-icons/fa"; // Import Facebook icon from react-icons
  import { loginAuthentication } from "../../../services/userService";
  export default function Login() {
    const navigate = useNavigate();
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [showPassword, setShowPassword] = useState(false);
    const [snackBarOpen, setSnackBarOpen] = useState(false);
    const [snackBarMessage, setSnackBarMessage] = useState("");
    const [snackBarSeverity, setSnackBarSeverity] = useState("success");
    const [errorMessage, setErrorMessage] = useState("");
  
    const handleCloseSnackBar = (event, reason) => {
      if (reason === "clickaway") {
        return;
      }
      setSnackBarOpen(false);
    };
  
    const validateForm = () => {
      if (!username.trim()) {
        setErrorMessage("Username không được để trống");
        return false;
      }
      if (!password.trim()) {
        setErrorMessage("Password không được để trống");
        return false;
      }
      if (password.length < 6) {
        setErrorMessage("Password phải có ít nhất 6 ký tự");
        return false;
      }
      return true;
    };
  
    useEffect(() => {
      const token = localStorage.getItem("token");
      if (token) {
        const roleId = localStorage.getItem("roleId");
        if (roleId === "1" || roleId === "2") {
          navigate("/admin/dashboard");
        } else if (roleId === "3") {
          navigate("/");
        }
      }
    }, [navigate]);
  
    const handleSubmit = async (event) => {
      event.preventDefault();
    
      if (!validateForm()) return;
    
      try {
        const result = await loginAuthentication(username, password);
    
        // Save user data to local storage
        localStorage.setItem("userId", result?.id);
        localStorage.setItem("token", result?.token);
        localStorage.setItem("username", result?.username);
        localStorage.setItem("roleId", result?.roleId);
        localStorage.setItem("email", result?.email);
    
        setSnackBarMessage("Đăng nhập thành công!");
        setSnackBarSeverity("success");
        setSnackBarOpen(true);
    
        setTimeout(() => {
          if (result.roleId === 1 || result.roleId === 2) {
            navigate("/admin/dashboard");
          } else if (result.roleId === 3) {
            navigate("/");
          }
        }, 3000);
      } catch (error) {
        setSnackBarMessage(error.message);
        setSnackBarSeverity("error");
        setSnackBarOpen(true);
      }
    };
    
  
    const handleGoogleLogin = () => {
    //   const callbackUrl = OAuthConfig.redirectUri;
    //   const authUrl = OAuthConfig.authUri;
    //   const googleClientId = OAuthConfig.clientId;
  
    //   const targetUrl = `${authUrl}?redirect_uri=${encodeURIComponent(
    //     callbackUrl
    //   )}&response_type=code&client_id=${googleClientId}&scope=openid%20email%20profile`;
  
    //   // Uncomment this when you are ready for Google login flow
    //   window.location.href = targetUrl;
    };
  
    const handleNavigateToRegister = () => {
      navigate("/register");
    };
  
    const togglePasswordVisibility = () => {
      setShowPassword((prevShowPassword) => !prevShowPassword);
    };
  
    const handleFacebookLogin = () => {
      // Implement Facebook login here (using a service or SDK)
    };
  
    return (
      <>
        <Snackbar
          open={snackBarOpen}
          onClose={handleCloseSnackBar}
          autoHideDuration={3000}
          anchorOrigin={{ vertical: "top", horizontal: "right" }}
        >
          <Alert
            onClose={handleCloseSnackBar}
            severity={snackBarSeverity}
            variant="filled"
            sx={{ width: "100%" }}
          >
            {snackBarMessage}
          </Alert>
        </Snackbar>
  
        <Box
          display="flex"
          flexDirection="column"
          alignItems="center"
          justifyContent="center"
          height="100vh"
          bgcolor={"#f0f2f5"}
        >
          <Card
            sx={{
              minWidth: 400,
              maxWidth: 500,
              boxShadow: 4,
              borderRadius: 4,
              padding: 4,
            }}
          >
            <CardContent>
              <Typography variant="h5" component="h1" gutterBottom>
                Welcome
              </Typography>
              <Box
                component="form"
                display="flex"
                flexDirection="column"
                alignItems="center"
                justifyContent="center"
                width="100%"
                onSubmit={handleSubmit}
              >
                <TextField
                  label="Username"
                  variant="outlined"
                  fullWidth
                  margin="normal"
                  value={username}
                  onChange={(e) => setUsername(e.target.value)}
                />
                <TextField
                  label="Password"
                  type={showPassword ? "text" : "password"}
                  variant="outlined"
                  fullWidth
                  margin="normal"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  InputProps={{
                    endAdornment: (
                      <InputAdornment position="end">
                        <IconButton onClick={togglePasswordVisibility} edge="end">
                          {showPassword ? <VisibilityOff /> : <Visibility />}
                        </IconButton>
                      </InputAdornment>
                    ),
                  }}
                />
                <Button
                  type="submit"
                  variant="contained"
                  color="primary"
                  size="large"
                  fullWidth
                  sx={{
                    mt: "15px",
                    mb: "25px",
                  }}
                >
                  Login
                </Button>
                <Divider />
              </Box>
  
              <Box
                display="flex"
                flexDirection="row"
                width="100%"
                gap="15px"
                justifyContent="space-between"
              >
                <Button
                  type="button"
                  variant="contained"
                  color="secondary"
                  size="large"
                  onClick={handleGoogleLogin}
                  fullWidth
                  sx={{ gap: "10px" }}
                >
                  <FaGoogle />
                  Google
                </Button>
                <Button
                  type="button"
                  variant="contained"
                  color="primary"
                  size="large"
                  onClick={handleFacebookLogin}
                  fullWidth
                  sx={{ gap: "10px" }}
                >
                  <FaFacebook style={{ fontSize: "24px" }} />
                  Facebook
                </Button>
              </Box>
  
              <Button
                type="button"
                variant="contained"
                color="success"
                size="large"
                onClick={handleNavigateToRegister}
                fullWidth
                sx={{
                  mt: "20px",
                }}
              >
                Create an account
              </Button>
            </CardContent>
          </Card>
        </Box>
      </>
    );
  }