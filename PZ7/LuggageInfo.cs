using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ7;
public class LuggageInfo {
    private int _flightNumber;
    private string _from;
    private int _carousel;

    public LuggageInfo(int flight, string from, int carousel) {
        _flightNumber = flight;
        _from = from;
        _carousel = carousel;
    }

    /// <summary>
    /// Номер рейса
    /// </summary>
    public int FlightNumber { get => _flightNumber; }
    /// <summary>
    /// Место отбытия
    /// </summary>
    public string From { get => _from; }
    /// <summary>
    /// Номер багажной карусели
    /// </summary>
    public int Carousel { get => _carousel; }
}
