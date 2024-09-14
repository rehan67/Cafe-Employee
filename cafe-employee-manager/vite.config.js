// vite.config.js
import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// Define the Vite configuration
export default defineConfig({
  base: '/', // Set base path to root if served from root
  build: {
    outDir: 'dist', // Ensure this matches the Dockerfile
    rollupOptions: {
      // Customize Rollup options if needed
    },
  },
  server: {
    // Optional: Configure server options for local development
    proxy: {
      '/api': 'http://localhost:26655', // Example proxy configuration
    },
  },
  plugins: [react()], // Use React plugin for Vite
});
