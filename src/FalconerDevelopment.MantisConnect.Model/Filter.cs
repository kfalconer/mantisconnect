using System;
using FalconerDevelopment.MantisConnect.Model.MantisConnectWebservice;

namespace FalconerDevelopment.MantisConnect.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Filter : IFilter
    {
        private readonly FilterData data;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterData"></param>
        internal Filter( FilterData filterData )
        {
            data = filterData;
        }

        public string FilterString
        {
            get { return data.filter_string; }
        }

        public long Id
        {
            get { return Convert.ToInt64(data.id); }
        }

        public bool? IsPublic
        {
            get { return data.is_publicSpecified ? (bool?)data.is_public : null; }
        }

        public string Name
        {
            get { return data.name; }
            set { data.name = value; }
        }

        public IAccount Owner
        {
            get { return new Account(data.owner); }
        }

        public long ProjectId
        {
            get { return Convert.ToInt64(data.project_id); }
        }
    }
}