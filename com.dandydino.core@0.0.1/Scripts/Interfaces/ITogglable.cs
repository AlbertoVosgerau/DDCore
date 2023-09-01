namespace DandyDino.Core
{
    public interface ITogglable
    {
        public bool IsEnabled { get; }
        public void SetEnabled(bool isEnabled);
    }
}