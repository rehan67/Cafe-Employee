// src/router/router.jsx
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Layout from '../pages/Layout';
import CafesPage from '../pages/CafesPage';
import EmployeesPage from '../pages/EmployeesPage';
import AddEditCafePage from '../pages/AddEditCafePage';
import AddEditEmployeePage from '../pages/AddEditEmployeePage';

export default function AppRouter() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route path="/cafes" element={<CafesPage />} />
          <Route path="/employees" element={<EmployeesPage />} />
          <Route path="/cafes/add" element={<AddEditCafePage />} />
          <Route path="/employees/add" element={<AddEditEmployeePage />} />
          <Route path="/cafes/edit/:id" element={<AddEditCafePage />} />
          <Route path="/employees/edit/:id" element={<AddEditEmployeePage />} />
        </Route>
      </Routes>
    </Router>
  );
}
