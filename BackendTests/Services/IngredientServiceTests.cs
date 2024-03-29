﻿using HogwartsPotions.Data;
using HogwartsPotions.Services;
using NSubstitute;
using NUnit.Framework;
using System;

namespace BackendTests.Services
{
    [TestFixture]
    public class IngredientServiceTests
    {
        private HogwartsContext subHogwartsContext;

        [SetUp]
        public async Task SetUp()
        {
            var init = new Initialize();
            this.subHogwartsContext = init.HogwartsContext;
            await init.InitializeDb(subHogwartsContext);
        }

        private IngredientService CreateService()
        {
            return new IngredientService(
                this.subHogwartsContext);
        }

        [Test]
        public void GetIngredientlistByName_Test()
        {
            var service = this.CreateService();
            // Arrange
            var potionIngredients = new List<string> { "Alcohol" };

            // Act
            var result = service.GetIngredientlistByName(
                potionIngredients);

            // Assert
            Assert.That(result[0].Name, Is.EqualTo("Alcohol"));
        }

        [Test]
        public void GetIngredientByName_Test()
        {
            // Arrange
            var service = this.CreateService();
            string ingredient = "Alcohol";

            // Act
            var result = service.GetIngredientByName(
                ingredient);

            // Assert
            Assert.That(result.Name, Is.EqualTo("Alcohol"));
        }

        [Test]
        public void GetAllIngredients_Test()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = service.GetAllIngredients();

            // Assert
            Assert.That(result, Is.EqualTo(subHogwartsContext.Ingredients.ToList()));
        }
        [TearDown]
        public void TearDown()
        {
            subHogwartsContext.Database.EnsureDeleted();
        }
    }
}
