import { isLoggedIn, getUserInfo } from "../auth/auth";
import { Navigate } from "react-router-dom";

const RoleRoute = ({ allowedRoles, children }) => {
    if (!isLoggedIn()) return <Navigate to="/login" />;
    const userInfo = getUserInfo();
    console.log(userInfo)
    if (!userInfo || !allowedRoles.includes(userInfo.role)) {
        return <Navigate to="/unauthorized" />;
    }
    return children;
};

export default RoleRoute;
