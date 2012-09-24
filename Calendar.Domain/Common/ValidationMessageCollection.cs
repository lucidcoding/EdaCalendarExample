using System.Collections.Generic;
using System.Linq;

namespace Calendar.Domain.Common
{
    public class ValidationMessageCollection : List<ValidationMessage>
    {
        public void AddError(string text)
        {
            Add(new ValidationMessage(MessageType.Error, text));
        }

        public void AddError(string field, string text)
        {
            Add(new ValidationMessage(MessageType.Error, field, text));
        }

        public void AddWarning(string text)
        {
            Add(new ValidationMessage(MessageType.Warning, text));
        }

        public List<ValidationMessage> Infos
        {
            get
            {
                return (from validationMessage
                        in this
                        where validationMessage.Type == MessageType.Info
                        select validationMessage).ToList();
            }
        }

        public List<ValidationMessage> Errors
        {
            get
            {
                return (from validationMessage
                        in this
                        where validationMessage.Type == MessageType.Error
                        select validationMessage).ToList();
            }
        }

        public List<ValidationMessage> Warnings
        {
            get
            {
                return (from validationMessage
                        in this
                        where validationMessage.Type == MessageType.Warning
                        select validationMessage).ToList();
            }
        }

        public bool ContainsError(string field, string text)
        {
            return Contains(new ValidationMessage(MessageType.Error, field, text));
        }

        public bool IsValid
        {
            get { return Errors.Count == 0; }
        }

        public string FullMessage
        {
            get 
            { 
                string returnValue = "";

                foreach (var message in this)
                {
                    returnValue += message.Text;
                    if (message != this.Last()) returnValue += " ";
                }
                    
                return returnValue;
            }
        }
    }
}
