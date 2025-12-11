import React from 'react';
import { DriverDto } from '../types';

interface DriverListProps {
  drivers: DriverDto[];
  onEdit: (driver: DriverDto) => void;
  onDelete: (id: number) => void;
}

const DriverList: React.FC<DriverListProps> = ({ drivers, onEdit, onDelete }) => {
  if (drivers.length === 0) {
    return <p>No drivers found. Add your first driver above.</p>;
  }

  return (
    <div>
      <h2>Driver List</h2>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Team</th>
            <th>Country</th>
            <th>Championship Points</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {drivers.map((driver) => (
            <tr key={driver.driverId}>
              <td>{driver.driverId}</td>
              <td>{driver.name}</td>
              <td>{driver.team}</td>
              <td>{driver.country}</td>
              <td>{driver.championshipPoints}</td>
              <td className="actions">
                <button className="secondary" onClick={() => onEdit(driver)}>
                  Edit
                </button>
                <button className="danger" onClick={() => onDelete(driver.driverId)}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default DriverList;
