import { DriverDto, CreateDriverDto } from './types';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000/api';

export const driverApi = {
  async getAllDrivers(): Promise<DriverDto[]> {
    const response = await fetch(`${API_BASE_URL}/drivers`);
    if (!response.ok) {
      throw new Error('Failed to fetch drivers');
    }
    return response.json();
  },

  async getDriverById(id: number): Promise<DriverDto> {
    const response = await fetch(`${API_BASE_URL}/drivers/${id}`);
    if (!response.ok) {
      throw new Error('Failed to fetch driver');
    }
    return response.json();
  },

  async createDriver(driver: CreateDriverDto): Promise<DriverDto> {
    const response = await fetch(`${API_BASE_URL}/drivers`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(driver),
    });
    if (!response.ok) {
      const error = await response.json();
      throw new Error(error.message || 'Failed to create driver');
    }
    return response.json();
  },

  async updateDriver(id: number, driver: CreateDriverDto): Promise<DriverDto> {
    const response = await fetch(`${API_BASE_URL}/drivers/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(driver),
    });
    if (!response.ok) {
      const error = await response.json();
      throw new Error(error.message || 'Failed to update driver');
    }
    return response.json();
  },

  async deleteDriver(id: number): Promise<boolean> {
    const response = await fetch(`${API_BASE_URL}/drivers/${id}`, {
      method: 'DELETE',
    });
    if (!response.ok) {
      throw new Error('Failed to delete driver');
    }
    return true;
  },
};
