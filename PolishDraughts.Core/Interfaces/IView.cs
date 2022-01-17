using System;
using System.Collections.Generic;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;

namespace PolishDraughts.Core.Interfaces
{
    public interface IView : IDependency
    {
        void Clear();
        void DisplayMsg(string message);
        void DisplayBoard(IBoard board);
        void DisplayCapturePaths(List<CapturePath> capturePaths);
        void DisplayChoiceMenu(IEnumerable<string> options);
        void DisplayMainMenu();
        void DisplayRules();
        void DisplayAbout();
        void DisplayPlayerChoice(Color color);
    }
}