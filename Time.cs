﻿using System;
using SFML.System;

namespace Aerohockey;

public static class Time
{
    public static float DeltaTime;

    private static Clock clock = new();

    public static void UpdateDeltaTime()
    {
        DeltaTime = clock.ElapsedTime.AsMilliseconds();
        clock.Restart();
    }
}
