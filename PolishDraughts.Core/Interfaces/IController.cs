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
        CapturePath GetPath(List<CapturePath> capturePaths);
        int GetOption(int optionsNumber, Action messageView, bool clearScreen = false);
        void GetExitKey();
        void Quit();
    }
}