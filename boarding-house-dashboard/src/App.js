import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Sidebar from './components/Sidebar';
import Dashboard from './components/Dashboard';
import RoomList from './components/RoomList';
import StudentList from './components/StudentList';
import AccountsList from './components/AccountsList';
import './App.css';

function App() {
    return (
        <Router>
            <div className="App">
                <div className="sidebar-panel">
                    <Sidebar />
                </div>
                <div className="search-bar">
                    <Dashboard />
                </div>
                <div className="main-content">
                    <Routes>
                        <Route path="/rooms" element={<RoomList boardingHouseId={2} />} />
                        <Route path="/students" element={<StudentList boardingHouseId={2} />} />
                        <Route path="/AccountsList" element={<AccountsList boardingHouseId={2} />} />
                    </Routes>
                </div>

            </div>
        </Router>
    );
}

export default App;
