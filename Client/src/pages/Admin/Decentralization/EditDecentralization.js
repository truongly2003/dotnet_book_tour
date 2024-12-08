import React, { useState, useEffect } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  Box,
  Typography,
  Checkbox,
  FormControlLabel,
} from "@mui/material";
import { getPermissionByRoleId, updatePermissions } from "../../../services/decentralizationService";
import Notification from "../../../components/Notification";

function EditDecentralization({ open, onClose, decentralization, onSave }) {
  const [formData, setFormData] = useState({
    id: "",
    roleId: "",
    roleName: "",
  });
  const [permissions, setPermissions] = useState([]); // Danh sách permissions
  const [notificationOpen, setNotificationOpen] = useState(false); // Trạng thái thông báo

  useEffect(() => {
    if (open && decentralization?.roleId) {
      setFormData({
        id: decentralization.id,
        roleId: decentralization.roleId,
        roleName: decentralization.roleName,
      });

      // Lấy dữ liệu permissions khi roleId thay đổi
      fetchPermissionsByRoleId(decentralization.roleId);
    }
  }, [open, decentralization]);

  // Hàm lấy permissions theo roleId
  const fetchPermissionsByRoleId = async (roleId) => {
    try {
      const response = await getPermissionByRoleId(roleId);
      if (response && response.code === 1000) {
        // Giả sử response.result là danh sách permissions
        const permissionsData = response.result || [];

        // Chuyển đổi danh sách permissions thành mảng với các permission name và operationIds
        const permissionList = permissionsData.map(permission => ({
          id: permission.id,
          permissionName: permission.permissionName,
          isGranted: permission.isGranted || true, // Mặc định là true (được tích chọn)
          operationIds: permission.operationIds || [1], // Đảm bảo có operationIds mặc định
        }));

        setPermissions(permissionList);
      }
    } catch (error) {
      console.error("Error fetching permissions:", error);
    }
  };

  // Hàm thay đổi trạng thái của permission (checked/unchecked)
  const handlePermissionChange = (permissionId) => {
    setPermissions((prev) =>
      prev.map((permission) =>
        permission.id === permissionId
          ? { ...permission, isGranted: !permission.isGranted }
          : permission
      )
    );
  };

  const handleSave = async () => {
    const payload = {
      roleId: formData.roleId,
      permissions: permissions
        .filter(permission => permission.isGranted) // Chỉ lấy các permission được tích
        .map(permission => ({
          permissionId: permission.id,
          // Đảm bảo rằng operationIds không rỗng
          operationIds: permission.operationIds || [1], // Mặc định là một mảng với 1 (hoặc bạn có thể lấy giá trị khác)
        })),
    };

    try {
      await updatePermissions(payload.roleId, payload.permissions);
      setNotificationOpen(true); // Hiển thị thông báo
      onSave(payload); // Gửi dữ liệu lên component cha
      onClose(); // Đóng dialog
    } catch (error) {
      console.error("Error saving permissions:", error);
    }
  };

  // Đóng thông báo khi đã hiển thị
  const handleNotificationClose = () => {
    setNotificationOpen(false); // Đóng thông báo
  };

  return (
    <>
      <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
        <DialogTitle>Edit Decentralization</DialogTitle>
        <DialogContent>
          <Box display="flex" flexDirection="column" gap={2} mt={2}>
            <TextField
              label="Role Name"
              value={formData.roleName}
              fullWidth
              disabled
            />
            <Typography variant="h6">List Permissions</Typography>
            {permissions.map((permission) => (
              <FormControlLabel
                key={permission.id}
                control={
                  <Checkbox
                    checked={permission.isGranted} // Kiểm tra trạng thái của permission
                    onChange={() => handlePermissionChange(permission.id)} // Cập nhật trạng thái khi thay đổi
                    name={permission.permissionName}
                  />
                }
                label={permission.permissionName}
              />
            ))}
          </Box>
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose}>Cancel</Button>
          <Button onClick={handleSave} color="primary">
            Save
          </Button>
        </DialogActions>
      </Dialog>

      <Notification
        open={notificationOpen}
        message="Permissions updated successfully!"
        onClose={handleNotificationClose}
      />
    </>
  );
}

export default EditDecentralization;
