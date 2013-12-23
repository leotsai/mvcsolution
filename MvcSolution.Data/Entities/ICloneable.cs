namespace MvcSolution.Data.Entities
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}
