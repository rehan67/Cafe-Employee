import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { useCafes, useDeleteCafe } from '../queries/queries';
import CafeTable from '../components/CafeTable';
import { Button, TextField, Container, Typography } from '@mui/material';

const CafesPage = () => {
  const [filter, setFilter] = useState('');
  const { data: cafes, isLoading, isError } = useCafes();
  const { mutate: deleteCafe } = useDeleteCafe();

  console.log('Cafe Page ', cafes);

  const handleDelete = (id) => {
  
      deleteCafe(id);
  
  };

  const handleFilterChange = (event) => {
    setFilter(event.target.value.trimStart());
  };

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error loading cafés</div>;

  // Filter cafes by location
  const filteredCafes = cafes?.filter((cafe) =>
    cafe.location.toLowerCase().includes(filter.trim().toLowerCase())
  );

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      {/* Header Section */}
      <Typography variant="h4" component="h1" gutterBottom sx={{ textAlign: 'center', mb: 4 }}>
        Cafés
      </Typography>

      {/* Add New Café Button */}
      <Button
  component={Link}
  to="/cafes/add"
  variant="contained"
  sx={{
    mb: 2,
    display: 'block',
    marginLeft: 'auto',
    marginRight: 'auto',
    backgroundColor: '#000', // Black background color
    color: '#fff', // White text color
    '&:hover': {
      backgroundColor: '#333', // Darker shade of black for hover
    },
  }}
>
  Add New Café
</Button>


      {/* Filter Input */}
      <TextField
        label="Filter by Location"
        value={filter}
        onChange={handleFilterChange}
        fullWidth
        margin="normal"
        variant="outlined"
        sx={{ mb: 4 }}
      />

      {/* Café Table */}
      <CafeTable cafes={filteredCafes} onDelete={handleDelete} />
    </Container>
  );
};

export default CafesPage;
