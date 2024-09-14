import React, { useState } from 'react';
import { Table, TableBody, TableCell, TableHead, TableRow, Button, Modal, Box, Typography } from '@mui/material';
import { Link } from 'react-router-dom';

const CafeTable = ({ cafes, onDelete }) => {
  const [selectedCafe, setSelectedCafe] = useState(null);
  const [open, setOpen] = useState(false);
  const [deleteConfirmationOpen, setDeleteConfirmationOpen] = useState(false);
  const [cafeToDelete, setCafeToDelete] = useState(null);

  const handleOpen = (cafe) => {
    setSelectedCafe(cafe);
    setOpen(true);
  };

  const handleClose = () => {
    setSelectedCafe(null);
    setOpen(false);
  };

  const handleDeleteOpen = (cafe) => {
    setCafeToDelete(cafe);
    setDeleteConfirmationOpen(true);
  };

  const handleDeleteClose = () => {
    setCafeToDelete(null);
    setDeleteConfirmationOpen(false);
  };

  const handleConfirmDelete = async () => {
    if (cafeToDelete) {
      try {
        await onDelete(cafeToDelete.id); // Assuming onDelete is an async function
        handleDeleteClose();
      } catch (error) {
        console.error('Failed to delete cafe:', error);
      }
    }
  };

  if (!cafes.length) return <p>No cafés found</p>;

  return (
    <>
      <Table
        sx={{
          borderCollapse: 'separate',
          borderSpacing: '0 8px', // Space between rows
        }}
      >
        <TableHead>
          <TableRow>
            <TableCell
              sx={{
                fontWeight: 'bold',
                backgroundColor: '#000', // Black background for header
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
              Description
            </TableCell>
            <TableCell
              sx={{
                fontWeight: 'bold',
                backgroundColor: '#000',
                color: 'white',
                textAlign: 'center',
              }}
            >
              Employees
            </TableCell>
            <TableCell
              sx={{
                fontWeight: 'bold',
                backgroundColor: '#000',
                color: 'white',
                textAlign: 'center',
              }}
            >
              Location
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
          {cafes.map((cafe) => (
            <TableRow
              key={cafe.id}
              sx={{
                backgroundColor: 'white', // White row background
                boxShadow: '0 2px 10px rgba(0, 0, 0, 0.1)', // Add shadow for modern look
              }}
            >
              <TableCell sx={{ textAlign: 'center' }}>
                <Button
                  variant="text"
                  onClick={() => handleOpen(cafe)}
                  sx={{ color: '#FF6F00', textDecoration: 'underline' }}
                >
                  {cafe.name}
                </Button>
              </TableCell>
              <TableCell sx={{ textAlign: 'center' }}>{cafe.description}</TableCell>
              <TableCell sx={{ textAlign: 'center' }}>{cafe.employees}</TableCell>
              <TableCell sx={{ textAlign: 'center' }}>{cafe.location}</TableCell>
              <TableCell sx={{ textAlign: 'center' }}>
                <Link to={`/cafes/edit/${cafe.id}`} style={{ textDecoration: 'none' }}>
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
                  onClick={() => handleDeleteOpen(cafe)}
                >
                  Delete
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>

      {/* Cafe Details Modal */}
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
          {selectedCafe && (
            <>
              <Typography variant="h6" gutterBottom>
                Cafe Details
              </Typography>
              <Typography variant="body1"><strong>Name:</strong> {selectedCafe.name}</Typography>
              <Typography variant="body1"><strong>Description:</strong> {selectedCafe.description}</Typography>
              <Typography variant="body1"><strong>Employees:</strong> {selectedCafe.employees}</Typography>
              <Typography variant="body1"><strong>Location:</strong> {selectedCafe.location}</Typography>
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
            Are you sure you want to delete {cafeToDelete?.name}?
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

export default CafeTable;
