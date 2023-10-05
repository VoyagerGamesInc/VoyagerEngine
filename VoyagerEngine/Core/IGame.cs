namespace VoyagerEngine.Core
{
    public interface IGame
    {
        void OnLoad();
        void OnUpdate(double deltaTime);
    }
}
