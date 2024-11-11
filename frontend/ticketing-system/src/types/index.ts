export interface Ticket {
    id: number;
    level: string;
    title: string;
    description: string;
    module: string;
    type: string;
    state: string;
    urgentLvl: string;
  }
  
  export interface Reply {
    id: number;
    ticketId: number;
    replyMessage: string;
    replyDate: string;
  }

  export interface TicketGeneralInfo {
    title: string;
    description: string;
    module: string;
    type: string;
    state: string;
  }
  
  export interface TicketViewModel extends TicketGeneralInfo {
    id: number;
    urgentLvl: string;
  }