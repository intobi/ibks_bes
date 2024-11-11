// src/hooks/useTickets.ts
// import { useState, useEffect } from 'react';
// import { Ticket } from '../types';


// export const useTickets = (itemsPerPage: number, currentPage: number) => {
//   const [tickets, setTickets] = useState<Ticket[]>([]);
//   const [replies, setReplies] = useState<string[]>([]);
//   const [loading, setLoading] = useState(false);
//   const [error, setError] = useState<string | null>(null);

//   useEffect(() => {
//     // Simulate loading data
//     setLoading(true);
//     setError(null);

//     setTimeout(() => {
//       // Simulate fetching tickets and paginate them based on itemsPerPage and currentPage
//       const startIndex = (currentPage - 1) * itemsPerPage;
//       const paginatedTickets = mockTickets.slice(startIndex, startIndex + itemsPerPage);
//       setTickets(paginatedTickets);
//       setLoading(false);
//     }, 500); // Simulate network delay
//   }, [itemsPerPage, currentPage]);

//   const fetchReplies = (ticketId: number) => {
//     // For testing, generate some mock replies based on ticketId
//     setReplies([
//       `Reply 1 for ticket ${ticketId}`,
//       `Reply 2 for ticket ${ticketId}`,
//       `Reply 3 for ticket ${ticketId}`,
//     ]);
//   };

//   return { tickets, replies, fetchReplies, loading, error };
// };


import { useState, useEffect } from 'react';
import { Ticket, Reply } from '../types';


interface UseTicketsReturn {
  tickets: Ticket[];
  replies: Reply[];
  fetchTickets: () => Promise<void>;
  fetchReplies: (ticketId: number) => Promise<void>;
  loading: boolean;
  error: string | null;
}

export const useTickets = (itemsPerPage: number, currentPage: number): UseTicketsReturn => {
  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [replies, setReplies] = useState<Reply[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchTickets = async () => {
    setLoading(true);
    setError(null);
    try {
      const response = await fetch(`http://localhost:61813/api/Tickets`);
      if (!response.ok) {
        throw new Error('Failed to fetch tickets');
      }
      const data: Ticket[] = await response.json();
      setTickets(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An unexpected error occurred');
    } finally {
      setLoading(false);
    }
  };

  const fetchReplies = async (ticketId: number) => {
    setLoading(true);
    setError(null);
    try {
      const response = await fetch(`http://localhost:61813/api/Tickets/${ticketId}/Reply`);
      if (!response.ok) {
        throw new Error('Failed to fetch replies');
      }
      const data: Reply[] = await response.json();
      setReplies(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An unexpected error occurred');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchTickets();
  }, [itemsPerPage, currentPage]);

  return { tickets, replies, fetchTickets, fetchReplies, loading, error };
};