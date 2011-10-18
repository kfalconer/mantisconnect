using System;
using FalconerDevelopment.MantisConnect.Model.MantisConnectWebservice;

namespace FalconerDevelopment.MantisConnect.Model
{
    public class MCAttribute : IMCAttribute
    {
        private readonly ObjectRef data;

        internal ObjectRef Data { get { return data; } }

        public long Id { get { return Convert.ToInt64(data.id); } }

        public string Name { get{ return data.name; } }

        
        public MCAttribute(long id, string name)
        {
            data = new ObjectRef {id = id.ToString(), name = name};
        }

        internal MCAttribute(ObjectRef objData)
        {
            data = objData;
        }
    }
}