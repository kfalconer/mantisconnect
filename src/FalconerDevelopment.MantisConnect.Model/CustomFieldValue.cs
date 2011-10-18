using System;
using FalconerDevelopment.MantisConnect.Model.MantisConnectWebservice;

namespace FalconerDevelopment.MantisConnect.Model
{
    public class CustomFieldValue : ICustomFieldValue
    {
        private readonly CustomFieldValueForIssueData data;

        internal CustomFieldValue(CustomFieldValueForIssueData customFieldValueData)
        {
            data = customFieldValueData;
        }

        public IMCAttribute Field
        {
            get { return new MCAttribute(Convert.ToInt64(data.field.id), data.field.name); }
            set { data.field = ((MCAttribute)value).Data; }
        }

        public string Value
        {
            get { return data.value; }
            set { data.value = value; }
        }
    }
}