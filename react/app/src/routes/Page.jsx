import React from 'react';
import logo from '../logo.svg';
import '../App.css';
import { useParams } from 'react-router-dom';

function Page() {
    var params = useParams();
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
            Welcome to {params.slug}!
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default Page;
