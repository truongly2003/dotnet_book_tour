import { Fragment } from "react";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import DefaultLayout from "./layouts/DefaultLayout";
import publicRoutes from "./route";

import Receipt from "./pages/Admin/Receipt";
import BookingDetail from "./pages/Admin/Receipt/detail"; // Điều chỉnh đường dẫn nếu cần

function App() {
    return (
        <Router>
            <div className="App">
                <Routes>
                    {
                        publicRoutes.map(
                            (route, index) => {
                                const Page = route.component;
                                let Layout = DefaultLayout;
                                if (route.layout) {
                                    Layout = route.layout;
                                } else if (route.layout === null) {
                                    Layout = Fragment;
                                }
                                return (
                                    <Route
                                        key={index}
                                        path={route.path}
                                        element={
                                            <Layout>
                                                <Page />
                                            </Layout>
                                        }
                                    />
                                );
                            }
                        )
                    }
                    {/* Thêm route cho trang chi tiết booking */}
                    <Route
                        path="/admin/booking/detail/:bookingId"
                        element={
                            <DefaultLayout>
                                <BookingDetail />
                            </DefaultLayout>
                        }
                    />
                </Routes>
            </div>
        </Router>
    );
}

export default App;