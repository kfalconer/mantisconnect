using System;
using FalconerDevelopment.MantisConnect.Model.MantisConnectWebservice;

namespace FalconerDevelopment.MantisConnect.Model
{
    public class Relationship : IRelationship
    {
        private readonly RelationshipData data;

        internal RelationshipData Data { get { return data; } }

        public long Id
        {
            get { return Convert.ToInt64(data.id); }
        }

        public long TargetId
        {
            get { return Convert.ToInt64(data.target_id); }
            set { data.target_id = value.ToString(); }
        }

        public IMCAttribute Type
        {
            get { return new MCAttribute(Convert.ToInt64(data.type.id), data.type.name); }
            set { data.type = ((MCAttribute)value).Data; }
        }

        internal Relationship(RelationshipData relData)
        {
            data = relData;
        }
    }
}