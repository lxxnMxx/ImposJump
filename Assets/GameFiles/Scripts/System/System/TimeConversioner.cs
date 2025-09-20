using System;

public static class TimeConversioner 
{
    public static (int, double) GetConvertedTime(float time)
    {
        return ((int)Math.Round(time / 120, 0), Math.Round(time % 60, 2));
    }
}