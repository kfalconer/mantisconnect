
namespace FalconerDevelopment.MantisConnect.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomFieldType
    {
        public string Name { get; set; }

        public CustomFieldType(string typeName)
        {
            Name = typeName;
        }
    }
}