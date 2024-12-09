import httpRequest from "../utils/httpRequest";

// Create a generic function to handle API calls with error handling
const fetchStatistics = async (endpoint, errorMessage) => {
    try {
        const response = await httpRequest.get(endpoint);
        return response.data.result;
    } catch (error) {
        console.error(errorMessage, error);
        throw error;
    }
};

// Refactored service functions using the generic fetchStatistics function
export const getBookingStatisticsByPaymentStatus = () =>
    fetchStatistics(
        "/statistics/booking-status",
        "Error fetching booking statistics by payment status:"
    );

export const getMonthlyRevenueStatistics = () =>
    fetchStatistics(
        "/statistics/monthly-revenue",
        "Error fetching monthly revenue statistics:"
    );
export const getPopularTourStatistics = () =>
    fetchStatistics(
        "/statistics/popular-tours",
        "Error fetching popular tour statistics:"
    ); // -------------------

export const getCustomerBookingStatistics = () =>
    fetchStatistics(
        "/statistics/customer-bookings",
        "Error fetching customer booking statistics:"
    ); //---------------------

export const getTourRatingStatistics = () =>
    fetchStatistics(
        "/statistics/tour-ratings",
        "Error fetching tour rating statistics:"
    );

export const getRouteTourStatistics = () =>
    fetchStatistics(
        "/statistics/route-tours",
        "Error fetching route tour statistics:"
    );

export const getPassengerTypeStatistics = () =>
    fetchStatistics(
        "/statistics/passenger-types",
        "Error fetching passenger type statistics:"
    );

export const getPassengerAgeGroupStatistics = () =>
    fetchStatistics(
        "/statistics/passenger-age-groups",
        "Error fetching passenger age group statistics:"
    );






//     import axios from 'axios';

// const API_URL = 'api/Statistics'; // Adjust base URL as needed

// export const getMonthlyRevenueStatistics = async () => {
//   const response = await axios.get(`${API_URL}/monthly-revenue`);
//   return response.data;
// };

// export const getBookingStatisticsByPaymentStatus = async () => {
//   const response = await axios.get(`${API_URL}/booking-status`);
//   return response.data;
// };

// export const getTourRatingStatistics = async () => {
//   const response = await axios.get(`${API_URL}/tour-ratings`);
//   return response.data;
// };

// export const getRouteTourStatistics = async () => {
//   const response = await axios.get(`${API_URL}/route-tours`);
//   return response.data;
// };

// export const getPopularTourStatistics = async () => {
//   const response = await axios.get(`${API_URL}/popular-tours`);
//   return response.data;
// };

// export const getCustomerBookingStatistics = async () => {
//   const response = await axios.get(`${API_URL}/customer-bookings`);
//   return response.data;
// };

// export const getPassengerTypeStatistics = async () => {
//   const response = await axios.get(`${API_URL}/passenger-types`);
//   return response.data;
// };

// export const getPassengerAgeGroupStatistics = async () => {
//   const response = await axios.get(`${API_URL}/passenger-age-groups`);
//   return response.data;
// };