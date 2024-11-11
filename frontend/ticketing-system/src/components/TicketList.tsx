import React from 'react';
import { Ticket } from '../types';
import PaginationControl from './PaginationControl';
import Table from 'react-bootstrap/Table';

interface TicketListProps {
  tickets: Ticket[];
  selectTicket: (ticket: Ticket) => void;
  itemsPerPage: number;
  currentPage: number;
  setItemsPerPage: (itemsPerPage: number) => void;
  setCurrentPage: (currentPage: number) => void;
  totalTickets: number;
}

const TicketList: React.FC<TicketListProps> = ({
  tickets,
  selectTicket,
  itemsPerPage,
  currentPage,
  setItemsPerPage,
  setCurrentPage,
  totalTickets,
}) => {
  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;
  const visibleTickets = tickets.slice(startIndex, endIndex);

  return (
    <div className="ticket-list">
      <Table className="table table-striped" responsive>
        <thead>
          <tr>
            <th>Level</th>
            <th>#</th>
            <th>Title</th>
            <th>Module</th>
            <th>Type</th>
            <th>State</th>
          </tr>
        </thead>
        <tbody>
          {visibleTickets.map((ticket) => (
            <tr key={ticket.id} onClick={() => selectTicket(ticket)}>
              <td>{ticket.urgentLvl}</td>
              <td>{ticket.id}</td>
              <td>{ticket.title}</td>
              <td>{ticket.module}</td>
              <td>{ticket.type}</td>
              <td>{ticket.state}</td>
            </tr>
          ))}
        </tbody>
      </Table>

      <PaginationControl
        itemsPerPage={itemsPerPage}
        setItemsPerPage={setItemsPerPage}
        currentPage={currentPage}
        setCurrentPage={setCurrentPage}
        totalItems={totalTickets}
      />
    </div>
  );
};

export default TicketList;