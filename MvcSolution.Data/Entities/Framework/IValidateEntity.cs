using MvcSolution.Data;

namespace MvcSolution.Data
{
    public interface IValidateEntity
    {
        void Validate();
    }
}

namespace MvcSolution
{
    public static class ValidateEntityExtensions
    {
        public static void NotNull<T>(this T entity, string value, string message) where T : IValidateEntity
        {
            entity.Valid(!string.IsNullOrWhiteSpace(value), message);
        }

        public static void Valid<T>(this T entity, bool valid, string message) where T : IValidateEntity
        {
            if (!valid)
            {
                throw new KnownException(message);
            }
        }
    }
}
