import httpRequest from "../utils/httpRequest";

// Lấy tất cả DetailRoute với phân trang
export const getAllRoutes = async (page, size) => {
  try {
    const response = await httpRequest.get(
      `https://localhost:7146/api/DetailRoute?page=${page}&size=${size}`
    );
    console.log("Response data:", response.data);
    return response.data;
  } catch (error) {
    console.error("Error fetching routes:", error);
    throw error;
  }
};

export const getTourUpdate = async(detailId) => {
  try {
    const response = await httpRequest.get(`https://localhost:7146/api/DetailRoute/${detailId}`);
    return response.data;
  }
  catch (error)
  {
    console.error("Error fetching detail routes:", error);
    throw error;
  }
}

// Thêm một DetailRoute mới
export const addTour = async (detailRouteData) => {
  try {
    const response = await httpRequest.post(`https://localhost:7146/api/DetailRoute/Insert`, detailRouteData);
    return response.data; // Trả về kết quả từ backend
  } catch (error) {
    console.error("Error adding detail route:", error);
    throw error;
  }
};

// Cập nhật một DetailRoute
export const updateTour = async (detailRouteId, detailRouteData) => {
  try {
    const response = await httpRequest.put(`https://localhost:7146/api/DetailRoute/Update/${detailRouteId}`, detailRouteData); // Đúng endpoint là /DetailRoute/{id}
    return response.data; // Trả về kết quả từ backend
  } catch (error) {
    console.error("Error updating detail route:", error);
    throw error;
  }
};

export const isCheckBooking = async (detailRouteId) => {
  try {
      const response = await httpRequest.get(`https://localhost:7146/api/DetailRoute/CheckExist/${detailRouteId}`);
      if (response.status === 200) {
          return response.data; // OK
      } else {
          console.warn("Request failed:", response.data);
          throw new Error(response.data?.message || "Request failed.");
      }
  } catch (error) {
      console.error("Error checking detail route:", error);
      throw new Error(error.response?.data?.message || "Server error.");
  }
};


// Xóa một DetailRoute
export const deleteTour = async (detailRouteId) => {
  try {
    const response = await httpRequest.delete(`https://localhost:7146/api/DetailRoute/Delete/${detailRouteId}`); // Đúng endpoint là /DetailRoute/{id}
    return response.data; // Trả về kết quả từ backend
  } catch (error) {
    console.error("Error deleting detail route:", error);
    throw error;
  }
};

