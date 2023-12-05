using CatalogService.Application.Validators.Menu;
using CatalogService.Tests.Fakers;
using FluentValidation.TestHelper;

namespace CatalogService.Tests.ValidationTests.Menu
{
    public class UpdateMenuValidatorTests
    {
        private readonly UpdateMenuValidator _updateMenuValidator;

        public UpdateMenuValidatorTests()
        {
            _updateMenuValidator = new UpdateMenuValidator();
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsValid_ShouldNotHaveValidationErrors()
        {
            //Arrange
            var updateMenuDTO = MenuDataFaker.GetFakedUpdateMenuDTO();

            //Act
            var result = _updateMenuValidator.TestValidate(updateMenuDTO);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForCost()
        {
            //Arrange
            var updateMenuDTO = MenuDataFaker.GetFakedUpdateMenuDTO();
            updateMenuDTO.Cost = 0;

            //Act
            var result = _updateMenuValidator.TestValidate(updateMenuDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(updateMenuDTO => updateMenuDTO.Cost);
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForFoodName()
        {
            //Arrange
            var updateMenuDTO = MenuDataFaker.GetFakedUpdateMenuDTO();
            updateMenuDTO.FoodName = string.Empty;

            //Act
            var result = _updateMenuValidator.TestValidate(updateMenuDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(updateMenuDTO => updateMenuDTO.FoodName);
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForFoodTypeId()
        {
            //Arrange
            var updateMenuDTO = MenuDataFaker.GetFakedUpdateMenuDTO();
            updateMenuDTO.FoodTypeId = 0;

            //Act
            var result = _updateMenuValidator.TestValidate(updateMenuDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(updateMenuDTO => updateMenuDTO.FoodTypeId);
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForRestaurantId()
        {
            //Arrange
            var updateMenuDTO = MenuDataFaker.GetFakedUpdateMenuDTO();
            updateMenuDTO.RestaurantId = 0;

            //Act
            var result = _updateMenuValidator.TestValidate(updateMenuDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(updateMenuDTO => updateMenuDTO.RestaurantId);
        }
    }
}
