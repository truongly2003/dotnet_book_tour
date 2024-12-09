import React, { useEffect, useState } from "react";
import { exportBookings } from "../../../services/bookingService"; 
import { Link } from "react-router-dom";

function Receipt() {
  const [bookings, setBookings] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchBookings = async () => {
      try {
        const data = await exportBookings();
        setBookings(data);
        setLoading(false);
      } catch (error) {
        console.error("Lỗi khi lấy dữ liệu đặt chỗ:", error);
        setLoading(false);
      }
    };

    fetchBookings();
  }, []);

  if (loading) {
    return <div>Đang tải...</div>;
  }

  return (
    <div>
      <h1>Biên lai đơn hàng</h1>
      <table className="table">
        <thead>
          <tr>
            <th>ID Đặt Chỗ</th>
            <th>Tên Tuyến Chi Tiết</th>
            <th>Trạng Thái Thanh Toán</th>
            <th>Thời Gian Khởi Hành</th>
            <th>Thời Gian Kết Thúc</th>
            <th>Thời Gian Đặt Hàng</th>
            <th>Tổng Số Hành Khách</th>
            <th>Tên Điểm Khởi Hành</th>
            <th>Chi Tiết</th>
          </tr>
        </thead>
        <tbody>
          {bookings.map((booking) => (
            <tr key={booking.bookingId}>
              <td>{booking.bookingId}</td>
              <td>{booking.detailRouteName}</td>
              <td>{booking.paymentStatusName}</td>
              <td>{booking.timeToDeparture}</td>
              <td>{booking.timeToFinish}</td>
              <td>{booking.timeToOrder}</td>
              <td>{booking.totalPassengers}</td>
              <td>{booking.departureName}</td>
              <td>
                <Link to={`/admin/booking/detail/${booking.bookingId}`}>Xem Chi Tiết</Link>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default Receipt;