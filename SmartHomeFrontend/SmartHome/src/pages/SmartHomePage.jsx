
import { Outlet } from "react-router-dom";
import Sidebar from "../components/Sidebar/Sidebar";

export default function SmartHomePage() {
    return (
        <>
            <Sidebar />
            <div>
                <Outlet />
            </div>
        </>
    );
}