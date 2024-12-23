import AdminSidebar from "../../components/AdminSidebar";
import AdminHeader from "../../components/AdminHeader";

function AdminLayout({ children }) {
  return (
    <div className="d-flex">
      <div className="col-2" >
        <AdminSidebar />
      </div>
      <div className="col-md-10 ms-sm-auto col-lg-10">
        <AdminHeader />
        <main className="p-2">{children}</main>
      </div>
    </div>
  );
}

export default AdminLayout;
