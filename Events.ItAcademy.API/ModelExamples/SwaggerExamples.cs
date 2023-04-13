using Events.ItAcademy.Application.Events.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace Events.ItAcademy.API.ModelExamples
{
    public class SwaggerExamples
    {
        public class EventCreate : IMultipleExamplesProvider<EventRequestModel>
        {
            public IEnumerable<SwaggerExample<EventRequestModel>> GetExamples()
            {
                yield return SwaggerExample.Create("example 1", new EventRequestModel
                {
                    Title = "Metallica concert in Tbilisi",
                    Description = "Metallica exclusively visited georgia to perform in city of Tbilisi",
                    StartDate = DateTime.Now.AddDays(15),
                    EndDate = DateTime.Now.AddDays(15).AddHours(8),
                    Price = 120,
                    TicketCount = 1000
                });

                yield return SwaggerExample.Create("example 2", new EventRequestModel
                {
                    Title = "Gore verbinski in Tbilisi",
                    Description = "Famous movie director is in georgia to spread his knowledge about filmmaking",
                    StartDate = DateTime.Now.AddDays(10),
                    EndDate = DateTime.Now.AddDays(10).AddHours(3),
                    Price = 50,
                    TicketCount = 200
                });

                yield return SwaggerExample.Create("example 3", new EventRequestModel
                {
                    Title = "Fashion week",
                    Description = "Famous models came into georgia to participate in fashion week",
                    StartDate = DateTime.Now.AddDays(20),
                    EndDate = DateTime.Now.AddDays(21),
                    Price = 200,
                    TicketCount = 100
                });

            }

        }

        public class EventUpdate : IMultipleExamplesProvider<EventRequestPutModel>
        {
            public IEnumerable<SwaggerExample<EventRequestPutModel>> GetExamples()
            {
                yield return SwaggerExample.Create("example 1", new EventRequestPutModel
                {
                    Title = "Screening of old western movies",
                    Description = "For old movie enthusiasts we will be showing movies like the good bad and the ugly",
                    StartDate = DateTime.Now.AddDays(5),
                    EndDate = DateTime.Now.AddDays(5).AddHours(9),
                    Price = 20,
                    TicketCount = 200
                });

                yield return SwaggerExample.Create("example 2", new EventRequestPutModel
                {
                    Title = "Car auction",
                    Description = "For car enthusiasts we will have auction of old cars. brands will include mercedes, bmw...",
                    StartDate = DateTime.Now.AddDays(9),
                    EndDate = DateTime.Now.AddDays(10).AddHours(6),
                    Price = 25,
                    TicketCount = 400
                });


            }

        }
    }
}
