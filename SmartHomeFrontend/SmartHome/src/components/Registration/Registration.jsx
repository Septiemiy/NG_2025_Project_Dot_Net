import "./index.css";
import axiosClient from "../../services/axiosClients";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function Registration() {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');
    const navigate = useNavigate();


    const handleRegistration = async (event) => {
        event.preventDefault();

        setError('');

        try {
            const response = await axiosClient.post('/user/register', {
                username,
                password,
                email,
                role: 0,
            });

            const data = response.data;
            
            console.log('Response data:', data);

            if(data.isSuccess) {
                setSuccess(data.message);
                localStorage.setItem('JwtToken', data.token);
                navigate('/');
                console.log('Registration successful:', data.message);
            } else {
                setError(data.message);
                console.error('Registration failed:', data.message);
            }

        } catch (error) {
            setError("Registration failed. Please try again...");
            console.error('Registration failed:', error);
        }
    };

    return (
        <>
            <div className="registration-container">
                <h1>Sign in</h1>
                <form className="registration-form" onSubmit={handleRegistration} method="post">
                    {success && <div className="success-message">{success}</div>}
                    {error && <div className="error-message">{error}</div>}
                    <div>
                        <input type="text" id="username" value={username} placeholder="Username"
                        onChange={(data) => setUsername(data.target.value)} required />
                    </div>
                    <div>
                        <input type="email" id="email" value={email} placeholder="Email"
                        onChange={(data) => setEmail(data.target.value)} required />
                    </div>
                    <div>
                        <input type="password" id="password" placeholder="Password"
                        onChange={(data) => setPassword(data.target.value)} required />
                    </div>
                    <button type="submit">Sign in</button>
                </form>
            </div>
        </>
    );
}