import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5035/api', // Adjust this URL to match your API's URL
    headers: {
        'Content-Type': 'application/json',
    },
});

export const boardingHouseId = 2;
export const getBoardingHouses = () => api.get('/boardinghouses');
export const getRoomsByBoardingHouseId = (id) => api.get(`/boardinghouses/${id}/rooms`);
export const getStudentsByBoardingHouseId = (id) => api.get(`/boardinghouse/${id}/students`); 
export const getStudentsByRoomId = (id) => api.get(`/rooms/${id}/students`);
export const getRentDetailsByStudentId = (id) => api.get(`/students/${id}/rents`);
export const getRepairsByRoomId = (id) => api.get(`/rooms/${id}/repairs`);
export const getRepairDetails = (id) => api.get(`/repair/${id}`);

export const createBoardingHouse = (boardingHouse) => api.post('/boardinghouses', boardingHouse);
export const createRoomForBoardingHouse = (id, room) => api.post(`/boardinghouses/${id}/rooms`, room);
export const createStudentForRoom = (id, student) => api.post(`/rooms/${id}/students`, student);
export const createRepairForRoom = (id, repair) => api.post(`/rooms/${id}/repairs`, repair);
export const createRentForStudent = (id,rent) => api.post(`/students/${id}/rent`, rent)

export const UpdateRepairDetails = (id, repair) => api.put(`/repair/${id}/`,repair)

export const deleteBoardingHouse = (id) => api.delete(`/boardinghouses/${id}`);
export const deleteRoom = (id) => api.delete(`/rooms/${id}`);
export const deleteStudent = (id) => api.delete(`/students/${id}`);

// Add other API methods similarly
export const getCostById = (id) => api.get(`/cost/${id}`);
// Updated function to accept query parameters for filtering
export const getAllCosts = (boardingHouseId, type, category, date, minAmount, maxAmount, noteToken) => 
    api.get('/cost', {
        params: {
            boardingHouseId,
            type,
            category,
            date,
            minAmount,
            maxAmount,
            noteToken   
        },
    });
export const addCost = (cost) => api.post('/cost', cost);
export const updateCost = (id, cost) => api.put(`/cost/${id}`, cost);
export const deleteCost = (id) => api.delete(`/cost/${id}`);
export const getCostsByType = (type) => api.get(`/cost/byType/${type}`);
export const getCostsByCategory = (category) => api.get(`/cost/byCategory/${category}`);
export const getCostsByDate = (date) => api.get(`/cost/byDate/${date}`);
export const getCostsByAmount = (amount) => api.get(`/cost/byAmount/${amount}`);
export const getCostsByAmountRange = (minAmount, maxAmount) => api.get(`/cost/byAmountRange`, { params: { minAmount, maxAmount } });
export const getAllCostCategories = () => api.get('/cost/categories');
export const getMonthlySummary = (month, year) => api.get('/cost/monthlySummary', { params: { month, year } });
export const getYearlySummary = (year) => api.get('/cost/yearlySummary', { params: { year } });