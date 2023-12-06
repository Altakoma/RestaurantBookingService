using FluentValidation.TestHelper;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Validators.Order;
using OrderService.Tests.Fakers;

namespace OrderService.Tests.ValidationTests
{
    public class UpdateOrderValidatorTests
    {
        private readonly UpdateOrderValidator _validator;

        public UpdateOrderValidatorTests()
        {
            _validator = new UpdateOrderValidator();
        }

        [Fact]
        public void UpdateOrderDTO_WhenItIsValid_ShouldNotHaveValidationErrors()
        {
            //Arrange
            UpdateOrderDTO updateOrderDTO = OrderDataFaker.GetFakedUpdateOrderDTO();

            //Act
            var result = _validator.TestValidate(updateOrderDTO);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void UpdateOrderDTO_WhenMenuIdIsZero_ShouldHaveValidationErrorForMenuId()
        {
            //Arrange
            UpdateOrderDTO updateOrderDTO = OrderDataFaker.GetFakedUpdateOrderDTO();
            updateOrderDTO.MenuId = 0;

            //Act
            var result = _validator.TestValidate(updateOrderDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(result => result.MenuId);
        }
    }
}
