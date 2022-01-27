using System;
using System.Collections.Generic;
using PolishDraughts.Core.Entities.Positions;

namespace PolishDraughts.Core.Interfaces
{
    public interface IController : IDependency
    {
        public event Action QuitCommand;
        IView View { get; }
        Position GetPosition(int kind);
        Move GetPath(List<Move> capturePaths);
        int GetOption(int optionsNumber, Action messageView, bool clearScreen = false);
        void GetExitKey();
        void Quit();
    }
}