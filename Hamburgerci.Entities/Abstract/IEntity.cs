namespace Hamburgerci.Entities.Abstract
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
