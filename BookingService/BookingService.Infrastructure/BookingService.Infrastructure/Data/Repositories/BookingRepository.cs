﻿using AutoMapper;
using BookingService.Application.RepositoryInterfaces;
using BookingService.Domain.Entities;
using BookingService.Domain.Exceptions;
using BookingService.Infrastructure.Data.ApplicationDbContext;
using BookingService.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Data.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingServiceDbContext bookingServiceDbContext, 
            IMapper mapper) : base(bookingServiceDbContext, mapper)
        {
        }
    }
}
