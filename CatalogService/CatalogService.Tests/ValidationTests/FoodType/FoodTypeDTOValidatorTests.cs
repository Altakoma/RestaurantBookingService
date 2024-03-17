using CatalogService.Application.Validators.FoodType;
using CatalogService.Tests.Fakers;
using FluentValidation.TestHelper;

namespace CatalogService.Tests.ValidationTests.FoodType
{
    public class FoodTypeDTOValidatorTests
    {
        private readonly FoodTypeDTOValidator _foodTypeDTOValidator;

        public FoodTypeDTOValidatorTests()
        {
            _foodTypeDTOValidator = new FoodTypeDTOValidator();
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsValid_ShouldNotHaveValidationErrors()
        {
            //Arrange
            var foodTypeDTO = FoodTypeDataFaker.GetFakedFoodTypeDTO();

            //Act
            var result = _foodTypeDTOValidator.TestValidate(foodTypeDTO);

            //Assert
            result.ShouldNotHaveValidationErrorFor(foodTypeDTO => foodTypeDTO.Name);
        }

        [Fact]
        public void TestFoodTypeDTOValidator_WhenItIsNotValid_ShouldHaveValidationErrorForName()
        {
            //Arrange
            var foodTypeDTO = FoodTypeDataFaker.GetFakedFoodTypeDTO();
            foodTypeDTO.Name = string.Empty;

            //Act
            var result = _foodTypeDTOValidator.TestValidate(foodTypeDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(foodTypeDTO => foodTypeDTO.Name);
        }
    }
}
