namespace PadelBackend.Exceptions
{
    public class NotFoundCustomEx : Exception
    {
        public string? errorMessageDetails = null;
        public NotFoundCustomEx(string? errorMessageDetails  = null) : base(errorMessageDetails) { }
    }
}
