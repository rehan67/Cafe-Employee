import React from 'react';
import { useEmployees } from '../queries/queries';
import { DataGrid } from '@mui/x-data-grid';
import { Button } from '@mui/material';

const EmployeeTable = () => {
  const { data: employees, refetch } = useEmployees();

  const handleEdit = (id) => {
    // Navigate to edit page
  };

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this employee?')) {
      await fetch(`/employees/${id}`, { method: 'DELETE' });
      refetch();
    }
  };

  const columns = [
    { field: 'id', headerName: 'ID', width: 90 },
    { field: 'name', headerName: 'Name', width: 150 },
    { field: 'email', headerName: 'Email', width: 150 },
    { field: 'phone', headerName: 'Phone Number', width: 150 },
    { field: 'daysWorked', headerName: 'Days Worked', width: 150 },
    { field: 'cafeName', headerName: 'Café Name', width: 150 },
    {
      field: 'actions',
      headerName: 'Actions',
      renderCell: (params) => (
        <>
          <Button onClick={() => handleEdit(params.row.id)}>Edit</Button>
          <Button onClick={() => handleDelete(params.row.id)}>Delete</Button>
        </>
      ),
    },
  ];

  return <DataGrid rows={employees || []} columns={columns} />;
};

export default EmployeeTable;
