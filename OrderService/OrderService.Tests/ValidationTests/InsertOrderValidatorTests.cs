using FluentValidation.TestHelper;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Validators.Order;
using OrderService.Tests.Fakers;

namespace OrderService.Tests.ValidationTests
{
    public class InsertOrderValidatorTests
    {
        private readonly InsertOrderValidator _validator;

        public InsertOrderValidatorTests()
        {
            _validator = new InsertOrderValidator();
        }

        [Fact]
        public void InsertOrderDTO_WhenItIsValid_ShouldNotHaveValidationErrors()
        {
            //Arrange
            InsertOrderDTO insertOrderDTO = OrderDataFaker.GetFakedInsertOrderDTO();

            //Act
            var result = _validator.TestValidate(insertOrderDTO);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void InsertOrderDTO_WhenMenuIdIsZero_ShouldHaveValidationErrorForMenuId()
        {
            //Arrange
            InsertOrderDTO insertOrderDTO = OrderDataFaker.GetFakedInsertOrderDTO();
            insertOrderDTO.MenuId = 0;

            //Act
            var result = _validator.TestValidate(insertOrderDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(result => result.MenuId);
        }

        [Fact]
        public void InsertOrderDTO_WhenMenuIdIsZero_ShouldHaveValidationErrorForBookingId()
        {
            //Arrange
            InsertOrderDTO insertOrderDTO = OrderDataFaker.GetFakedInsertOrderDTO();
            insertOrderDTO.BookingId = 0;

            //Act
            var result = _validator.TestValidate(insertOrderDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(result => result.BookingId);
        }
    }
}
