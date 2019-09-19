namespace Shared.Commands
{
    public interface ICommandHanler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
