using CatalogService.Application.Validators.Restaurant;
using CatalogService.Tests.Fakers;
using FluentValidation.TestHelper;

namespace CatalogService.Tests.ValidationTests.Restaurant
{
    public class UpdateRestaurantValidatorTests
    {
        private readonly UpdateRestaurantValidator _updateRestaurantValidator;

        public UpdateRestaurantValidatorTests()
        {
            _updateRestaurantValidator = new UpdateRestaurantValidator();
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsValid_ShouldNotHaveValidationErrors()
        {
            //Arrange
            var updateRestaurantDTO = RestaurantDataFaker.GetFakedUpdateRestaurantDTO();

            //Act
            var result = _updateRestaurantValidator.TestValidate(updateRestaurantDTO);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForName()
        {
            //Arrange
            var updateRestaurantDTO = RestaurantDataFaker.GetFakedUpdateRestaurantDTO();
            updateRestaurantDTO.Name = string.Empty;

            //Act
            var result = _updateRestaurantValidator.TestValidate(updateRestaurantDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(updateRestaurantDTO => updateRestaurantDTO.Name);
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForCity()
        {
            //Arrange
            var updateRestaurantDTO = RestaurantDataFaker.GetFakedUpdateRestaurantDTO();
            updateRestaurantDTO.City = string.Empty;

            //Act
            var result = _updateRestaurantValidator.TestValidate(updateRestaurantDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(updateRestaurantDTO => updateRestaurantDTO.City);
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForStreet()
        {
            //Arrange
            var updateRestaurantDTO = RestaurantDataFaker.GetFakedUpdateRestaurantDTO();
            updateRestaurantDTO.Street = string.Empty;

            //Act
            var result = _updateRestaurantValidator.TestValidate(updateRestaurantDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(updateRestaurantDTO => updateRestaurantDTO.Street);
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForHouse()
        {
            //Arrange
            var updateRestaurantDTO = RestaurantDataFaker.GetFakedUpdateRestaurantDTO();
            updateRestaurantDTO.House = string.Empty;

            //Act
            var result = _updateRestaurantValidator.TestValidate(updateRestaurantDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(updateRestaurantDTO => updateRestaurantDTO.House);
        }
    }
}
