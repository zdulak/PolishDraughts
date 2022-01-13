namespace PolishDraughts.Core.Interfaces
{
    public interface IView : IDependency
    {
        void Clear();
        void DisplayMsg(string message);
        void DisplayBoard(IBoard board);
    }
}