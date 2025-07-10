import DeviceDetail from "../components/DeviceDetail/DeviceDetail";
import { Outlet } from "react-router-dom";

export default function SmartHomePage() {
    return (
        <>
            <Outlet />
        </>
    );
}