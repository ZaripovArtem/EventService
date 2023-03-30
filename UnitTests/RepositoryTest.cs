using Features.Events.Data;
using Features.Events.Domain;

namespace UnitTests
{
    public class Tests
    {
        public FakeData Data = new();

        [Test]
        public async Task Add_And_GetById()
        {
            

            var testEvent = new Event()
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa1"),
                EventName = "BuyBusiness",
                StartEvent = DateTimeOffset.Now,
                EndEvent = DateTimeOffset.Now,
                Ticket = new List<Ticket>()
                {
                    new()
                    {
                        Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa2"),
                        Place = 1
                    }
                },
                HasPlace = true
            };

            await Data.AddEvent(testEvent);
            var param1 = Data.GetEventById(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa1"));
            if (param1.Result != null) 
                Assert.AreEqual(param1.Result.Id, testEvent.Id);
        }

        [Test]
        public async Task Update()
        {
            var testEvent = new Event()
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa1"),
                EventName = "BuyBusinessInMiami",
                StartEvent = DateTimeOffset.Now,
                EndEvent = DateTimeOffset.Now,
                Ticket = new List<Ticket>()
                {
                    new()
                    {
                        Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa2"),
                        Place = 1
                    }
                },
                HasPlace = true
            };

            await Data.UpdateEvent(testEvent);

            var param1 = Data.GetEventById(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa1"));
            if (param1.Result != null)
                Assert.AreEqual(param1.Result.EventName, "BuyBusinessInMiami");
        }

        [Test]
        public async Task Delete()
        {
            var testEvent = new Event()
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                EventName = "TestToDelete",
                StartEvent = DateTimeOffset.Now,
                EndEvent = DateTimeOffset.Now,
                Ticket = new List<Ticket>()
                {
                    new()
                    {
                        Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa2"),
                        Place = 1
                    }
                },
                HasPlace = true
            };

            await Data.AddEvent(testEvent);
            var param1 = Data.GetEventById(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));
            await Data.DeleteEvent(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));
            var param2 = Data.GetEventById(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));
            Assert.AreNotSame(param1, param2);
        }
    }
}