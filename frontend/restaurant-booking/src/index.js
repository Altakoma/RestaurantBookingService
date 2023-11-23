import React from 'react';
import ReactDOM from 'react-dom/client';
import './styles/index.css';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Welcome from './components/Welcome/Welcome';
import Login from './components/Login/Login';
import Register from './components/Registration/Register';
import Lobby from './components/Lobby/Lobby';
import NotFound from './components/NotFound/NotFound';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <Router>
      <Routes>
        <Route path="/" element={<Welcome />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/lobby" element={<Lobby />} />
        <Route path="*" element={<NotFound route={window.location.href}/>} />
      </Routes>
    </Router>
  </React.StrictMode>
);

reportWebVitals();
