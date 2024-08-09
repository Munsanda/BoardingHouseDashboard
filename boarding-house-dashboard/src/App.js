import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Sidebar from './components/Sidebar';
import Dashboard from './components/Dashboard';
import RoomList from './components/RoomList';
import StudentList from './components/StudentList';
import AccountsList from './components/AccountsList';
import RentList from './components/RentList';
import RepairList from './components/RepairList'
import './App.css';

function App() {
    return (
        <Router>
            <div className="App">

                <div className="sidebar">
                    <Sidebar />
                </div>

                <div className="content">
                    <div className="search-bar">
                        <Dashboard />
                    </div>
                    <div className="title-and-stats">
                        <div class="card">
                            <div class="icon">
                                {/* <img src="folder-icon.png" alt="Folder Icon"> */}
                            </div>
                            <div class="content">
                                <h2>18</h2>
                                <p>Projects</p>
                                <small>2 Completed</small>
                            </div>
                        </div>
                        <div class="card">
                            <div class="icon">
                                {/* <img src="folder-icon.png" alt="Folder Icon"> */}
                            </div>
                            <div class="content">
                                <h2>18</h2>
                                <p>Projects</p>
                                <small>2 Completed</small>
                            </div>
                        </div>
                        <div class="card">
                            <div class="icon">
                                {/* <img src="folder-icon.png" alt="Folder Icon"> */}
                            </div>
                            <div class="content">
                                <h2>18</h2>
                                <p>Projects</p>
                                <small>2 Completed</small>
                            </div>
                        </div>
                        <div class="card">
                            <div class="icon">
                                {/* <img src="folder-icon.png" alt="Folder Icon"> */}
                            </div>
                            <div class="content">
                                <h2>18</h2>
                                <p>Projects</p>
                                <small>2 Completed</small>
                            </div>
                        </div>
                    </div>

                    <div className="main-content">
                        <Routes>
                            <Route path="/rooms" element={<RoomList boardingHouseId={2} />} />
                            <Route path="/students" element={<StudentList boardingHouseId={2} />} />
                            <Route path="/AccountsList" element={<AccountsList boardingHouseId={2} />} />
                        </Routes>
                    </div>
                </div>

            </div>
        </Router>
    );
}

export default App;
