// src/components/RoomList.js
import React, { useEffect, useState } from 'react';
import { getRoomsByBoardingHouseId } from '../services/apiService';
import '../styles/RoomList.css';

const RoomList = ({ boardingHouseId }) => {
    const [rooms, setRooms] = useState([]);

    useEffect(() => {
        const fetchRooms = async () => {
            const result = await getRoomsByBoardingHouseId(boardingHouseId);
            setRooms(result.data);
        };
        fetchRooms();
    }, [boardingHouseId]);

    return (
        <div className="room-list">
            <h2>Rooms</h2>
            <ul>
                {rooms.map(room => (
                    <li key={room.id}>{room.name}</li>
                ))}
            </ul>
        </div>
    );
};

export default RoomList;
