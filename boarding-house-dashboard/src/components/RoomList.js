import React, { useEffect, useState } from 'react';
import { getRoomsByBoardingHouseId, createRoomForBoardingHouse } from '../services/apiService';
import AddStudent from './AddStudent'; // Import AddStudent as default
import AddRent from './AddRent'; // Import AddStudent as default
import '../styles/RoomList.css';

const RoomList = ({ boardingHouseId }) => {
    const [rooms, setRooms] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [newRoomName, setNewRoomName] = useState('');

    const fetchRooms = async () => {
        try {
            const result = await getRoomsByBoardingHouseId(boardingHouseId);
            console.log('API Result:', result); // Debugging: log the API result
            const roomsData = result.data.$values; // Adjusting to your data structure

            if (Array.isArray(roomsData)) {
                // Flattening the rooms array to include references
                const flattenedRooms = flattenRooms(roomsData);
                setRooms(flattenedRooms);
            } else {
                throw new Error('Unexpected data structure');
            }
        } catch (error) {
            setError(error);
        } finally {
            setLoading(false);
        }
    };

    // Function to flatten rooms data structure
    const flattenRooms = (roomsArray) => {
        const roomsMap = {};

        const addRoomToMap = (room) => {
            if (!roomsMap[room.id]) {
                roomsMap[room.id] = room;
            }
        };

        roomsArray.forEach(room => {
            if (room.$ref) {
                // If it's a reference, it should already be in the map
                return;
            }
            addRoomToMap(room);

            // If the room has nested rooms, add them as well
            if (room.boardingHouse?.rooms?.$values) {
                room.boardingHouse.rooms.$values.forEach(nestedRoom => {
                    if (nestedRoom.$ref) {
                        // Handle references if needed
                        const refId = nestedRoom.$ref.split("/")[1];
                        const referencedRoom = roomsArray.find(r => r.$id === refId);
                        addRoomToMap(referencedRoom);
                    } else {
                        addRoomToMap(nestedRoom);
                    }
                });
            }
        });

        return Object.values(roomsMap);
    };

    useEffect(() => {
        fetchRooms();
    }, [boardingHouseId]);

    const handleAddRoom = async () => {
        if (!newRoomName.trim()) {
            alert('Room name cannot be empty');
            return;
        }
        try {
            await createRoomForBoardingHouse(boardingHouseId, { name: newRoomName }); // Adjust the payload as needed
            setNewRoomName(''); // Clear the input field
            await fetchRooms(); // Re-fetch the rooms after adding
        } catch (error) {
            setError(error);
        }
    };

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error loading rooms: {error.message}</div>;

    return (
        <div>
            <div className="room-list">
                <h2>Rooms</h2>
                <div className="room-list-header">
                    <span>Room Name</span>
                    <span>Students</span>
                    <span>Rent Status</span>
                    <span>Repairs</span>
                </div>
                {rooms.length > 0 ? (
                    rooms.map(room => (
                        <div key={room.id} className="room-list-item">
                            <div className="room-name">{room.name}</div>
                            <div className="room-details">
                                <div className="students">
                                    {room.students?.$values?.map(student => (
                                        <div key={student.id}>{student.fname} {student.lname}</div>
                                    ))}
                                    <AddStudent roomId={room.id} fetchRooms={fetchRooms} setError={setError} />
                                </div>

                            </div>
                            <div className="room-details">
                                <div className="rent">
                                    {room.students?.$values?.map(student => (
                                        <div key={student.id}>{
                                            <AddRent StudentId={student.id} fetchRooms={fetchRooms} setError={setError} />
                                        }</div>
                                    ))}
                                </div>
                            </div>
                        </div>
                    ))
                ) : (
                    <div>No rooms available.</div>
                )}
            </div>
            <div className="add-room">
                <input
                    type="text"
                    value={newRoomName}
                    onChange={(e) => setNewRoomName(e.target.value)}
                    placeholder="Enter room name"
                />
                <button onClick={handleAddRoom}>Add Room</button>
            </div>
        </div>
    );
};

export default RoomList;
