import React, { useState, useEffect } from 'react';
import { getStudentDetails, createStudentForRoom } from '../services/apiService';

const UpdateStudent = ({ studentId, fetchRooms, setError }) => {
    const [fname, setFname] = useState('');
    const [lname, setLname] = useState('');
    const [IDNumber, setIDNumber] = useState('');
    const [DOE, setDOE] = useState('');
    const [studentDetails, setStudentDetails] = useState([]);
    const [room, setRoom] = useState([]);
    const [isEditable, setIsEditable] = useState(false); // New state to handle editability

    useEffect(() => {
        const fetchStudentDetails = async () => {
            const result = await getStudentDetails(studentId);
            setStudentDetails(result.data);
           console.log(result);
        };
        fetchStudentDetails();
    }, [studentId]); 

    useEffect(() => {

        const setParameters = async () => {
            if (studentDetails) {
                setFname(studentDetails.fname);
                setLname(studentDetails.lname);
                setIDNumber(studentDetails.idNumber);
                setDOE(studentDetails.dateOfEntry ? studentDetails.dateOfEntry.split('T')[0] : '');
                setRoom(studentDetails.room);
    
                // Check if the student has been in for less than one month
                if (studentDetails.dateOfEntry) {
                    setIsEditable(!hasBeenMoreThanOneMonth(studentDetails.dateOfEntry));
                }
            }
        }

        setParameters();

    }, [studentDetails]);

    const hasBeenMoreThanOneMonth = (dateOfEntry) => {
        const entryDate = new Date(dateOfEntry);
        const currentDate = new Date();
    
        // Calculate the difference in years and months
        const yearDifference = currentDate.getFullYear() - entryDate.getFullYear();
        const monthDifference = currentDate.getMonth() - entryDate.getMonth();
    
        // Calculate the total number of months between the two dates
        const totalMonthsDifference = yearDifference * 12 + monthDifference;
    
        // Check if it's been more than one month, considering the day of the month
        if (totalMonthsDifference > 1) {
            return true;
        } else if (totalMonthsDifference === 1) {
            // If it's exactly one month, check the day of the month
            return currentDate.getDate() >= entryDate.getDate();
        } else {
            return false;
        }
    };
    

    const handleUpdateStudent = async () => {
        if (!fname.trim() || !lname.trim() || !IDNumber.trim() || !DOE.trim()) {
            alert('All fields are required');
            return;
        }

        if (hasBeenMoreThanOneMonth(DOE)) {
            alert('This student has been in their room for more than one month.');
            return;
        }

        const student = {
            fname,
            lname,
            idNumber: IDNumber,
            dateOfEntry: DOE
        };

        try {
            //await createStudentForRoom(roomId, student);
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
        <div>
            <div className="update-student">
                <input
                    type="text"
                    value={fname}
                    onChange={(e) => setFname(e.target.value)}
                    placeholder="First name"
                    disabled={!isEditable} // Disable if the student has been in for more than one month
                />
                <input
                    type="text"
                    value={lname}
                    onChange={(e) => setLname(e.target.value)}
                    placeholder="Last name"
                    disabled={!isEditable} // Disable if the student has been in for more than one month
                />
                <input
                    type="text"
                    value={IDNumber}
                    onChange={(e) => setIDNumber(e.target.value)}
                    placeholder="ID Number"
                    disabled={!isEditable} // Disable if the student has been in for more than one month
                />
                <input
                    type="date"
                    value={DOE}
                    onChange={(e) => setDOE(e.target.value)}
                    placeholder="Date Of Entry"
                    disabled    
                />
                {(!isEditable) ? (
                    <label>Oops, cannot edit student, try removing from room.</label>
                ): ''}
                <button disabled={!isEditable}  onClick={handleUpdateStudent}>Update Student</button>
            </div>
            <div className='room-actions'>
            <label></label>    [change room] [remove from room]    
            </div>
        </div>
    );
};

export default UpdateStudent;
