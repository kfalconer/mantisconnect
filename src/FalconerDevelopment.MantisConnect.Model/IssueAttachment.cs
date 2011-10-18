using System;
using FalconerDevelopment.MantisConnect.Model.MantisConnectWebservice;

namespace FalconerDevelopment.MantisConnect.Model
{
    public class IssueAttachment : IIssueAttachment
    {
        private readonly AttachmentData data;
        public string ContentType
        {
            get { return ContentType; }
        }

        public DateTime? DateSubmitted
        {
            get { return data.date_submittedSpecified ? (DateTime?) data.date_submitted : null; }
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

        internal IssueAttachment( AttachmentData attachData )
        {
            data = attachData;   
        }
    }
}