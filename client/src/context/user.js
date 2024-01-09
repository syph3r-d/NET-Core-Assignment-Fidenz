import { createContext, useReducer,useContext } from "react";
import { ERROR, GET_USER, LOADING, LOGIN, LOGOUT } from "./actions";
import { TOKEN_KEY } from "../utils/constants";

const initialState = {
  user: null,
  token: localStorage.getItem(TOKEN_KEY),
  error: false,
  loading: false,
};

const reducer = (state = initialState, action) => {
  switch (action.type) {
    case LOADING:
      return {
        ...state,
        loading: true,
        error: false,
      };
    case LOGIN:
      return {
        ...state,
        token: action.payload.token,
        error:false,
      };
    case LOGOUT:
      return {
        ...state,
        user: null,
        token: null,
        loading: false,
        error: false,
      };
    case GET_USER:
      return {
        ...state,
        user: action.payload,
        loading: false,
      };
    case ERROR:
      return {
        ...state,
        loading: false,
        error: action.payload,
      };
    default:
      return state;
  }
};

export const UserContext = createContext();

export const useUserContext = () => {
  return useContext(UserContext);
};

export const UserProvider = ({ children }) => {
  const [state, dispatch] = useReducer(reducer, initialState);

  return (
    <UserContext.Provider value={[state, dispatch]}>
      {children}
    </UserContext.Provider>
  );
};
