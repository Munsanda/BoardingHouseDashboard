import React, { useState } from 'react';
import { createStudentForRoom } from '../services/apiService';

const AddStudent = ({ roomId, fetchRooms, setError }) => {
    const [fname, setFname] = useState('');
    const [lname, setLname] = useState('');
    const [IDNumber, setIDNumber] = useState('');
    const [DOE, setDOE] = useState('');
    
    const handleAddStudent = async () => {
        if (!fname.trim() || !lname.trim() || !IDNumber.trim() || !DOE.trim()) {
            alert('All fields are required');
            return;
        }
        const student = {
            fname,
            lname,
            idNumber: IDNumber,
            dateOfEntry: DOE
        };
        try {
            await createStudentForRoom(roomId, student);
            setFname('');
            setLname('');
            setIDNumber('');
            setDOE('');
            await fetchRooms(); // Re-fetch the rooms to update the list with the new student
        } catch (error) {
            setError(error);
        }
    };

    return (
        <div className="add-student">
            <input
                type="text"
                value={fname}
                onChange={(e) => setFname(e.target.value)}
                placeholder="First name"
            />
            <input
                type="text"
                value={lname}
                onChange={(e) => setLname(e.target.value)}
                placeholder="Last name"
            />
            <input
                type="text"
                value={IDNumber}
                onChange={(e) => setIDNumber(e.target.value)}
                placeholder="ID Number"
            />
            <input
                type="date"
                value={DOE} 
                onChange={(e) => setDOE(e.target.value)}
                placeholder="Date Of Entry"
            />
            <button onClick={handleAddStudent}>Add Student</button>
        </div>
    );
};

export default AddStudent;
