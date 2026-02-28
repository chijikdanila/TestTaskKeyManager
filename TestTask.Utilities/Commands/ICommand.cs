namespace TestTask.Utilities.Commands;

public interface ICommand : System.Windows.Input.ICommand
{
    void Execute() => ExecuteAsync().ConfigureAwait(false);
    void System.Windows.Input.ICommand.Execute(object? parameter) => ExecuteAsync().ConfigureAwait(false);
    bool System.Windows.Input.ICommand.CanExecute(object? parameter) => true;

    Task ExecuteAsync();
    Task<bool> CanExecuteAsync();

    public static RelayCommand From(Action action) => new(action);
    public static RelayCommand From(Action action, Func<bool> canExecuteFunc) => new(action, canExecuteFunc);
    public static RelayCommand From(Action action, Func<Task<bool>> canExecuteFunc) => new(action, canExecuteFunc);
    public static RelayCommand From(Func<Task> action) => new(action);
    public static RelayCommand From(Func<Task> action, Func<bool> canExecuteFunc) => new(action, canExecuteFunc);
    public static RelayCommand From(Func<Task> action, Func<Task<bool>> canExecuteFunc) => new(action, canExecuteFunc);
}

public interface ICommand<T> : System.Windows.Input.ICommand
{
    void System.Windows.Input.ICommand.Execute(object? parameter) => ExecuteAsync((T)parameter).ConfigureAwait(false);
    bool System.Windows.Input.ICommand.CanExecute(object? parameter) => true;

    Task ExecuteAsync(T parameter);
    Task<bool> CanExecuteAsync();

    public static RelayCommand<T> From(Action<T> action) => new(action);
    public static RelayCommand<T> From(Action<T> action, Func<bool> canExecuteFunc) => new(action, canExecuteFunc);
    public static RelayCommand<T> From(Action<T> action, Func<Task<bool>> canExecuteFunc) => new(action, canExecuteFunc);
    public static RelayCommand<T> From(Func<T, Task> action) => new(action);
    public static RelayCommand<T> From(Func<T, Task> action, Func<bool> canExecuteFunc) => new(action, canExecuteFunc);
    public static RelayCommand<T> From(Func<T, Task> action, Func<Task<bool>> canExecuteFunc) => new(action, canExecuteFunc);
}
