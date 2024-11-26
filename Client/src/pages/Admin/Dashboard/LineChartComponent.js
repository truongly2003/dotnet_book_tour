import React, { useState, useEffect } from 'react';
// import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  CartesianGrid, 
  Tooltip,
  Legend,
  ResponsiveContainer,
  BarChart,
  Bar
} from 'recharts';
import { 
  getMonthlyRevenueStatistics,
  getBookingStatisticsByPaymentStatus,
  getPopularTourStatistics
} from '../../../services/statisticsService';

// Utility function to shorten route names
const shortenRouteName = (name) => {
  // Remove common prefixes and suffixes
  const cleanedName = name
    .replace(/^Tour\s*/i, '')
    .replace(/\s*Tour$/i, '')
    .replace(/\s*Route$/i, '');
  
  // Split the name and take first few words or characters
  const words = cleanedName.split(/\s+/);
  if (words.length > 2) {
    return words.slice(0, 2).map(word => word.charAt(0)).join('');
  }
  return cleanedName.substring(0, 10);
};

const LineChartComponent = () => {
  const [monthlyData, setMonthlyData] = useState([]);
  const [paymentStatusData, setPaymentStatusData] = useState([]);
  const [popularTourData, setPopularTourData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        
        // Fetch monthly revenue statistics
        const monthlyResult = await getMonthlyRevenueStatistics();
        const chartData = monthlyResult.map(item => ({
          name: `${item.year}-${item.month}`, // Format as YYYY-MM
          totalBookings: item.totalBookings,
          totalRevenue: item.totalRevenue / 1000000, // Convert to millions
        })).sort((a, b) => a.name.localeCompare(b.name));
        
        // Fetch booking statistics by payment status
        const paymentStatusResult = await getBookingStatisticsByPaymentStatus();
        const processedPaymentStatusData = paymentStatusResult.map(item => ({
          statusName: item.statusName,
          totalBookings: item.totalBookings,
          totalRevenue: item.totalRevenue / 1000000, // Convert to millions
        }));

        // Fetch popular tour statistics
        const popularTourResult = await getPopularTourStatistics();
        const processedPopularTourData = popularTourResult.map(item => ({
          detailRouteName: shortenRouteName(item.detailRouteName),
          totalBookings: item.totalBookings,
          totalRevenue: item.totalRevenue / 1000000, // Convert to millions
          averageRating: item.averageRating
        })).sort((a, b) => b.totalBookings - a.totalBookings); // Sort by total bookings
        
        setMonthlyData(chartData);
        setPaymentStatusData(processedPaymentStatusData);
        setPopularTourData(processedPopularTourData);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching statistics:", error);
        setError(error);
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  if (loading) {
    return <div>Loading statistics...</div>;
  }

  if (error) {
    return <div>Error loading statistics: {error.message}</div>;
  }

  return (
    <div className="space-y-6">
      {/* Monthly Revenue Line Chart */}
      <ResponsiveContainer width="100%" height={300} style={{ backgroundColor: "transparent" }}>
        <LineChart
          data={monthlyData}
          margin={{
            top: 5,
            right: 30,
            left: 20,
            bottom: 5,
          }}
        >
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis dataKey="name" />
          <YAxis 
            label={{ value: 'Revenue (triệu đồng)', angle: -90, position: 'insideLeft' }}
          />
          <Tooltip 
            formatter={(value, name) => {
              if (name === 'totalRevenue') {
                return [`${value.toFixed(2)} triệu`, 'Doanh thu'];
              }
              return [value, 'Số booking'];
            }}
          />
          <Legend 
            formatter={(value) => {
              const legendMap = {
                totalBookings: 'Số lượng booking',
                totalRevenue: 'Doanh thu'
              };
              return legendMap[value] || value;
            }}
          />
          <Line 
            type="monotone" 
            dataKey="totalBookings" 
            name="totalBookings"
            stroke="#8884d8" 
            activeDot={{ r: 8 }} 
          />
          <Line 
            type="monotone" 
            dataKey="totalRevenue" 
            name="totalRevenue"
            stroke="#82ca9d" 
          />
        </LineChart>
      </ResponsiveContainer>

      {/* Payment Status Statistics */}
      <div className="bg-white p-4 rounded-lg shadow">
        <h3 className="text-lg font-semibold mb-3">Booking Statistics by Payment Status</h3>
        <div className="grid grid-cols-2 md:grid-cols-3 gap-4">
          {paymentStatusData.map((status, index) => (
            <div 
              key={index} 
              className="p-3 border rounded-lg shadow-sm"
            >
              <p className="font-medium text-gray-700">{status.statusName}</p>
              <p className="text-blue-600">Total Bookings: {status.totalBookings}</p>
              <p className="text-green-600">Total Revenue: {status.totalRevenue.toFixed(2)} triệu</p>
            </div>
          ))}
        </div>
      </div>

      {/* Popular Tours Statistics */}
      <div className="bg-white p-4 rounded-lg shadow">
        <h3 className="text-lg font-semibold mb-3">Popular Tours</h3>
        <ResponsiveContainer width="100%" height={300}>
          <BarChart data={popularTourData}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis 
              dataKey="detailRouteName" 
              angle={-45} 
              textAnchor="end"
              interval={0}
              height={100}
            />
            <YAxis 
              label={{ 
                value: 'Bookings & Revenue (triệu đồng)', 
                angle: -90, 
                position: 'insideLeft' 
              }} 
            />
            <Tooltip 
              formatter={(value, name, props) => {
                if (name === 'totalRevenue') {
                  return [`${value.toFixed(2)} triệu`, 'Doanh thu'];
                }
                return [value, 'Số booking'];
              }}
            />
            <Legend 
              formatter={(value) => {
                const legendMap = {
                  totalBookings: 'Số lượng booking',
                  totalRevenue: 'Doanh thu'
                };
                return legendMap[value] || value;
              }}
            />
            <Bar 
              dataKey="totalBookings" 
              name="totalBookings" 
              fill="#8884d8" 
            />
            <Bar 
              dataKey="totalRevenue" 
              name="totalRevenue" 
              fill="#82ca9d" 
            />
          </BarChart>
        </ResponsiveContainer>
      </div>
    </div>
  );
};

export default LineChartComponent;