using FluentValidation.TestHelper;
using IdentityService.API.Tests.Fakers;
using IdentityService.BusinessLogic.Validatiors.User;

namespace IdentityService.API.Tests.ValidatorsTests
{
    public class UpdateUserValidationTests
    {
        private readonly UpdateUserValidator _updateUserValidator;

        public UpdateUserValidationTests()
        {
            _updateUserValidator = new UpdateUserValidator();
        }

        [Fact]
        public void TestUpdateUserDTO_WhenNameIsEmpty_ShouldHaveValidationError()
        {
            //Arrange
            var updateUserDTO = UserDataFaker.GetFakedUpdateUserDTO();
            updateUserDTO.Name = string.Empty;

            //Act
            var result = _updateUserValidator.TestValidate(updateUserDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(updateUserDTO => updateUserDTO.Name);
        }

        [Theory]
        [InlineData("testName")]
        public void TestUpdateUserDTO_WhenNameItIsNotEmpty_ShouldNotHaveValidationError(
            string name)
        {
            //Arrange
            var updateUserDTO = UserDataFaker.GetFakedUpdateUserDTO();
            updateUserDTO.Name = name;

            //Act
            var result = _updateUserValidator.TestValidate(updateUserDTO);

            //Assert
            result.ShouldNotHaveValidationErrorFor(updateUserDTO => updateUserDTO.Name);
        }

        [Fact]
        public void TestUpdateUserDTO_WhenLoginIsEmpty_ShouldHaveValidationError()
        {
            //Arrange
            var updateUserDTO = UserDataFaker.GetFakedUpdateUserDTO();
            updateUserDTO.Login = string.Empty;

            //Act
            var result = _updateUserValidator.TestValidate(updateUserDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(updateUserDTO => updateUserDTO.Login);
        }

        [Theory]
        [InlineData("testLogin")]
        public void TestUpdateUserDTO_WhenLoginIsNotEmpty_ShouldNotHaveValidationError(
            string login)
        {
            //Arrange
            var updateUserDTO = UserDataFaker.GetFakedUpdateUserDTO();
            updateUserDTO.Login = login;

            //Act
            var result = _updateUserValidator.TestValidate(updateUserDTO);

            //Assert
            result.ShouldNotHaveValidationErrorFor(updateUserDTO => updateUserDTO.Login);
        }

        [Fact]
        public void TestUpdateUserDTO_WhenPasswordIsEmpty_ShouldHaveValidationError()
        {
            //Arrange
            var updateUserDTO = UserDataFaker.GetFakedUpdateUserDTO();
            updateUserDTO.Password = string.Empty;

            //Act
            var result = _updateUserValidator.TestValidate(updateUserDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(updateUserDTO => updateUserDTO.Password);
        }

        [Theory]
        [InlineData("testPassword")]
        public void TestUpdateUserDTO_WhenPasswordIsNotEmpty_ShouldNotHaveValidationError(
            string password)
        {
            //Arrange
            var updateUserDTO = UserDataFaker.GetFakedUpdateUserDTO();
            updateUserDTO.Password = password;

            //Act
            var result = _updateUserValidator.TestValidate(updateUserDTO);

            //Assert
            result.ShouldNotHaveValidationErrorFor(updateUserDTO => updateUserDTO.Password);
        }

        [Fact]
        public void TestUpdateUserDTO_WhenUserRoleIdIsZero_ShouldHaveValidationError()
        {
            //Arrange
            var updateUserDTO = UserDataFaker.GetFakedUpdateUserDTO();
            updateUserDTO.UserRoleId = 0;

            //Act
            var result = _updateUserValidator.TestValidate(updateUserDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(updateUserDTO => updateUserDTO.UserRoleId);
        }

        [Fact]
        public void TestUpdateUserDTO_WhenUserRoleIdIsNotZero_ShouldNotHaveValidationError()
        {
            //Arrange
            var updateUserDTO = UserDataFaker.GetFakedUpdateUserDTO();

            //Act
            var result = _updateUserValidator.TestValidate(updateUserDTO);

            //Assert
            result.ShouldNotHaveValidationErrorFor(updateUserDTO => updateUserDTO.UserRoleId);
        }
    }
}
