import React, { useState, useEffect } from 'react';
import { PieChart, Pie, Cell, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import { getPassengerTypeStatistics } from '../../../services/statisticsService';

const COLORS = ['#0088FE', '#00C49F'];

function PieChartComponent() {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await getPassengerTypeStatistics();
        const transformedData = response.map(item => ({
          name: item.objectName,
          value: item.totalPassengers
        }));
        setData(transformedData);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <div className="bg-white p-6 rounded-xl shadow-lg">
      <h3 className="text-xl font-bold mb-6 text-gray-800">
        Phân Loại Hành Khách
      </h3>
      <ResponsiveContainer width="100%" height={290}>
        <PieChart>
          <Pie
            data={data}
            cx="50%"
            cy="50%"
            labelLine={false}
            outerRadius={90}
            fill="#8884d8"
            dataKey="value"
            label={({ name, percent }) => `${name}: ${(percent * 100).toFixed(0)}%`}
          >
            {data.map((entry, index) => (
              <Cell 
                key={`cell-${index}`} 
                fill={COLORS[index % COLORS.length]}
              />
            ))}
          </Pie>
          <Tooltip />
          <Legend />
        </PieChart>
      </ResponsiveContainer>
    </div>
  );
}

export default PieChartComponent;