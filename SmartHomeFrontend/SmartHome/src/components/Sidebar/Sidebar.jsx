import { NavLink } from "react-router-dom";
import { useState, useEffect } from "react";
import getUserRole from "../../services/getUserRole";
import axiosClient from "../../services/axiosClients";
import CategoryMW from "../ModalWindows/CategoryMW";
import { DEVICE_TYPES, DEVICE_LOCATION } from "../../constants/optionData";
import './index.css';

export default function Sidebar() {
  const [expandedDevices, setExpandedDevices] = useState([]);
  const [categories, setCategories] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalWindowTitle, setModalWindowTitle] = useState("");
  const [editingId, setEditingId] = useState(null);
  const [categoryName, setCategoryName] = useState("");
  const [locationFilter, setLocationFilter] = useState("");
  const [typeFilter, setTypeFilter] = useState("");
  const [filteredCategories, setFilteredCategories] = useState([]);
  const isAdmin = getUserRole() === "Admin";

  const loadCategoriesAndDevices = async () => {
    try {
      const [ categoriesResponse, devicesResponse ] = await Promise.all([
        axiosClient.get("/category/getAll"),
        axiosClient.get("/device/getAll")
      ]);

      const categoriesData = categoriesResponse.data;
      const devicesData = devicesResponse.data;

      const grouped = categoriesData.map(category => ({
        ...category,
        devices: devicesData.filter(device => device.categoryId === category.categoryId)
      }));

      setCategories(grouped);
    } catch (error) {
      console.error("Failed to get categories:", error);
    }
  };

  useEffect(() => { 
    loadCategoriesAndDevices(); 
  }, []);

  useEffect(() => {
    setFilteredCategories(categories);
  }, [categories]);

  const addCategory = async (name) => {
    const { data } = await axiosClient.post("/category/addCategory", { categoryId: "0", name: name });
    return data;
  };

  const updateCategory = async (id, name) => {
    await axiosClient.post(`/category/changeCategoryName`, { categoryId: id, name: name });
  };

  const toggle = (id) => {
    setExpandedDevices((prev) =>
      prev.includes(id) ? prev.filter((x) => x !== id) : [...prev, id]
    );
  };

  const handleFilter = () => {
    const filtered = categories.map(category => ({
      ...category,
      devices: category.devices.filter(device => {
        const location = locationFilter ? device.location === locationFilter : true
        const type = typeFilter ? device.type === typeFilter : true
        return location && type
      })
    }))

    setFilteredCategories(filtered);
  }

  const handleAddCategory = () => {
    setModalWindowTitle("Add category")
    setCategoryName("")
    setEditingId(null)
    setIsModalOpen(true)
  };

  const handleEditCategory = (id, currentName) => {
    setModalWindowTitle("Edit category name")
    setCategoryName(currentName)
    setEditingId(id)
    setIsModalOpen(true)
  };

  const handleSave = async (name) => {
    if(editingId) {
      try {
        await updateCategory(editingId, name);
        await loadCategoriesAndDevices();
      } catch (error) {
        console.error("Update category failed:", error);
      }
    } else {
      try {
        const newCategory = await addCategory(name);
        setCategories((prev) => [...prev, { ...newCategory, devices: [] }]);
        await loadCategoriesAndDevices();
      } catch (error) {
        console.error("Add category failed:", error);
      }
    }

    setIsModalOpen(false)
  }

  return (
    <aside className="sidebar">
      <div className="sidebar-content">
        <div className="filters">
          <label>
            <select value={locationFilter} onChange={(e) => setLocationFilter(e.target.value)}>
              <option value="">All locations</option>
              {DEVICE_LOCATION.map((location) => (
                <option key={location.value} value={location.value}>{location.label}</option>
              ))}
            </select>
          </label>

          <label>
            <select value={typeFilter} onChange={(e) => setTypeFilter(e.target.value)}>
              <option value="">All types</option>
              {DEVICE_TYPES.map((type) => (
                <option key={type.value} value={type.value}>{type.label}</option>
              ))}
            </select>
          </label>

          <button onClick={handleFilter}>Filter</button>
        </div>
        <h2>Devices</h2>

        {filteredCategories.map((category) => (
          <div key={category.categoryId}>
            <div>
              <button className="category-button" onClick={() => toggle(category.categoryId)}>
                {category.name}
                {isAdmin && (
                  <span onClick={(e) => {
                    e.stopPropagation();
                    handleEditCategory(category.categoryId, category.name)}}>✏️</span>    
                )}
              </button> 
            </div>

            {expandedDevices.includes(category.categoryId) && (
              <div className="device-list">
                {category.devices?.map((device) => (
                  <NavLink className="device" key={device.deviceId} to={`/device/${device.deviceId}`}>
                    {device.name}
                  </NavLink>
                ))}
                {(!category.devices || category.devices.length === 0) && (
                  <p>No devices</p>
                )}
              </div>
            )}
          </div>
        ))}

        {isAdmin && (
          <button onClick={handleAddCategory}>
            ➕ Add Category
          </button>
        )}

        <CategoryMW isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} 
          onSave={handleSave} categoryName={categoryName} title={modalWindowTitle} />
      </div>
    </aside>
  );
}
