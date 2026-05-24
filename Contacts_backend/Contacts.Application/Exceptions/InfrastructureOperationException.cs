namespace Contacts.Application.Exceptions
{
    public class InfrastructureOperationException : Exception
    {
        public string Operation { get; }

        public InfrastructureOperationException(string operation, Exception innerException): base(
                $"Infrastructure error during {operation}: {innerException.Message}",innerException)
        {
            Operation = operation;
        }
    }
}
