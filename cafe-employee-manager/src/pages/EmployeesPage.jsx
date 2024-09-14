import React, { useState } from 'react';
import { Link } from "react-router-dom";
import { useEmployees, useDeleteEmployee } from "../queries/queries";
import EmployeeTable from "../components/EmployeeTable";
import { Button, TextField, Container, Typography } from "@mui/material";

const EmployeesPage = () => {
  const [filter, setFilter] = useState('');
  const { data: employees, isLoading, isError } = useEmployees();
  const { mutate: deleteEmployee } = useDeleteEmployee();

  console.log('Employee Page ', employees);

  const handleDelete = (id) => {
    
      deleteEmployee(id);
    
  };

  const handleFilterChange = (event) => {
    setFilter(event.target.value.trimStart());
  };

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error loading employees</div>;

  // Filter employees by cafe name
  const filteredEmployees = employees?.filter((employee) =>
    employee?.cafe?.cafe.toLowerCase().includes(filter.trim().toLowerCase())
  );

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      {/* Header Section */}
      <Typography variant="h4" component="h1" gutterBottom sx={{ textAlign: 'center', mb: 4 }}>
        Employees
      </Typography>

      <Button
  component={Link}
  to="/employees/add"
  variant="contained"
  sx={{
    mb: 2,
    display: 'block',
    marginLeft: 'auto',
    marginRight: 'auto',
    backgroundColor: '#000',  // Black background
    color: '#fff',  // White text
    '&:hover': {
      backgroundColor: '#333',  // Darker black for hover effect
    },
  }}
>
  Add New Employee
</Button>

      {/* Filter Input */}
      <TextField
        label="Filter by Cafe"
        value={filter}
        onChange={handleFilterChange}
        fullWidth
        margin="normal"
        variant="outlined"
        sx={{ mb: 4 }}
      />

      {/* Employee Table */}
      <EmployeeTable employees={filteredEmployees} onDelete={handleDelete} />
    </Container>
  );
};

export default EmployeesPage;
