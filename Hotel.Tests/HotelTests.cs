using Hotel.Domain.Data;
using Xunit;
using Hotel.Domain.Entities;

namespace Hotel.Tests;

/// <summary>
/// Юнит-тесты по доменной области
/// </summary>
public class HotelTests
{
    /// <summary>
    /// 1. Информация о клиентах, проживавших в номерах определённого типа
    /// </summary>
    [Fact]
    public void GetClientsByRoomType()
    {
        List<Client> expected = [ TestData.Clients[5], TestData.Clients[2], TestData.Clients[7] ];
        var roomType = TestData.RoomTypes[5];

        var result = TestData.Bookings
            .Where(booking => booking.Room.Type == roomType)
            .Select(booking => booking.Client)
            .OrderBy(client => client.Surname).ToList();

        Assert.Equal(result, expected);
    }

    /// <summary>
    /// 2. Информация о забронированных номерах
    /// </summary>
    [Fact]
    public void GetBookedRooms()
    {
        List<Room> expected = [ TestData.Rooms[6], TestData.Rooms[3] ];
        var date = new DateOnly(2026, 1, 25);
        
        var result = TestData.Bookings
            .Where(booking => 
                (booking.CheckInDate <= date) && 
                (booking.CheckInDate.AddDays((int)booking.DaysOfStay) >= date))
            .Select(booking => booking.Room)
            .ToList();

        Assert.Equal(result, expected);
    }

    /// <summary>
    /// 3. Информация о 5 наиболее часто снимаемых комнатах
    /// </summary>
    [Fact]
    public void GetTop5MostPopularRooms()
    {
        List<Room> expected = 
        [
            TestData.Rooms[5],
            TestData.Rooms[6],
            TestData.Rooms[2],
            TestData.Rooms[3],
            TestData.Rooms[0]
        ];
        
        var result = TestData.Bookings
            .GroupBy(booking => booking.Room)
            .OrderBy(rooms => rooms.Count())
            .Reverse()
            .Take(5)
            .Select(rooms => rooms.Key)
            .ToList();
        
        Assert.Equal(result, expected);
    }

    /// <summary>
    /// 4. Информация о колличестве бронирований у каждой комнаты
    /// </summary>
    [Fact]
    public void GetBookingCountOfEveryRoom()
    {
        List<int> expected = [ 2, 2, 2, 1, 1, 3, 3, 1 ];

        var result = TestData.Bookings
            .GroupBy(booking => booking.Room.Number)
            .OrderBy(group => group.Key)
            .Select(group => group.Count())
            .ToList();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// 5. Топ 5 клиентов по суммарной стоимости проживания
    /// </summary>
    [Fact]
    public void GetTop5MostRichClients()
    {
        List<Client> expected = 
        [
            TestData.Clients[4],
            TestData.Clients[0],
            TestData.Clients[5],
            TestData.Clients[7],
            TestData.Clients[2],
        ];

        var result = TestData.Bookings
            .GroupBy(booking => booking.Client)
            .OrderBy(group => group.Sum(booking => booking.TotalPrice))
            .Reverse()
            .Select(group => group.Key)
            .Take(5)
            .ToList();

        Assert.Equal(expected, result);
    }
}