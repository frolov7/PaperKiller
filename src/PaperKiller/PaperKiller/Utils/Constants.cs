namespace PaperKiller.Utils
{
    public class Constants
    {
        public enum UserStatus
        {
            COMMANDANT,
            MANAGER,
            STUDENT,
            NONEXISTENT
        }

        public enum InputError
        {
            FieldERROR,
            GenderERROR,
            LoginERROR,
            StudentIDERROR,
            PasswordERROR,
            PhoneERROR,
            SUCCESS
        }

        public enum RegistrationResult
        {
            SUCCESS,
            ValidationError,
            ServerError
        }
        public enum AuthorizationResult
        {
            SUCCESS,
            Unauthorized,
            ServerError
        }

        public enum RoomStatus
        {
            EVICTED,
            LIVES,
            NOT_ASSIGNED
        }

        public enum ExchangeResult
        {
            SUCCESS,
            ERROR,
            ItemNotFound,
            ItemAlreadyExchanged,
            NoAvailableItem
        }
    }
}
