import { Navigate } from "react-router-dom";

export default function CheckUserLogin({ children }) {
    const token = localStorage.getItem('JwtToken');

    if (!token) {
        return <Navigate to="/login" replace />;
    }

    return children;
}