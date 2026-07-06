namespace EmailTriageBot.Interfaces
{
    public interface IDataProcessor
    {
        void Analyze(Models.EmailDataModel email);
    }
}
