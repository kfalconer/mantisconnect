using System;
using FalconerDevelopment.MantisConnect.Model.MantisConnectWebservice;

namespace FalconerDevelopment.MantisConnect.Model
{
    class CustomFieldDefinition : ICustomFieldDefinition
    {
        private readonly CustomFieldDefinitionData data;

        public AccessLevel AccessLevelR
        {
            get { return (AccessLevel)Enum.Parse(typeof(AccessLevel), data.access_level_r); }
        }

        public AccessLevel AccessLevelRW
        {
            get { return (AccessLevel)Enum.Parse(typeof(AccessLevel), data.access_level_rw); }
        }

        public bool? Advanced
        {
            get { return data.advancedSpecified ? (bool?) data.advanced : null; }
        }

        public bool IsAdvancedSpecified
        {
            get { return data.advancedSpecified; }
        }

        public string DefaultValue
        {
            get { return data.default_value;  }
        }

        public bool? DisplayClose
        {
            get { return data.display_closedSpecified ? (bool?) data.display_closed : null; }
        }

        public bool? DisplayReport
        {
            get { return data.display_reportSpecified ? (bool?)data.display_report : null; }
        }

        public bool? DisplayResolve
        {
            get { return data.display_resolvedSpecified ? (bool?) data.display_resolved : null; }
        }

        public bool? DisplayUpdate
        {
            get { return data.display_updateSpecified ? (bool?) data.display_update : null; }
        }

        public IMCAttribute Field
        {
            get { return new MCAttribute(Convert.ToInt64(data.field.id), data.field.name); }
        }

        public long LengthMax
        {
            get { return Convert.ToInt64(data.length_max); }
        }

        public long LengthMin
        {
            get { return Convert.ToInt64(data.length_min); }
        }

        public string PossibleValues
        {
            get { return data.possible_values; }
        }

        public bool? RequireClosed
        {
            get { return data.require_closedSpecified ? (bool?)data.require_closed : null; }
        }

        public bool? RequireReport
        {
            get { return data.require_reportSpecified ? (bool?) data.require_report : null; }
        }

        public bool? RequireResolved
        {
            get { return data.require_resolvedSpecified ? (bool?) data.require_resolved : null; }
        }

        public bool? RequireUpdate
        {
            get { return data.require_updateSpecified ? (bool?) data.require_update : null; }
        }

        public CustomFieldType Type
        {
            get { return new CustomFieldType(data.type); }
        }

        public string ValidRegexp
        {
            get { return data.valid_regexp; }
        }

        internal CustomFieldDefinition(CustomFieldDefinitionData customFieldData)
        {
            data = customFieldData;
        }
    }
}