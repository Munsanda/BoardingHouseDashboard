// src/components/RepairList.js
import React, { useEffect, useState } from 'react';
import { getRepairsByRoomId } from '../services/apiService';
import '../styles/RepairList.css';

const RepairList = ({ roomId }) => {
    const [repairs, setRepairs] = useState([]);

    useEffect(() => {
        const fetchRepairs = async () => {
            const result = await getRepairsByRoomId(roomId);
            setRepairs(result.data);
        };
        fetchRepairs();
    }, [roomId]);

    return (
        <div className="repair-list">
            <h2>Repairs</h2>
            <ul>
                {repairs.map(repair => (
                    <li key={repair.id}>{repair.notes} - {repair.status}</li>
                ))}
            </ul>
        </div>
    );
};

export default RepairList;
