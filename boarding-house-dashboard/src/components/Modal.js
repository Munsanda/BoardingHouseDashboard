// Modal.js
import React from 'react';
import '../styles/Modal.css'; // Add appropriate styling for the modal

const Modal = ({ children, isOpen, onClose }) => {
    if (!isOpen) return null;

    return (
        <div className="modal-overlay" onClick={onClose}>
            <div className="modal-content" onClick={e => e.stopPropagation()}>
                <button className="modal-close" onClick={onClose}>X</button>
                {children}
            </div>
        </div>
    );
};

export default Modal;
