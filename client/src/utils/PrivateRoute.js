import { Navigate } from "react-router-dom";
import { useUserContext } from "../context/user";

const Protected = ({ children }) => {
  const [state, dispatch] = useUserContext();
  if (state?.token) {
    return children;
  }
  return <Navigate to="/login" replace />;
};

export default Protected;
