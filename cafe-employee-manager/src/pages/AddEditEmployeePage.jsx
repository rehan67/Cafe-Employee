import React from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { useForm, Controller } from 'react-hook-form';
import * as yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import {
  TextField,
  Button,
  MenuItem,
  FormControl,
  InputLabel,
  Select,
  FormHelperText,
  Typography,
  Box,
  Grid,
} from '@mui/material';
import { styled } from '@mui/material/styles';
import { useEmployee, useCreateEmployee, useUpdateEmployee, useCafes } from '../queries/queries';

// Define the validation schema with yup
const schema = yup.object({
  name: yup.string().min(6, 'Name must be between 6 and 10 characters').max(10).required('Name is required'),
  emailAddress: yup.string().email('Invalid email address').required('Email is required'),
  phoneNumber: yup.string().matches(/^[89]\d{7}$/, 'Invalid phone number (should start with 8 or 9 and be 8 digits)').required('Phone number is required'),
  gender: yup.string().required('Gender is required'),
  cafeId: yup.string().nullable(),
});

// Styled components
const StyledContainer = styled(Box)(({ theme }) => ({
  maxWidth: 600,
  margin: '20px auto',
  padding: theme.spacing(3),
  backgroundColor: '#1c1c1c', // Dark background
  borderRadius: 8, // Rounded corners
  boxShadow: theme.shadows[3],
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

const StyledSelect = styled(Select)(({ theme }) => ({
  color: '#fff',
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

const SubmitButton = styled(Button)(({ theme }) => ({
  backgroundColor: '#FF6F00',
  color: '#fff',
  marginTop: theme.spacing(2),
  '&:hover': {
    backgroundColor: '#FF3D00',
  },
}));

const CancelButton = styled(Button)(({ theme }) => ({
  backgroundColor: '#333',
  color: '#fff',
  marginTop: theme.spacing(2),
  marginLeft: theme.spacing(1),
  '&:hover': {
    backgroundColor: '#555',
  },
}));

const AddEditEmployeePage = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const isEditMode = !!id;

  // Fetch employee and cafes data
  const { data: employee } = useEmployee(id);
  const { data: cafes } = useCafes();

  // Hooks for create and update employee operations
  const { mutate: createEmployee } = useCreateEmployee();
  const { mutate: updateEmployee } = useUpdateEmployee();

  // Initialize form methods from react-hook-form
  const {
    register,
    handleSubmit,
    formState: { errors },
    control,
    reset,
  } = useForm({
    resolver: yupResolver(schema),
    defaultValues: {
      name: '',
      emailAddress: '',
      phoneNumber: '',
      gender: 'male',
      cafeId: '',
    },
  });

  // Populate form values if editing an existing employee
  React.useEffect(() => {
    if (employee) {
      reset({
        name: employee.name || '',
        emailAddress: employee.emailAddress || '',
        phoneNumber: employee.phoneNumber || '',
        gender: employee.gender || 'male',
        cafeId: employee.cafe?.cafeId || '', // Optional chaining
      });
    }
  }, [employee, reset]);

  // Handle form submission
  const onSubmit = (data) => {
    if (isEditMode) {
      updateEmployee({ ...data, id });
    } else {
      createEmployee(data);
    }
    navigate('/employees');
  };

  return (
    <StyledContainer>
      <Typography variant="h4" align="center" gutterBottom>
        {isEditMode ? 'Edit Employee' : 'Add Employee'}
      </Typography>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Grid container spacing={2}>
          <Grid item xs={12}>
            <StyledTextField
              {...register('name')}
              label="Name"
              error={!!errors.name}
              helperText={errors.name?.message}
              fullWidth
              variant="outlined"
              InputLabelProps={{ shrink: true }} // Ensure label is fixed on top
            />
          </Grid>
          <Grid item xs={12}>
            <StyledTextField
              {...register('emailAddress')}
              label="Email"
              error={!!errors.emailAddress}
              helperText={errors.emailAddress?.message}
              fullWidth
              variant="outlined"
              InputLabelProps={{ shrink: true }} // Ensure label is fixed on top
            />
          </Grid>
          <Grid item xs={12}>
            <StyledTextField
              {...register('phoneNumber')}
              label="Phone"
              error={!!errors.phoneNumber}
              helperText={errors.phoneNumber?.message}
              fullWidth
              variant="outlined"
              InputLabelProps={{ shrink: true }} // Ensure label is fixed on top
            />
          </Grid>
          <Grid item xs={12}>
            <FormControl fullWidth margin="normal" variant="outlined">
              <InputLabel style={{ color: '#FF6F00' }}>Gender</InputLabel>
              <Controller
                name="gender"
                control={control}
                render={({ field }) => (
                  <StyledSelect {...field} error={!!errors.gender}>
                    <MenuItem value="male">Male</MenuItem>
                    <MenuItem value="female">Female</MenuItem>
                  </StyledSelect>
                )}
              />
              {errors.gender && <FormHelperText>{errors.gender.message}</FormHelperText>}
            </FormControl>
          </Grid>
          <Grid item xs={12}>
            <FormControl fullWidth margin="normal" variant="outlined">
              <InputLabel style={{ color: '#FF6F00' }}>Assigned Café</InputLabel>
              <Controller
                name="cafeId"
                control={control}
                render={({ field }) => (
                  <StyledSelect {...field} value={field.value || ''}>
                    <MenuItem value="">None</MenuItem>
                    {cafes?.map((cafe) => (
                      <MenuItem key={cafe.id} value={cafe.id}>
                        {cafe.name}
                      </MenuItem>
                    ))}
                  </StyledSelect>
                )}
              />
            </FormControl>
          </Grid>
        </Grid>
        <SubmitButton type="submit" variant="contained">
          Submit
        </SubmitButton>
        <CancelButton type="button" onClick={() => navigate('/employees')} variant="contained">
          Cancel
        </CancelButton>
      </form>
    </StyledContainer>
  );
};

export default AddEditEmployeePage;
