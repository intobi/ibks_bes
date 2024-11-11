import React, { useState, useEffect } from 'react';
import { Button, Alert, Form, InputGroup } from 'react-bootstrap';
import { Ticket, Reply, TicketViewModel } from '../types';

interface TicketDetailsProps {
  ticket: Ticket;
  replies: Reply[];
}

const API_BASE_URL = "http://localhost:61813/api/Tickets";

export function mapTicketToViewModel(ticket: Ticket): TicketViewModel {
  return {
    id: ticket.id,
    title: ticket.title,
    description: ticket.description,
    module: ticket.module,
    type: ticket.type,
    state: ticket.state,
    urgentLvl: ticket.urgentLvl,
  };
}

async function updateTicket(ticket: Ticket): Promise<Ticket | null> {
  try {
    const ticketViewModel = mapTicketToViewModel(ticket);
    const response = await fetch(`${API_BASE_URL}/${ticket.id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(ticketViewModel),
    });

    if (!response.ok) {
      throw new Error(`Failed to update ticket with ID ${ticket.id}: ${response.statusText}`);
    }

    const updatedTicket: Ticket = await response.json();
    return updatedTicket;
  } catch (error) {
    console.error("Error updating ticket:", error);
    return null;
  }
}

const TicketDetails: React.FC<TicketDetailsProps> = ({ ticket, replies }) => {
  // State initialization from ticket prop
  const [module, setModule] = useState(ticket.module);
  const [urgentLvl, setUrgentLvl] = useState(ticket.urgentLvl);
  const [type, setType] = useState(ticket.type);
  const [description, setDescription] = useState(ticket.description);

  // Effect to update state when ticket prop changes
  useEffect(() => {
    setModule(ticket.module);
    setUrgentLvl(ticket.urgentLvl);
    setType(ticket.type);
    setDescription(ticket.description);
  }, [ticket]);

  const handleSave = async () => {
    const updatedTicket = {
      ...ticket,
      module,
      urgentLvl,
      type,
      description,
    };
    const updatedTicketFromApi = await updateTicket(updatedTicket);
    if (updatedTicketFromApi) {
      // You can do something with the updatedTicketFromApi, like notifying the user or updating local state
    }
  };

  return (
    <div className="ticket-details">
      <div className='d-flex mt-4'>
        <div className="d-flex justify-content-between align-items-center">
          <div className="ticket-number">
            <strong>Ticket#</strong>
          </div>
        </div>
        <div className="title" style={{ textAlign: 'center', flex: 1 }}>
          <p><b>{ticket.id} - {ticket.title}</b></p>
        </div>
        <div className="d-flex">
          <Button variant="outline-secondary" className="me-2 adaptive-control-button">Close</Button>
          <Button onClick={handleSave} variant="outline-primary" className="adaptive-control-button">Save</Button>
        </div>
      </div>
      {replies.length > 0 && (
        <div className="d-flex mt-4">
          <Alert variant="warning" className="col-12">
            {replies[0].replyMessage}
          </Alert>
        </div>
      )}
      <div className="general-adaptive-container">
        <div className="left-general-container">
          <div className="d-flex mt-4">
            <div className="col-12 ticket-general-details">
              <div><p>Module</p></div>
              <Form>
                <Form.Select
                  className="adaptive-button"
                  value={module}
                  onChange={(e) => setModule(e.target.value)}
                >
                  <option value="HR">HR</option>
                  <option value="Loader">Loader</option>
                  <option value="Finance">Finance</option>
                  <option value="Ingress">Ingress</option>
                  <option value="Clusters">Clusters</option>
                  <option value="Planner">Planner</option>
                </Form.Select>
              </Form>
            </div>
          </div>
          <div className="d-flex mt-1">
            <div className="col-12 ticket-general-details">
              <div><p>Urgent Lvl</p></div>
              <Form>
                <Form.Select
                  className="adaptive-button"
                  value={urgentLvl}
                  onChange={(e) => setUrgentLvl(e.target.value)}
                >
                  <option value="Low">Low</option>
                  <option value="Medium">Medium</option>
                  <option value="High">High</option>
                  <option value="Priority">Priority</option>
                  <option value="None">None</option>
                </Form.Select>
              </Form>
            </div>
          </div>
          <div className="d-flex mt-1">
            <div className="col-12 ticket-general-details">
              <div><p>Type</p></div>
              <Form>
                <Form.Select
                  className="adaptive-button"
                  value={type}
                  onChange={(e) => setType(e.target.value)}
                >
                  <option value="Question">Question</option>
                  <option value="Issue">Issue</option>
                  <option value="Suggestion">Suggestion</option>
                  <option value="Feedback">Feedback</option>
                </Form.Select>
              </Form>
            </div>
          </div>
          <div className="d-flex mt-4">
            <div className="col-12">
              <InputGroup>
                <Form.Control
                  className="adaptive-description"
                  as="textarea"
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                  aria-label="With textarea"
                />
              </InputGroup>
            </div>
          </div>
        </div>
        <div className="right-general-container">
          <div className="centered-replies">
            <h3>Replies</h3>
            <div className="replies">
              {replies.map((reply) => (
                <div key={reply.id} className="reply">
                  <p>{reply.replyMessage}</p>
                </div>
              ))}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default TicketDetails;
