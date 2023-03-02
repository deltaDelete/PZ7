using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ7;
public class Monitor : IObserver<LuggageInfo> {
    private string _name;
    private List<string> _flightInfos = new List<string>();
    private IDisposable _cancellation;
    private string _format = "{0,-20}\t{1,5}\t{2, 3}";

    public event EventHandler<List<string>> OnUpdateStatus;

    public string Name { get => _name; }

    public Monitor(string name) {
        if (String.IsNullOrEmpty(name))
            throw new ArgumentNullException("У наблюдателя обязано быть имя");

        _name = name;
    }

    public virtual void Subscribe(LuggageHandler provider) {
        _cancellation = provider.Subscribe(this);
    }

    public virtual void Unsubscribe() {
        _cancellation.Dispose();
        _flightInfos.Clear();
    }

    public virtual void OnCompleted() {
        _flightInfos.Clear();
    }

    public virtual void OnError(Exception e) {
        // не используется, но из-за интерфейса обязан существовать
    }

    public virtual void OnNext(LuggageInfo info) {
        bool updated = false;

        if (info.Carousel == 0) {
            var flightsToRemove = new List<string>();
            string flightNo = String.Format("{0,5}", info.FlightNumber);

            foreach (var flightInfo in _flightInfos) {
                if (flightInfo.Substring(21, 5).Equals(flightNo)) {
                    flightsToRemove.Add(flightInfo);
                    updated = true;
                }
            }
            foreach (var flightToRemove in flightsToRemove)
                _flightInfos.Remove(flightToRemove);

            flightsToRemove.Clear();
        }
        else {
            string flightInfo = String.Format(_format, info.From, info.FlightNumber, info.Carousel);
            if (!_flightInfos.Contains(flightInfo)) {
                _flightInfos.Add(flightInfo);
                updated = true;
            }
        }
        if (updated) {
            _flightInfos.Sort();
            //Console.WriteLine("Информация о прибытии из {0}", this._name);
            //foreach (var flightInfo in _flightInfos)
            //    Console.WriteLine(flightInfo);

            //Console.WriteLine();
            RaiseUpdateStatus(_flightInfos);
        }
    }

    private void RaiseUpdateStatus(List<string> e) {
        OnUpdateStatus?.Invoke(this, e);
    }
}
