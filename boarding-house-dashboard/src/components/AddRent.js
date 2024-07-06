import React, { useState } from 'react';
import { createStudentForRoom } from '../services/apiService';

const AddRent = ({ StudentId, fetchRooms, setError }) => {
    //const [StudentId, setStudentId] = useState('');
    const [Amount, setAmount] = useState('');
    const [StartDate, setStartDate] = useState('');
    const [EndDate, setEndDate] = useState('');
    
    const handleAddRent = async () => {
        if ( !Amount.trim() || !StartDate.trim() || !EndDate.trim()) {
            alert('All fields are required');
            return;
        }
        const rent = {
            StudentId,
            Amount,
            StartDate: StartDate,
            dateOfEntry: EndDate
        };
        try {
            await createStudentForRoom(StudentId, rent);
            //setStudentId('');
            setAmount('');
            setStartDate('');
            setEndDate('');
            await fetchRooms(); // Re-fetch the rooms to update the list with the new student
        } catch (error) {
            setError(error);
        }
    };

    return (
        <div className="add-rent">
            <input
                type="decimal"
                value={Amount}
                onChange={(e) => setAmount(e.target.value)}
                placeholder="Amount Paid"
            />
            <input
                type="date"
                value={StartDate}
                onChange={(e) => setStartDate(e.target.value)}
                placeholder="Start Date"
            />
            <input
                type="date"
                value={EndDate} 
                onChange={(e) => setEndDate(e.target.value)}
                placeholder="End Date"
            />
            <button onClick={handleAddRent}>Add Rent</button>
        </div>
    );
};

export default AddRent;
