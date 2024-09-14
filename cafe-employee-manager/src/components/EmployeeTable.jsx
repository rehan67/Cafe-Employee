import React, { useState } from 'react';
import { Table, TableBody, TableCell, TableHead, TableRow, Button, Modal, Box, Typography } from '@mui/material';
import { Link, useNavigate } from "react-router-dom";

const EmployeeTable = ({ employees, onDelete }) => {
  const [selectedEmployee, setSelectedEmployee] = useState(null);
  const [open, setOpen] = useState(false);
  const [deleteConfirmationOpen, setDeleteConfirmationOpen] = useState(false);
  const [employeeToDelete, setEmployeeToDelete] = useState(null);
  const navigate = useNavigate();

  const handleOpen = (employee) => {
    setSelectedEmployee(employee);
    setOpen(true);
  };

  const handleClose = () => {
    setSelectedEmployee(null);
    setOpen(false);
  };

  const handleDeleteOpen = (employee) => {
    setEmployeeToDelete(employee);
    setDeleteConfirmationOpen(true);
  };

  const handleDeleteClose = () => {
    setEmployeeToDelete(null);
    setDeleteConfirmationOpen(false);
  };

  const handleConfirmDelete = async () => {
    if (employeeToDelete) {
      try {
        await onDelete(employeeToDelete.id); // Assuming onDelete is an async function
        handleDeleteClose();
        navigate(0); // Refresh the page
      } catch (error) {
        console.error('Failed to delete employee:', error);
      }
    }
  };

  if (!employees.length) return <p>No Employees found</p>;

  return (
    <>
      <Table
        sx={{
          borderCollapse: 'separate',
          borderSpacing: '0 8px', // Space between rows for modern look
        }}
      >
        <TableHead>
          <TableRow>
            <TableCell
              sx={{
                fontWeight: 'bold',
                backgroundColor: '#000', // Black header
                color: 'white',
                textAlign: 'center',
              }}
            >
              Employee ID
            </TableCell>
            <TableCell
              sx={{
                fontWeight: 'bold',
                backgroundColor: '#000',
                color: 'white',
                textAlign: 'center',
              }}
            >
              Name
            </TableCell>
            <TableCell
              sx={{
                fontWeight: 'bold',
                backgroundColor: '#000',
                color: 'white',
                textAlign: 'center',
              }}
            >
              Email
            </TableCell>
            <TableCell
              sx={{
                fontWeight: 'bold',
                backgroundColor: '#000',
                color: 'white',
                textAlign: 'center',
              }}
            >
              Phone
            </TableCell>
            <TableCell
              sx={{
                fontWeight: 'bold',
                backgroundColor: '#000',
                color: 'white',
                textAlign: 'center',
              }}
            >
              Days Worked
            </TableCell>
            <TableCell
              sx={{
                fontWeight: 'bold',
                backgroundColor: '#000',
                color: 'white',
                textAlign: 'center',
              }}
            >
              Café
            </TableCell>
            <TableCell
              sx={{
                fontWeight: 'bold',
                backgroundColor: '#000',
                color: 'white',
                textAlign: 'center',
              }}
            >
              Actions
            </TableCell>
          </TableRow>
        </TableHead>

        <TableBody>
          {employees.map((emp) => (
            <TableRow
              key={emp.id}
              sx={{
                backgroundColor: 'white', // White background for row
                boxShadow: '0 2px 10px rgba(0, 0, 0, 0.1)', // Modern shadow effect
              }}
            >
              <TableCell sx={{ textAlign: 'center' }}>
                <Button
                  variant="text"
                  onClick={() => handleOpen(emp)}
                  sx={{ color: '#FF6F00', textDecoration: 'underline' }}
                >
                  {emp.id}
                </Button>
              </TableCell>
              <TableCell sx={{ textAlign: 'center' }}>{emp.name}</TableCell>
              <TableCell sx={{ textAlign: 'center' }}>{emp.emailAddress}</TableCell>
              <TableCell sx={{ textAlign: 'center' }}>{emp.phoneNumber}</TableCell>
              <TableCell sx={{ textAlign: 'center' }}>{emp.daysWorked}</TableCell>
              <TableCell sx={{ textAlign: 'center' }}>{emp?.cafe?.cafe}</TableCell>
              <TableCell sx={{ textAlign: 'center' }}>
                <Link to={`/employees/edit/${emp.id}`} style={{ textDecoration: 'none' }}>
                  <Button
                    variant="contained"
                    sx={{
                      backgroundColor: '#28A745', // Green for Edit button
                      color: 'white',
                      '&:hover': {
                        backgroundColor: '#218838',
                      },
                      marginRight: '8px', // Space between buttons
                    }}
                  >
                    Edit
                  </Button>
                </Link>
                <Button
                  variant="contained"
                  sx={{
                    backgroundColor: '#DC3545', // Red for Delete button
                    color: 'white',
                    '&:hover': {
                      backgroundColor: '#C82333',
                    },
                  }}
                  onClick={() => handleDeleteOpen(emp)}
                >
                  Delete
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>

      {/* Employee Details Modal */}
      <Modal
        open={open}
        onClose={handleClose}
        sx={{ display: 'flex', alignItems: 'center', justifyContent: 'center' }}
      >
        <Box
          sx={{
            width: 400,
            bgcolor: '#1c1c1c',
            color: '#fff',
            p: 4,
            borderRadius: 2,
            boxShadow: 24,
          }}
        >
          {selectedEmployee && (
            <>
              <Typography variant="h6" gutterBottom>
                Employee Details
              </Typography>
              <Typography variant="body1"><strong>ID:</strong> {selectedEmployee.id}</Typography>
              <Typography variant="body1"><strong>Name:</strong> {selectedEmployee.name}</Typography>
              <Typography variant="body1"><strong>Email:</strong> {selectedEmployee.emailAddress}</Typography>
              <Typography variant="body1"><strong>Phone:</strong> {selectedEmployee.phoneNumber}</Typography>
              <Typography variant="body1"><strong>Days Worked:</strong> {selectedEmployee.daysWorked}</Typography>
              <Typography variant="body1"><strong>Café:</strong> {selectedEmployee?.cafe?.cafe}</Typography>
              <Button
                onClick={handleClose}
                variant="contained"
                sx={{
                  backgroundColor: '#FF6F00', // Orange for Close button
                  color: '#fff',
                  mt: 2,
                  '&:hover': {
                    backgroundColor: '#FF3D00',
                  },
                }}
              >
                Close
              </Button>
            </>
          )}
        </Box>
      </Modal>

      {/* Delete Confirmation Modal */}
      <Modal
        open={deleteConfirmationOpen}
        onClose={handleDeleteClose}
        sx={{ display: 'flex', alignItems: 'center', justifyContent: 'center' }}
      >
        <Box
          sx={{
            width: 400,
            bgcolor: '#1c1c1c',
            color: '#fff',
            p: 4,
            borderRadius: 2,
            boxShadow: 24,
          }}
        >
          <Typography variant="h6" gutterBottom>
            Confirm Deletion
          </Typography>
          <Typography variant="body1">
            Are you sure you want to delete {employeeToDelete?.name}?
          </Typography>
          <Box sx={{ mt: 2, display: 'flex', justifyContent: 'flex-end' }}>
            <Button
              onClick={handleDeleteClose}
              variant="contained"
              sx={{
                backgroundColor: '#6c757d', // Gray for Cancel button
                color: '#fff',
                mr: 1,
                '&:hover': {
                  backgroundColor: '#5a6268',
                },
              }}
            >
              Cancel
            </Button>
            <Button
              onClick={handleConfirmDelete}
              variant="contained"
              sx={{
                backgroundColor: '#DC3545', // Red for Confirm Delete button
                color: '#fff',
                '&:hover': {
                  backgroundColor: '#C82333',
                },
              }}
            >
              Confirm
            </Button>
          </Box>
        </Box>
      </Modal>
    </>
  );
};

export default EmployeeTable;
