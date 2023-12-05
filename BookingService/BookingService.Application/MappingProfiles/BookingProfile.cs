using AutoMapper;
using BookingService.Application.DTOs.Booking;
using BookingService.Domain.Entities;

namespace BookingService.Application.MappingProfiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<InsertBookingDTO, Booking>()
                .ForMember(booking => booking.TableId,
                           options => options.MapFrom(insertBookingDTO => insertBookingDTO.TableId))
                .ForMember(booking => booking.BookingTime,
                           options => options.MapFrom(insertBookingDTO => insertBookingDTO.BookingTime))
                .ForMember(booking => booking.ClientId,
                           options => options.Ignore())
                .ForMember(booking => booking.Id,
                           options => options.Ignore())
                .ForMember(booking => booking.Table,
                           options => options.Ignore())
                .ForMember(booking => booking.Client,
                           options => options.Ignore())
            .ReverseMap();

            CreateMap<UpdateBookingDTO, Booking>()
                .ForMember(booking => booking.TableId,
                           options => options.MapFrom(updateBookingDTO => updateBookingDTO.TableId))
                .ForMember(booking => booking.BookingTime,
                           options => options.MapFrom(updateBookingDTO => updateBookingDTO.BookingTime))
                .ForMember(booking => booking.ClientId,
                           options => options.Ignore())
                .ForMember(booking => booking.Id,
                           options => options.Ignore())
                .ForMember(booking => booking.Table,
                           options => options.Ignore())
                .ForMember(booking => booking.Client,
                           options => options.Ignore())
            .ReverseMap();

            CreateMap<Booking, ReadBookingDTO>()
                .ForMember(readBookingDTO => readBookingDTO.ClientId,
                           options => options.MapFrom(booking => booking.ClientId))
                .ForMember(readBookingDTO => readBookingDTO.TableId,
                           options => options.MapFrom(booking => booking.TableId))
                .ForMember(readBookingDTO => readBookingDTO.BookingTime,
                           options => options.MapFrom(booking => booking.BookingTime))
                .ForMember(readBookingDTO => readBookingDTO.ClientName,
                           options => options.MapFrom(booking => booking.Client.Name))
                .ForMember(readBookingDTO => readBookingDTO.Id,
                           options => options.MapFrom(booking => booking.Id))
                .ForMember(readBookingDTO => readBookingDTO.RestaurantName,
                           options => options.MapFrom(booking => booking.Table.Restaurant.Name));

            CreateMap<Booking, Booking>();
        }
    }
}
