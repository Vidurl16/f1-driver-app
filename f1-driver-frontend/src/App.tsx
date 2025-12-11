import { useState, useEffect } from 'react';
import { DriverDto } from './types';
import { driverApi } from './api';
import DriverList from './components/DriverList';
import DriverForm from './components/DriverForm';

function App() {
  const [drivers, setDrivers] = useState<DriverDto[]>([]);
  const [editingDriver, setEditingDriver] = useState<DriverDto | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    loadDrivers();
  }, []);

  const loadDrivers = async () => {
    try {
      setLoading(true);
      const data = await driverApi.getAllDrivers();
      setDrivers(data);
      setError(null);
    } catch (err) {
      setError('Failed to load drivers');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id: number) => {
    if (!confirm('Are you sure you want to delete this driver?')) {
      return;
    }

    try {
      await driverApi.deleteDriver(id);
      await loadDrivers();
    } catch (err) {
      setError('Failed to delete driver');
      console.error(err);
    }
  };

  const handleEdit = (driver: DriverDto) => {
    setEditingDriver(driver);
  };

  const handleFormSubmit = async () => {
    setEditingDriver(null);
    await loadDrivers();
  };

  const handleCancel = () => {
    setEditingDriver(null);
  };

  return (
    <div className="container">
      <h1>F1 Driver Manager</h1>
      
      {error && <div className="error">{error}</div>}
      
      <DriverForm
        driver={editingDriver}
        onSubmit={handleFormSubmit}
        onCancel={handleCancel}
      />

      {loading ? (
        <div className="loading">Loading drivers...</div>
      ) : (
        <DriverList
          drivers={drivers}
          onEdit={handleEdit}
          onDelete={handleDelete}
        />
      )}
    </div>
  );
}

export default App;
