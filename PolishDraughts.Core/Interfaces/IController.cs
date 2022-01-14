using System.Collections.Generic;
using PolishDraughts.Core.Entities.Positions;

namespace PolishDraughts.Core.Interfaces
{
    public interface IController : IDependency
    {
        IView View { get; }
        Position GetPosition(int kind);
        CapturePath GetPath(List<CapturePath> capturePaths);
    }
}