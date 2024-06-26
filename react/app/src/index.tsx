import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import reportWebVitals from './reportWebVitals';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import Layout, {loader as layoutLoader } from './routes/_layout';
import ErrorPage from './error-page';
import Page, {loader as pageLoader} from './routes/page';
import Resume, {loader as resumeLoader} from './routes/resume/resume';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

const router = createBrowserRouter([
  { path: '/resume', element: <Resume />, errorElement: <ErrorPage />, loader: resumeLoader },
  { path: '/', element: <Layout />, errorElement: <ErrorPage />, loader: layoutLoader,
    children: [
      { path: "/:slug?", element: <Page />, loader: pageLoader },
    ]
  }
]);

root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals(console.log);
