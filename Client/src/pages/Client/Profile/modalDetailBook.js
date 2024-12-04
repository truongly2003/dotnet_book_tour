import { useEffect, useState } from "react";
import { getDetailBookingByUserId } from "../../../services/bookingService";
function ModalDetailBook({ booking, onClose }) {
  console.log(booking);
  const [details, setDetail] = useState({});
  useEffect(() => {
    const fet = async () => {
      try {
        if (booking) {
          const data = await getDetailBookingByUserId(6, booking.bookingId);
          setDetail(data);
        }
      } catch (error) {
        console.error("Error fetching booking details:", error);
      }
    };
    fet();
  }, [booking]);
  
  if (!booking) return null;

  return (
    <div className="modal show d-block" tabIndex="-1">
      <div className="modal-dialog modal-lg">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title text-center w-100">Thông tin đặt chỗ</h5>
            <button
              type="button"
              className="btn-close"
              onClick={onClose}
            ></button>
          </div>
          <div className="modal-body">
            <div className="border-top pt-3">
              <h6>
                <a
                  href={booking.tour_link}
                  target="_blank"
                  rel="noopener noreferrer"
                >
                  {booking.tour_name}
                </a>
              </h6>
              <div className="row">
                <div className="col-6">
                  <p>
                    <strong>Mã tour:</strong> {booking.bookingId}
                  </p>
                  <p>
                    <strong>Thời gian:</strong>{" "}
                    {Math.ceil(
                      (new Date(booking.timeToFinish) -
                        new Date(booking.timeToDeparture)) /
                        (1000 * 60 * 60 * 24)
                    )}{" "}
                    - ngày
                  </p>
                  <p>
                    <strong>Ngày khởi hành:</strong> {booking.timeToDeparture}
                  </p>
                </div>
                <div className="col-6">
                  <p>
                    <strong>Khởi hành từ:</strong> {booking.departureName}
                  </p>
                  <p>
                    <strong>Số khách:</strong> {booking.totalPassengers} {" "}
                  </p>
                  <p>
                    <strong>Ngày kết thúc:</strong> {booking.timeToFinish}
                  </p>
                </div>
              </div>
            </div>

            <div className="border-top pt-3">
              <h6>Thông tin khách hàng</h6>
              <p>
                <strong>Họ tên:</strong> {details.customerName}
              </p>
              <p>
                <strong>Điện thoại:</strong> {details.customerPhone}
              </p>
              <p>
                <strong>Email:</strong>{" "}
                <a href={`mailto:${booking.email}`}>{details.customerEmail}</a>
              </p>
            </div>

            <div className="border-top pt-3">
              <h6>Giá tour chi tiết</h6>
              <table className="table table-bordered">
                <thead>
                  <tr>
                    <th>Nội dung</th>
                    <th>Giá/người</th>
                    <th>Số lượng</th>
                    <th>Thành tiền</th>
                  </tr>
                </thead>
                <tbody>
                  {details.listTicket?.map((item, index) => (
                    <tr key={index}>
                      <td>{item.objectName}</td>
                      <td>{item.price} VND</td>
                      <td>{item.quantity}</td>
                      <td>{item.price * item.quantity} VND</td>
                    </tr>
                  ))}
                </tbody>
                <tfoot>
                  <tr>
                    <th colSpan="3" className="text-end">
                      Tổng giá tour
                    </th>
                    <th>{booking.totalPrice} VND</th>
                  </tr>
                </tfoot>
              </table>
            </div>
          </div>
          <div className="modal-footer">
            <button className="btn btn-secondary" onClick={onClose}>
              Đóng
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ModalDetailBook;
