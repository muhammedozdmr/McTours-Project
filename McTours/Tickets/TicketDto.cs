﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Tickets
{
    public class TicketDto
    {
        public int Id { get; set; }
        public int BusTripId { get; set; }
        public int PassengerId { get; set; }
        public short SeatNumber { get; set; }
        public decimal Price { get; set; }
    }
}
