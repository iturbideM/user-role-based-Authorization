namespace Exceptions
{
    public class RepeatedException : Exception
    {
        public RepeatedException(string message) : base(message) { }
    }
}