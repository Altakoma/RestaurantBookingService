using CatalogService.Application.Validators.Restaurant;
using CatalogService.Tests.Fakers;
using FluentValidation.TestHelper;

namespace CatalogService.Tests.ValidationTests.Restaurant
{
    public class InsertRestaurantValidatorTests
    {
        private readonly InsertRestaurantValidator _insertRestaurantValidator;

        public InsertRestaurantValidatorTests()
        {
            _insertRestaurantValidator = new InsertRestaurantValidator();
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsValid_ShouldNotHaveValidationErrors()
        {
            //Arrange
            var insertRestaurantDTO = RestaurantDataFaker.GetFakedInsertRestaurantDTO();

            //Act
            var result = _insertRestaurantValidator.TestValidate(insertRestaurantDTO);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForName()
        {
            //Arrange
            var insertRestaurantDTO = RestaurantDataFaker.GetFakedInsertRestaurantDTO();
            insertRestaurantDTO.Name = string.Empty;

            //Act
            var result = _insertRestaurantValidator.TestValidate(insertRestaurantDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(insertRestaurantDTO => insertRestaurantDTO.Name);
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForCity()
        {
            //Arrange
            var insertRestaurantDTO = RestaurantDataFaker.GetFakedInsertRestaurantDTO();
            insertRestaurantDTO.City = string.Empty;

            //Act
            var result = _insertRestaurantValidator.TestValidate(insertRestaurantDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(insertRestaurantDTO => insertRestaurantDTO.City);
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForStreet()
        {
            //Arrange
            var insertRestaurantDTO = RestaurantDataFaker.GetFakedInsertRestaurantDTO();
            insertRestaurantDTO.Street = string.Empty;

            //Act
            var result = _insertRestaurantValidator.TestValidate(insertRestaurantDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(insertRestaurantDTO => insertRestaurantDTO.Street);
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForHouse()
        {
            //Arrange
            var insertRestaurantDTO = RestaurantDataFaker.GetFakedInsertRestaurantDTO();
            insertRestaurantDTO.House = string.Empty;

            //Act
            var result = _insertRestaurantValidator.TestValidate(insertRestaurantDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(insertRestaurantDTO => insertRestaurantDTO.House);
        }
    }
}
