import "./App.css";
import Login from "./components/Auth/Login";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Landing from "./components/Landing";
import Customers from "./components/Dashboard/Customers";
import Navbar from "./components/Layout/Navbar";
import { useEffect } from "react";
import { loadUser } from "./APIs/auth";
import { useUserContext } from "./context/user";
import Protected from "./utils/PrivateRoute";

function App() {
  const [userState, userDispatch] = useUserContext();
  useEffect(() => {
    const load = async () => {
      await loadUser(userDispatch);
    };
    load();
  }, []);
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/" element={<Landing />} />
        <Route
          path="/customers"
          element={
            <Protected>
              <Customers />
            </Protected>
          }
        />
      </Routes>
    </Router>
  );
}

export default App;
