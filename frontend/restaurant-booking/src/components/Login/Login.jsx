import '../../styles/Login.css';
import { postAuthData } from '../../auth.js';
import LoginHeader from './LoginHeader';
import Footer from '../Welcome/Footer';

function Login() {
  return (
    <div className='container'>
      <LoginHeader />
      <div className="auth-container">
        <div class="form-container">
            <form method="post">
                <h2>Login</h2>
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

  const data = {
      login: userLogin,
      password: userPassword,
  };

  const jsonData = JSON.stringify(data);
  const url = 'http://localhost:8074/api/identity/auth/login';
  const exceptionHandle = () => {};

  postAuthData(url, jsonData, handleLogin, exceptionHandle);
}

export function handleLogin(data)
{
  const encodedToken = data.encodedToken;
  setCookie("Authorization", encodedToken);

  if(data.encodedToken === undefined) {
    throw new Error();
  }

  if(window.location.pathname !== "/lobby")
  {
    window.location.replace("/lobby");
  }
}

function setCookie(name, value) {
  let updatedCookie = encodeURIComponent(name) + 
                      "=" + encodeURIComponent(value);

  document.cookie = updatedCookie;
}

export function getCookie(cookieName) {
  let name = cookieName + "=";
  let decodedCookie = decodeURIComponent(document.cookie);
  let ca = decodedCookie.split(';');
  for(let i = 0; i <ca.length; i++) {
    let c = ca[i];
    while (c.charAt(0) === ' ') {
      c = c.substring(1);
    }
    if (c.indexOf(name) === 0) {
      return c.substring(name.length, c.length);
    }
  }
  return "";
}

export default Login;
