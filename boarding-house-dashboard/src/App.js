import React from 'react';
import { BrowserRouter as Router, Route, Switch, Routes } from 'react-router-dom';
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
                    <Route path="/rooms" component={RoomList} />
                    <Route path="/students" component={StudentList} />
                    <Route path="/rent" component={RentList} />
                    <Route path="/repairs" component={RepairList} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
