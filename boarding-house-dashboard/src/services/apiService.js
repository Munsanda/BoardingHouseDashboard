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

export const createBoardingHouse = (boardingHouse) => api.post('/boardinghouses', boardingHouse);
export const createRoomForBoardingHouse = (id, room) => api.post(`/boardinghouses/${id}/rooms`, room);
export const createStudentForRoom = (id, student) => api.post(`/rooms/${id}/students`, student);
export const createRepairForRoom = (id, repair) => api.post(`/rooms/${id}/repairs`, repair);
export const createRentForStudent = (id,rent) => api.post(`/students/${id}/rent`, rent)

export const deleteBoardingHouse = (id) => api.delete(`/boardinghouses/${id}`);
export const deleteRoom = (id) => api.delete(`/rooms/${id}`);
export const deleteStudent = (id) => api.delete(`/students/${id}`);

// Add other API methods similarly
