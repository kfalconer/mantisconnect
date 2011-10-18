using System;
using System.Collections.Generic;
using FalconerDevelopment.MantisConnect.Model.MantisConnectWebservice;

namespace FalconerDevelopment.MantisConnect.Model
{
    public enum FieldType
    {
        Status = 0,
        Priority = 1,
        Severity = 2,
        Reproducibility = 3,
        Projection = 4,
        ETA = 5,
        Resolution = 6,
        AccessLevel = 7,
        ProjectStatus = 8,
        ProjectViewStates = 9,
        ViewStates = 10,
        CustomFieldTypes = 11
    }

    public enum ViewState
    {
        Private = 50,
        Public = 10
    }

    public class Issue : IIssue
    {
        private readonly IssueData data;

        internal IssueData Data
        {
            get { return data; }
        }

        public string AdditionalInformation
        {
            get { return data.additional_information; }
            set { data.additional_information = value; }
        }

        public IEnumerable<IIssueAttachment> Attachments
        {
            get
            {
                var attchments = new List<IIssueAttachment>(data.attachments.Length);
                foreach (var item in data.attachments)
                {
                    attchments.Add(new IssueAttachment(item));
                }
                return attchments;
            }
        }

        public string Build
        {
            get { return data.build; }
            set { data.build = value; }
        }

        public string Category
        {
            get { return data.category; }
            set { data.category = value; }
        }

        public IEnumerable<ICustomFieldValue> CustomFields
        {
            get
            {
                var fields = new List<ICustomFieldValue>(data.custom_fields.Length);
                foreach (var item in data.custom_fields)
                {
                    fields.Add(new CustomFieldValue(item));
                }
                return fields;
            }
        }

        public ICustomFieldValue this[string index]
        {
            get
            {
                var fields = (List<ICustomFieldValue>) CustomFields;
                return fields.Find(item => item.Field.Name == index);
            }
        }

        public ICustomFieldValue this[long index]
        {
            get
            {
                var fields = (List<ICustomFieldValue>) CustomFields;
                return fields.Find(item => item.Field.Id == index);
            }
        }

        public DateTime? DateSubmitted
        {
            get { return data.date_submittedSpecified ? (DateTime?) data.date_submitted : null; }
        }

        public string Description
        {
            get { return data.description; }
            set { data.description = value; }
        }

        public DateTime? DateDue
        {
            get { return data.due_dateSpecified ? (DateTime?) data.due_date : null; }
            set { data.due_date = value.HasValue ? value.Value : DateTime.MinValue; }
        }

        public IMCAttribute Eta
        {
            get { return new MCAttribute(Convert.ToInt64(data.eta.id), data.eta.name); }
            set { data.eta = ((MCAttribute)value).Data; }
        }

        public string FixedInVersion
        {
            get { return data.fixed_in_version; }
            set { data.fixed_in_version = value; }
        }

        public IAccount Handler
        {
            get{ return data.handler == null ? null : new Account(data.handler); }
            set { data.handler = ((Account)value).Data; }
        }

        public long Id
        {
            get { return Convert.ToInt64(data.id); }
        }

        public DateTime? DateLastUpdated
        {
            get { return data.last_updatedSpecified ? (DateTime?) data.last_updated : null; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<INote> Notes
        {
            get
            {
                var notes = new List<INote>();

                if (data.notes != null)
                {
                    foreach (var item in data.notes)
                    {
                        notes.Add(new Note(item));
                    }
                }
                return notes;
            }
            set
            {
                IEnumerable<INote> newNotes = value;
                IList<IssueNoteData> newNoteData = new List<IssueNoteData>();
                
                foreach (INote note in newNotes)
                {
                    newNoteData.Add(new IssueNoteData
                                        {
                                            id = note.Id.ToString(),
                                            date_submitted = GetNoteDateSubmitted(note),
                                            date_submittedSpecified = note.DateSubmitted.HasValue,
                                            reporter = ((Account) note.Reporter).Data,
                                            text = note.Text,
                                            view_state = ((Note) note).Data.view_state
                                        });
                }
                data.notes = ((List<IssueNoteData>) newNoteData).ToArray();
            }
        }

        private static DateTime GetNoteDateSubmitted(INote note)
        {
            if(note!= null && note.DateSubmitted.HasValue) {
                return note.DateSubmitted.Value;
            }
            return DateTime.MinValue;
        }

        public string OsBuild
        {
            get { return data.os_build; }
            set { data.os_build = value; }
        }

        public string Os
        {
            get { return data.os; }
            set { data.os = value; }
        }

        public string Platform
        {
            get { return data.platform; }
            set { data.platform = value; }
        }

        public IMCAttribute Priority
        {
            get { return new MCAttribute(Convert.ToInt64(data.priority.id), data.priority.name); }
            set { data.priority = ((MCAttribute)value).Data; }
        }

        public IMCAttribute Project
        {
            get { return new MCAttribute(Convert.ToInt64(data.project.id), data.project.name); }
            set { data.project = ((MCAttribute)value).Data; }
        }

        public IMCAttribute Projection
        {
            get { return new MCAttribute(Convert.ToInt64(data.projection.id), data.projection.name); }
            set { data.projection = ((MCAttribute)value).Data; }
        }

        public IEnumerable<IRelationship> Relationships
        {
            get
            {
                var attchments = new List<IRelationship>(data.relationships.Length);
                foreach (var item in data.relationships)
                {
                    attchments.Add(new Relationship(item));
                }
                return attchments;
            }
        }

        public IAccount Reporter
        {
            get { return new Account(data.reporter); }
            set { data.reporter = ((Account)value).Data; }
        }

        public IMCAttribute Reproducibility
        {
            get { return new MCAttribute(Convert.ToInt64(data.reproducibility.id), data.reproducibility.name); }
            set { data.reproducibility = ((MCAttribute)value).Data; }
        }

        public IMCAttribute Resolution
        {
            get { return new MCAttribute(Convert.ToInt64(data.resolution.id), data.resolution.name); }
            set { data.resolution = ((MCAttribute)value).Data; }
        }

        public IMCAttribute Severity
        {
            get { return new MCAttribute(Convert.ToInt64(data.severity.id), data.severity.name); }
            set { data.severity = ((MCAttribute)value).Data; }
        }

        public long SponsorshipTotal
        {
            get { return Convert.ToInt64(data.sponsorship_total); }
            set { data.sponsorship_total = value.ToString(); }
        }

        public IMCAttribute Status
        {
            get { return new MCAttribute(Convert.ToInt64(data.status.id), data.status.name); }
            set { data.status = ((MCAttribute)value).Data; }
        }

        public string StepsToReproduce
        {
            get { return data.steps_to_reproduce; }
            set { data.steps_to_reproduce = value; }
        }

        public string Summary
        {
            get { return data.summary; }
            set { data.summary = value; }
        }

        public string TargetVersion
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Version
        {
            get { return data.version; }
            set { data.version = value; }
        }

        public ViewState ViewState
        {
            get { return (ViewState) Enum.Parse(typeof (ViewState), data.view_state.id); }
            set { throw new System.NotImplementedException(); }
        }

        internal Issue(IssueData issueData)
        {
            data = issueData;
        }

        public Issue(long projectId)
        {
            data = new IssueData {id = projectId.ToString()};
        }
    }
}