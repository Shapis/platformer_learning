using System;

public class TimeFormatHandler
{
    private TimeSpan timeSpan;

    public string FormatTime(float myTime)
    {
        timeSpan = TimeSpan.FromSeconds(myTime);

        char milliseconds = String.Format("{0:D3}", timeSpan.Milliseconds)[0];

        if (timeSpan.Hours != 0 && timeSpan.Hours <= 9)
        {
            return string.Format("{0:D1}h{1:D2}m", timeSpan.Hours, timeSpan.Minutes);
        }
        else if (timeSpan.Hours != 0)
        {
            return string.Format("{0:D2}h{1:D2}m", timeSpan.Hours, timeSpan.Minutes);
        }
        else if (timeSpan.Minutes != 0 && timeSpan.Minutes <= 9)
        {
            return string.Format("{0:D1}m{1:D2}.{2}s", timeSpan.Minutes, timeSpan.Seconds, milliseconds);
        }
        else if (timeSpan.Minutes != 0)
        {
            return string.Format("{0:D2}m{1:D2}.{2}s", timeSpan.Minutes, timeSpan.Seconds, milliseconds);
        }
        else
        {
            return string.Format("{0:D2}.{1}s", timeSpan.Seconds, milliseconds);
        }
    }

    private int takeNDigits(int number, int N)
    {
        // this is for handling negative numbers, we are only insterested in postitve number
        number = Math.Abs(number);
        // special case for 0 as Log of 0 would be infinity
        if (number == 0)
            return number;
        // getting number of digits on this input number
        int numberOfDigits = (int)Math.Floor(Math.Log10(number) + 1);
        // check if input number has more digits than the required get first N digits
        if (numberOfDigits >= N)
            return (int)Math.Truncate((number / Math.Pow(10, numberOfDigits - N)));
        else
            return number;
    }

}
