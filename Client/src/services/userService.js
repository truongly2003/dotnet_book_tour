import httpRequest from "../utils/httpRequest";
export const getUsers = async () => {
  try {
    const token =
      "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJseXRydW9uZyIsInVzZXJfaWQiOjAsInNjb3BlIjoiUk9MRV9BZG1pbiIsImlzcyI6ImhvYW5ndHVhbi5jb20iLCJleHAiOjE3Mjk5MjU2MjgsImlhdCI6MTcyOTkyMjAyOCwianRpIjoiODk3NGQ5ZDgtNzhlMi00NDlhLTg5ODQtOGNmYmJkZTcwZDZhIn0.OJ5h1nvcqq-5h_teLdoH6W97iQeMDyPUk7vbeQvumDhepAPtI02zJ-rH1QLKCPHFoCbqxG43Y-CH4nSwD4TKIg";
    const response = await httpRequest.get("/user", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching user");
    throw error;
  }
};
export const getTour = async () => {
  try {
    const response = await httpRequest.get("/tour");
    return response.data;
  } catch (error) {
    console.error("Error fetching user");
    throw error;
  }
};
export const getListUser = async (page, size) => {
  try {
    const response = await httpRequest.get(`/User?page=${page}&size=${size}`);
    return response.data;
  } catch (error) {
    console.error("Error fetching user");
    throw error;
  }
};
export const getUserByAllSearch = async (username) => {
  try {
    const response = await httpRequest.get(`/User/search?username=${username}`);
    return response.data;
  } catch (error) {}
};

export const getAllRole = async () => {
  try {
    const response = await httpRequest.get(`/Role`);
    return response.data;
  } catch (error) {}
};

export const loginAuthentication = async (username, password) => {
  try {
    const response = await httpRequest.post("/User/login", {
      username,
      password,
    });
    if (response.data.code !== 1000) {
      throw new Error(response.data.message);
    }
    return response.data.result;
  } catch (error) {
    console.error("Error during login:", error);
    throw error;
  }
};


// trưởng
export const GetProfileByUserId = async (id) => {
  try {
    const response = await httpRequest.get(`/User/profile/user?userId=${id}`);
    return response.data;
  } catch (error) {
    console.error("Error fetching user");
    throw error;
  }
};


export const UpdateProfileByUserId = async (userId, data) => {
  try {
    const response = await httpRequest.put(`/User/profile/user/update?userId=${userId}`, data, {
      headers: {
        "Content-Type": "application/json",
      },
    });
    return response.data;
  } catch (error) {
    throw error.response?.data || error.message;
  }
};