import React, { useState, useEffect } from 'react';
import {
  BarChart, Bar, XAxis, YAxis, CartesianGrid, 
  Tooltip, Legend, ResponsiveContainer
} from 'recharts';
import { getCustomerBookingStatistics } from '../../../services/statisticsService';

function CustomerBookingsChart() {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const response = await getCustomerBookingStatistics();
        
        // Transform and clean data
        const transformedData = response.map((item, index) => ({
          name: item.customerName || `Khách ${index + 1}`,
          bookings: item.totalBookings,
          spent: item.totalSpent / 1000000 // Convert to millions
        }));

        // Sort by total spent
        const sortedData = transformedData.sort((a, b) => b.spent - a.spent);
        
        // Take top 10 customers
        setData(sortedData.slice(0, 10));
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  if (loading) {
    return (
      <div className="flex justify-center items-center h-64">
        <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-blue-500"/>
      </div>
    );
  }

  if (error) {
    return (
      <div className="text-red-500 p-4 text-center border rounded">
        Error: {error}
      </div>
    );
  }

  const CustomTooltip = ({ active, payload, label }) => {
    if (active && payload && payload.length) {
      return (
        <div className="bg-white p-4 rounded-lg shadow-lg border">
          <p className="font-semibold text-gray-800">{label}</p>
          <p className="text-blue-600">
            Chi tiêu: {payload[0].value.toFixed(2)}M VND
          </p>
          <p className="text-green-600">
            Số đơn: {payload[1].value}
          </p>
        </div>
      );
    }
    return null;
  };

  return (
    <div className="bg-white p-6 rounded-xl shadow-lg">
      <h3 className="text-xl font-bold mb-6 text-gray-800">
        Top 10 Khách Hàng Theo Chi Tiêu
      </h3>
      <div style={{ overflowX: 'auto' }}>
        <div style={{ width: `${data.length * 100}px` }}>
          <ResponsiveContainer width="100%" height={400}>
            <BarChart
              data={data}
              margin={{ top: 20, right: 30, left: 20, bottom: 60 }}
            >
              <CartesianGrid strokeDasharray="3 3" stroke="#f0f0f0" />
              <XAxis 
                dataKey="name"
                tick={{ fill: '#666666' }}
                angle={-45}
                textAnchor="end"
                height={60}
              />
              <YAxis 
                yAxisId="left"
                orientation="left"
                stroke="#8884d8"
                label={{ 
                  value: 'Chi Tiêu (Triệu VND)', 
                  angle: -90, 
                  position: 'insideLeft',
                  style: { fill: '#666666' }
                }}
              />
              <YAxis 
                yAxisId="right"
                orientation="right"
                stroke="#82ca9d"
                label={{ 
                  value: 'Số Đơn Đặt', 
                  angle: 90, 
                  position: 'insideRight',
                  style: { fill: '#666666' }
                }}
              />
              <Tooltip content={<CustomTooltip />} />
              <Legend />
              <Bar 
                yAxisId="left"
                dataKey="spent" 
                name="Chi Tiêu" 
                fill="#8884d8" 
                radius={[5, 5, 0, 0]}
              />
              <Bar 
                yAxisId="right"
                dataKey="bookings" 
                name="Số Đơn" 
                fill="#82ca9d"
                radius={[5, 5, 0, 0]}
              />
            </BarChart>
          </ResponsiveContainer>
        </div>
      </div>
    </div>
  );
}

export default CustomerBookingsChart;