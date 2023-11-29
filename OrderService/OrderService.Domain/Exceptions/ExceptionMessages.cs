﻿namespace OrderService.Domain.Exceptions
{
    public static class ExceptionMessages
    {
        public const string NotEnteredProperty = "Please ensure you have entered your {PropertyName}";

        public const string NotFoundExceptionMessage = "Object of type {0} hasn't been found by {1} key";

        public const string NullReferenceMessage = "Object of type {0} has been null";

        public const string NotClientBookingMessage = "Booking of key {0} is not your and you can not do anything with it";

        public const string BadConfigurationProvidedMessage = "Configuration string {0} was not defined or was uncorrect";

        public const string NotCommittedTransactionAtBehaviorMessage = "A transaction hasn't been committed at the" +
            " transactional behavior";

        public const string EmployeeAuthorizationExceptionMessage = "Token provided {0} key of {1} and it doesn't give you" +
            "permission to do that action, because there is no such a {2}";

        public const string NotRestaurantEmployeeAuthorizationExceptionMessage = "Token provided {0} key of {1} and it doesn't give you" +
            "permission to do that action, because you aren't an employee of this restaurant";

        public const string DbOperationExceptionMessage = "Method {0} has called database exception when" +
            " attempting to perform an action on an object of type {1} having {2} key wasn't found";
    }
}
