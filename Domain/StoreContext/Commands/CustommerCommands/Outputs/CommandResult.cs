using Shared.Commands;

namespace Domain.StoreContext.Commands.CustommerCommands.Outputs
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool sucess, string message, object data) : this(sucess, message)
        {
            Sucess = sucess;
            Message = message;
            Data = data;
        }

        public CommandResult(bool sucess, string message)
        {
            Sucess = sucess;
            Message = message;
        }

        public bool Sucess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
