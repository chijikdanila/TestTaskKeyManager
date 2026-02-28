namespace TestTask.Utilities.Commands;

public class RelayCommand : ICommand
{
    private readonly Action? _action;
    private readonly Func<Task>? _actionAsync;

    private readonly Func<bool>? _canExecuteFunc;
    private readonly Func<Task<bool>>? _canExecuteAsyncFunc;

    public RelayCommand(Action action)
    {
        _action = action;
    }

    public RelayCommand(Action action, Func<bool> canExecute) : this(action)
    {
        _canExecuteFunc = canExecute;
    }

    public RelayCommand(Action action, Func<Task<bool>> canExecuteAsync) : this(action)
    {
        _canExecuteAsyncFunc = canExecuteAsync;
    }

    public RelayCommand(Func<Task> asyncAction)
    {
        _actionAsync = asyncAction;
    }

    public RelayCommand(Func<Task> asyncAction, Func<bool> canExecute) : this(asyncAction)
    {
        _canExecuteFunc = canExecute;
    }

    public RelayCommand(Func<Task> asyncAction, Func<Task<bool>> canExecuteAsync) : this(asyncAction)
    {
        _canExecuteAsyncFunc = canExecuteAsync;
    }

    public event EventHandler? CanExecuteChanged;

    public async Task<bool> CanExecuteAsync()
    {
        if (_canExecuteFunc is not null)
        {
            return _canExecuteFunc.Invoke();
        }

        if (_canExecuteAsyncFunc is not null)
        {
            await _canExecuteAsyncFunc.Invoke();
        }

        return true;
    }

    public async Task ExecuteAsync()
    {
        if (await CanExecuteAsync() is false)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            return;
        }

        if (_action is not null)
        {
            _action.Invoke();
        }
        else if (_actionAsync is not null)
        {
            await _actionAsync.Invoke();
        }
    }
}

public class RelayCommand<T> : ICommand<T>
{
    private readonly Action<T>? _execute;
    private readonly Func<T, Task>? _executeAsync;

    private readonly Func<bool>? _canExecuteFunc;
    private readonly Func<Task<bool>>? _canExecuteAsyncFunc;

    public RelayCommand(Action<T> execute)
    {
        _execute = execute;
    }

    public RelayCommand(Action<T> action, Func<bool> canExecute) : this(action)
    {
        _canExecuteFunc = canExecute;
    }

    public RelayCommand(Action<T> action, Func<Task<bool>> canExecuteAsync) : this(action)
    {
        _canExecuteAsyncFunc = canExecuteAsync;
    }

    public RelayCommand(Func<T, Task> func)
    {
        _executeAsync = func;
    }

    public RelayCommand(Func<T, Task> asyncAction, Func<bool> canExecute) : this(asyncAction)
    {
        _canExecuteFunc = canExecute;
    }

    public RelayCommand(Func<T, Task> asyncAction, Func<Task<bool>> canExecuteAsync) : this(asyncAction)
    {
        _canExecuteAsyncFunc = canExecuteAsync;
    }

    public event EventHandler? CanExecuteChanged;

    public async Task<bool> CanExecuteAsync()
    {
        if (_canExecuteFunc is not null)
        {
            return _canExecuteFunc.Invoke();
        }

        if (_canExecuteAsyncFunc is not null)
        {
            await _canExecuteAsyncFunc.Invoke();
        }

        return true;
    }

    public async Task ExecuteAsync(T parameter)
    {
        if (await CanExecuteAsync() is false)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            return;
        }

        if (_execute is not null)
        {
            _execute?.Invoke(parameter);
        }
        else if (_executeAsync is not null)
        {
            await _executeAsync.Invoke(parameter);
        }
    }
}
