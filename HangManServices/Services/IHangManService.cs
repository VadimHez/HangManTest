namespace HangManServices
{
    public interface IHangManService
    {
        void SetSecret(string secret);
        HangManModel NextStep(char? entered = null);
        bool IsGameFinished();
    }
}
