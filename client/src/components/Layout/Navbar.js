import React from "react";
import { Link } from "react-router-dom";
import { useUserContext } from "../../context/user";
import { logout } from "../../APIs/auth";

const Navbar = () => {
  const [userState, userDispatch] = useUserContext();
  return (
    <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
      <div className="container-fluid">
        <a className="navbar-brand">FidenzCustomers</a>
        <button className="navbar-toggler" type="button">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
          <ul className="navbar-nav flex-grow-1">
            <li className="nav-item">
              <Link to={"/"} className="nav-link text-dark">
                Home
              </Link>
            </li>
            <li className="nav-item">
              <Link to={"/customers"} className="nav-link text-dark">
                Customers
              </Link>
            </li>
          </ul>
          <ul className="navbar-nav ">
            {userState.user ? (
              <>
                <li className="nav-item">
                  <div className="nav-link text-dark">
                    {userState.user.name}
                  </div>
                </li>
                <li className="nav-item">
                  <div
                    className="nav-link text-dark"
                    onClick={() => logout(userDispatch)}
                  >
                    Logout
                  </div>
                </li>
              </>
            ) : (
              <li className="nav-item">
                <Link to={"/login"} className="nav-link text-dark">
                  Login
                </Link>
              </li>
            )}
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
