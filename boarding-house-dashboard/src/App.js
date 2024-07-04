import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Dashboard from './components/Dashboard';
import RoomList from './components/RoomList';
import StudentList from './components/StudentList';
import RentList from './components/RentList';
import RepairList from './components/RepairList';
import './App.css';

function App() {
    return (
        <Router>
            <div className="App">
                <Dashboard />
                <Routes>
                    <Route path="/rooms" element={<RoomList boardingHouseId={2} />} />
                    <Route path="/students" element={<StudentList />} />
                    <Route path="/rent" element={<RentList />} />
                    <Route path="/repairs" element={<RepairList />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
