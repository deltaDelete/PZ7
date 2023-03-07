using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ7b;
public class ErrorInfo {
    private int _errorCode;
    private string _message;
    private string _location;

    public ErrorInfo(int errorCode, string message, string location) {
        _errorCode = errorCode;
        _message = message;
        _location = location;
    }

    /// <summary>
    /// Код ошибки
    /// </summary>
    public int ErrorCode { get => _errorCode; }
    /// <summary>
    /// Сообщение
    /// </summary>
    public string Message { get => _message; }
    /// <summary>
    /// Место где произошла ошибка
    /// </summary>
    public string Location { get => _location; }
}
