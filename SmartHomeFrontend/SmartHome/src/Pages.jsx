import LoginPage from './pages/LoginPage.jsx';
import RegistrationPage from './pages/RegistrationPage.jsx';

const Pages = [
    {
        path: '/login',
        element: <LoginPage />
    },
    {
        path: '/register',
        element: <RegistrationPage />
    }
]

export default Pages;