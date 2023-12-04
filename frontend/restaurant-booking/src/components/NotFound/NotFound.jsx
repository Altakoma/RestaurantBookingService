import "../../styles/NotFound.css";
import NotFoundHeader from './NotFoundHeader';

function NotFound(props) {
    return (
        <>
        <NotFoundHeader />
        <h1>Not found {props.route}</h1>
        </>
    );
}

export default NotFound;