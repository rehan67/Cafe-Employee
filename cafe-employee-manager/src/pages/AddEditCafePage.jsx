import React from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import * as yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { TextField, Button, Typography, Card } from '@mui/material';
import { styled } from '@mui/material/styles';
import { useCafe, useCreateCafe, useUpdateCafe } from '../queries/queries';

// Define schema for validation
const schema = yup.object({
  name: yup.string().min(6).max(50).required('Name is required'),
  description: yup.string().max(256).required('Description is required'),
  location: yup.string().required('Location is required'),
});

// Styled components
const StyledCard = styled(Card)(({ theme }) => ({
  maxWidth: 600,
  margin: '20px auto',
  padding: theme.spacing(3),
  backgroundColor: '#1c1c1c', // Dark background for card
  color: '#fff', // Light text color for contrast
}));

const StyledTextField = styled(TextField)(({ theme }) => ({
  marginBottom: theme.spacing(2),
  '& .MuiInputBase-input': {
    color: '#fff', // White text
  },
  '& .MuiInputLabel-root': {
    color: '#FF6F00', // Orange label
  },
  '& .MuiFormHelperText-root': {
    color: '#FF6F00', // Orange for errors
  },
  '& .MuiOutlinedInput-root': {
    '& fieldset': {
      borderColor: '#FF6F00', // Orange border
    },
    '&:hover fieldset': {
      borderColor: '#FF3D00', // Darker orange on hover
    },
    '&.Mui-focused fieldset': {
      borderColor: '#FF3D00', // Darker orange when focused
    },
  },
}));

const StyledButton = styled(Button)(({ theme }) => ({
  marginTop: theme.spacing(2),
  marginRight: theme.spacing(1),
  backgroundColor: '#FF6F00', // Orange background
  color: '#fff', // White text
  '&:hover': {
    backgroundColor: '#FF3D00', // Darker orange on hover
  },
}));

const CancelButton = styled(Button)(({ theme }) => ({
  marginTop: theme.spacing(2),
  backgroundColor: '#333', // Dark background
  color: '#fff', // White text
  '&:hover': {
    backgroundColor: '#555', // Lighter dark on hover
  },
}));

const AddEditCafePage = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { data: cafe } = useCafe(id);
  const { mutate: createCafe } = useCreateCafe();
  const { mutate: updateCafe } = useUpdateCafe();

  const { register, handleSubmit, formState: { errors }, reset } = useForm({
    resolver: yupResolver(schema),
    defaultValues: {
      name: '',
      description: '',
      location: '',
    },
  });

  React.useEffect(() => {
    if (cafe) {
      reset({
        name: cafe.name,
        description: cafe.description,
        location: cafe.location,
      });
    }
  }, [cafe, reset]);

  const onSubmit = (data) => {
    if (id) {
      updateCafe({ ...data, id });
    } else {
      createCafe(data);
    }
    navigate('/cafes');
  };

  return (
    <StyledCard>
      <Typography variant="h4" align="center" gutterBottom>
        {id ? 'Edit Cafe' : 'Add Cafe'}
      </Typography>
      <form onSubmit={handleSubmit(onSubmit)}>
        <StyledTextField
          {...register('name')}
          label="Name"
          error={!!errors.name}
          helperText={errors.name?.message}
          fullWidth
          variant="outlined"
        />
        <StyledTextField
          {...register('description')}
          label="Description"
          error={!!errors.description}
          helperText={errors.description?.message}
          fullWidth
          variant="outlined"
        />
        <StyledTextField
          {...register('location')}
          label="Location"
          error={!!errors.location}
          helperText={errors.location?.message}
          fullWidth
          variant="outlined"
        />
        <StyledButton type="submit" variant="contained">
          Submit
        </StyledButton>
        <CancelButton type="button" onClick={() => navigate('/cafes')} variant="contained">
          Cancel
        </CancelButton>
      </form>
    </StyledCard>
  );
};

export default AddEditCafePage;
