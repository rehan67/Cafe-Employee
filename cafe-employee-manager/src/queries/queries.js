import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import axios from 'axios';
import API_BASE_URL from '../config'; 

// Fetch Cafes
export const useCafes = () => {
  return useQuery({
    queryKey: ['cafes'],
    queryFn: () => axios.get(`${API_BASE_URL}/cafes`).then((res) => res.data),
  });
};

// Fetch Employees
export const useEmployees = () => {
  return useQuery({
    queryKey: ['employees'],
    queryFn: () => axios.get(`${API_BASE_URL}/employees`).then((res) => res.data),
  });
};

// Fetch Cafe by ID
export const useCafe = (id) => {
  return useQuery({
    queryKey: ['cafe', id],
    queryFn: () => axios.get(`${API_BASE_URL}/cafes/${id}`).then((res) => res.data),
    enabled: !!id, // Only fetch if id exists
  });
};

// Fetch Employee by ID
export const useEmployee = (id) => {
  return useQuery({
    queryKey: ['employee', id],
    queryFn: () => axios.get(`${API_BASE_URL}/employees/${id}`).then((res) => res.data),
    enabled: !!id, // Only fetch if id exists
  });
};

// Create Cafe
export const useCreateCafe = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (newCafe) => axios.post(`${API_BASE_URL}/cafes/add-cafes`, newCafe),
    onSuccess: () => {
      queryClient.invalidateQueries(['cafes']);
    },
  });
};

// Update Cafe
export const useUpdateCafe = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (updatedCafe) => axios.put(`${API_BASE_URL}/cafes/${updatedCafe.id}`, updatedCafe),
    onSuccess: () => {
      queryClient.invalidateQueries(['cafes']);
    },
  });
};

// Delete Cafe
export const useDeleteCafe = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id) => axios.delete(`${API_BASE_URL}/cafes/${id}`),
    onSuccess: () => {
      queryClient.invalidateQueries(['cafes']);
    },
  });
};

// Create Employee
export const useCreateEmployee = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (newEmployee) => axios.post(`${API_BASE_URL}/employees`, newEmployee),
    onSuccess: () => {
      queryClient.invalidateQueries(['employees']);
    },
  });
};

// Update Employee
export const useUpdateEmployee = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (updatedEmployee) => axios.put(`${API_BASE_URL}/employees/${updatedEmployee.id}`, updatedEmployee),
    onSuccess: () => {
      queryClient.invalidateQueries(['employees']);
    },
  });
};

// Delete Employee
export const useDeleteEmployee = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id) => axios.delete(`${API_BASE_URL}/employees/${id}`),
    onSuccess: () => {
      queryClient.invalidateQueries(['employees']);
    },
  });
};
