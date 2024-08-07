import React, { useState, useEffect } from 'react';
import { getAllCosts } from '../services/apiService'; // Assuming the API functions are in api.js

const AccountList = ({boardingHouseId}) => {
    // State for costs and filters
    const [costs, setCosts] = useState([]);
    const [filters, setFilters] = useState({
      boardingHouseId: '',
      type: '',
      category: '',
      fromDate: '', // New field for start date
      toDate: '',   // New field for end date
      minAmount: '',
      maxAmount: '',
    });
  
    // Fetch costs whenever filters change
    useEffect(() => {
      const fetchCosts = async () => {
        try {
          const response = await getAllCosts(
            boardingHouseId,
            filters.type || null,
            filters.category || null,
            null, // Not using the single date field anymore
            filters.minAmount || null,
            filters.maxAmount || null
          );
  
          // Filter costs by date range if both dates are provided
          const filteredCosts = response.data.$values.filter(cost => {
            const costDate = new Date(cost.date);
            const fromDate = filters.fromDate ? new Date(filters.fromDate) : null;
            const toDate = filters.toDate ? new Date(filters.toDate) : null;
  
            return (
              (!fromDate || costDate >= fromDate) &&
              (!toDate || costDate <= toDate)
            );
          });
  
          setCosts(filteredCosts);
        } catch (error) {
          console.error('Error fetching costs:', error);
        }
      };
  
      fetchCosts();
    }, [filters]);
    
    // Handle filter input changes
    const handleInputChange = (e) => {
      const { name, value } = e.target;
      setFilters({
        ...filters,
        [name]: value,
      });
    };
  return (
    <div>
      <h1>Account List</h1>

      {/* Filter Bar */}
      <div className="filter-bar">
        <input
          type="number"
          name="boardingHouseId"
          placeholder="Boarding House ID"
          value={filters.boardingHouseId}
          onChange={handleInputChange}
        />
        <input
          type="text"
          name="type"
          placeholder="Type"
          value={filters.type}
          onChange={handleInputChange}
        />
        <input
          type="text"
          name="category"
          placeholder="Category"
          value={filters.category}
          onChange={handleInputChange}
        />
        <input
        type="date"
        name="fromDate"
        placeholder="From Date"
        value={filters.fromDate}
        onChange={handleInputChange}
        />
        <input
        type="date"
        name="toDate"
        placeholder="To Date"
        value={filters.toDate}
        onChange={handleInputChange}
        />
        <input
          type="number"
          name="minAmount"
          placeholder="Min Amount"
          value={filters.minAmount}
          onChange={handleInputChange}
        />
        <input
          type="number"
          name="maxAmount"
          placeholder="Max Amount"
          value={filters.maxAmount}
          onChange={handleInputChange}
        />
      </div>

      {/* Costs Table */}
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Amount</th>
            <th>Description</th>
            <th>Date</th>
            <th>Type</th>
            <th>Category</th>
            <th>Boarding House ID</th>
          </tr>
        </thead>
        <tbody>
          {costs.map((cost) => (
            <tr key={cost.id}>
              <td>{cost.id}</td>
              <td>{cost.amount}</td>
              <td>{cost.description}</td>
              <td>{new Date(cost.date).toLocaleDateString()}</td>
              <td>{cost.type}</td>
              <td>{cost.category}</td>
              <td>{cost.boardingHouseId}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default AccountList;
