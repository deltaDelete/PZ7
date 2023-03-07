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

    public event EventHandler<List<ErrorInfo>> OnUpdateStatus;

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
        _errorInfos.Sort();
        RaiseUpdateStatus(_errorInfos);
    }

    private void RaiseUpdateStatus(List<ErrorInfo> e) {
        OnUpdateStatus?.Invoke(this, e);
    }
}
