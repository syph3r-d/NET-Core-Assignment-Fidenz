import axios from "axios";

export const getCustomers = async () => {
  try {
    const res = await axios.get("/api/customers");
    return res.data;
  } catch (error) {
    return error.response.data;
  }
};

