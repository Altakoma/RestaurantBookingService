import '../../styles/Welcome.css';

function WelcomeHeader() {
    return (
        <div className="header-container">
            <a className='' href='/login'>Login</a>
            <a className='' href='/register'>Register</a>
            <a className='' href='/lobby'>Lobby</a>
        </div>
    );
}

export default WelcomeHeader;