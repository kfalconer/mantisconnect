using System;

namespace FalconerDevelopment.MantisConnect.Model
{
    public interface IProjectVersion
    {
        DateTime? DateOrder { get; set; }

        String Description { get; set; }

        long Id { get; }

        String Name { get; set; }

        long ProjectId { get; set; }

        bool? Released { get; set; }
    }
}