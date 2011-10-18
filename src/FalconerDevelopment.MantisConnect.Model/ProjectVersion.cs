using System;
using FalconerDevelopment.MantisConnect.Model.MantisConnectWebservice;

namespace FalconerDevelopment.MantisConnect.Model
{
    public class ProjectVersion : IProjectVersion
    {
        private readonly ProjectVersionData data;

        internal ProjectVersionData Data { get { return data; } }

        public DateTime? DateOrder
        {
            get { return data.date_orderSpecified ? (DateTime?) data.date_order : null; }
            set
            {
                if (value.HasValue)
                {
                    data.date_order = value.Value;
                    data.date_orderSpecified = true;
                }
                else
                {                    
                    data.date_orderSpecified = false;
                }
            }
        }

        public string Description
        {
            get { return data.description; }
            set { data.description = value; }
        }

        public long Id
        {
            get { return Convert.ToInt64(data.id); }
        }

        public string Name
        {
            get { return data.name; }
            set { data.name = value; }
        }

        public long ProjectId
        {
            get { return Convert.ToInt64(data.project_id); }
            set { data.project_id = value.ToString(); }
        }

        public bool? Released
        {
            get { return data.releasedSpecified ? data.released as bool? : null; }
            set { data.released = value.Value; }
        }

        internal ProjectVersion(ProjectVersionData pvd)
        {
            data = pvd;
        }
    }
}