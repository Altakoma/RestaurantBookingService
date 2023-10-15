namespace IdentityService.DataAccess.Exceptions
{
    public static class ExceptionMessages
    {
        public const string NotEnteredPropertyMessage = "Please ensure you have entered your {0}";

        public const string DetectionZeroPropertyMessage = "Please ensure you didn't put 0 in {0}";

        public const string NotFoundExceptionMessage = "Object of type {0} having {1} key wasn't found";

        public const string DbOperationExceptionMessage = "Method {0} has called database exception when" +
            " attempting to perform an action on an object of type {1} having {2} key wasn't found";
    }
}
