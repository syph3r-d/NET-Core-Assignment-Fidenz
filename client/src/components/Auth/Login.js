import React, { useEffect, useState } from "react";
import { loginUser } from "../../APIs/auth";
import { useUserContext } from "../../context/user";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    Email: "",
    Password: "",
  });
  const [userState, userDispatch] = useUserContext();
  const { email, password } = formData;
  const onChange = (e) =>
    setFormData({ ...formData, [e.target.name]: e.target.value });

  const onSubmit = async (e) => {
    e.preventDefault();
    await loginUser(userDispatch, formData);
  };
  useEffect(() => {
    if (userState.user) {
      navigate("/");
    }
  }, [userState]);
  return (
    <div class="container pt-5">
      <div class="card shadow border">
        <div action="card-header">
          <div class="row">
            <h1 class="col-12 text-center p-2">Login</h1>
          </div>
        </div>
        <div class="card-body p-4">
          <div class="row">
            <div class="col-md-12">
              <section>
                <form>
                  <div class="form-floating mb-3">
                    <input
                      type="email"
                      class="form-control"
                      value={email}
                      name="Email"
                      onChange={(e) => onChange(e)}
                    />
                    <label class="floatingInput">Email</label>
                  </div>
                  <div class="form-floating mb-3">
                    <input
                      type="password"
                      class="form-control"
                      aria-required="true"
                      value={password}
                      name="Password"
                      onChange={(e) => onChange(e)}
                    />
                    <label asp-for="Password" class="floatingInput">
                      Password
                    </label>
                  </div>
                  <button
                    class="w-100 btn btn-lg btn-outline-success"
                    onClick={(e) => onSubmit(e)}
                  >
                    Login
                  </button>
                </form>
              </section>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
