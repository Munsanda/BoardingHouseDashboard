// src/components/StudentList.js
import React, { useEffect, useState } from 'react';
import { getStudentsByBoardingHouseId } from '../services/apiService';
import AddStudent from './AddStudent'; // Import AddStudent as default
import '../styles/StudentList.css';

const StudentList = ({ boardingHouseId }) => {
    const [students, setStudents] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const fetchStudents = async () => {
        try {
            const result = await getStudentsByBoardingHouseId(boardingHouseId);
            console.log('API Result:', result); // Debugging: log the API result
            const roomsData = result.data.$values; // Adjusting to your data structure

            if (Array.isArray(roomsData)) {
                // Flattening the rooms array to extract students
                const allStudents = roomsData.flatMap(room => room.$values);
                setStudents(allStudents);
            } else {
                throw new Error('Unexpected data structure');
            }
        } catch (error) {
            setError(error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchStudents();
    }, [boardingHouseId]);

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error loading students: {error.message}</div>;

    return (
        <div>
            
            <div className='student-list'>
                <h2>Students</h2>
                <div className="student-list-header">
                    <span>ID</span>
                    <span>First Name</span>
                    <span>Last Name</span>
                    <span>ID Number</span>
                    <span>Room ID</span>
                    <span>Date of Entry</span>
                    <span>Warnings</span>
                </div>
                {students.length > 0 ? (
                    students.map(student => (
                        <div key={student.id} className="student-list-item">
                            <span>{student.id}</span>
                            <span>{student.fname}</span>
                            <span>{student.lname}</span>
                            <span>{student.idNumber}</span>
                            <span>{student.roomId}</span>
                            <span>{new Date(student.dateOfEntry).toLocaleDateString()}</span>
                            <span>{student.numberOfWarnings}</span>
                        </div>
                    ))
                ) : (
                    <div>No students available.</div>
                )}
            </div>
        </div>
    );
};

export default StudentList;
