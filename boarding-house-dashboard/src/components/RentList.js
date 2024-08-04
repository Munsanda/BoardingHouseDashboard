// src/components/RentList.js
import React, { useEffect, useState } from 'react';
import { getRentDetailsByStudentId } from '../services/apiService';
import '../styles/RentList.css';

const RentList = ({ studentId }) => {
    const [rentDetails, setRentDetails] = useState([]);

    useEffect(() => {
        const fetchRentDetails = async () => {
            const result = await getRentDetailsByStudentId(studentId);
            setRentDetails(result.data);
        };
        fetchRentDetails();
    }, [studentId]); 

    return (
        <div className="rent-list">
            <h2>Rent Details</h2>
            <ul>
                {rentDetails.map(detail => (
                    <li key={detail.id}>{detail.amount} - {detail.status}</li>
                ))}
            </ul>
        </div>
    );
};

export default RentList;
