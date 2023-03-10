using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PZ7b;
public class ErrorMonitor : IObserver<ErrorInfo> {
    private List<ErrorInfo> _errorInfos = new List<ErrorInfo>();
    private IDisposable _cancellation;

    public event EventHandler<ErrorInfo> OnUpdateStatus;

    public ErrorMonitor() {
    }

    public virtual void Subscribe(ErrorHandler provider) {
        _cancellation = provider.Subscribe(this);
    }

    public virtual void Unsubscribe() {
        _cancellation.Dispose();
        _errorInfos.Clear();
    }

    public virtual void OnCompleted() {
        _errorInfos.Clear();
    }

    public virtual void OnError(Exception e) {
        // не используется, но из-за интерфейса обязан существовать
    }

    public virtual void OnNext(ErrorInfo info) {
        _errorInfos.Add(info);
        RaiseUpdateStatus(info);
    }

    private void RaiseUpdateStatus(ErrorInfo e) {
        OnUpdateStatus?.Invoke(this, e);
    }
}
