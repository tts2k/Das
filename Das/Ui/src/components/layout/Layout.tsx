import { useState } from "react";
import { Router } from "../../routing/Router";
import { Sidebar } from "./Sidebar/Sidebar";
import { Header } from "./Header/Header";
import './Layout.css';

export const Layout = () => {
    const [isSidebarOpen, setSidebarOpen] = useState(false);

    const toggerSidebar = () => {
        setSidebarOpen(!isSidebarOpen)
    }

    return (
        <>
            <Sidebar isOpen={ isSidebarOpen }/>
            <div className={ isSidebarOpen ? "wrapper-sidebar-open" : "wrapper-sidebar-close"}>
                <Header toggerSidebar={ toggerSidebar } isSidebarOpen={ isSidebarOpen }/>
                <Router />
            </div>
        </>
    )
}
