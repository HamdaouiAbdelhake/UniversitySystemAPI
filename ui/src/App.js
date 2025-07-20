import { Content, Header } from 'antd/es/layout/layout';
import './App.css';
import { BrowserRouter as Router, Link, Routes, Route, useLocation } from 'react-router-dom';
import { Menu, Layout } from 'antd';

import Login from './pages/Login';
import PrivateRoute from './components/PrivateRoute';
import RoleRoute from './components/RoleRoute';
import Grades from './pages/Grades';
import Students from './pages/Students';
import { logout } from './auth/auth';
import CreateStudent from './pages/CreateStudent';
import EditStudent from './pages/EditStudent';
import DeleteStudent from './pages/DeleteStudent';

// Create a separate component to access useLocation hook
function AppContent() {
  const location = useLocation();

  const menuItems = [
    {
      key: "/login",
      label: <Link to="/login">Login</Link>,
    },
    {
      key: "/students",
      label: <Link to="/students">Students</Link>,
    },
    {
      key: "/grades",
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

  // Get current path for highlighting
  const getCurrentMenuKey = () => {
    const pathname = location.pathname;
    
    // Handle nested routes - if we're on a student sub-page, highlight "students"
    if (pathname.startsWith('/students')) {
      return ['/students'];
    }
    if (pathname.startsWith('/grades')) {
      return ['/grades'];
    }
    if (pathname === '/login') {
      return ['/login'];
    }
    
    return [];
  };

  return (
    <Layout>
      <Header>
        <Menu
          mode="horizontal"
          selectedKeys={getCurrentMenuKey()}
          theme="dark"
          items={menuItems}
          onClick={handleMenuClick}
        />
      </Header>
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
          <Route
            path="/students/create"
            element={
              <RoleRoute allowedRoles={["Teacher"]}>
                <CreateStudent />
              </RoleRoute>
            }
          />
          <Route
            path="/students/:id/edit"
            element={
              <RoleRoute allowedRoles={["Teacher"]}>
                <EditStudent />
              </RoleRoute>
            }
          />
          <Route
            path="/students/:id/delete"
            element={
              <RoleRoute allowedRoles={["Teacher"]}>
                <DeleteStudent />
              </RoleRoute>
            }
          />
          <Route path="/unauthorized" element={<div>Unauthorized</div>} />
        </Routes>
      </Content>
    </Layout>
  );
}

function App() {
  return (
    <Router>
      <AppContent />
    </Router>
  );
}

export default App;