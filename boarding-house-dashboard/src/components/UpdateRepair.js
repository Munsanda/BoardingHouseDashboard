import React, {useEffect, useState } from 'react';
import { getRepairDetails, UpdateRepairDetails } from '../services/apiService';


const UpdateRepair = ({ repairId, fetchRooms, setError }) => {
    //const [StudentId, setStudentId] = useState('');
    const [notes, setNotes] = useState('');
    const [cost, setCost] = useState('');
    const[state, setState] = useState(false);
    const [repairDetails, setRepairDetails] = useState([]);

    useEffect(() => {
        const fetchRepairDetails = async () => {
            const result = await getRepairDetails(repairId);
            setRepairDetails(result.data);

        };
        fetchRepairDetails();

    }, [repairId]); 

    useEffect(() => {
        setCost(repairDetails.cost);
        setNotes(repairDetails.notes);
        setState(repairDetails.repairsComplete)
        
    }, [repairDetails])
    
    console.log(repairDetails)

    const handleUpdateRepair = async () => {
        if ( !notes.trim()) {
            alert('All fields are required');
            return;
        }
        const repair = {
            notes: notes,
            cost: parseFloat(cost) || 0, // Ensure cost is a number
        };

        console.log('Repair object:', repair);

        try {
            await UpdateRepairDetails( repairId, repair);
            //setStudentId('');
            setNotes('');
            setCost('');

            
            await fetchRooms(); // Re-fetch the rooms to update the list with the new repair
        } catch (error) {
            setError(error);
        }
    };

    return (
        <div className="update-repair">
            {
                <div>
                    <input
                    type="text"
                    value={notes}
                    onChange={(e) => setNotes(e.target.value)}
                    placeholder="notes"
                    />
                    <input
                        type="text"
                        readOnly = {(state === true)? true:false}
                        disabled ={(state === true)? true:false}
                        value={cost}
                        onChange={(e) => setCost(e.target.value)}
                        placeholder="cost"
                    />
            </div>}
            <button onClick={handleUpdateRepair}>Update Repair</button>
        </div>
    );
};

export default UpdateRepair;
