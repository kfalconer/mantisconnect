using System;
using FalconerDevelopment.MantisConnect.Model.MantisConnectWebservice;

namespace FalconerDevelopment.MantisConnect.Model
{
    public class IssueHeader : IIssueHeader
    {
        private readonly IssueHeaderData data;

        public long AttachmentsCount
        {
            get { return Convert.ToInt64(data.attachments_count); }
        }

        public string Category
        {
            get { return data.category; }
        }

        public long Handler
        {
            get { return Convert.ToInt64(data.handler); }
        }

        public long Id
        {
            get { return Convert.ToInt64(data.id); }
        }

        public DateTime DateLastUpdated
        {
            get { return data.last_updated; }
        }

        public long NotesCount
        {
            get { return Convert.ToInt64(data.notes_count); }
        }

        public long Priority
        {
            get { return Convert.ToInt64(data.priority); }
        }

        public long Project
        {
            get { return Convert.ToInt64(data.project); }
        }

        public string Reporter
        {
            get { return data.reporter; }
        }

        public long Resolution
        {
            get { return Convert.ToInt64(data.resolution); }
        }

        public long Severity
        {
            get { return Convert.ToInt64(data.severity); }
        }

        public long Status
        {
            get { return Convert.ToInt64(data.status); }
        }

        public string Summary
        {
            get { return data.summary; }
        }

        public bool Private
        {
            get { throw new System.NotImplementedException(); }
        }

        internal IssueHeader(IssueHeaderData ihd)
        {
            data = ihd;
        }
    }
}