namespace Test.Core
{
    public interface IEnergy : IReadOnlyEnergy
    {
        void Increase(int count);
      
        void Decrease(int count);
    }
}