import { jwtDecode } from 'jwt-decode';

export default function getUserRole() {
    const jwtToken = localStorage.getItem('JwtToken');

    if (!jwtToken) {
        console.error('No JWT token found in localStorage');
        return null;
    }

    const decodedToken = jwtDecode(jwtToken);

    return decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ?? "User";
}