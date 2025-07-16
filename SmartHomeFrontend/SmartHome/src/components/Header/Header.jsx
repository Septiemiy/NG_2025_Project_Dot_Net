import "./index.css";
import getUserName from "../../services/getUserName";
import getUserRole from "../../services/getUserRole";
import { useNavigate } from "react-router-dom";


export default function Header() {
    const userName = getUserName();
    const userRole = getUserRole();
    const navigate = useNavigate();

    const handleLogout = () => {
        localStorage.removeItem('JwtToken');
        navigate('/login');
    }

    return (
        <header className="header">
            <div className="header-left">
                <h2>Smart Home Dashboard</h2>
                <div className="user-info">
                    <span className="user-name">{userName}</span>
                    <span className={`role-${userRole}`}>{userRole}</span>
                </div>
                <button className="register-device-btn" onClick={() => navigate("/register-device")}>Register Device</button>
            </div>
            <button className="logout-btn" onClick={handleLogout}>Log Out</button>
        </header>
    );
}