﻿using BookTour.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookTour.Application.Dto.Request;
using BookTour.Domain.Entity;

namespace BookTour.Application.Interface
{
    public interface IBookingService
    {
        Task<Page<BookingResponse>> GetAllBookingByUserIdAsync(int UserId,int page,int size);
        Task<BookingDetailResponse> GetDetailBookingResponseByUserIdAsync(int UserId,int BookingId);
        Task<bool> CheckAvailableQuantityAsync(int detailTourId, int quantity);
        Task<bool> UpdateBookingStatusAsync(int bookingId, int statusId);
        Task<int> CreateCustomerAsync(BookingRequest request);
        Task<List<int>> CreatePassengersAsync(BookingRequest request);
        Task<int> CreateBookingAsync(BookingRequest request, int customerId, int passengerCount);
        Task<bool> CreateTicketsAsync(List<int> passengerIds, int bookingId);

        Task<List<BookingResponse>> GetAllBookingsAsync();
        Task<BookingDetailResponse> GetBookingDetailByIdAsync(int bookingId);
    }
}