import React, { useState } from 'react';
import { createRepairForRoom } from '../services/apiService';

const AddRepair = ({ RoomId, fetchRooms, setError }) => {
    //const [StudentId, setStudentId] = useState('');
    const [notes, setNotes] = useState('');
    const [repairsComplete, setRepairsComplete] = useState(false);
    const [cost, setCost] = useState('');
    const [dateOfCompletion, setDateOfCompletion] = useState('');
    
    const handleAddRepair = async () => {
        const currentDate = new Date().toISOString();
        if ( !notes.trim()) {
            alert('All fields are required');
            return;
        }
        const repair = {
            dateOfReport: currentDate,
            notes: notes,
            repairsComplete: repairsComplete,
            cost: parseFloat(cost) || 0, // Ensure cost is a number
            dateOfCompletion: dateOfCompletion || currentDate,
            roomId: RoomId,
        };

        console.log('Repair object:', repair);

        try {
            await createRepairForRoom( RoomId, repair);
            //setStudentId('');
            setNotes('');
            setRepairsComplete(false);
            setCost('');
            setDateOfCompletion('');

            
            await fetchRooms(); // Re-fetch the rooms to update the list with the new repair
        } catch (error) {
            setError(error);
        }
    };

    return (
        <div className="add-repair">
            <input
                type="text"
                value={notes}
                onChange={(e) => setNotes(e.target.value)}
                placeholder="notes"
            />
            <button onClick={handleAddRepair}>Add Repair</button>
        </div>
    );
};

export default AddRepair;
