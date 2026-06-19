using Xunit;
using Moq;
using FluentAssertions;
using BackendAwSmartstay.API.Bookings.Application.Internal.CommandServices;
using BackendAwSmartstay.API.Bookings.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Bookings.Domain.Model.Commands;
using BackendAwSmartstay.API.Bookings.Domain.Repositories;
using BackendAwSmartstay.API.Shared.Domain.Repositories;

namespace BackendAwSmartstay.Tests.Bookings.Application.Internal.CommandServices;

/// <summary>
/// Verifies the behavior of <see cref="BookingCommandService"/>
/// when processing booking-related commands.
/// </summary>
public class BookingCommandServiceTests
{
    /// <summary>
    /// Ensures that when a valid booking creation command is handled,
    /// a new booking is created, persisted through the repository,
    /// and the transaction is completed successfully.
    /// </summary>
    /// <remarks>
    /// Scenario:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// Given a command containing valid booking information.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// When the command is handled by the application service.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// Then a booking should be created, persisted, and returned,
    /// and the unit of work should complete the transaction.
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>
    [Fact]
    public async Task GivenValidBookingData_WhenHandleCreateBookingCommand_ThenShouldSaveAndReturnBooking()
    {
        // =========================================================================
        // ARRANGE (Given)
        // =========================================================================
        var mockBookingRepository = new Mock<IBookingRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(u => u.CompleteAsync())
            .Returns(Task.CompletedTask);

        var commandService = new BookingCommandService(
            mockBookingRepository.Object,
            mockUnitOfWork.Object);

        var command = new CreateBookingCommand(
            RoomId: 101,
            GuestName: "Mariana López",
            GuestEmail: "mariana@upc.pe",
            CheckInDate: new DateTime(2026, 07, 15),
            CheckOutDate: new DateTime(2026, 07, 20));

        // =========================================================================
        // ACT (When)
        // =========================================================================
        var result = await commandService.Handle(command);

        // =========================================================================
        // ASSERT (Then)
        // =========================================================================
        result.Should().NotBeNull();
        result!.GuestName.Should().Be("Mariana López");
        result.RoomId.Should().Be(101);

        mockBookingRepository.Verify(
            repository => repository.AddAsync(It.IsAny<Booking>()),
            Times.Once);

        mockUnitOfWork.Verify(
            unitOfWork => unitOfWork.CompleteAsync(),
            Times.Once);
    }
}