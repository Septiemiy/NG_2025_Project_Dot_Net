import "./index.css";
import axiosClient from "../../services/axiosClients";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function Login() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');
    const navigate = useNavigate();


    const handleLogin = async (event) => {
        event.preventDefault();

        try {
            const response = await axiosClient.post('/user/login', {
                username,
                password
            });

            const data = response.data;

            if(data.isSuccess) {
                setSuccess(data.message);
                localStorage.setItem('JwtToken', data.token);
                navigate('/');
                console.log('Login successful:', data.message);
            } else {
                setError(data.message);
                console.error('Login failed:', data.message);
            }

        } catch (error) {
            setError("Login failed. Please try again...");
            console.error('Login failed:', error);
        }
    };

    return (
        <>
            <div className="login-container">
                <h1>Login in</h1>
                <form className="login-form" onSubmit={handleLogin} method="post">
                    {success && <div className="success-message">{success}</div>}
                    {error && <div className="error-message">{error}</div>}
                    <div>
                        <input type="text" id="username" value={username} placeholder="Username"
                        onChange={(data) => setUsername(data.target.value)} required />
                    </div>
                    <div>
                        <input type="password" id="password" placeholder="Password"
                        onChange={(data) => setPassword(data.target.value)} required />
                    </div>
                    <button type="submit">Login</button>
                    <p>Not registered yet? <span onClick={() => navigate("/register")}>Sign up</span></p>
                </form>
            </div>
        </>
    );
}