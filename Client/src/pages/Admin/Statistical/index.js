// // Statistical/index.js
// import React, { useEffect, useState } from "react";
// import DashBoard from "../Dashboard"; // Adjust the path as needed
// import { getBookingStatisticsByPaymentStatus } from "../../../services/statisticsService"; // Adjust the path if necessary

// const Statistical = () => {
//   const [data, setData] = useState([]);
//   const [loading, setLoading] = useState(true);
//   const [error, setError] = useState(null);

//   // Function to fetch data
//   const fetchData = async () => {
//     setLoading(true); // Show loading state
//     setError(null);   // Reset previous errors
//     try {
//       const result = await getBookingStatisticsByPaymentStatus();
//       const chartData = result.map((item, index) => ({
//         name: item.statusName.trim(), // Use the status name from API
//         totalBookings: item.totalBookings,
//         totalRevenue: item.totalRevenue,
//       }));
//       setData(chartData); // Save data to state
//     } catch (err) {
//       setError("Failed to fetch booking statistics"); // Set error message
//       console.error("Error fetching data in Statistical:", err);
//     } finally {
//       setLoading(false); // Hide loading state
//     }
//   };

//   // Fetch data when component mounts
//   useEffect(() => {
//     fetchData();
//   }, []);

//   if (loading) return <p>Loading...</p>; // Show loading indicator
//   if (error) return <p>Error: {error}</p>; // Show error message

//   return (
//     <div>
//       <h2>Booking Statistics</h2>
//       <DashBoard bookingData={data} />
//     </div>
//   );
// };

// export default Statistical;