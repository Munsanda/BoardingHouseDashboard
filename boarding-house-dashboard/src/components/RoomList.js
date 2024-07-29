import React, { useEffect, useState } from 'react';
import { getRoomsByBoardingHouseId, createRoomForBoardingHouse, createRepairForRoom } from '../services/apiService';
import AddStudent from './AddStudent'; // Import AddStudent as default
import AddRent from './AddRent'; // Import AddStudent as default
import AddRepair from './AddRepair';
import Modal from './Modal';
import '../styles/RoomList.css';


const RoomList = ({ boardingHouseId }) => {
    const [rooms, setRooms] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [newRoomName, setNewRoomName] = useState('');
    const [isModalOpen, setModalOpen] = useState(false);
    const [modalContent, setModalContent] = useState(null);
    // Function to open the modal
    const openModalWithContent = (content) => {
        setModalContent(content);
        setModalOpen(true);
    };
    // Function to close the modal
    const closeModal = () => {
        setModalOpen(false);
        setModalContent(null);
    };
    const [currentStudentId, setCurrentStudentId] = useState(null);



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

    // A function to format the date
    const formatDate = (dateString) => {
        const date = new Date(dateString);
        return new Intl.DateTimeFormat('en-US', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        }).format(date);
    };

    // Utility function to determine rent status color
    const getRentStatusColor = (endDate) => {
        const today = new Date();
        const rentEndDate = new Date(endDate);

        console.log(endDate);
            console.log(today);
        if (rentEndDate < today) {
            
            return 'red'; // Rent is overdue
        } else if(rentEndDate > today) {
            return 'green'; // Rent is paid or not yet due
        }else {
            return 'gray';
        }
    };

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error loading rooms: {error.message}</div>;

    return (
        <div>
            <div className="add-room">
                <input
                    type="text"
                    value={newRoomName}
                    onChange={(e) => setNewRoomName(e.target.value)}
                    placeholder="Enter room name"
                />
                <button onClick={handleAddRoom}>Add Room</button>
            </div>
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
                                     <button onClick={() =>openModalWithContent(<AddStudent roomId={room.id} fetchRooms={fetchRooms} setError={setError} />)}>Add Student</button>
                                    
                                </div>

                            </div>
                            <div className="room-details">
                                <div className="rent">
                                    {room.students?.$values?.map(student => (
                                        <div key={student.id}>{
                                            <div>
                                                <div
                                                    className="rent-status"
                                                    style={{
                                                        
                                                        width: '15px', // Size of the square
                                                        height: '15px',
                                                        backgroundColor: getRentStatusColor(student.rents?.$values[(student.rents?.$values.length) - 1]?.endDate),
                                                        display: 'inline-block',
                                                        marginRight: '5px',
                                                    }}
                                                ></div>
                                                <button onClick={() => openModalWithContent(<AddRent StudentId={student.id} fetchRooms={fetchRooms} setError={setError} />)}>Add Rent</button>
                                            </div>
                                        }
                                        </div>
                                        
                                    ))}
                                </div>
                            </div>
                            <div className="room-details">
                                <div className="rent">
                                    {room.repairs?.$values?.map((repair) => (
                                    <div key={repair.id} style={{ 
                                        color: repair.repairsComplete ? 'green' : 'red' 
                                    }}>
                                     {repair.notes} -  {formatDate(repair.dateOfReport)} - {repair.repairsComplete ? 'Complete' : 'Incomplete'}
                                    </div>
                                    
                                    ))}
                                    
                                    <button onClick={ () => openModalWithContent(<AddRepair RoomId = {room.id} fetchRooms={fetchRooms} setError={setError}/>)}>Add Repair</button>
                                </div>
                            </div>
                            <Modal isOpen={isModalOpen} onClose={closeModal}>
                                {modalContent}
                            </Modal>
                        </div>
                    ))
                ) : (
                    <div>No rooms available</div>
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
