import { useEffect, useState } from "react";
import axios from "axios";
import PaginationComponent from "../../../components/Pagination";
import ModalDetailBook from "./modalDetailBook";
import { getBookingByUserId } from "../../../services/bookingService";
import { Line } from "recharts";
import { Link } from "react-router-dom";
import { IconButton, Button } from "@mui/material";
import { Visibility, Delete } from "@mui/icons-material";
import Notification from "../../../components/Notification";

function Book() {
  const [bookings, setBooking] = useState([]);
  const [showModal, setShowModal] = useState(true);
  const [selectedBooking, setSelectedBooking] = useState(null);
  //
  const pageSize = 4;
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);

  const [notificationOpen, setNotificationOpen] = useState(false);
  const [notificationMessage, setNotificationMessage] = useState("");
  const [notificationType, setNotificationType] = useState("success");

  useEffect(() => {
    const fetch = async () => {
      var userId = localStorage.getItem("userId");
      try {
        const response = await getBookingByUserId(
          userId,
          currentPage,
          pageSize
        );
        setBooking(response.data);
        setTotalPages(response.totalPages);
      } catch (error) {
        console.error("Error fetching user data:", error);
      }
    };

    fetch();
  }, [currentPage, pageSize]);

  const handlePaymentNow = async (Amount, BookingId) => {
    try {
      const response = await axios.post(
        "https://localhost:7146/api/Payment/create-payment-url",
        {
          Amount: Amount,
          BookingId: BookingId,
        },
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      const paymentUrl = response.data.paymentUrl;
      window.location.href = paymentUrl;
    } catch (error) {
      console.error("Error during payment creation:", error);
    }
  };

  const handleCancelClick = async (bookingId, statusId) => {
    try {
      const url = `https://localhost:7146/api/Booking/cancel-tour?bookingId=${bookingId}&statusId=${statusId}`;

      const response = await fetch(url, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (!response.ok) {
        throw new Error("Network response was not ok");
      }

      const data = await response.json();

      if (data.code === 200) {
        setNotificationMessage("Huỷ đơn hàng thành công");
        setNotificationType("success");
        setNotificationOpen(true);

        // Cập nhật paymentStatusName sau khi huỷ thành công
        setBooking((prevBookings) =>
          prevBookings.map((booking) =>
            booking.bookingId === bookingId
              ? { ...booking, paymentStatusName: "Đã huỷ" }
              : booking
          )
        );

        // Gọi lại hàm fetch() để tải lại danh sách bookings
        var userId = localStorage.getItem("userId");
        const responseBookings = await getBookingByUserId(
          userId,
          currentPage,
          pageSize
        );
        setBooking(responseBookings.data);
        setTotalPages(responseBookings.totalPages);
      } else {
        console.error("Error:", data.message);
        setNotificationMessage("Huỷ đơn hàng thất bại");
        setNotificationType("error");
        setNotificationOpen(true);
      }
    } catch (error) {
      console.error("Error during payment creation:", error);
    }
  };

  const handleShowModal = (bookings) => {
    setSelectedBooking(bookings);
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setSelectedBooking(null);
  };

  const handleCloseNotification = () => setNotificationOpen(false);
  return (
    <div className="container">
      <h5 className="mt-2">Lịch Sử Đặt Chỗ</h5>
      <div>
        {bookings.length > 0 ? (
          <>
            {bookings.map((booking) => (
              <div key={booking.id} className="card border mb-3">
                <div className="card-header order-header d-flex justify-content-between align-items-center">
                  <span className="order-id">
                    Mã đơn hàng:{booking.bookingId}
                  </span>
                  <div className="btn btn-sm btn-check-room">
                    {/* Kiểm tra nếu trạng thái là 'Chờ thanh toán' */}
                    {booking.paymentStatusName === "Chờ thanh toán\r\n" && (
                      <button
                        className="btn btn-outline-info btn-sm btn-pay-now"
                        onClick={() =>
                          handlePaymentNow(
                            booking.totalPrice,
                            booking.bookingId
                          )
                        }
                      >
                        Thanh toán ngay
                      </button>
                    )}
                    <span>Trạng thái: </span>
                    <span
                      className={`fs-6 badge text-wrap ${
                        booking.paymentStatusName === "Chờ thanh toán\r\n"
                          ? "bg-warning"
                          : booking.paymentStatusName === "Đã thanh toán"
                          ? "bg-success"
                          : booking.paymentStatusName === "Đã hủy"
                          ? "bg-secondary"
                          : ""
                      }`}
                    >
                      {booking.paymentStatusName}
                    </span>
                  </div>
                </div>
                <div className="card-body d-flex align-items-center justify-content-between">
                  <div>
                    <h5 className="tour-title mb-2">
                      {booking.detailRouteName}
                    </h5>
                    <span className="mb-2">
                      Ngày đặt : {booking.timeToOrder}
                    </span>
                    <div className="tour-info text-muted mt-2">
                      <div className="d-flex align-items-center mb-2">
                        <i className="far fa-calendar-alt mr-2"></i>
                        <span>
                          {booking.timeToDeparture} → {booking.timeToFinish}
                        </span>
                      </div>
                      <div className="d-flex align-items-center">
                        <i className="fas fa-user mr-2"></i>
                        <span>{booking.totalPassengers} người</span>
                      </div>
                    </div>
                  </div>
                  <div className="d-flex align-items-center">
                    <Button
                      onClick={() => handleShowModal(booking)}
                      startIcon={<Visibility />}
                    ></Button>

                    {/* Button Xoá */}
                    {booking.paymentStatusName === "Chờ thanh toán\r\n" && (
                      <IconButton
                        color="error"
                        style={{ marginLeft: "10px" }}
                        onClick={() => handleCancelClick(booking.bookingId, 3)} // Gọi hàm trong một hàm ẩn danh
                      >
                        <Delete />
                      </IconButton>
                    )}
                  </div>
                </div>
              </div>
            ))}
            <div className="mt-3 mb-4 ">
              <PaginationComponent
                currentPage={currentPage}
                totalPages={totalPages}
                onPageChange={(page) => setCurrentPage(page)}
              />
            </div>
          </>
        ) : (
          <div className="d-flex justify-content-center align-content-center">
            <span>
              Chưa có tour nào được đặt.<Link to="/route">đặt tour ngay</Link>
            </span>
          </div>
        )}
      </div>
      {showModal && (
        <ModalDetailBook booking={selectedBooking} onClose={handleCloseModal} />
      )}
      <Notification
        open={notificationOpen}
        message={notificationMessage}
        onClose={handleCloseNotification}
        type={notificationType}
      />
    </div>
  );
}

export default Book;
