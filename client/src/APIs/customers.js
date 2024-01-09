import axios from "axios";
import { GET_ALL_CUSTOMERS } from "../utils/urls";

export const getCustomers = async () => {
  try {
    const res = await axios.get(GET_ALL_CUSTOMERS);
    return res.data;
  } catch (error) {
    console.error(error);
  }
};
