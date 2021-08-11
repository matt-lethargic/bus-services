namespace BusServices.Scheduling.Domain.Projections
{
    public interface IProjection<out TId>
    {
        TId Id { get; }
    }
}