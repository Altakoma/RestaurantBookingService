import '../../styles/Login.css';
import { postAuthData } from '../../auth.js';
import RegisterHeader from './RegisterHeader';
import Footer from '../Welcome/Footer';

function Register() {
  return (
    <div className='container'>
        <RegisterHeader />
        <div className="auth-container">
            <div class="form-container">
                <form method="post">
                    <h2>Register</h2>
                    <div class="input-box">
                        <input type="text" id="userName" required/>
                        <label>User name</label>
                    </div>
                    <div class="input-box">
                        <input type="text" id="login" required/>
                        <label>login</label>
                    </div>
                    <div class="input-box">
                        <input type="password" id="password" required/>
                        <label>password</label>
                    </div>
                    <input type="button" id="login-button" value="login" onClick={postData}/>
                </form>
            </div>
        </div>
        <Footer/>
    </div>
  );
}

function postData() {
  const userLogin = document.getElementById("login").value;
  const userPassword = document.getElementById("password").value;
  const userName = document.getElementById("userName").value;

  const data = {
     userRoleId: 2,
      login: userLogin,
      password: userPassword,
      name: userName,
  };

  const jsonData = JSON.stringify(data);
  const url = 'http://localhost:8074/api/identity/auth/register';
  const exceptionHandle = () => {};

  postAuthData(url, jsonData, register, exceptionHandle);
}

function register(data)
{
  window.location.replace("/login");
}

export default Register;
