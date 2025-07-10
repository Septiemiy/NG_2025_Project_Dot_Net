import "./index.css";
import axiosClient from "../../services/axiosClients";
import { useState } from "react";

export default function Login() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');


    const handleLogin = async (event) => {
        event.preventDefault();

        try {
            const response = await axiosClient.post('/user/login', {
                username,
                password
            });

            const token = response.data;
            localStorage.setItem('JwtToken', token);

            console.log('Login successful:', response.data);

        } catch (error) {
            console.error('Login failed:', error);
        }
    };

    return (
        <>
            <div className="login-container">
                <form onSubmit={handleLogin} method="post">
                    <div>
                        <label htmlFor="username">Username:</label>
                        <input type="text" id="username" value={username}
                        onChange={(data) => setUsername(data.target.value)} required />
                    </div>
                    <div>
                        <label htmlFor="password">Password:</label>
                        <input type="password" id="password"
                        onChange={(data) => setPassword(data.target.value)} required />
                    </div>
                    <button type="submit">Login</button>
                </form>
            </div>
        </>
    );
}