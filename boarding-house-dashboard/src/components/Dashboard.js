// src/components/Dashboard.js
import React from 'react';
import '../styles/Dashboard.css';

const Dashboard = () => {
    return (
        <div className="dashboard">
            <div className="main-searchbar">
                <div className="house-title">
                    <h2>Boarding House 1</h2>
                </div>
                
                <div className="search-box">
                    <input type="text" placeholder="Search for rooms..." />
                    <button>Filter</button>
                </div>

                {/* Add routing here */}
            </div>
        </div>
    );
};

export default Dashboard;
