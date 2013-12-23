namespace MvcSolution.Data.Dtos
{
    public class SimpleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SimpleEntity<T> : SimpleEntity
    {
        public T Value { get; set; }
    }
}
