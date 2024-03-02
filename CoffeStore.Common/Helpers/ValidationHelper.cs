namespace CoffeStore.Common.Helpers
{
    public static class ValidationHelper
    {
        public const ushort MIN_NAME_LENGTH = 3;
        public const ushort DOCUMENT_LENGTH = 11;
        public const ushort ZIP_CODE_LENGTH = 8;
        public const ushort MIN_PASSWORD_LENGTH = 8;
        public const ushort MIN_ADDRESS_LENGTH = 5;
        public const ushort STATE_LENGTH = 2;

        public const string ADDRESS_REGEX = @"^[a-zA-ZÀ-ÖØ-öø-ÿ0-9º\s]*$";
        public const string ZIP_CODE_REGEX = @"^\d{8}$";
        public const string STATE_REGEX = @"^[A-Z]{2}$";

        public static DateOnly GetMinBirthDate() => DateOnly.FromDateTime(DateTime.Now.AddYears(-120));
        public static DateOnly GetMaxBirthDate() => DateOnly.FromDateTime(DateTime.Today);

        public static bool BeValidDocument(string document)
        {
            if (string.IsNullOrWhiteSpace(document)) { return false; }

            document = new string(document.Where(char.IsDigit).ToArray());

            var invalidDocuments = new List<string>
            {
                "00000000000", "11111111111", "22222222222", "33333333333",
                "44444444444", "55555555555", "66666666666", "77777777777",
                "88888888888", "99999999999"
            };
            if (invalidDocuments.Contains(document)) { return false; }

            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(document[i].ToString()) * (10 - i);
            }

            int remainder = sum % 11;
            int checksum1 = remainder < 2 ? 0 : 11 - remainder;

            if (int.Parse(document[9].ToString()) != checksum1) { return false; }

            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(document[i].ToString()) * (11 - i);
            }
            remainder = sum % 11;
            int checksum2 = remainder < 2 ? 0 : 11 - remainder;

            return int.Parse(document[10].ToString()) == checksum2;
        }

    }
}
