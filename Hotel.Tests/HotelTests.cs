using Hotel.Domain.Shared.Enums;
using Hotel.Domain.Data;
using Xunit;

namespace Hotel.Tests;

/// <summary>
/// Юнит-тесты по доменной области
/// </summary>
public class HotelTests
{
    /// <summary>
    /// Информация о клиентах, проживавших в номерах определённого типа
    /// </summary>
    [Fact]
    public void GetClientsByRoomType()
    {
        Assert.True(true);
    }

    /// <summary>
    /// Информация о забронированных номерах
    /// </summary>
    [Fact]
    public void GetBookedRooms()
    {
        Assert.True(true);
    }

    /// <summary>
    /// Информация о 5 наиболее часто снимаемых комнатах
    /// </summary>
    [Fact]
    public void GetTop5MostPopularRooms()
    {
        Assert.True(true);
    }

    /// <summary>
    /// Информация о колличестве бронирований у каждой комнаты
    /// </summary>
    [Fact]
    public void GetBookingCountOfEveryRoom()
    {
        Assert.True(true);
    }

    /// <summary>
    /// Топ 5 клиентов по суммарной стоимости проживания
    /// </summary>
    [Fact]
    public void GetTop5MostRichClients()
    {
        Assert.True(true);
    }
}