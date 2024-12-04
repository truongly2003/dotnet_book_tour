import httpRequest from "../utils/httpRequest";

export const getRouteDetailById = async (id) => {
    try {
        const response = await httpRequest.get(`/Booking/${id}`);
        return response.data;
    } catch (error) {
        console.error("Error fetching route");
    }
};

export const handleBooking = async (payload) => {
    try {
        const response = await httpRequest.post(`/Booking/handle-booking`, payload);
        return response.data;
    } catch (error) {
        console.error("Error handling booking:", error);
        throw error; // Rethrow to handle it in the caller
    }
};

export const getBookingByUserId = async (id,page,size) => {
    try {
        const response = await httpRequest.get(`/Booking/profile/user?UserId=${id}&page=${page}&size=${size}`);
        return response.data;
    } catch (error) {
        console.error("Error fetching route");
    }
};

export const getDetailBookingByUserId = async (userId,BookingId) => {
    try {
        const response = await httpRequest.get(`/Booking/profile/detail?UserId=${userId}&BookingId=${BookingId}`);
        return response.data;
    } catch (error) {
        console.error("Error fetching route");
    }
};

export const checkAvailablequantity = async (detailRouteId, totalPassengers) => {
    try {
        const response = await httpRequest.get('/Booking/check-available-quantity', {
            params: {
                detailRouteId: detailRouteId,
                totalPassengers: totalPassengers
            }
        });
        return response.data; // Trả về dữ liệu từ API
    } catch(error) {
        console.error("Error fetching booking", error);
        throw error;
    }
};
