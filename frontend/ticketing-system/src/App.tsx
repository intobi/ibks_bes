import React, { useState } from 'react';
import TicketList from './components/TicketList';
import TicketDetails from './components/TicketDetails';
import { Ticket } from './types';
import { useTickets } from './hooks/useTickets';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';

const App: React.FC = () => {
  const [selectedTicket, setSelectedTicket] = useState<Ticket | null>(null);
  const [itemsPerPage, setItemsPerPage] = useState(10);
  const [currentPage, setCurrentPage] = useState(1);

  const { tickets, replies, fetchReplies, loading, error } = useTickets(itemsPerPage, currentPage);

  const selectTicket = (ticket: Ticket) => {
    setSelectedTicket(ticket);
    fetchReplies(ticket.id);
  };

  const totalTickets = tickets.length;
  const handleItemsPerPageChange = (newItemsPerPage: number) => {
    setItemsPerPage(newItemsPerPage);
    setCurrentPage(1);
  };

  return (
    <div className="app adaptive-app">
      <TicketList
        tickets={tickets}
        selectTicket={selectTicket}
        itemsPerPage={itemsPerPage}
        currentPage={currentPage}
        setItemsPerPage={handleItemsPerPageChange}
        setCurrentPage={setCurrentPage}
        totalTickets={totalTickets}
      />
      {selectedTicket && (
        <TicketDetails 
        ticket={selectedTicket}
        replies={replies}/>
      )}
      {loading && <p>Loading...</p>}
      {error}
    </div>
  );
};

export default App;
