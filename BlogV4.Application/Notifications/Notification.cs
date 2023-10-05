namespace BlogV4.Application.Notifications
{
    public class Notification<T>
    {

        public T Value { get; init; }
        public T Value1 { get; init; }
        public T Value2 { get; init; }
        private List<string> ListErrors { get; init; } = new List<string>();
        public IReadOnlyCollection<string> Notifications => ListErrors;


        public Notification(T value, List<string> errors)
        {
            Value = value;
            ListErrors = errors;
        }
        public Notification(T value)
        {
            Value = value;
        }

        public Notification(T value, T value1, T value2)
        {
            Value = value;
            Value1 = value1;
            Value2 = value2;
        }

        public Notification(List<string> errors)
        {
            ListErrors = errors;
        }
        public Notification(string error)
        {
            ListErrors.Add(error);
        }

    }
}
