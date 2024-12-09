import React, { useState, useEffect } from 'react';
import LineChartComponent from "./LineChartComponent";
import PieChartComponent from "./PieChartComponent";
import CustomerBookingsChart from './CustomerBookingsChart';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUsers, faMoneyBillWave, faStar, faRoute } from '@fortawesome/free-solid-svg-icons';
import {
  getMonthlyRevenueStatistics,
  getBookingStatisticsByPaymentStatus,
  getTourRatingStatistics,
  getRouteTourStatistics
} from '../../../services/statisticsService';

function DashBoard() {
  const [stats, setStats] = useState({
    totalRevenue: 0,
    totalBookings: 0,
    averageRating: 0,
    totalRoutes: 0
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // index.js
  useEffect(() => {
    const fetchStats = async () => {
      try {
        setLoading(true);
        setError(null);

        const [revenue, bookings, ratings, routes] = await Promise.all([
          getMonthlyRevenueStatistics(),
          getBookingStatisticsByPaymentStatus(),
          getTourRatingStatistics(),
          getRouteTourStatistics()
        ]);

        // Calculate total revenue from the result array
        const totalRevenue = revenue?.reduce((sum, item) => sum + item.totalRevenue, 0) || 0;

        setStats(prevStats => ({
          ...prevStats,
          totalRevenue,
          totalBookings: bookings?.reduce((sum, item) => sum + item.totalBookings, 0) || 0,
          averageRating: ratings?.averageRating || 0,
          totalRoutes: routes?.length || 0
        }));

      } catch (error) {
        console.error('Error fetching statistics:', error);
        setError('Failed to load dashboard statistics');
      } finally {
        setLoading(false);
      }
    };

    fetchStats();
  }, []);

  // Update the display in the card
  <h3>
    {loading ? "..." : `${(stats.totalRevenue / 1000000).toFixed(2)}M VND`}
  </h3>

  if (error) {
    return (
      <div className="alert alert-danger m-3" role="alert">
        {error}
      </div>
    );
  }

  return (
    <div className="container-fluid">
      <div className="d-flex justify-content-between mb-4">
        <h6 className="text-lg mb-0">Dashboard Overview</h6>
        <select className="form-select form-select-sm w-auto">
          <option>Year</option>
          <option>Month</option>
          <option>Week</option>
          <option>Day</option>
        </select>
      </div>

      <div className="row g-3 mb-4">
        <div className="col-md-3">
          <div className="card bg-primary text-white h-100">
            <div className="card-body">
              <div className="d-flex justify-content-between">
                <div>
                  <h6 className="text-uppercase">Total Revenue</h6>
                  <h3>{loading ? "..." : `${(stats.totalRevenue / 1000000).toFixed(2)}M VND`}</h3>
                </div>
                <FontAwesomeIcon icon={faMoneyBillWave} size="2x" />
              </div>
            </div>
          </div>
        </div>

        <div className="col-md-3">
          <div className="card bg-success text-white h-100">
            <div className="card-body">
              <div className="d-flex justify-content-between">
                <div>
                  <h6 className="text-uppercase">Total Bookings</h6>
                  <h3>{loading ? "..." : stats.totalBookings}</h3>
                </div>
                <FontAwesomeIcon icon={faUsers} size="2x" />
              </div>
            </div>
          </div>
        </div>

        <div className="col-md-3">
          <div className="card bg-warning text-white h-100">
            <div className="card-body">
              <div className="d-flex justify-content-between">
                <div>
                  <h6 className="text-uppercase">Average Rating</h6>
                  <h3>{loading ? "..." : stats.averageRating.toFixed(1)}/5</h3>
                </div>
                <FontAwesomeIcon icon={faStar} size="2x" />
              </div>
            </div>
          </div>
        </div>

        <div className="col-md-3">
          <div className="card bg-info text-white h-100">
            <div className="card-body">
              <div className="d-flex justify-content-between">
                <div>
                  <h6 className="text-uppercase">Total Routes</h6>
                  <h3>{loading ? "..." : stats.totalRoutes}</h3>
                </div>
                <FontAwesomeIcon icon={faRoute} size="2x" />
              </div>
            </div>
          </div>
        </div>
      </div>

      <div className="row mt-2 g-3">
        <div className="col-lg-7 border p-2 mb-3">
          <LineChartComponent />
        </div>
        <div className="col-lg-5 border p-2 mb-3">
          <PieChartComponent />
        </div>
      </div>

      <div className="row mt-4">
        <div className="col-12">
          <CustomerBookingsChart />
        </div>
      </div>
      
    </div>
  );
}

export default DashBoard;



// import LineChartComponent from "./LineChartComponent";
// import PieChartComponent from "./PieChartComponent";

// function DashBoard() {
//   return (
//     <div className=" ">
//       <div className="d-flex justify-content-between">
//         <h6 className="text-lg mb-0">Doanh Thu</h6>
//         <select className="form-select form-select-sm w-auto">
//           <option>Năm</option>
//           <option>Tháng</option>
//           <option>Tuần</option>
//           <option>Ngày</option>
//         </select>
//       </div>
//       <div className="row mt-2 g-3">
//         <div className="col-lg-7 border p-2 mb-3" style={{ height: "300px" }}>
//           <LineChartComponent />
//         </div>
//         <div className="col-lg-5 border p-2 mb-3" style={{ height: "300px" }}>
//           <PieChartComponent />
//         </div>
//       </div>
//     </div>
//   );
// }

// export default DashBoard;
