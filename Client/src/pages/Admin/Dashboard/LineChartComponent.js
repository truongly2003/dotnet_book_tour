import React, { useState, useEffect } from 'react';
import { Line } from 'react-chartjs-2';
import { Chart, registerables } from 'chart.js';
import zoomPlugin from 'chartjs-plugin-zoom';
import { getMonthlyRevenueStatistics } from '../../../services/statisticsService';

Chart.register(...registerables, zoomPlugin);

function LineChartComponent() {
  const [monthlyData, setMonthlyData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const data = await getMonthlyRevenueStatistics();
        
        const sortedData = [...data].sort((a, b) => {
          if (a.year !== b.year) return a.year - b.year;
          return a.month - b.month;
        });

        setMonthlyData(sortedData.map(item => ({
          name: `${item.month}/${item.year}`,
          revenue: item.totalRevenue / 1000000,
          totalBookings: item.totalBookings
        })));
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

  const data = {
    labels: monthlyData.map(item => item.name),
    datasets: [
      {
        label: 'Doanh Thu (Triệu VND)',
        data: monthlyData.map(item => item.revenue),
        borderColor: '#6366f1',
        backgroundColor: 'rgba(99, 102, 241, 0.2)',
        fill: true,
        tension: 0.4,
      },
    ],
  };

  const options = {
    responsive: true,
    plugins: {
      legend: {
        display: true,
        position: 'top',
      },
      tooltip: {
        callbacks: {
          label: function (context) {
            return `Doanh thu: ${context.raw.toFixed(2)} triệu VND`;
          },
        },
      },
      zoom: {
        pan: {
          enabled: true,
          mode: 'x',
        },
        zoom: {
          wheel: {
            enabled: true,
          },
          pinch: {
            enabled: true,
          },
          mode: 'x',
        },
      },
    },
    scales: {
      x: {
        ticks: {
          maxTicksLimit: 8,
        },
      },
      y: {
        title: {
          display: true,
          text: 'Doanh Thu (Triệu VND)',
        },
      },
    },
  };

  return (
    <div className="bg-white p-6 rounded-xl shadow-lg">
      <h3 className="text-xl font-bold mb-6 text-gray-800">
        Biểu Đồ Doanh Thu Theo Tháng
      </h3>
      <Line data={data} options={options} />
    </div>
  );
}

export default LineChartComponent;