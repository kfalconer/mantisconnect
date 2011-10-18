using System;
using System.Collections.Generic;
using System.ComponentModel;
using FalconerDevelopment.MantisConnect.Model.MantisConnectWebservice;

namespace FalconerDevelopment.MantisConnect.Model
{
    [TypeConverter(typeof(Project))]
    public class Project : TypeConverter, IProject
    {
        private readonly ProjectData data;

        internal ProjectData Data { get { return data; } }

        public AccessLevel AccessLevelMin
        {
            get { return (AccessLevel)Enum.Parse(typeof(AccessLevel), data.access_min.id); }
        }

        public string Description
        {
            get { return data.description; }
            set { data.description = value; }
        }

        public bool? Enabled
        {
            get { return data.enabledSpecified ? (bool?)data.enabled : null; }
            set
            {
                if(value.HasValue)
                {
                    data.enabled = value.Value;
                    data.enabledSpecified = true;
                }
                else
                {
                    data.enabledSpecified = false;
                }
            }
        }

        public string FilePath
        {
            get { return data.file_path; }
            set { data.file_path = value; }
        }

        public long Id
        {
            get { return Convert.ToInt64(data.id);  }
        }

        public string Name
        {
            get { return data.name; }
            set { data.name = value; }
        }

        public IMCAttribute Status
        {
            get { return new MCAttribute(Convert.ToInt64(data.status.id), data.status.name); }
        }

        public IEnumerable<IProject> SubProjects
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public IMCAttribute ViewState
        {
            get { throw new NotImplementedException(); }
        }

        public IProject GetSubProject(long projectId)
        {
            throw new System.NotImplementedException();
        }

        public IProject GetSubProject(string name)
        {
            throw new System.NotImplementedException();
        }

        public Project(ProjectData projectData)
        {
            data = projectData;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(IProject) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return value.GetType() == typeof(ProjectData) ? new Project((ProjectData)value) : base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(IProject) || base.CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return destinationType == typeof(IProject) ? new Project((ProjectData)value) : base.ConvertTo(context, culture, value, destinationType);
        }
    }
}