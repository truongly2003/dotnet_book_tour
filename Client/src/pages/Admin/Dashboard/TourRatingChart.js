import React, { useState, useEffect } from 'react';
import { Bar } from 'react-chartjs-2';
import { Chart, registerables } from 'chart.js';
import zoomPlugin from 'chartjs-plugin-zoom';
import { getTourRatingStatistics } from '../../../services/statisticsService';

Chart.register(...registerables, zoomPlugin);

function TourRatingChart() {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await getTourRatingStatistics();
                const transformedData = response.map(item => ({
                    name: item.detailRouteName
                        .replace('Tour Trung Quốc', 'TQ')
                        .replace(/[:-]/g, '')
                        .split(' ')
                        .slice(0, 4)
                        .join(' ') + '...',
                    averageRating: item.averageRating,
                    totalFeedback: item.totalFeedback
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
        return <div className="text-center p-4">Loading...</div>;
    }

    if (error) {
        return <div className="text-center text-red-500 p-4">Error: {error}</div>;
    }

    const chartData = {
        labels: data.map(item => item.name),
        datasets: [
            {
                label: 'Đánh Giá TB',
                data: data.map(item => item.averageRating),
                backgroundColor: '#8884d8',
                borderColor: '#8884d8',
                borderWidth: 1
            },
            {
                label: 'Số Lượt Đánh Giá',
                data: data.map(item => item.totalFeedback),
                backgroundColor: '#82ca9d',
                borderColor: '#82ca9d',
                borderWidth: 1
            }
        ]
    };

    const options = {
        responsive: true,
        plugins: {
            legend: {
                position: 'top',
                labels: {
                    font: {
                        size: 12
                    }
                }
            },
            zoom: {
                pan: {
                    enabled: true,
                    mode: 'x'
                },
                zoom: {
                    wheel: {
                        enabled: true
                    },
                    pinch: {
                        enabled: true
                    },
                    mode: 'x'
                }
            }
        },
        scales: {
            x: {
                ticks: {
                    maxRotation: 45,
                    minRotation: 45,
                    font: {
                        size: 10
                    }
                }
            },
            y: {
                beginAtZero: true,
                max: 5,
                title: {
                    display: true,
                    text: 'Đánh Giá',
                    font: {
                        size: 12
                    }
                },
                ticks: {
                    font: {
                        size: 10
                    }
                }
            }
        }
    };

    return (
        <div className="bg-white p-4 rounded-xl shadow-lg">
            <h3 className="text-xl font-bold mb-4 text-gray-800">
                Đánh Giá Tour
            </h3>
            <div style={{ height: '600px', width: '100%' }}> 
                <Bar data={chartData} options={options} />
            </div>
        </div>
    );
}

export default TourRatingChart;