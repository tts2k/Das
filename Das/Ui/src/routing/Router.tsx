import { BrowserRouter, Routes, Route } from "react-router-dom";
import { routeData } from "./routeData";

export const Router = () => {
    return (
        <BrowserRouter>
            <Routes>
                { routeData.map((e, k) => <Route key={ k } path={ e.link } element = { e.view }/>)}
            </Routes>
        </BrowserRouter>
    )
}
