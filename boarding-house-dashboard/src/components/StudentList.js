// src/components/StudentList.js
import React, { useEffect, useState } from 'react';
import { getStudentsByRoomId } from '../services/apiService';
import '../styles/StudentList.css';

const StudentList = ({ roomId }) => {
    const [students, setStudents] = useState([]);

    useEffect(() => {
        const fetchStudents = async () => {
            const result = await getStudentsByRoomId(roomId);
            setStudents(result.data);
        };
        fetchStudents();
    }, [roomId]);

    return (
        <div className="student-list">
            <h2>Students</h2>
            <ul>
                {students.map(student => (
                    <li key={student.id}>{student.fname} {student.lname}</li>
                ))}
            </ul>
        </div>
    );
};

export default StudentList;
