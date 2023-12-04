import '../../styles/Welcome.css';

function NotFoundHeader() {
    return (
        <div className="header-container">
            <a className='' href='/login'>Login</a>
            <a className='' href='/register'>Register</a>
            <a className='' href='/'>Welcome</a>
            <a className='' href='/lobby'>Lobby</a>
        </div>
    );
}

export default NotFoundHeader;