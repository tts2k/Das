import { useEffect, useState } from "react";
import { routeData } from "../../../routing/routeData";
import "./Sidebar.css";

interface ISidebarProps {
    isOpen : boolean
}

export const Sidebar = (props: ISidebarProps) => {
    const className = "bg-dark Sidebar" + (props.isOpen ? " Sidebar-open" : " Sidebar-close");
    const [hostname, setHostname] = useState("");

    useEffect(() => {
        fetch("/api/platform/hostname")
        .then((res) =>{
            res.json()
            .then((data) =>{
                setHostname(data.value);
            })
        });
    }, []);

    return (
        <div className={ className }>
            <h1 className="SidebarTitle text-light">{ hostname }</h1>
            <ul>
                { routeData.map((e, k) =>
                <li key={ k }>
                    <a className="SidebarItem text-light" href={ e.link }>{ e.title }</a>
                </li>
                )}
            </ul>
        </div>
    )
}
