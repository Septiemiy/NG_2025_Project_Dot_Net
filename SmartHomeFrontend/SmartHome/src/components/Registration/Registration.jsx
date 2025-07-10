import "./index.css";
import axiosClient from "../../services/axiosClients";
import { useState } from "react";

export default function Registration() {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');


    const handleRegistration = async (event) => {
        event.preventDefault();

        try {
            const response = await axiosClient.post('/user/register', {
                username,
                password,
                email,
                role: 0,
            });

            const token = response.data;
            localStorage.setItem('JwtToken', token);

            console.log('Registration successful:', response.data);

        } catch (error) {
            console.error('Registration failed:', error);
        }
    };

    return (
        <>
            <div className="registration-container">
                <form onSubmit={handleRegistration} method="post">
                    <div>
                        <label htmlFor="username">Username:</label>
                        <input type="text" id="username" value={username}
                        onChange={(data) => setUsername(data.target.value)} required />
                    </div>
                    <div>
                        <label htmlFor="email">Email:</label>
                        <input type="email" id="email" value={email}
                        onChange={(data) => setEmail(data.target.value)} required />
                    </div>
                    <div>
                        <label htmlFor="password">Password:</label>
                        <input type="password" id="password"
                        onChange={(data) => setPassword(data.target.value)} required />
                    </div>
                    <button type="submit">Sign in</button>
                </form>
            </div>
        </>
    );
}