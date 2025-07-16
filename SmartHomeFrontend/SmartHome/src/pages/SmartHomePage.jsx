import "./css/SmartHomePage.css";
import { Outlet } from "react-router-dom";
import Sidebar from "../components/Sidebar/Sidebar";
import Header from "../components/Header/Header";

export default function SmartHomePage() {
    return (
        <>
        <div className="header-page">
            <Header />
            <div className="page-container">
                <Sidebar />
                <div className="content-container">
                    <Outlet />
                </div>
            </div>
        </div>
        </>
    );
}