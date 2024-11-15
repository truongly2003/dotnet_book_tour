import { useState } from "react";
import PaginationComponent from "../../../components/Pagination";
import ModalDetailBook from "./modalDetailBook";
const data = [
  {
    id: "1",
    name: "Tour Nha Trang",
    status: "chờ thanh toán",
    start_date: "20-10-2003",
    end_date: "24-10-2003",
    quantity: "1",
  },
  {
    id: "2",
    name: "Tour Đà Lạt",
    status: "đã thanh toán",
    start_date: "15-11-2003",
    end_date: "19-11-2003",
    quantity: "2",
  },
  {
    id: "3",
    name: "Tour Hạ Long",
    status: "chờ kiểm tra phòng",
    start_date: "05-12-2003",
    end_date: "09-12-2003",
    quantity: "3",
  },
];

function Book() {
  const [showModal, setShowModal] = useState(true); 
  const [selectedBooking, setSelectedBooking] = useState(null); 
  const handleShowModal = (booking) => {
    setSelectedBooking(booking); 
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
        {data.map((booking) => (
          <div key={booking.id} className="card border mb-3">
            <div className="card-header order-header d-flex justify-content-between align-items-center">
              <span className="order-id">Mã đơn hàng: DL00{booking.id}</span>
              <button className="btn btn-sm btn-check-room">{booking.status}</button>
            </div>
            <div className="card-body d-flex align-items-center justify-content-between">
              <div>
                <h5 className="tour-title mb-2">{booking.name}</h5>
                <div className="tour-info text-muted">
                  <div className="d-flex align-items-center mb-2">
                    <i className="far fa-calendar-alt mr-2"></i>
                    <span>
                      {booking.start_date} → {booking.end_date}
                    </span>
                  </div>
                  <div className="d-flex align-items-center">
                    <i className="fas fa-user mr-2"></i>
                    <span>{booking.quantity} người</span>
                  </div>
                </div>
              </div>
              <div>
                <button className="rounded btn btn-success"   onClick={() => handleShowModal(booking)}>Xem chi tiết</button>
              </div>
            </div>
          </div>
        ))}
        <div className="mt-3 mb-4 ">
          <PaginationComponent />
        </div>
      </div>

      {/* modal */}
        {/* Modal hiển thị */}
        {showModal && (
        <ModalDetailBook booking={selectedBooking} onClose={handleCloseModal} />
      )}
    </div>
  );
}

export default Book;
