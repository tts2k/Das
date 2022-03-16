import { Home } from "../views/Home/Home";
import { System } from "../views/System/System";
import { Login } from "../views/Login/Login";

export const routeData = [
    {
        title: "Home",
        link: "/",
        view: <Home/>,
    },
    {
        title: "System",
        link: "/system",
        view: <System/>,
    },
    {
        title: "Login",
        link: "/login",
        view: <Login/>,
    },
]
