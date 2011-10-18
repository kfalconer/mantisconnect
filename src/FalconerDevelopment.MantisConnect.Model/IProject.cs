using System;
using System.Collections.Generic;

namespace FalconerDevelopment.MantisConnect.Model
{
    public interface IProject
    {
        AccessLevel AccessLevelMin { get; }

        String Description { get; set; }

        bool? Enabled { get; set; }

        String FilePath { get; set; }

        long Id { get; }

        String Name { get; set; }

        IMCAttribute Status { get; }

        IEnumerable<IProject> SubProjects { get; set; }

        IMCAttribute ViewState { get; }

        IProject GetSubProject(long projectId);

        IProject GetSubProject(String name);
    }
}