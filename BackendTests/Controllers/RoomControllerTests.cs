﻿namespace BackendTests.Controllers
{
    [TestFixture]
    public class RoomControllerTests
    {

        private RoomController subRoomController;
        private Initialize context;

        [SetUp]
        public void SetUp()
        {
            this.context = new Initialize();
            this.subRoomController = new RoomController(context.HogwartsContext);
        }


        [Test]
        public async Task GetAllRooms_Test()
        {
            // Arrange
            var rooms = context.HogwartsContext.GetAllRooms().Result;
            // Act
            var result = await subRoomController.GetAllRooms();

            // Assert
            Assert.That(result, Is.EqualTo(rooms));
        }

        [Test]
        public async Task AddRoom_Test()
        {
            // Arrange

            Room room = new Room();
            var rooms = context.HogwartsContext.GetAllRooms().Result;
            // Act
            await subRoomController.AddRoom(
                room);
            var result = context.HogwartsContext.GetAllRooms().Result;
            // Assert
            Assert.That(result, Is.Not.EqualTo(rooms));
        }

        [Test]
        public async Task GetRoomById_Test()
        {
            // Arrange

            long id = 1;
            var room = context.HogwartsContext.GetRoom(id).Result;
            // Act
            var result = await subRoomController.GetRoomById(
                id);

            // Assert
            Assert.That(result, Is.EqualTo(room));
        }

        [Test]
        public void UpdateRoomById_Test()
        {
            // Arrange

            long id = 1;
            var room = context.HogwartsContext.GetRoom(id).Result;
            Room updatedRoom = room;
            updatedRoom.Capacity = 2;

            // Act
            subRoomController.UpdateRoomById(
                id,
                updatedRoom);
            var result = context.HogwartsContext.GetRoom(id).Result;
            // Assert
            Assert.That(result.Capacity, Is.EqualTo(2));
        }

        [Test]
        public async Task DeleteRoomById_Test()
        {
            // Arrange

            long id = 1;

            // Act
            await subRoomController.DeleteRoomById(
                id);
            var result = context.HogwartsContext.GetRoom(id).Result;
            // Assert
            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public async Task GetRoomsForRatOwners_Test()
        {
            // Arrange


            // Act
            var result = await subRoomController.GetRoomsForRatOwners();

            // Assert
            Assert.That(result, Is.EqualTo(new List<Room>()));
        }

        [TearDown]
        public void TearDown()
        {
            context.HogwartsContext.Database.EnsureDeleted();
        }
    }
}
