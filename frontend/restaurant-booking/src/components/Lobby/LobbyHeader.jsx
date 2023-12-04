import '../../styles/Welcome.css';

function LobbyHeader() {
    return (
        <div className="header-container">
            <a className='' href='/login'>Login</a>
            <a className='' href='/register'>Register</a>
            <a className='' href='/'>Welcome</a>
        </div>
    );
}

export default LobbyHeader;