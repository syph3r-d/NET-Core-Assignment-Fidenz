import React, { useEffect, useState } from "react";
import { getCustomers } from "../../APIs/customers";

const Customers = () => {
  const [customers, setCustomers] = useState([]);
  useEffect(() => {
    const getData = async () => {
      const res = await getCustomers();
      if (res) {
        setCustomers(res);
      }
    };
    getData();
  }, []);
  return (
    <div className="main-container">
      <h1>Customers</h1>

      <table class="table">
        <thead>
          <tr>
            <th>Index</th>
            <th>Age</th>
            <th>EyeColor</th>
            <th>Name</th>
            <th>Gender</th>
            <th>Company</th>
            <th>Email</th>
            <th>Phone</th>
            <th>About</th>
            <th>Registered</th>
            <th>Latitude</th>
            <th>Longitude</th>
          </tr>
        </thead>
        <tbody>
          {customers.map((customer, index) => (
            <tr key={index}>
              <td>{customer.index}</td>
              <td>{customer.age}</td>
              <td>{customer.eyeColor}</td>
              <td>{customer.name}</td>
              <td>{customer.gender}</td>
              <td>{customer.company}</td>
              <td>{customer.email}</td>
              <td>{customer.phone}</td>
              <td>{customer.about}</td>
              <td>{customer.registered}</td>
              <td>{customer.latitude}</td>
              <td>{customer.longitude}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default Customers;
