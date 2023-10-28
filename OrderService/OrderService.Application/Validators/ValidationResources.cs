namespace BookingService.Application.Validators
{
    public static class ValidationResources
    {
        public const int ZeroId = 0;

        public const int MaximumLengthForSmallLengthColumn = 50;

        public const int MaximumLengthForMiddleLengthColumn = 100;

        public const string NotEnteredPropertyMessage = "Please ensure you have entered your {0}";

        public const string DetectionZeroPropertyMessage = "Please ensure you didn't put 0 in {0}";

        public const string ProvidedDataIsNotValid = "Provided datetime is less or equal of datetime.now()";
    }
}
