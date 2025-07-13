import LoginPage from './pages/LoginPage.jsx';
import RegistrationPage from './pages/RegistrationPage.jsx';
import RegisterDevicePage from './pages/RegisterDevicePage.jsx';

const Pages = [
    {
        path: '/login',
        element: <LoginPage />
    },
    {
        path: '/register',
        element: <RegistrationPage />
    },
    {
        path: '/register-device',
        element: <RegisterDevicePage />
    }
]

export default Pages;