namespace MvcSolution.Data.Models
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}
