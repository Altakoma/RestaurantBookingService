﻿namespace BookingService.Domain.Exceptions
{
    public static class ExceptionMessages
    {
        public const string NotEnteredProperty = "Please ensure you have entered your {PropertyName}";

        public const string NotFoundExceptionMessage = "Object of type {0} hasn't been found by {1} key";

        public const string DbOperationExceptionMessage = "Method {0} has called database exception when" +
            " attempting to perform an action on an object of type {1} having {2} key wasn't found";
    }
}
