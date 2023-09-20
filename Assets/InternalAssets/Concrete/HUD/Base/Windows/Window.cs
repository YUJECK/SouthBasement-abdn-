namespace SouthBasement.HUD.Base
{
    public interface IWindow
    {
        bool CurrentlyOpened { get; }
        
        void Open();
        void Close();
        void UpdateWindow();
    }
}