import axios from "axios";
import setAuthToken from "../utils/setAuthToken";
import { LOGIN, GET_USER } from "../utils/urls";
import {
  ERROR,
  LOADING,
  GET_USER as GET_USER_ACTION,
  LOGOUT,
  LOGIN as LOGIN_ACTION,
} from "../context/actions";
import { TOKEN_KEY, TOKEN_PREFIX } from "../utils/constants";



export async function loginUser(dispatch, loginPayload) {
  const requestOptions = {
    headers: { "Content-Type": "application/json" },
  };

  const body = JSON.stringify(loginPayload);
  try {
    dispatch({ type: LOADING });
    let response = await axios.post(LOGIN, body, requestOptions);
    const data = response.data;
    dispatch({ type: LOGIN_ACTION, payload: data });

    if (data) {
      setAuthToken(data.token);
      localStorage.setItem(TOKEN_KEY, TOKEN_PREFIX + data.token);
      loadUser(dispatch);
      return data;
    }

    return;
  } catch (error) {
    dispatch({ type: ERROR, payload: error.response.data });
    console.error(error);
  }
}

export async function loadUser(dispatch) {
  if (localStorage.token) {
    setAuthToken(localStorage.token);
  }
  try {
    const res = await axios.get(GET_USER);
    dispatch({
      type: GET_USER_ACTION,
      payload: res.data,
    });
  } catch (err) {
    localStorage.removeItem(TOKEN_KEY);
    dispatch({
      type: ERROR,
    });
  }
}

export async function logout(dispatch) {
  dispatch({ type: LOGOUT });
  setAuthToken();
  localStorage.removeItem(TOKEN_KEY);
}
