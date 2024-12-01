import React from 'react';
import { useState, useEffect } from 'react';
import { IconButton } from '@mui/material';
import { Search } from '@mui/icons-material';
import PaginationComponent from '../../../components/Pagination';
import { Visibility, Edit, Delete } from '@mui/icons-material';
import { getAllRoutes,isCheckBooking ,deleteTour} from '../../../services/detailRouteService';
import { Link,useNavigate } from 'react-router-dom';
import { useNotification } from '../../../components/NotificationProvider';
const sort_options = [
    {
        id: 1,
        title: 'Mặc Định',
        value: '',
    },
    {
        id: 2,
        title: 'Giá : Thấp Tăng Dần',
        value: 'asc',
    },
    {
        id: 3,
        title: 'Giá : Giảm Dần',
        value: 'desc',
    },
    {
        id: 4,
        title: 'Đánh Giá Tốt Nhất',
        value: 'rating',
    },
    {
        id: 5,
        title: 'Ưu Đãi Tốt Nhất',
        value: '',
    },
];

function ListTour() {
    const notify = useNotification();
    const pageSize = 10;
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [totalElements, setTotalElements] = useState(0);
    const [routes, setRoutes] = useState([]);
    const [searchKeyword, setSearchKeyword] = useState('');

    const navigate = useNavigate(); 
    useEffect(() => {
        const fet = async () => {
            try {
                let data;
                if (searchKeyword) {
                    // data = await searhcByDetailRouteId(currentPage, pageSize, 'asc', searchKeyword);
                } else {
                    data = await getAllRoutes(currentPage, pageSize);
                }
                console.log('API response:', data); // Kiểm tra dữ liệu trả về
        
                // Cập nhật cách lấy dữ liệu
                if (data && data.data) {
                    setRoutes(data.data || []);
                    setTotalPages(data.totalPages || 0);
                    setTotalElements(data.totalElement || 0);
                } else {
                    console.warn('Invalid data format from API');
                    setRoutes([]);
                    setTotalPages(0);
                    setTotalElements(0);
                }
            } catch (error) {
                console.error('Error fetching routes:', error);
                setRoutes([]);
                setTotalPages(0);
                setTotalElements(0);
            }
        };
        
        
        fet();
    }, [currentPage,searchKeyword]);
    const handleSearch = () => {
        setCurrentPage(1); 
    };
    const handleEdit = async (detailRouteId) => {
        try {
            const isCheck = await isCheckBooking(detailRouteId);
            if (!isCheck) {
                notify("Không thể cập nhật. Tour này đã có người đặt.");
                return;
            }
            navigate(`/admin/tour/update-tour/${detailRouteId}`);
        } catch (error) {
            notify("Có lỗi xảy ra khi kiểm tra trạng thái đặt chỗ.");
        }
    };
    

    const handleDelete = async (detailRouteId) => {
        try {
            const isCheck = await isCheckBooking(detailRouteId);
            if (!isCheck) {
                notify("Không thể xóa. Tour này đã có người đặt.");
                return;
            }
            const data = await deleteTour(detailRouteId);
            
            // Kiểm tra nếu `data` là object, trích xuất message
            const message = typeof data === "object" ? data.message || "Xóa tour thành công!" : data;
            
            notify(message);
    
            // Cập nhật danh sách
            const updatedRoutes = routes.filter(route => route.detailRouteId !== detailRouteId);
            setRoutes(updatedRoutes);
        } catch (error) {
            console.error("Lỗi khi xóa tour:", error);
            notify("Có lỗi xảy ra. Vui lòng thử lại.");
        }
    };
    
    return (
        <div className="p-2">
            <div>
                <h5>Danh sách tour</h5>
            </div>
            <div className="p-3 border rounded">
                <div className="d-flex justify-content-between align-items-center mb-3">
                    <div className="d-flex">
                        <div className="input-group me-2">
                            <span className="input-group-text">
                                <Search />
                            </span>
                            <input type="text" className="form-control" placeholder="Search"   onChange={(e) => setSearchKeyword(e.target.value)} />
                            <button className="btn btn-primary"  onClick={handleSearch}>Tìm kiếm</button>
                        </div>

                        <select className="form-select w-auto">
                            <option>Mặc định</option>
                            <option></option>
                            <option>Inactive</option>
                        </select>
                    </div>
                    <Link className='btn btn-primary' variant="contained" color="primary" to="/admin/tour/add-tour">
                        + Thêm Tour mới
                    </Link>
                </div>

                {/* User Table */}
                <div className="border rounded">
                    <table className="table table-striped ">
                        <thead className="table-light">
                            <tr>
                                <th>Id</th>
                                <th>Name</th>
                                <th>Ngày bắt đầu</th>
                                <th>Ngày kết thúc</th>
                                <th>Số lượng</th>
                                <th>Giá</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            {routes.map((route, index) => (
                                <tr key={index}>
                                    <td>{route.detailRouteId}</td>
                                    <td>
                                        <div className="d-flex align-items-center">{route.detailRouteName}</div>
                                    </td>
                                    <td>{route.timeToDeparture}</td>
                                    <td>{route.timeToFinish}</td>
                                    <td>{route.stock}</td>
                                    <td>{route.price}</td>
                                    <td>
                                        <div className="d-flex align-items-center justify-content-center gap-2">
                                            {/* View Icon */}
                                            <IconButton
                                                style={{
                                                    backgroundColor: 'rgba(0, 123, 255, 0.2)',
                                                    borderRadius: '50%',
                                                }}
                                             //onClick={() => handleView(route.detailRouteId)}
                                            >
                                                <Visibility style={{ color: '#007bff' }} />
                                            </IconButton>
                                            {/* Edit Icon */}
                                            <IconButton
                                                style={{
                                                    backgroundColor: 'rgba(40, 167, 69, 0.2)',
                                                    borderRadius: '50%',
                                                }}
                                                onClick={() => handleEdit(route.detailRouteId)}
                                            >
                                                <Edit style={{ color: '#28a745' }} />
                                            </IconButton>
                                            {/* Delete Icon */}
                                            <IconButton
                                                style={{
                                                    backgroundColor: 'rgba(220, 53, 69, 0.2)',
                                                    borderRadius: '50%',
                                                }}
                                                onClick={() => handleDelete(route.detailRouteId)}
                                            >
                                                <Delete style={{ color: '#dc3545' }} />
                                            </IconButton>
                                        </div>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
                <div className="mt-2 mb-3 fs-3 d-flex justify-content-between">
                    <div>
                        <span className='fs-4'>Tổng cộng {totalElements} Tour</span>
                    </div>
                    <PaginationComponent
                        currentPage={currentPage}
                        totalPages={totalPages}
                        onPageChange={(page) => setCurrentPage(page)}
                    />
                </div>
            </div>
        </div>
    );
}

export default ListTour;
