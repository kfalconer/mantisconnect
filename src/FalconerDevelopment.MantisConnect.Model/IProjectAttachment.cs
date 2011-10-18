using System;

namespace FalconerDevelopment.MantisConnect.Model
{
    public interface IProjectAttachment : IAttachment
    {
        String Description { get; set; }

        String Title { get; set; }
    }
}