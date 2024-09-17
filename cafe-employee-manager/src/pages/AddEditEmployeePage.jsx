import React, { useState } from 'react';
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
  Snackbar,
  Alert,
} from '@mui/material';
import { styled } from '@mui/material/styles';
import { useEmployee, useCreateEmployee, useUpdateEmployee, useCafes } from '../queries/queries';

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

  // Define the validation schema with yup
  const schema = yup.object({
    Id: isEditMode
      ? yup.string() // Optional in edit mode
      : yup
          .string()
          .matches(/^UI\d{9}$/, 'Employee Id must be in the format UIXXXXXXXXX, where X is a digit.')
          .required('Employee Id is required'),
    name: yup.string().min(4, 'Name must be between 4 and 10 characters').max(10).required('Name is required'),
    emailAddress: yup.string().email('Invalid email address').required('Email is required'),
    phoneNumber: yup
      .string()
      .matches(/^[89]\d{7}$/, 'Invalid phone number (should start with 8 or 9 and be 8 digits)')
      .required('Phone number is required'),
    gender: yup.string().required('Gender is required'),
    cafeId: yup.string().nullable(),
  });

  // Fetch employee and cafes data
  const { data: employee } = useEmployee(id);
  const { data: cafes } = useCafes();

  // Hooks for create and update employee operations
  const { mutate: createEmployee, isError: isCreateError, error: createError } = useCreateEmployee();
  const { mutate: updateEmployee, isError: isUpdateError, error: updateError } = useUpdateEmployee();

  // Snackbar state for error notifications
  const [errorMessage, setErrorMessage] = useState(null);
  const [openSnackbar, setOpenSnackbar] = useState(false);

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
        Id: employee.Id || '',
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

      console.log("Edit" , data)
      updateEmployee({ ...data, id }, {
        onError: (error) => {
          handleError(error);
        },
        onSuccess: () => {
          navigate('/employees');
        },
      });
    } else {
      createEmployee(data, {
        onError: (error) => {
          handleError(error);
        },
        onSuccess: () => {
          navigate('/employees');
        },
      });
    }
  };

  // Handle error for API mutations
  const handleError = (error) => {
    const errorMessage = error?.response?.data?.message || 'An error occurred while saving the employee.';
    setErrorMessage(errorMessage);
    setOpenSnackbar(true);
  };

  // Close snackbar
  const handleCloseSnackbar = () => {
    setOpenSnackbar(false);
  };

  return (
    <StyledContainer>
      <Typography variant="h4" align="center" gutterBottom>
        {isEditMode ? 'Edit Employee' : 'Add Employee'}
      </Typography>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Grid container spacing={2}>
          <Grid item xs={12}>
            {!isEditMode && (
              <StyledTextField
                {...register('Id')}
                label="Employee Id (UIXXXXXXX)"
                error={!!errors.Id}
                helperText={errors.Id?.message}
                fullWidth
                variant="outlined"
                InputLabelProps={{ shrink: true }}
              />
            )}
          </Grid>
          <Grid item xs={12}>
            <StyledTextField
              {...register('name')}
              label="Name"
              error={!!errors.name}
              helperText={errors.name?.message}
              fullWidth
              variant="outlined"
              InputLabelProps={{ shrink: true }}
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
              InputLabelProps={{ shrink: true }}
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
              InputLabelProps={{ shrink: true }}
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

      {/* Snackbar for error notifications */}
      <Snackbar open={openSnackbar} autoHideDuration={6000} onClose={handleCloseSnackbar}>
        <Alert onClose={handleCloseSnackbar} severity="error" sx={{ width: '100%' }}>
          {errorMessage}
        </Alert>
      </Snackbar>
    </StyledContainer>
  );
};

export default AddEditEmployeePage;
