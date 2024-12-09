import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { getRoleAndPermissionsByUserId } from "../../services/decentralizationService"; // Import API
import sidebar_items from "../../constants/sidebar_items"; // Sidebar items
import CircleIcon from "@mui/icons-material/Circle";
import ArrowDropDownIcon from '@mui/icons-material/ArrowDropDown';
import logo from "../../assets/images/logo.png";
import styles from "./AdminSidebar.module.css";

function AdminSidebar({ isSidebarCollapsed }) {
  const [filteredSidebarItems, setFilteredSidebarItems] = useState([]);
  const [permissions, setPermissions] = useState([]);
  const [activeIndex, setActiveIndex] = useState(null);
  const userId = localStorage.getItem("userId"); // Get userId from localStorage

  useEffect(() => {
    const fetchPermissions = async () => {
      const result = await getRoleAndPermissionsByUserId(userId); // Call API
      if (result && result.permissions) {
        setPermissions(result.permissions); // Save the permissions
      }
    };

    fetchPermissions();
  }, [userId]);

  useEffect(() => {
    // Filter parent items and sub-items based on permissions
    const filteredItems = sidebar_items
      .filter((item) => permissions.includes(item.title)) // Display parent item if user has permission
      .map((item) => {
        const parentItem = { ...item }; // Clone to avoid mutation
        if (parentItem.item) {
          // Filter sub-items based on permissions of the parent
          parentItem.item = parentItem.item.filter((subItem) =>
            permissions.includes(item.title)
          );
        }
        return parentItem;
      });
    setFilteredSidebarItems(filteredItems); // Update sidebar items
  }, [permissions]);

  const handleToggle = (id) => {
    setActiveIndex(activeIndex === id ? null : id); // Toggle active state for sub-items
  };

  const renderSubItems = (subItems, isOpen) => (
    <ul
      className={`nav flex-column ms-4 ${styles.submenu} ${
        isOpen ? styles["submenu-open"] : ""
      }`}
    >
      {subItems.map((subItem, index) => (
        <li key={index} className={styles.submenuItem}>
          <Link
            to={subItem.to}
            className={`nav-link text-dark ${styles.submenuLink}`}
          >
            <CircleIcon style={{ fontSize: "10px", marginRight: "10px" }} />
            {!isSidebarCollapsed && subItem.title}
          </Link>
        </li>
      ))}
    </ul>
  );

  return (
    <div
      className={`d-flex flex-column ${styles.sidebar}`}
      style={{
        maxHeight: "100vh",
        overflow: "auto",
        width: isSidebarCollapsed ? "60px" : "250px",
        transition: "width 0.3s ease",
        position: "relative",
      }}
    >
      {/* Logo */}
      <div
        style={{
          position: "fixed",
          backgroundColor: "#dee2e6" /* Màu nền xanh */,
          boxShadow: "2px 0 5px rgba(0, 0, 0, 0.1)" /* Đổ bóng */,
          transition: "width 0.3s ease",
          borderRadius: "24px" /* Bo tròn góc */,
          margin: "8px 0 8px 8px" /* Thêm margin xung quanh */,
          height: "100vh" /* Chiều cao bằng 100% chiều cao viewport */,
          width: "100% !important" /* Đặt chiều rộng 100% */,
        }}
      >
        <div
          className="d-flex align-items-center mb-4"
          style={{
            justifyContent: isSidebarCollapsed ? "center" : "flex-start",
          }}
        >
          <img
            src={logo}
            alt="Logo"
            style={{
              width: isSidebarCollapsed ? "40px" : "200px",
              height: isSidebarCollapsed ? "40px" : "130px",
              marginRight: isSidebarCollapsed ? "0" : "10px",
            }}
          />
        </div>

        {/* Sidebar menu */}
        <ul className="nav nav-pills flex-column mb-auto">
          {filteredSidebarItems.map((item) => (
            <li className="nav-item" key={item.id}>
              <div
                onClick={() => handleToggle(item.id)}
                className="d-flex justify-content-between align-items-center"
                style={{ cursor: "pointer" }}
              >
                <Link
                  to={item.to}
                  className={`nav-link text-dark ${styles.menuLink}`}
                  style={{
                    justifyContent: isSidebarCollapsed
                      ? "center"
                      : "flex-start",
                  }}
                >
                  <span
                    style={{
                      marginRight: isSidebarCollapsed ? "0" : "10px",
                    }}
                  >
                    {item.icon}
                  </span>
                  {!isSidebarCollapsed && item.title}
                </Link>
                {item.item && !isSidebarCollapsed && (
                  <span
                    style={{
                      transition: "transform 0.3s",
                      transform:
                        activeIndex === item.id
                          ? "rotate(180deg)"
                          : "rotate(0deg)",
                    }}
                  >
                    <ArrowDropDownIcon /> {/* Mũi tên xuống từ MUI */}
                  </span>
                )}
              </div>
              {item.item &&
                activeIndex === item.id &&
                renderSubItems(item.item, activeIndex === item.id)}
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
}

export default AdminSidebar;
