namespace PolishDraughts.Core.Interfaces
{
    public interface IView
    {
        void Clear();
        void DisplayMsg(string message);
        void DisplayBoard(IBoard board);
    }
}