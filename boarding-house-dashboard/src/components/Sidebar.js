// src/components/Sidebar.js
import React from 'react';
import { Link } from 'react-router-dom';
import '../styles/Sidebar.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faBars  } from '@fortawesome/free-solid-svg-icons'

const Sidebar = () => {
    return (
        <div className="sidebar">
            <FontAwesomeIcon icon={faBars} />
            <h2>Dashboard</h2>
            <nav>
                <ul>
                    <li><Link to="/rooms">Rooms</Link></li>
                    <li><Link to="/students">Students</Link></li>
                    <li><Link to="/rent">Rent</Link></li>
                    <li><Link to="/repairs">Repairs</Link></li>
                </ul>
            </nav>
        </div>
    );
};

export default Sidebar;
