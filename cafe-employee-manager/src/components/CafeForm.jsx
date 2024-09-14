import React from 'react';
import { useForm } from 'react-hook-form';
import { useNavigate } from '@tanstack/react-router';
import { useAddCafe } from '../queries/queries';
import { TextField, Button } from '@mui/material';

const CafeForm = ({ cafe }) => {
  const { register, handleSubmit, formState: { errors } } = useForm({ defaultValues: cafe });
  const navigate = useNavigate();
  const { mutate: addCafe } = useAddCafe();

  const onSubmit = (data) => {
    addCafe(data);
    navigate('/cafes');
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <TextField {...register('name', { required: true, minLength: 6, maxLength: 10 })} label="Name" />
      {errors.name && <span>Name is required and must be between 6 and 10 characters</span>}

      <TextField {...register('description', { maxLength: 256 })} label="Description" />
      
      <TextField {...register('logo')} type="file" label="Logo" />
      
      <TextField {...register('location', { required: true })} label="Location" />
      {errors.location && <span>Location is required</span>}
      
      <Button type="submit">Submit</Button>
      <Button onClick={() => navigate('/cafes')}>Cancel</Button>
    </form>
  );
};

export default CafeForm;
