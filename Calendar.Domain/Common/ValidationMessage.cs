namespace Calendar.Domain.Common
{
    public enum MessageType
    {
        Info = 0,
        Warning = 1,
        Error = 2
    }

    public class ValidationMessage
    {
        protected MessageType _type;
        protected string _field;
        protected string _text;

        public MessageType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Field
        {
            get { return _field; }
            set { _field = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public ValidationMessage(MessageType type, string text)
        {
            _type = type;
            _text = text;
        }

        public ValidationMessage(MessageType type, string field, string text)
        {
            _type = type;
            _field = field;
            _text = text;
        }
    }
}
