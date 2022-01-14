using System.Collections.Generic;
using PolishDraughts.Core.Entities.Positions;

namespace PolishDraughts.Core.Interfaces
{
    public interface IView : IDependency
    {
        void Clear();
        void DisplayMsg(string message);
        void DisplayBoard(IBoard board);
        void DisplayCapturePaths(List<CapturePath> capturePaths);
        void DisplayMainMenu();
        void DisplayRules();
        void DisplayAbout();
    }
}