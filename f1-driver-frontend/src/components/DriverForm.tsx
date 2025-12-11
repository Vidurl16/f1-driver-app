import React, { useState, useEffect } from 'react';
import { DriverDto, CreateDriverDto } from '../types';
import { driverApi } from '../api';

interface DriverFormProps {
  driver: DriverDto | null;
  onSubmit: () => void;
  onCancel: () => void;
}

const DriverForm: React.FC<DriverFormProps> = ({ driver, onSubmit, onCancel }) => {
  const [formData, setFormData] = useState<CreateDriverDto>({
    name: '',
    team: '',
    country: '',
    championshipPoints: 0,
  });
  const [errors, setErrors] = useState<{ [key: string]: string }>({});
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    if (driver) {
      setFormData({
        name: driver.name,
        team: driver.team,
        country: driver.country,
        championshipPoints: driver.championshipPoints,
      });
    } else {
      setFormData({
        name: '',
        team: '',
        country: '',
        championshipPoints: 0,
      });
    }
    setErrors({});
  }, [driver]);

  const validate = (): boolean => {
    const newErrors: { [key: string]: string } = {};

    if (!formData.name.trim()) {
      newErrors.name = 'Name is required';
    }
    if (!formData.team.trim()) {
      newErrors.team = 'Team is required';
    }
    if (!formData.country.trim()) {
      newErrors.country = 'Country is required';
    }
    if (formData.championshipPoints < 0) {
      newErrors.championshipPoints = 'Championship points must be 0 or greater';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!validate()) {
      return;
    }

    try {
      setSubmitting(true);
      if (driver) {
        await driverApi.updateDriver(driver.driverId, formData);
      } else {
        await driverApi.createDriver(formData);
      }
      onSubmit();
    } catch (err) {
      setErrors({ submit: 'Failed to save driver' });
      console.error(err);
    } finally {
      setSubmitting(false);
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === 'championshipPoints' ? parseInt(value) || 0 : value,
    }));
  };

  const handleCancelClick = () => {
    setFormData({
      name: '',
      team: '',
      country: '',
      championshipPoints: 0,
    });
    setErrors({});
    onCancel();
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>{driver ? 'Edit Driver' : 'Add New Driver'}</h2>

      {errors.submit && <div className="error">{errors.submit}</div>}

      <div className="form-group">
        <label htmlFor="name">Name *</label>
        <input
          type="text"
          id="name"
          name="name"
          value={formData.name}
          onChange={handleChange}
          disabled={submitting}
        />
        {errors.name && <div className="error">{errors.name}</div>}
      </div>

      <div className="form-group">
        <label htmlFor="team">Team *</label>
        <input
          type="text"
          id="team"
          name="team"
          value={formData.team}
          onChange={handleChange}
          disabled={submitting}
        />
        {errors.team && <div className="error">{errors.team}</div>}
      </div>

      <div className="form-group">
        <label htmlFor="country">Country *</label>
        <input
          type="text"
          id="country"
          name="country"
          value={formData.country}
          onChange={handleChange}
          disabled={submitting}
        />
        {errors.country && <div className="error">{errors.country}</div>}
      </div>

      <div className="form-group">
        <label htmlFor="championshipPoints">Championship Points *</label>
        <input
          type="number"
          id="championshipPoints"
          name="championshipPoints"
          value={formData.championshipPoints}
          onChange={handleChange}
          disabled={submitting}
          min="0"
        />
        {errors.championshipPoints && (
          <div className="error">{errors.championshipPoints}</div>
        )}
      </div>

      <div className="actions">
        <button type="submit" className="primary" disabled={submitting}>
          {submitting ? 'Saving...' : driver ? 'Update Driver' : 'Add Driver'}
        </button>
        {driver && (
          <button
            type="button"
            className="secondary"
            onClick={handleCancelClick}
            disabled={submitting}
          >
            Cancel
          </button>
        )}
      </div>
    </form>
  );
};

export default DriverForm;
