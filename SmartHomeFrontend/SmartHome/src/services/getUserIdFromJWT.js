import { jwtDecode } from 'jwt-decode';

export default function getUserId() {
    const jwtToken = localStorage.getItem('JwtToken');

    if (!jwtToken) {
        console.error('No JWT token found in localStorage');
        return null;
    }

    const decodedToken = jwtDecode(jwtToken);

    return decodedToken.sub;
}