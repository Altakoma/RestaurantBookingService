using CatalogService.Application.Validators.FoodType;
using CatalogService.Application.Validators.Menu;
using CatalogService.Tests.Fakers;
using FluentValidation.TestHelper;

namespace CatalogService.Tests.ValidationTests.Menu
{
    public class InsertMenuValidatorTests
    {
        private readonly InsertMenuValidator _insertMenuValidator;

        public InsertMenuValidatorTests()
        {
            _insertMenuValidator = new InsertMenuValidator();
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsValid_ShouldNotHaveValidationErrors()
        {
            //Arrange
            var insertMenuDTO = MenuDataFaker.GetFakedInsertMenuDTO();

            //Act
            var result = _insertMenuValidator.TestValidate(insertMenuDTO);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForCost()
        {
            //Arrange
            var insertMenuDTO = MenuDataFaker.GetFakedInsertMenuDTO();
            insertMenuDTO.Cost = 0;

            //Act
            var result = _insertMenuValidator.TestValidate(insertMenuDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(insertMenuDTO => insertMenuDTO.Cost);
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForFoodName()
        {
            //Arrange
            var insertMenuDTO = MenuDataFaker.GetFakedInsertMenuDTO();
            insertMenuDTO.FoodName = string.Empty;

            //Act
            var result = _insertMenuValidator.TestValidate(insertMenuDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(insertMenuDTO => insertMenuDTO.FoodName);
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForFoodTypeId()
        {
            //Arrange
            var insertMenuDTO = MenuDataFaker.GetFakedInsertMenuDTO();
            insertMenuDTO.FoodTypeId = 0;

            //Act
            var result = _insertMenuValidator.TestValidate(insertMenuDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(insertMenuDTO => insertMenuDTO.FoodTypeId);
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForRestaurantId()
        {
            //Arrange
            var insertMenuDTO = MenuDataFaker.GetFakedInsertMenuDTO();
            insertMenuDTO.RestaurantId = 0;

            //Act
            var result = _insertMenuValidator.TestValidate(insertMenuDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(insertMenuDTO => insertMenuDTO.RestaurantId);
        }
    }
}
