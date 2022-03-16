import { Navbar, Button} from "react-bootstrap";
import "./Header.css";

interface IHeaderProps {
    toggerSidebar: () => void,
    isSidebarOpen: boolean
}

export const Header = (props: IHeaderProps) => {

    return (
        <Navbar bg="light" className="navbar shadow-sm p-2 mb-5 bg-white expand">
            <Button variant="outline-dark" onClick={() => props.toggerSidebar()}>
                <i className="bi bi-list"></i>
            </Button>
        </Navbar>
    )
}
