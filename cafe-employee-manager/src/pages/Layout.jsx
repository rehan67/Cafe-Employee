import React from 'react';
import { Outlet, Link } from 'react-router-dom';
import { AppBar, Toolbar, Typography, Container, Card, CardContent, Button, Box } from '@mui/material';
import { styled } from '@mui/material/styles';

// Define custom styles for the card and its content
const StyledCard = styled(Card)(({ theme }) => ({
  maxWidth: 300,
  margin: '0 auto',
  cursor: 'pointer',
  transition: 'transform 0.3s, box-shadow 0.3s',
  boxShadow: `0 4px 8px rgba(0, 0, 0, 0.2)`,
  '&:hover': {
    transform: 'scale(1.05)',
    boxShadow: `0 8px 16px rgba(0, 0, 0, 0.3)`,
  },
}));

const CardImageWrapper = styled(Box)({
  width: '100%',
  height: '150px', // Ensure all images have the same height
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  overflow: 'hidden',
});

const CardImage = styled('img')({
  width: '100%',
  height: '100%',
  objectFit: 'cover', // Ensure images cover the entire container
});

const CardContentWrapper = styled(CardContent)(({ theme }) => ({
  textAlign: 'center',
  backgroundColor: '#000',  // Dark background for better contrast
  color: '#fff',
  padding: theme.spacing(2),
}));

const ButtonStyled = styled(Button)(({ theme }) => ({
  marginTop: '10px',
  backgroundColor: '#FF6F00', // Default orange color
  color: '#fff',
  '&:hover': {
    backgroundColor: '#FF8C00', // Darker orange for hover effect
  },
}));

const Layout = () => {
  return (
    <div>
      <AppBar position="static" style={{ backgroundColor: '#000' }}>
        <Toolbar>
          <Typography variant="h6" style={{ flexGrow: 1, color: '#fff' }}>
            Cafe Management
          </Typography>
          <Button color="inherit" component={Link} to="/cafes">
            Cafés
          </Button>
          <Button color="inherit" component={Link} to="/employees">
            Employees
          </Button>
        </Toolbar>
      </AppBar>
      <Container style={{ marginTop: '20px' }}>
        <Box display="flex" justifyContent="center" gap={4}>
          <StyledCard component={Link} to="/cafes">
            <CardImageWrapper>
              <CardImage src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRQR4Sh6P1UG_-ynnJdhw_VQKqNTy4jofj8RA&s" alt="Cafe" />
            </CardImageWrapper>
            <CardContentWrapper>
              <Typography variant="h6">Cafés</Typography>
              <ButtonStyled variant="contained">
                View Cafés
              </ButtonStyled>
            </CardContentWrapper>
          </StyledCard>
          <StyledCard component={Link} to="/employees">
            <CardImageWrapper>
              <CardImage src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcToI2Qf__Q9jMT184gBBYXoldGytbIfLdEfJA&s" alt="Employee" />
            </CardImageWrapper>
            <CardContentWrapper>
              <Typography variant="h6">Employees</Typography>
              <ButtonStyled variant="contained">
                View Employees
              </ButtonStyled>
            </CardContentWrapper>
          </StyledCard>
        </Box>
        <Outlet />
      </Container>
    </div>
  );
};

export default Layout;
