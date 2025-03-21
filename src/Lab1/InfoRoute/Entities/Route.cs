﻿using Itmo.ObjectOrientedProgramming.Lab1.InfoTrain.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab1.InfoRoute.Entities;

public class Route
{
    private readonly List<IRouteSegment> _sections = new();

    private readonly double _speedLimit;

    public Route(double speedLimit)
    {
        if (speedLimit < 0) throw new ArgumentException("speedLimit cannot be negative", nameof(speedLimit));
        _speedLimit = speedLimit;
    }

    public void AddSection(IRouteSegment section)
    {
        _sections.Add(section);
    }

    public RouteResult Simulate(Train train)
    {
        double timeSum = 0;

        foreach (IRouteSegment section in _sections)
        {
            RouteResult result = section.Passing(train);
            if (result is RouteResult.Success succes) timeSum += succes.Time;
            else return result;
        }

        return train.Speed <= _speedLimit ?
            new RouteResult.Success(timeSum) : new RouteResult.RouteSpeedLimitFail();
    }
}