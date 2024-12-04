using System;
using System.ComponentModel.DataAnnotations;

namespace BookTour.Application.Dto.Request
{
    public class PassengerRequestList
    {
        public int passengerObjectId { get; set; }
        
        public string passengerName { get; set; }

        public string passengerGender { get; set; }

        public DateOnly passengerDateBirth { get; set; }
    }
}