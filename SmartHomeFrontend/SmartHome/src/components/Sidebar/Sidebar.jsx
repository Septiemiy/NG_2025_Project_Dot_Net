import { NavLink } from 'react-router-dom';

export default function Sidebar() {
  const devices = [
    { id: '1', name: 'Sensor A' },
    { id: '2', name: 'Light Kitchen' },
  ];

  return (
    <aside>
      <h2>Devices</h2>
      {devices.map((device) => (
        <NavLink
          key={device.id}
          to={`/device/${device.id}`}
        >
          {device.name}
        </NavLink>
      ))}
    </aside>
  );
}
