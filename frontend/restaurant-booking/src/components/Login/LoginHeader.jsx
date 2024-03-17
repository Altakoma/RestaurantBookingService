import '../../styles/Welcome.css';

function LoginHeader() {
    return (
        <div className="header-container">
            <a className='' href='/register'>Register</a>
            <a className='' href='/'>Welcome</a>
            <a className='' href='/lobby'>Lobby</a>
        </div>
    );
}

export default LoginHeader;