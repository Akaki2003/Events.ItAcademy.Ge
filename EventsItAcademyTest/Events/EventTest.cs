using Events.ItAcademy.Application.ArchivedEvents;
using Events.ItAcademy.Application.ArchivedEvents.Repositories;
using Events.ItAcademy.Application.Events;
using Events.ItAcademy.Application.Events.Repositories;
using Events.ItAcademy.Application.Events.Requests;
using Events.ItAcademy.Application.EventsSetups.Repositories;
using Events.ItAcademy.Application.Exceptions.Events;
using Events.ItAcademy.Application.UserTickets.Repositories;
using Events.ItAcademy.Domain.Events;
using Events.ItAcademy.Domain.EventSetup;
using Events.ItAcademy.Domain.UserTickets;

namespace EventsItAcademyTest.Events
{
    public class EventTest
    {
        private EventRequestPutModel _eventRequestPut;
        private EventSetup _eventSetup;
        private Event _eventDomain;
        private List<Event> _eventDomains;
        private readonly string userId = "4";
        private readonly Mock<IEventRepository> mockRepo = new Mock<IEventRepository> { DefaultValue = DefaultValue.Empty };
        private readonly Mock<IArchivedEventRepository> archivedEventMockRepo = new Mock<IArchivedEventRepository> { DefaultValue = DefaultValue.Empty };
        private readonly Mock<IEventSetupRepository> setupMockRepo = new Mock<IEventSetupRepository> { DefaultValue = DefaultValue.Empty };
        private readonly new Mock<IUserTicketRepository> ticketMockRepo = new Mock<IUserTicketRepository> { DefaultValue = DefaultValue.Empty };
        private readonly CancellationToken cancellationToken = new CancellationToken();
        private readonly EventService eventService;
        private readonly ArchivedEventService archivedEventService;


        public EventTest()
        {
            eventService = new EventService(mockRepo.Object, setupMockRepo.Object, ticketMockRepo.Object);
            archivedEventService = new ArchivedEventService(archivedEventMockRepo.Object, mockRepo.Object, ticketMockRepo.Object);
            _eventRequestPut = GetEventRequestPutModel();
            _eventDomain = GetEventDomain();
            _eventSetup = GetEventSetup();
            _eventDomains = GetEventDomains();
        }
        [Fact]
        public async Task ShouldThrowException_WhenArgumentIsNull()
        {
            //arrange

            mockRepo.Setup(x => x.CreateAsync(cancellationToken, null, null));

            //act
            var task = async () => await eventService.CreateAsync(cancellationToken, null, null);

            //assert
            await Assert.ThrowsAsync<ArgumentNullException>(task);
        }
        [Fact]///
        public async Task ShouldReturnActiveEvents_WithoutException()
        {
            //arrange

            mockRepo.Setup(x => x.GetAllAsyncActive(cancellationToken)).ReturnsAsync(new List<Event>()
            {
                _eventDomain
            });

            //act
            var returnedEvent = await eventService.GetAllAsyncActive(cancellationToken);

            //assert
            Assert.NotNull(returnedEvent);
        }

        [Fact]///
        public async Task ShouldReturnEventsByUserId()
        {
            //arrange

            mockRepo.Setup(x => x.GetAllAsyncByUserId(cancellationToken, It.IsAny<string>())).ReturnsAsync(new List<Event>()
            {
               _eventDomain
            });


            //act
            var returnedEvent = await eventService.GetAllAsyncByUserId(cancellationToken, It.IsAny<string>());

            //assert
            Assert.NotNull(returnedEvent);
        }

        [Fact]///
        public async Task ShouldReturnInactiveEventsByUserId()
        {
            //arrange

            mockRepo.Setup(x => x.GetAllAsyncByUserIdNonActive(cancellationToken, It.IsAny<string>())).ReturnsAsync(new List<Event>()
            {
             _eventDomain
            });


            //act
            var returnedEvent = await eventService.GetAllAsyncByUserIdNonActive(cancellationToken, It.IsAny<string>());

            //assert
            Assert.NotNull(returnedEvent);
        }


        [Fact]///
        public async Task ShouldReturnInactiveEvents()
        {
            //arrange

            mockRepo.Setup(x => x.GetAllNonActiveAsync(cancellationToken)).ReturnsAsync(new List<Event>()
            {
              _eventDomain
            });


            //act
            var returnedEvent = await eventService.GetAllNonActiveAsync(cancellationToken);

            //assert
            Assert.NotNull(returnedEvent);
        }


        [Fact]///
        public async Task WhenEventGetById_ShouldReturnException_IfEventNotExist()
        {
            //arrange

            mockRepo.Setup(x => x.Exists(cancellationToken, It.IsAny<int>())).ReturnsAsync(false);

            //ticketMockRepo.Setup(x => x.GetCurrentTicketCountByEventId(cancellationToken, It.IsAny<int>())).ReturnsAsync(It.IsAny<int>());


            //act
            var returnedEvent = async () => await eventService.GetByIdAsync(cancellationToken, It.IsAny<int>());

            //assert
            await Assert.ThrowsAsync<EventNotFoundException>(returnedEvent);
        }


        [Fact]///
        public async Task WhenEventNotFoundWhileUpdatingEvent_ShouldThrowException()
        {
            //arrange
            Event ev = new Event();
            ev = null;
            mockRepo.Setup(x => x.GetEventByIdAsync(cancellationToken, It.IsAny<int>())).ReturnsAsync(ev);
            setupMockRepo.Setup(x => x.GetEventSetupAsync(cancellationToken)).ReturnsAsync(It.IsAny<EventSetup>());

            //act
            var task = async () => await eventService.UpdateEventAsync(cancellationToken, _eventRequestPut, userId);

            //assert

            await Assert.ThrowsAsync<EventNotFoundException>(task);

        }

        [Fact]//
        public async Task WhenEventUpdateExpiredWhileUpdatingEvent_ShouldThrowException()
        {
            //arrange
            _eventDomain.CreatedAt = DateTime.Now.AddYears(-100);
            mockRepo.Setup(x => x.GetEventByIdAsync(cancellationToken, It.IsAny<int>())).ReturnsAsync(_eventDomain);
            setupMockRepo.Setup(x => x.GetEventSetupAsync(cancellationToken)).ReturnsAsync(_eventSetup);
            //act
            var task = async () => await eventService.UpdateEventAsync(cancellationToken, _eventRequestPut, userId);

            //assert

            await Assert.ThrowsAsync<EventEditExpiredException>(task);

        }

        [Fact]
        public async Task WhenDeletingAndEventNotExists_Shuld()
        {
            //arrange

            mockRepo.Setup(x => x.Exists(cancellationToken, 3)).ReturnsAsync(false);

            //act
            var task = async () => await eventService.DeleteAsync(cancellationToken, 3, userId);

            //assert
            await Assert.ThrowsAsync<EventNotFoundException>(task);
        }
        [Fact]
        public async Task WhenEventNotExistWhileActivating_ShouldThrowNotFound()
        {
            //arrange
            mockRepo.Setup(x => x.Exists(cancellationToken, 3)).ReturnsAsync(false);

            //act
            var task = async () => await eventService.DeleteAsync(cancellationToken, 3, userId);

            //assert
            await Assert.ThrowsAsync<EventNotFoundException>(task);

        }

        [Fact]
        public async Task WhenAddingToArchive_ShouldntThrowException()
        {
            //arrange
            mockRepo.Setup(x => x.GetAllExpiredEvents(cancellationToken)).ReturnsAsync(_eventDomains);
            archivedEventMockRepo.Setup(x => x.AddToArchivedEvents(cancellationToken, _eventDomains)); 
            ticketMockRepo.Setup(x => x.GetAllTicketsByEventId(cancellationToken, It.IsAny<int>())).ReturnsAsync(It.IsAny<List<UserTicket>>()); 
            //act
            var task = async () => await archivedEventService.AddToArchivedEvents(cancellationToken);

            var test = await Record.ExceptionAsync(task);

            //assert
            Assert.Null(test);

        }


        [Fact]
        public async Task WhenActivatingEventAndEventDoesntExist_ShouldThrowEventNotFoundException()
        {
            //arrange
            mockRepo.Setup(x=>x.Exists( cancellationToken,3)).Throws<EventNotFoundException>();

            //act
            var task = async () => await eventService.ActivateEvent(cancellationToken,3);

            //assert
            await Assert.ThrowsAsync<EventNotFoundException>(task);
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
        private List<Event> GetEventDomains()
        {
            List<Event> events = new List<Event>()
            {
                new Event
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
                           },
                    };
            return events;
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
    }
}