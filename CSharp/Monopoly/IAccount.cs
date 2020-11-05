namespace Monopoly
{
    public interface IAccount<T>
    {
        T Debit(T value);
        T Credit(T value);
    }
}