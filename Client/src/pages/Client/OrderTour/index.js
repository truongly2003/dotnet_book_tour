import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { getRouteDetailById } from '../../../services/bookingService';
import { handleBooking } from '../../../services/bookingService';
import { getAllPaymentMethods} from "../../../services/PaymentService";
import { checkAvailablequantity} from "../../../services/bookingService";
import Notification from "../../../components/Notification";

import ConfirmationNumberOutlinedIcon from '@mui/icons-material/ConfirmationNumberOutlined';
import CalendarMonthOutlinedIcon from '@mui/icons-material/CalendarMonthOutlined';
import StarBorderPurple500OutlinedIcon from '@mui/icons-material/StarBorderPurple500Outlined';

function BookingTour() {
    const { id } = useParams();
    const [tour, setTour] = useState(null);
    const [loading, setLoading] = useState(true);
    const [listPaymentMethod, setListPaymentMethod] = useState([]);
    const userId = localStorage.getItem('userId');
    const [notificationOpen, setNotificationOpen] = useState(false);
    const [notificationMessage, setNotificationMessage] = useState("");
    const [notificationType, setNotificationType] = useState("success");
    const [validateList, setValidateList] = useState([]);

    const [formData, setFormData] = useState({
        total_price: 0,
        detailRouteId: id,
        userId: userId || '',
        customerName: '',
        customerEmail: '',
        customerAddress: '',
        customerPhone: '',
        paymentMethod: 0,
        adults: 1,
        children: '0',
        infants: '0'
    });

    const totalPrice = () => {
        const adultPrice = tour.result.price;
        const childPrice = tour.result.price * 0.8;
        const infantPrice = tour.result.price * 0.5;
        return (formData.adults * adultPrice) + (formData.children * childPrice) + (formData.infants * infantPrice);
    };

    const [passengerRequestList, setPassengers] = useState([{ passengerName: '', passengerGender: 'Nam', passengerDateBirth: '', passengerObjectId: 1 }]);

    const payload = {
        ...formData,
        passengerRequestList,
    }

    useEffect(() => {
        if (tour) {
            const calculatedTotalPrice = totalPrice(); 
            setFormData(prevState => ({
                ...prevState,
                total_price: calculatedTotalPrice  
            }));
        }
    }, [formData.adults, formData.children, formData.infants, tour]);  

    const updatePassengerCount = (type, count) => {
        const newCount = Math.max(0, count);
        setFormData((prev) => ({ ...prev, [type]: newCount }));

        let updatedPassengers = [...passengerRequestList];
        const totalPassengers = formData.adults + formData.children + formData.infants;
        const difference = newCount - formData[type];

        if (difference > 0) {
            for (let i = 0; i < difference; i++) {
                const objectId = getPassengerType(type); 
                updatedPassengers.push({ passengerName: '', passengerGender: 'Nam', passengerDateBirth: '', passengerObjectId: objectId });
            }
        } else if (difference < 0) {
            updatedPassengers.splice(totalPassengers + difference, -difference);
        }

        setPassengers(updatedPassengers);
    };

    const getPassengerType = (type) => {
        switch (type) {
            case 'adults':
                return 1;
            case 'children':
                return 2;
            case 'infants':
                return 3;
            default:
                return null;
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handlePassengerChange = (index, event) => {
        const { name, value } = event.target;

        setPassengers((prevPassengers) => {
            const updatedPassengers = [...prevPassengers];
            updatedPassengers[index] = { ...updatedPassengers[index], [name]: value };
            return updatedPassengers;
        });
    };

    const getTotalPassengers = () => {
        return (
          Number(formData.adults) +
          Number(formData.children) +
          Number(formData.infants)
        );
    };

    const checkAvailability = async () => {
        try {
            const totalPassengers = getTotalPassengers() + 1; 
            const detailRouteId = id;  
            const data = await checkAvailablequantity(detailRouteId, totalPassengers);

            return data; 
        } catch (error) {
            console.error("Error checking availability:", error);
            return false; 
        }
    };

    const handleIncrement = async (passengerType) => {
        const isAvailable = await checkAvailability();
        if (isAvailable) {
            updatePassengerCount(passengerType, formData[passengerType] + 1);
        } else {
            setNotificationMessage(
              "Tour không còn đủ chỗ."
            );
            setNotificationType("warning"); 
            setNotificationOpen(true);
        }
    };

    useEffect(() => {
        const fetchListPaymentMethod = async () => {
            try {
                const response = await getAllPaymentMethods(); 
                const paymentMethodsArray = Object.values(response);
                console.log(paymentMethodsArray[0])
                setListPaymentMethod(paymentMethodsArray);
                setLoading(false); 
            } catch (error) {
                console.error('Error fetching payment methods:', error);
                setLoading(false);  
            }
        }

        fetchListPaymentMethod();
    }, []);

    useEffect(() => {
        const fetchTourData = async () => {
            try {
                const response = await getRouteDetailById(id);
                console.log(response.result)
                setTour(response);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching tour data:', error);
                setLoading(false);
            }
        };

        fetchTourData();
    }, [id]);

    const handleSubmit = async (e) => {
        e.preventDefault();
    
        // Kiểm tra userId trong localStorage
        const userId = localStorage.getItem('userId');
        if (!userId) {
            setNotificationMessage("Vui lòng đăng nhập trước khi thực hiện đặt chỗ!");
            setNotificationType("error");
            setNotificationOpen(true);
            return; // Ngừng xử lý nếu không có userId
        }
    
        try {
            const response = await fetch('https://localhost:7146/api/Booking/handle-booking', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(payload),
            });
    
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
    
            const data = await response.json();
            setValidateList(data);
            console.log(validateList['paymentMethod']);
            
            if(data.message) {
                setNotificationMessage(
                    "Đặt chỗ thành công, vui lòng thanh toán trong 24h, nếu không đơn giữ chỗ sẽ tự động huỷ!"
                );
                setNotificationType("success");
                setNotificationOpen(true);
            }
        } catch (error) {
            console.error('Error submitting form:', error);
            setNotificationMessage(
                "Đã có lỗi trong lúc đặt tour, vui lòng thử lại!"
            );
            setNotificationType("error");
            setNotificationOpen(true);
        }
    };
    

    

    const handlePaymentMethodChange = (event) => {
        setFormData({
            ...formData,
            paymentMethod: event.target.value
        })
    }

    if (loading) {
        return <div>Loading...</div>;
    }

    if (!tour) {
        return <div>No tour found!</div>;
    }

    const handleCloseNotification = () => setNotificationOpen(false);
    return (
        <div className="container py-4">
            {/* Left Section - Contact Information */}
            <div className="row">
                <div className="card p-4 mb-4 col-lg-7 col-md-12">
                    <h5 className="card-title fs-4 fw-bolder lh-sm pb-3">Thông tin liên hệ</h5>
                    <div className="row mb-3">
                        <div className="col-md-6">
                            <div className="mb-3">
                                <label htmlFor="customerName" className="form-label fw-bolder">Họ tên *</label>
                                <input
                                  type="text"
                                  className="form-control"
                                  placeholder="Nhập họ tên"
                                  name="customerName"
                                  value={formData.customerName}
                                  onChange={handleChange}
                                />
                                {validateList['customerName'] && <div className="text-danger">{validateList['customerName']}</div>}
                            </div>
                        </div>
                        <div className="col-md-6">
                            <div className="mb-3">
                                <label htmlFor="customerPhone" className="form-label fw-bolder">Số điện thoại *</label>
                                <input
                                  type="text"
                                  className="form-control"
                                  placeholder="Nhập số điện thoại"
                                  name="customerPhone"
                                  value={formData.customerPhone}
                                  onChange={handleChange}
                                />
                                <div className="text-danger">{validateList['customerPhone']}</div>
                            </div>
                        </div>
                    </div>
                    <div className="row mb-3">
                        <div className="col-md-6">
                            <div className="mb-3">
                                <label htmlFor="customerEmail" className="form-label fw-bolder">Email *</label>
                                <input
                                  type="email"
                                  className="form-control"
                                  placeholder="name@example.com"
                                  name="customerEmail"
                                  value={formData.customerEmail}
                                  onChange={handleChange}
                                />
                                <div className="text-danger">{validateList['customerEmail']}</div>
                            </div>
                        </div>
                        <div className="col-md-6">
                            <div className="mb-3">
                                <label htmlFor="customerAddress" className="form-label fw-bolder">Địa chỉ *</label>
                                <input
                                  type="text"
                                  className="form-control"
                                  placeholder="Nhập địa chỉ"
                                  name="customerAddress"
                                  value={formData.customerAddress}
                                  onChange={handleChange}
                                />
                                <div className="text-danger">{validateList['customerAddress']}</div>
                            </div>

                        </div>
                        <div className="col-md-6">
                        <div className="mb-3">
                                <label htmlFor="address" className="form-label fw-bolder">Phương thức thanh toán *</label>
                                <select class="form-select form-select-sm" aria-label=".form-select-sm example" name="paymentMethod" id="payment" value={formData.paymentMethod}
                                        onChange={handlePaymentMethodChange}>
                                    <option value="">Chọn phương thức</option>
                                    {
                                        listPaymentMethod.map((paymentMethod, index) => (
                                          <option key={index}
                                                  value={paymentMethod.paymentId}>{paymentMethod.paymentName}</option>
                                        ))
                                    }
                                </select>
                                <div class="text-danger">{ validateList['paymentMethod'] }</div>
                            </div>
                        </div>
                    </div>
                    <h5 className="card-title fs-4 fw-bolder lh-sm pb-3">Hành khách</h5>
                    <div className="row mb-3">
                        <div className="col-md-4">
                            <PassengerCounter
                              label="Người lớn"
                              count={formData.adults}
                              onIncrement={() => handleIncrement('adults')}
                              onDecrement={() => updatePassengerCount('adults', formData.adults - 1)}
                            />
                        </div>

                        <div className="col-md-4">
                            <PassengerCounter
                              label="Trẻ em"
                                count={formData.children}
                                onIncrement={() => handleIncrement('children')}
                                onDecrement={() => updatePassengerCount('children', formData.children - 1)}
                            />
                        </div>
                        <div className="col-md-4">
                            <PassengerCounter
                                label="Trẻ sơ sinh"
                                count={formData.infants}
                                onIncrement={() => handleIncrement('infants')}
                                onDecrement={() => updatePassengerCount('infants', formData.infants - 1)}
                            />
                        </div>
                    </div>

                    <h5 className="card-title fs-4 fw-bolder lh-sm pb-3">Thông tin hành khách</h5>
                    {passengerRequestList.map((passenger, index) => (
                        <div key={index} className="row">
                            <div className="col-md-4">
                                <div className="mb-3">
                                    <label htmlFor={`passengerName${index}`} className="form-label fw-bolder">Họ tên *</label>
                                    <input
                                      type="text"
                                      className="form-control"
                                      placeholder="Nhập họ tên"
                                      name="passengerName"
                                      value={passenger.passengerName}
                                      onChange={(e) => handlePassengerChange(index, e)}
                                    />
                                    <div className="text-danger">{validateList[`passengerRequestList[${index}].passengerName`]}</div>
                                </div>
                            </div>
                            <div className="col-md-3">
                            <div className="mb-3">
                                    <label htmlFor={`gender${index}`} className="form-label fw-bolder">Giới tính *</label>
                                    <select
                                        name="passengerGender"
                                        className="form-select"
                                        value={passenger.passengerGender}
                                        onChange={(e) => handlePassengerChange(index, e)}
                                    >
                                        <option value="Nam">Nam</option>
                                        <option value="Nữ">Nữ</option>
                                    </select>
                                </div>
                            </div>
                            <div className="col-md-3">
                                <div className="mb-3">
                                    <label htmlFor={`birthDate${index}`} className="form-label fw-bolder">Ngày sinh *</label>
                                    <input
                                      type="date"
                                      className="form-control"
                                      name="passengerDateBirth"
                                      value={passenger.passengerDateBirth}
                                      onChange={(e) => handlePassengerChange(index, e)}
                                    />
                                    <div className="text-danger">{validateList[`passengerRequestList[${index}].passengerDateBirth`]}</div>
                                </div>
                            </div>
                            {/* Nếu bạn muốn hiển thị object_id */}
                            <div className="col-md-2">
                                <div className="mb-3">
                                    <label className="form-label fw-bolder">Loại vé *</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        value={passenger.passengerObjectId === 1 ? "Người lớn" : passenger.passengerObjectId === 2 ? "Trẻ em" : "Trẻ sơ sinh"}
                                        readOnly
                                    />
                                </div>
                            </div>
                            <hr></hr>
                        </div>

                    ))}
                </div>

                {/* Right Section - Tour Summary */}
                <div className="col-lg-5 col-md-12">
                    <div className="card p-4">
                        <img src={require(`../../../assets/images/Tour/${tour.result.imageList[0].textImage}`)} style={{ width: '100%', height: '300px' }} className="img-fluid card-img-top" alt="Tour" />
                        <div className="card-body">
                            <h5 className="card-title fs-4 fw-normal lh-sm">{tour.result.detailRouteName}</h5>
                            <ul className="list-unstyled">
                                <li className="pb-1">
                                    <ConfirmationNumberOutlinedIcon className="me-2" />
                                    Mã tour: {tour.result.detailRouteId}
                                </li>
                                <li className="pb-1">
                                    <CalendarMonthOutlinedIcon className="me-2"/>
                                    Ngày khởi hành: {tour.result.timeToDeparture}
                                </li>
                                <li className="pb-1">
                                    <CalendarMonthOutlinedIcon className="me-2"/>
                                    Ngày kết thúc: {tour.result.timeToFinish}
                                </li>
                                <li className="pb-1">
                                    <StarBorderPurple500OutlinedIcon className="me-2"/>
                                    Đánh giá: {tour.result.rating}
                                </li>
                            </ul>

                            <hr/>

                            <h5 class="fw-bolder">KHÁCH HÀNG + PHỤ THU</h5>
                            <ul className="list-unstyled">
                                <li class="d-flex justify-content-between">
                                    <li>Người lớn</li>

                                    <li>{formData.adults} x {tour.result.price.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</li>

                                </li>
                                <li class="d-flex justify-content-between">
                                    <li>Trẻ em </li>
                                    <li>{formData.children} x {(tour.result.price * 0.8).toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</li>
                                </li>
                                <li class="d-flex justify-content-between">
                                    <li>Em bé </li>
                                    <li>{formData.infants} x {(tour.result.price * 0.5).toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</li>
                                </li>
                            </ul>

                            <hr/>

                            <h5 className="d-flex justify-content-between text-danger list-unstyled fw-bolder">
                                <li>TỔNG TIỀN:</li>
                                <li>{totalPrice().toLocaleString()} ₫</li>
                            </h5>
                        </div>
                        <button type="button" className="btn btn btn-outline-info mt-4" onClick={handleSubmit}>
                            Đặt tour
                        </button>
                    </div>
                </div>

            </div>
            <Notification
              open={notificationOpen}
              message={notificationMessage}
              onClose={handleCloseNotification}
              type={notificationType}
            />
        </div>

    );
}

// Passenger Counter Component
const PassengerCounter = ({ label, count, onIncrement, onDecrement }) => (
    <div className="d-flex align-items-center justify-content-between border rounded p-2">
        <span>{label}</span>
        <button className="btn btn-outline-secondary btn-sm" onClick={onDecrement}>-</button>
        <span>{count}</span>
        <button className="btn btn-outline-secondary btn-sm" onClick={onIncrement}>+</button>
    </div>
);

export default BookingTour;
