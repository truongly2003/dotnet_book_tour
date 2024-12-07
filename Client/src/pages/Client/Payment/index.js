import { useEffect, useState } from 'react';
import './Payment.css';

const Payment = () => {
  const [paymentInfo, setPaymentInfo] = useState(null);

  useEffect(() => {
    // Lấy query params từ URL
    const urlParams = new URLSearchParams(window.location.search);
    const paymentData = {};
    urlParams.forEach((value, key) => {
      paymentData[key] = value;
    });

    // Lưu thông tin thanh toán vào state
    setPaymentInfo(paymentData);
  }, []);

  if (!paymentInfo) {
    return <div className="loading">Loading...</div>;
  }

  return (
    <div className="payment-container">
      <div className="payment-header">
        <h2>Thông tin hoá đơn</h2>
      </div>
      <div className="payment-details">
        <div className="info-row">
          <div className="label">Mã giao dịchr:</div>
          <div className="value">{paymentInfo.bankTranNo}</div>
        </div>
        <div className="info-row">
          <div className="label">Thông tin đơn hàng:</div>
          <div className="value">{paymentInfo.orderInfo}</div>
        </div>
        <div className="info-row">
          <div className="label">Tổng tiền:</div>
          <div className="value">{paymentInfo.amount}</div>
        </div>
        <div className="info-row">
          <div className="label">Trạng thái:</div>
          <div className="value">{paymentInfo.transactionStatus === '00' ? 'Thành công' : 'Thất bại'}</div>
        </div>
        <div className="info-row">
          <div className="label">Ngân hàng:</div>
          <div className="value">{paymentInfo.bankCode}</div>
        </div>
        <div className="info-row">
          <div className="label">Ngày giao dịch:</div>
          <div className="value">{paymentInfo.transactionDate}</div>
        </div>
      </div>
      <div className="payment-footer">
        <button className="back-button" onClick={() => window.location.href = 'http://localhost:3000/profile/book'}>
          Trở về
        </button>
      </div>
    </div>
  );
};

export default Payment;
