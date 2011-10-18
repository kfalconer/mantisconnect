
namespace FalconerDevelopment.MantisConnect.Model
{
    public interface IRelationship
    {
        long Id { get; }

        long TargetId { get; set; }

        IMCAttribute Type { get; set; }
    }
}