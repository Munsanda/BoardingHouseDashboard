// src/components/Dashboard.js
import React from 'react';
import Sidebar from './Sidebar';
import '../styles/Dashboard.css';

const Dashboard = () => {
    return (
        <div className="dashboard">
            <Sidebar />
            <div className="content">
                <h1>Boarding House 1</h1>
                <input type="text" placeholder="Search for rooms..." />
                <button>Filter</button>
                {/* Add routing here */}
            </div>
        </div>
    );
};

export default Dashboard;
