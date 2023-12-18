import axios from "axios";
import setAuthToken from "../utils/setAuthToken";

axios.defaults.baseURL = "https://localhost:7139/";

export async function loginUser(dispatch, loginPayload) {
  const requestOptions = {
    headers: { "Content-Type": "application/json" },
  };

  const body = JSON.stringify(loginPayload);
  try {
    dispatch({ type: "LOADING" });
    let response = await axios.post("api/account/login", body, requestOptions);
    const data = response.data;
    dispatch({ type: "LOGIN", payload: data });

    if (data) {
      setAuthToken(data.token);
      localStorage.setItem("token", "Bearer " + data.token);
      loadUser(dispatch);
      return data;
    }

    return;
  } catch (error) {
    dispatch({ type: "ERROR", error: error });
    throw error;
  }
}

export async function loadUser(dispatch) {
  if (localStorage.token) {
    setAuthToken(localStorage.token);
  }
  try {
    const res = await axios.get("api/account/getuser");
    dispatch({
      type: "GET_USER",
      payload: res.data,
    });
  } catch (err) {
    localStorage.removeItem("token");
    dispatch({
      type: "ERROR",
    });
  }
}

export async function logout(dispatch) {
  dispatch({ type: "LOGOUT" });
  setAuthToken();
  localStorage.removeItem("token");
}
