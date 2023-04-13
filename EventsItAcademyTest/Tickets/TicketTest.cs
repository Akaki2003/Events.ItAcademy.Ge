using Events.ItAcademy.Application.Events.Repositories;
using Events.ItAcademy.Application.Events.Requests;
using Events.ItAcademy.Application.Events;
using Events.ItAcademy.Application.EventsSetups.Repositories;
using Events.ItAcademy.Application.UserTickets.Repositories;
using Events.ItAcademy.Domain.Events;
using Events.ItAcademy.Domain.EventSetup;
using Events.ItAcademy.Domain.UserTickets;
using Events.ItAcademy.Application.UserTickets;
using Events.ItAcademy.Application.Exceptions.Events;
using Events.ItAcademy.Application.Exceptions.Tickets;

namespace EventsItAcademyTest.Tickets
{
    public class TicketTest
    {
        private EventSetup _eventSetupDomain;
        private Event _eventDomain;
        private UserTicket _userTicketDomain;
        private readonly Mock<IEventRepository> eventMockRepo = new Mock<IEventRepository> { DefaultValue = DefaultValue.Empty };
        private readonly Mock<IEventSetupRepository> setupMockRepo = new Mock<IEventSetupRepository> { DefaultValue = DefaultValue.Empty };
        private readonly new Mock<IUserTicketRepository> ticketMockRepo = new Mock<IUserTicketRepository> { DefaultValue = DefaultValue.Empty };
        private readonly CancellationToken cancellationToken = new CancellationToken();
        private readonly UserTicketService ticketService;

        public TicketTest()
        {
            ticketService = new UserTicketService(ticketMockRepo.Object, eventMockRepo.Object, setupMockRepo.Object);
            _eventDomain = GetEventDomain();
            _eventSetupDomain = GetEventSetup();
            _userTicketDomain = GetUserTicket();
        }


        [Fact]
        public async Task ShouldReturnAllTicketsByUserId_WithoutException()
        {
            
            //arrange

            ticketMockRepo.Setup(x => x.GetAllTicketsByUserId(cancellationToken,It.IsAny<string>())).ReturnsAsync(new List<UserTicket>()
            {
                _userTicketDomain
            });

            //act
            var returnedTicket = await ticketService.GetAllTicketsByUserId(cancellationToken,It.IsAny<string>());

            //assert
            Assert.NotNull(returnedTicket);
        }

        [Fact]
        public async Task WhenBuyingReservedTicketAndTicketNotFound_ShouldThrowTicketNotFoundException()
        {

            //arrange
            _userTicketDomain = null;
            ticketMockRepo.Setup(x => x.GetTicketByTicketAndUserId(cancellationToken, It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_userTicketDomain);
        
            //act
            var test = async () => await ticketService.BuyReservedTicket(cancellationToken,It.IsAny<int>(), It.IsAny<string>());

            //assert
            await Assert.ThrowsAsync<TicketNotFoundException>(test);

        }


        [Fact]
        public async Task WhenBuyingReservedTicketAndTicketIsAlreadyPurchased_ShouldThrowTicketAlreadyPurchasedException()
        {
            _userTicketDomain.Reserved = false;
            //arrange
            ticketMockRepo.Setup(x => x.GetTicketByTicketAndUserId(cancellationToken, It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_userTicketDomain);

            //act
            var test = async () => await ticketService.BuyReservedTicket(cancellationToken, It.IsAny<int>(), It.IsAny<string>());

            //assert
            await Assert.ThrowsAsync<TicketAlreadyPurchasedException>(test);

        }


        [Fact] 
        public async Task WhenReservingTicketAndEventDoesntExist_ShouldThrowEventNotFoundException()
        {

            //arrange
            _eventDomain = null;
            eventMockRepo.Setup(x => x.GetEventByIdAsync(cancellationToken, It.IsAny<int>())).ReturnsAsync(_eventDomain);

            //act
            var test = async () => await ticketService.ReserveTicket(cancellationToken, It.IsAny<int>(), It.IsAny<string>());

            //assert
            await Assert.ThrowsAsync<EventNotFoundException>(test);

        }

        [Fact]
        public async Task WhenReservingTicketAndTicketDoesntExist_ShouldThrowTicketNotFoundException()
        {

            //arrange
            eventMockRepo.Setup(x => x.GetEventByIdAsync(cancellationToken, It.IsAny<int>())).ReturnsAsync(_eventDomain);
            ticketMockRepo.Setup(x => x.GetCurrentTicketCountByEventId(cancellationToken, It.IsAny<int>())).ReturnsAsync(_eventDomain.TicketCount);

            //act
            var test = async () => await ticketService.ReserveTicket(cancellationToken, It.IsAny<int>(), It.IsAny<string>());

            //assert
            await Assert.ThrowsAsync<TicketNotFoundException>(test);

        }  
        [Fact]
        public async Task WhenStraightBuyingTicketAndEventNotFound_ShouldThrowEventNotFoundException()
        {
            _eventDomain = null;
            //arrange
            eventMockRepo.Setup(x => x.GetEventByIdAsync(cancellationToken, It.IsAny<int>())).ReturnsAsync(_eventDomain);
            ticketMockRepo.Setup(x => x.GetCurrentTicketCountByEventId(cancellationToken, It.IsAny<int>())).ReturnsAsync(0);

            //act
            var test = async () => await ticketService.BuyStraightTicket(cancellationToken, It.IsAny<int>(), It.IsAny<string>());

            //assert
            await Assert.ThrowsAsync<EventNotFoundException>(test);

        }
        [Fact]
        public async Task WhenStraightBuyingTicketAndTicketNotFound_ShouldThrowTicketNotFoundException()
        {

            //arrange
            eventMockRepo.Setup(x => x.GetEventByIdAsync(cancellationToken, It.IsAny<int>())).ReturnsAsync(_eventDomain);
            ticketMockRepo.Setup(x => x.GetCurrentTicketCountByEventId(cancellationToken, It.IsAny<int>())).ReturnsAsync(_eventDomain.TicketCount);

            //act
            var test = async () => await ticketService.BuyStraightTicket(cancellationToken, It.IsAny<int>(), It.IsAny<string>());

            //assert
            await Assert.ThrowsAsync<TicketNotFoundException>(test);

        }

        [Fact]
        public async Task WhenRemovingReservedTicketAndTicketNotFound_ShouldThrowTicketNotFoundException()
        {
            _userTicketDomain = null;
            //arrange
            ticketMockRepo.Setup(x => x.GetTicketByTicketAndUserId(cancellationToken, It.IsAny<int>(),It.IsAny<string>())).ReturnsAsync(_userTicketDomain);

            //act
            var test = async () => await ticketService.RemoveReservedTicket(cancellationToken, It.IsAny<int>(), It.IsAny<string>());

            //assert
            await Assert.ThrowsAsync<TicketNotFoundException>(test);

        }


        [Fact]
        public async Task ShouldRemoveTicketsWithoutException()
        {

            //arrange
            ticketMockRepo.Setup(x => x.GetTicketByTicketAndUserId(cancellationToken, It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_userTicketDomain);
            setupMockRepo.Setup(x => x.GetEventSetupAsync(cancellationToken)).ReturnsAsync(_eventSetupDomain);
            //act
            var test = async () => await ticketService.RemoveReservedTickets(cancellationToken);
            var exception = await Record.ExceptionAsync(test);

            //assert
            Assert.Null(exception);

        }


    
        private EventRequestPutModel GetEventRequestPutModel()
        {
            EventRequestPutModel eventRequest = new EventRequestPutModel()
            {
                Id = 1,
                UserId = "1",
                Title = "New event",
                Description = "New event happening in centre of town tomorrow",
                Price = 30,
                TicketCount = 200,
                StartDate = DateTime.Now.AddDays(4),
                EndDate = DateTime.Now.AddDays(4).AddHours(3),
            };
            return eventRequest;
        }

        private Event GetEventDomain()
        {
            Event @event = new Event()
            {
                Title = "New event",
                Description = "New event happening in centre of town tomorrow",
                Price = 30,
                TicketCount = 200,
                CreatedAt = DateTime.UtcNow,
                IsActive = false,
                ModifiedAt = DateTime.UtcNow,
                Id = 4,
                UserId = "4",
                StartDate = DateTime.Now.AddDays(4),
                EndDate = DateTime.Now.AddDays(4).AddHours(3),
            };
            return @event;
        }

        private EventSetup GetEventSetup()
        {
            EventSetup eventSetup = new EventSetup()
            {
                EditEventAfterUploadInDays = 4,
                Id = 4,
                ReserveTimeLengthInMinutes = 4
            };
            return eventSetup;
        }

        private UserTicket GetUserTicket()
        {
            UserTicket userTicket = new UserTicket()
            {
                ReservationDate = DateTime.Now,
                EventId = 3,
                Reserved = true,
                TicketId = 3,
                UserId = "3"
            };
            return userTicket;
        }
    }
}
