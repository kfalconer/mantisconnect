using System;

namespace FalconerDevelopment.MantisConnect.Model
{
    public interface ICustomFieldValue
    {
        IMCAttribute Field { get; set; }

        String Value { get; set; }
    }
}