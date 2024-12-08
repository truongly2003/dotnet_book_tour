import React, { useState, useEffect } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Box,
  Typography,
  Checkbox,
  FormControlLabel,
  Select,
  MenuItem,
  InputLabel,
  FormControl,
} from "@mui/material";
import Notification from "../../../components/Notification";
import {
  getAssignedPermissions,
  getAllPermission,
  addPermission,
  getAllOperation,
  getPermissionByRoleId,
} from "../../../services/decentralizationService";
import { getAllRole } from "../../../services/userService";

function AddDecentralization({ open, onClose, onSave }) {
  const [formData, setFormData] = useState({
    roleId: "",
    roleName: "",
  });
  const [permissions, setPermissions] = useState([]);
  const [roles, setRoles] = useState([]);

  const [notificationOpen, setNotificationOpen] = useState(false);
  const [snackBarMessage, setSnackBarMessage] = useState("");

  useEffect(() => {
    const fetchRoles = async () => {
      try {
        const response = await getAllRole();
        console.log(response);
        if (response && response.code === 1000) {
          setRoles(response.result || []);
        }
      } catch (error) {
        console.error("Error fetching roles:", error);
      }
    };

    fetchRoles();
  }, []);

  const fetchPermissionsAndOperations = async (roleId) => {
    try {
      console.log("roleid :", roleId);
      const allPermissionsResponse = await getAllPermission();
      console.log("allPermissionsResponse", allPermissionsResponse);

      const assignedPermissionsResponse = await getPermissionByRoleId(roleId);
      console.log("assignedPermissionsResponse", assignedPermissionsResponse);

      if (allPermissionsResponse && assignedPermissionsResponse) {
        const allPermissions = allPermissionsResponse.result || [];
        const assignedPermissions = assignedPermissionsResponse.result || [];

        const permissionsWithAssignedStatus = allPermissions.map(
          (permission) => {
            const isAssigned = assignedPermissions.some(
              (assigned) => assigned.id === permission.id
            );

            return {
              ...permission,
              isAssigned, 
            };
          }
        );

        setPermissions(permissionsWithAssignedStatus);
      }
    } catch (error) {
      console.error("Error fetching permissions and operations:", error);
    }
  };

  const handleRoleChange = async (event) => {
    const selectedRole = event.target.value;
    console.log('selected role id:', selectedRole);
    if (selectedRole) {
      setFormData({
        roleId: selectedRole,
        roleName: selectedRole.roleName,
      });

      await fetchPermissionsAndOperations(selectedRole);
    }
  };

  const handleSave = async () => {
    try {
      const payload = {
        roleId: formData.roleId,
        permissions: permissions.map((permission) => ({
          permissionId: permission.id,
          operationIds: permission.isAssigned ? [1] : [], // Giả sử "1" là ID của operation khi permission được chọn
        })),
      };
  
      // Gọi API để thêm permission
      await addPermission(payload.roleId, payload.permissions);
      setNotificationOpen(true);
  
      onSave(payload);
      onClose();
    } catch (error) {
      console.error("Error saving permissions:", error);
    }
  };
  

  const handlePermissionChange = (permissionId, isChecked) => {
    setPermissions((prevPermissions) =>
      prevPermissions.map((permission) =>
        permission.id === permissionId
          ? { ...permission, isAssigned: isChecked }
          : permission
      )
    );
  };

  const handleNotificationClose = () => {
    setNotificationOpen(false);
  };

  return (
    <>
      <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
        <DialogTitle>Add Decentralization</DialogTitle>
        <DialogContent>
          <Box display="flex" flexDirection="column" gap={2} mt={2}>
            <FormControl fullWidth>
              <InputLabel id="role-select-label">Select Role</InputLabel>
              <Select
                labelId="role-select-label"
                value={formData.roleId}
                onChange={handleRoleChange}
              >
                {roles.map((role) => (
                  <MenuItem key={role.roleId} value={role.roleId}>
                    {role.roleId} - {role.roleName}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>

            <Typography variant="h6" mt={3}>
              Permissions
            </Typography>
            <ul style={{ listStyle: "none", padding: 0, margin: 0 }}>
              {permissions.map((permission) => (
                <li key={permission.id}>
                  <Box display="flex" alignItems="center" gap={2}>
                    <Checkbox
                      checked={permission.isAssigned}
                      onChange={(e) =>
                        handlePermissionChange(permission.id, e.target.checked)
                      }
                    />
                    <Typography variant="subtitle1">
                      {permission.permissionName}
                    </Typography>
                  </Box>
                </li>
              ))}
            </ul>
          </Box>
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose} color="primary">
            Cancel
          </Button>
          <Button onClick={handleSave} variant="contained" color="primary">
            Save
          </Button>
        </DialogActions>
      </Dialog>

      <Notification
        open={notificationOpen}
        message={snackBarMessage}
        onClose={handleNotificationClose}
        type="success"
      />
    </>
  );
}

export default AddDecentralization;
