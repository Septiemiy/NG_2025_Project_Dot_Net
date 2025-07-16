import "./index.css";
import axiosClient from "../../services/axiosClients";
import { DEVICE_TYPES, DEVICE_LOCATION } from "../../constants/optionData";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import getUserId from "../../services/getUserIdFromJWT";

export default function RegisterDevice() {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [type, setType] = useState('');
  const [location, setRoom] = useState('');
  const [categoryId, setCategoryId] = useState('');
  const [categories, setCategories] = useState([]);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const getCategories = async () => {
      try {
        const responseCategory = await axiosClient.get('/category/getAll');
        setCategories(responseCategory.data);
      } catch (error) {
        console.error("Failed to fetch categories:", error);
        setError("Failed to fetch categories. Please try again...");
      }
    };
    getCategories();
  }, []);

  const handleDeviceRegistration = async (event) => {
    event.preventDefault();

    setError('');

    const userId = getUserId();
    if (!userId) {
      setError("User ID not found. Please log in again.");
      navigate('/login');
      return;
    }

    try {
      const responseRegisterDevice = await axiosClient.post('/device/registerDevice', {
        name,
        description,
        type,
        location,
        categoryId,
        userId
      });

      const data = responseRegisterDevice.data;

      if (data.isSuccess) {
        setSuccess(data.message);
        navigate('/'); 
        console.log('Device registration successful:', data.message);
      } else {
        setError(data.message);
        console.error('Device registration failed:', data.message);
      }
    } catch (error) {
      setError("Device registration failed. Please try again...");
      console.error('Device registration failed:', error.response.data);
    }
  };

  return (
    <>
      <div className="register-device-container">
        <h2>Register device</h2>
        <form className="register-device-form" onSubmit={handleDeviceRegistration} method="post">
          {success && <div className="success-message">{success}</div>}
          {error && <div className="error-message">{error}</div>}
          <div>
            <input name="name" required placeholder="Device name"
            onChange={(data) => setName(data.target.value)}/>
          </div>
          <div>
            <input name="description" required placeholder="Device description"
            onChange={(data) => setDescription(data.target.value)}/>
          </div>
          <div>
            <label>Device type</label>
            <select name="type" required onChange={(data) => setType(data.target.value)}>
              <option value="">-- Choose type --</option>
              {DEVICE_TYPES.map((type) => (
                <option key={type.label} value={type.value}>{type.label}</option>
              ))}
            </select>
          </div>
          <div>
            <label>Device location</label>
            <select name="room" required onChange={(data) => setRoom(data.target.value)}>
              <option value="">-- Choose location --</option>
              {DEVICE_LOCATION.map((location) => (
                <option key={location.label} value={location.value}>{location.label}</option>
              ))}
            </select>
          </div>
          <div>
            <label>Device category</label>
            <select name="category" required onChange={(data) => setCategoryId(data.target.value)}>
              <option value="">-- Choose category --</option>
              {categories.map((category) => (
                <option key={category.categoryId} value={category.categoryId}>{category.name}</option>
              ))}
            </select>
          </div>
          <button type="submit">Register</button>
          <button type="button" onClick={() => navigate("/")}>Back</button>
        </form>
      </div>
    </>
  );
}
