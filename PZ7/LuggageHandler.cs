using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ7;
public class LuggageHandler : IObservable<LuggageInfo> {
    private List<IObserver<LuggageInfo>> _observers;
    private List<LuggageInfo> _flights;


    public LuggageHandler() {
        _observers = new();
        _flights = new();
    }

    public IDisposable Subscribe(IObserver<LuggageInfo> observer) {
        if (!_observers.Contains(observer)) {
            _observers.Add(observer);
            foreach (var item in _flights) {
                observer.OnNext(item);
            }
        }
        return new Unsubscriber<LuggageInfo>(_observers, observer);
    }

    public void LuggageStatus(int flight) {
        LuggageStatus(flight, string.Empty, 0);
    }

    public void LuggageStatus(int flight, string from, int carousel) {
        var info = new LuggageInfo(flight, from, carousel);

        if (carousel > 0 && !_flights.Contains(info)) {
            _flights.Add(info);
            foreach (var observer in _observers)
                observer.OnNext(info);
        }
        else if (carousel == 0) {

            var flightsToRemove = new List<LuggageInfo>();
            foreach (var luggage in _flights) {
                if (info.FlightNumber == luggage.FlightNumber) {
                    flightsToRemove.Add(luggage);
                    foreach (var observer in _observers)
                        observer.OnNext(info);
                }
            }
            foreach (var flightToRemove in flightsToRemove)
                _flights.Remove(flightToRemove);

            flightsToRemove.Clear();
        }
    }

    public void LastBaggageClaimed() {
        foreach (var observer in _observers) {
            observer.OnCompleted();
        }

        _observers.Clear();
    }
}
