import '../../styles/Welcome.css';
import '../../styles/index.css';
import WelcomeHeader from './WelcomeHeader';

function Welcome() {
    return (
      <div className='container'>
        <WelcomeHeader />
        <div className="welcome-body-container">
          <h1>Welcome to our booking</h1>
        </div>
        <div className="footer-container">
        </div>
      </div>
    );
}

export default Welcome;