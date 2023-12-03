using FluentValidation.TestHelper;
using IdentityService.API.Tests.Fakers;
using IdentityService.BusinessLogic.Validatiors.User;

namespace IdentityService.API.Tests.ValidatorsTests
{
    public class InsertUsertValidationTests
    {
        private readonly InsertUserValidator _insertUserValidator;

        public InsertUsertValidationTests()
        {
            _insertUserValidator = new InsertUserValidator();
        }

        [Fact]
        public void TestInsertUserDTO_WhenNameIsEmpty_ShouldHaveValidationError()
        {
            //Arrange
            var insertUserDTO = UserDataFaker.GetFakedInsertUserDTO();
            insertUserDTO.Name = string.Empty;

            //Act
            var result = _insertUserValidator.TestValidate(insertUserDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(insertUserDTO => insertUserDTO.Name);
        }

        [Theory]
        [InlineData("testName")]
        public void TestInsertUserDTO_WhenNameItIsNotEmpty_ShouldNotHaveValidationError(
            string name)
        {
            //Arrange
            var insertUserDTO = UserDataFaker.GetFakedInsertUserDTO();
            insertUserDTO.Name = name;

            //Act
            var result = _insertUserValidator.TestValidate(insertUserDTO);

            //Assert
            result.ShouldNotHaveValidationErrorFor(insertUserDTO => insertUserDTO.Name);
        }

        [Fact]
        public void TestInsertUserDTO_WhenLoginIsEmpty_ShouldHaveValidationError()
        {
            //Arrange
            var insertUserDTO = UserDataFaker.GetFakedInsertUserDTO();
            insertUserDTO.Login = string.Empty;

            //Act
            var result = _insertUserValidator.TestValidate(insertUserDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(insertUserDTO => insertUserDTO.Login);
        }

        [Theory]
        [InlineData("testLogin")]
        public void TestInsertUserDTO_WhenLoginIsNotEmpty_ShouldNotHaveValidationError(
            string login)
        {
            //Arrange
            var insertUserDTO = UserDataFaker.GetFakedInsertUserDTO();
            insertUserDTO.Login = login;

            //Act
            var result = _insertUserValidator.TestValidate(insertUserDTO);

            //Assert
            result.ShouldNotHaveValidationErrorFor(insertUserDTO => insertUserDTO.Login);
        }

        [Fact]
        public void TestInsertUserDTO_WhenPasswordIsEmpty_ShouldHaveValidationError()
        {
            //Arrange
            var insertUserDTO = UserDataFaker.GetFakedInsertUserDTO();
            insertUserDTO.Password = string.Empty;

            //Act
            var result = _insertUserValidator.TestValidate(insertUserDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(insertUserDTO => insertUserDTO.Password);
        }

        [Theory]
        [InlineData("testPassword")]
        public void TestInsertUserDTO_WhenPasswordIsNotEmpty_ShouldNotHaveValidationError(
            string password)
        {
            //Arrange
            var insertUserDTO = UserDataFaker.GetFakedInsertUserDTO();
            insertUserDTO.Password = password;

            //Act
            var result = _insertUserValidator.TestValidate(insertUserDTO);

            //Assert
            result.ShouldNotHaveValidationErrorFor(insertUserDTO => insertUserDTO.Password);
        }

        [Fact]
        public void TestInsertUserDTO_WhenUserRoleIdIsZero_ShouldHaveValidationError()
        {
            //Arrange
            var insertUserDTO = UserDataFaker.GetFakedInsertUserDTO();
            insertUserDTO.UserRoleId = 0;

            //Act
            var result = _insertUserValidator.TestValidate(insertUserDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(insertUserDTO => insertUserDTO.UserRoleId);
        }

        [Fact]
        public void TestInsertUserDTO_WhenUserRoleIdIsNotZero_ShouldNotHaveValidationError()
        {
            //Arrange
            var insertUserDTO = UserDataFaker.GetFakedInsertUserDTO();

            //Act
            var result = _insertUserValidator.TestValidate(insertUserDTO);

            //Assert
            result.ShouldNotHaveValidationErrorFor(insertUserDTO => insertUserDTO.UserRoleId);
        }
    }
}
