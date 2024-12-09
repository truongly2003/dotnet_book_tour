import React, { useEffect, useState } from "react";
import { getBookingDetailById } from "../../../services/bookingService"; // Điều chỉnh đường dẫn nếu cần
import { useParams } from "react-router-dom";

function BookingDetail() {
    const { bookingId } = useParams();
    const [bookingDetail, setBookingDetail] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchBookingDetail = async () => {
            try {
                const data = await getBookingDetailById(bookingId);
                setBookingDetail(data);
                setLoading(false);
            } catch (error) {
                console.error("Lỗi khi lấy chi tiết đặt chỗ:", error);
                setLoading(false);
            }
        };

        fetchBookingDetail();
    }, [bookingId]);

    if (loading) {
        return <div>Đang tải...</div>;
    }

    if (!bookingDetail) {
        return <div>Không tìm thấy chi tiết đặt chỗ</div>;
    }

    return (
        <div>
            <h1>Chi tiết đặt chỗ</h1>
            <p>Tên Khách Hàng: {bookingDetail.customerName}</p>
            <p>Email Khách Hàng: {bookingDetail.customerEmail}</p>
            <p>Số Điện Thoại Khách Hàng: {bookingDetail.customerPhone}</p>
            <h2>Danh Sách Vé</h2>
            <table className="table">
                <thead>
                    <tr>
                        <th>Tên Đối Tượng</th>
                        <th>Số Lượng</th>
                        <th>Giá</th>
                        <th>Tổng Giá</th>
                    </tr>
                </thead>
                <tbody>
                    {bookingDetail.listTicket.map((ticket, index) => (
                        <tr key={index}>
                            <td>{ticket.objectName}</td>
                            <td>{ticket.quantity}</td>
                            <td>{ticket.price}</td>
                            <td>{ticket.totalPrice}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default BookingDetail;