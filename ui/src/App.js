import { Content, Header } from 'antd/es/layout/layout';
import './App.css';
import { BrowserRouter as Router, Link, Routes, Route } from 'react-router-dom';
import { Menu, Layout } from 'antd';

import Login from './pages/Login';
import PrivateRoute from './components/PrivateRoute';
import RoleRoute from './components/RoleRoute';
import Grades from './pages/Grades';
import Students from './pages/Students';
import { logout } from './auth/auth';

function App() {
  const menuItems = [
    {
      key: "login",
      label: <Link to="/login">Login</Link>,
    },
    {
      key: "students",
      label: <Link to="/students">Students</Link>,
    },
    {
      key: "grades",
      label: <Link to="/grades">Grades</Link>,
    },
    {
      key: "logout",
      label: "Logout",
    },
  ];

  // Handle menu click events
  const handleMenuClick = (e) => {
    if (e.key === 'logout') {
      logout();
    }
  };

  return (
    <Router>
      <Layout>
        <Header>
          <Menu
            mode="horizontal"
            defaultSelectedKeys={["login"]}
            theme="dark"
            items={menuItems}
            onClick={handleMenuClick}
          />
        </Header>
      </Layout>
      <Content style={{ padding: "20px" }}>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route
            path="/students"
            element={
              <PrivateRoute>
                <Students />
              </PrivateRoute>
            }
          />
          <Route
            path="/grades"
            element={
              <RoleRoute allowedRoles={["Teacher"]}>
                <Grades />
              </RoleRoute>
            }
          />
          <Route path="/unauthorized" element={<div>Unauthorized</div>} />
        </Routes>
      </Content>

    </Router>
  );
}

export default App;
