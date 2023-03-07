using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ7b;
public class ErrorHandler : IObservable<ErrorInfo> {
    private List<IObserver<ErrorInfo>> _observers;
    private List<ErrorInfo> _errors;


    public ErrorHandler() {
        _observers = new();
        _errors = new();
    }

    public IDisposable Subscribe(IObserver<ErrorInfo> observer) {
        if (!_observers.Contains(observer)) {
            _observers.Add(observer);
            foreach (var item in _errors) {
                observer.OnNext(item);
            }
        }
        return new Unsubscriber<ErrorInfo>(_observers, observer);
    }

    public void ErrorStatus(int code) {
        ErrorStatus(code, string.Empty, string.Empty);
    }

    public void ErrorStatus(int code, string message, string location) {
        var info = new ErrorInfo(code, message, location);

        if (!_errors.Contains(info)) {
            _errors.Add(info);
            foreach (var observer in _observers)
                observer.OnNext(info);
        }
        else {

            var errorsToRemove = new List<ErrorInfo>();
            foreach (var error in _errors) {
                if (info.ErrorCode == error.ErrorCode) {
                    errorsToRemove.Add(error);
                    foreach (var observer in _observers)
                        observer.OnNext(info);
                }
            }
            foreach (var errorToRemove in errorsToRemove)
                _errors.Remove(errorToRemove);

            errorsToRemove.Clear();
        }
    }
}
