import { useEffect, useState } from "react";
import PaginationComponent from "../../../components/Pagination";
import ModalDetailBook from "./modalDetailBook";
import { getBookingByUserId } from "../../../services/bookingService";
import { Line } from "recharts";
import { Link } from "react-router-dom";

function Book() {
  const [bookings, setBooking] = useState([]);
  const [showModal, setShowModal] = useState(true);
  const [selectedBooking, setSelectedBooking] = useState(null);
  //
  const pageSize = 4;
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);

  useEffect(() => {
    const fetch = async () => {
      try {
        const response = await getBookingByUserId(1, currentPage, pageSize);
        setBooking(response.data);
        setTotalPages(response.totalPages);
      } catch (error) {
        console.error("Error fetching user data:", error);
      }
    };

    fetch();
  }, [currentPage, pageSize]);

  const handleShowModal = (bookings) => {
    setSelectedBooking(bookings);
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setSelectedBooking(null);
  };

  return (
    <div className="container">
      <h5 className="mt-2">Lịch Sử Đặt Chỗ</h5>
      <div>
        {bookings.length > 0 ? (
          <>
            {bookings.map((booking) => (
              <div key={booking.id} className="card border mb-3">
                <div className="card-header order-header d-flex justify-content-between align-items-center">
                  <span className="order-id">Mã đơn hàng:{booking.bookingId}</span>
                  <div className="btn btn-sm btn-check-room ">
                    <span>Trạng thái: </span>
                    <span className="fs-6"> {booking.paymentStatusName}</span>
                  </div>
                </div>
                <div className="card-body d-flex align-items-center justify-content-between">
                  <div>
                    <h5 className="tour-title mb-2">{booking.detailRouteName}</h5>
                    <span className="mb-2">Ngày đặt : {booking.timeToOrder}</span>
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
                  <div>
                    <button
                      className="rounded btn btn-success"
                      onClick={() => handleShowModal(booking)}
                    >
                      Xem chi tiết
                    </button>
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
            <span>Chưa có tour nào được đặt.<Link to="/route">đặt tour ngay</Link></span>
          </div>
        )}
      </div>
      {showModal && (
        <ModalDetailBook booking={selectedBooking} onClose={handleCloseModal} />
      )}
    </div>
  );
}

export default Book;
