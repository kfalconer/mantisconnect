using System;
using FalconerDevelopment.MantisConnect.Model.MantisConnectWebservice;

namespace FalconerDevelopment.MantisConnect.Model
{
    class ProjectAttachment : IProjectAttachment
    {
        private readonly ProjectAttachmentData data;

        internal ProjectAttachmentData Data { get { return data; } }

        public string ContentType
        {
            get { return data.content_type; }
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

        public Uri DownloadUri
        {
            get { return new Uri(data.download_url); }
        }

        public string Filename
        {
            get { return data.filename; }
        }

        public long Id
        {
            get { return Convert.ToInt64(data.id); }
        }

        public long Size
        {
            get { return Convert.ToInt64(data.size); }
        }

        public string Title
        {
            get { return data.title; }
            set { data.title = value; }
        }

        internal ProjectAttachment(ProjectAttachmentData pad)
        {
            data = pad;
        }
    }
}