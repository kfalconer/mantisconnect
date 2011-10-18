using System;

namespace FalconerDevelopment.MantisConnect.Model
{
    public interface IFilter
    {
        String FilterString { get; }

        long Id { get; }

        bool? IsPublic { get; }

        String Name { get; set; }

        IAccount Owner { get; }

        long ProjectId { get; }
    }
}