import { useEffect, useState } from "react"
import { Container, Row, Col, Card, ListGroup, ListGroupItem, ProgressBar} from "react-bootstrap"
import axios from "axios";

interface IPlatformInformation {
    distro: string,
    arch: string,
    kernel: string;
}

interface ISystemInformation {
    cpuLoad: number,
    mem: number
}

export const Home = () => {
    const [platformInfo, setPlatformInfo] = useState<IPlatformInformation>(
        { distro: "", arch: "", kernel: "" }
    );
    const [systemInfo, setSystemInfo] = useState<ISystemInformation>({
        cpuLoad: 0,
        mem: 0
    })

    useEffect(() => {
        axios.get("api/platform")
        .then((res) => {
            setPlatformInfo(res.data);
        });

        const interval = setInterval( async () =>{
            await getSystemInfo();
        }, 1000);

        return () => {
            clearInterval(interval);
        }
    }, []);

    const getSystemInfo = async () => {
        try {
            let cpu = await axios.get("api/system/cpu");
            let mem = await axios.get("api/system/mem");

            setSystemInfo({
                cpuLoad: cpu.data.value * 100,
                mem: mem.data.used / mem.data.total * 100
            });
        }
        catch (err: any) {
            // Omit error message when switching page during a fetch
            if (err.message !== "Request aborted") {
                console.error(err);
            }
        }

    }

    const InfoCard = (props: any) => {
        return (
            <Card>
                <Card.Header>
                    { props.header }
                </Card.Header>
                { props.children }
            </Card>
        )
    }

    return (
        <Container>
            <Row>
                <Col>
                    <InfoCard header="Information">
                        <ListGroup variant="flush">
                            <ListGroupItem>
                                <strong>OS: </strong>
                                { platformInfo.distro }
                            </ListGroupItem>
                            <ListGroupItem>
                                <strong>Arch: </strong>
                                { platformInfo.arch }
                            </ListGroupItem>
                            <ListGroupItem>
                                <strong>Kernel: </strong>
                                { platformInfo.kernel }
                            </ListGroupItem>
                        </ListGroup>
                    </InfoCard>
                </Col>
                <Col>
                    <InfoCard header="Statistics">
                        <Card.Body>
                            <strong>CPU: </strong>
                            <ProgressBar now={ systemInfo.cpuLoad } />
                            <strong>RAM: </strong>
                            <ProgressBar now={ systemInfo.mem } />
                        </Card.Body>
                    </InfoCard>
                </Col>
            </Row>
        </Container>
    )
}
