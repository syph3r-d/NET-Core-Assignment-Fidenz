import axios from "axios";
import { AUTHORIZATION_HEADER } from "./constants";

const setAuthToken = (token) => {
  if (token) {
    axios.defaults.headers.common[AUTHORIZATION_HEADER] = token;
  } else {
    delete axios.defaults.headers.common[AUTHORIZATION_HEADER];
  }
};

export default setAuthToken;
