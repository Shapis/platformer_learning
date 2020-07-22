using System;

public interface IScoreKeeperEvents
{
    void OnScoreUpdate(object sender, int totalScore);
}