namespace CSharpGame.EventHandlers
{
    using System;

    public delegate void GameActionEventHandler();

    public class GameActionEventArgs : EventArgs
    {
        //public bool IsJustStarted { get; set; }

        //public bool IsOngoing { get; set; }

        //public float RunningTime { get; set; }

        //public GameActionEventArgs(bool isJustStarted, bool isOngoing, float runningTime)
        //{
        //    this.IsJustStarted = isJustStarted;
        //    this.IsOngoing = isOngoing;
        //    this.RunningTime = runningTime;
        //}
    }
}
