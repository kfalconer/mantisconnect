using System;
using System.Collections.Generic;

namespace FalconerDevelopment.MantisConnect.Model
{
    public interface IIssue
    {
        String AdditionalInformation { get; set; }

        IEnumerable<IIssueAttachment> Attachments { get; }

        String Build { get; set; }

        String Category { get; set; }

        IEnumerable<ICustomFieldValue> CustomFields { get; }

        ICustomFieldValue this[string index] { get; }

        ICustomFieldValue this[long index] { get; }

        DateTime? DateSubmitted { get; }

        String Description { get; set; }

        DateTime? DateDue { get; set; }

        IMCAttribute Eta { get; set; }

        String FixedInVersion { get; set; }

        IAccount Handler { get; set; }

        long Id { get; }

        DateTime? DateLastUpdated { get; }

        IEnumerable<INote> Notes { get; set; }

        String OsBuild { get; set; }

        String Os { get; set; }

        String Platform { get; set; }

        IMCAttribute Priority { get; set; }

        IMCAttribute Project { get; set; }

        IMCAttribute Projection { get; set; }

        IEnumerable<IRelationship> Relationships { get; }

        IAccount Reporter { get; set; }

        IMCAttribute Reproducibility { get; set; }

        IMCAttribute Resolution { get; set; }

        IMCAttribute Severity { get; set; }

        long SponsorshipTotal { get; set; }

        IMCAttribute Status { get; set; }

        String StepsToReproduce { get; set; }

        String Summary { get; set; }

        String TargetVersion { get; set; }

        String Version { get; set; }

        ViewState ViewState { get; set; }
    }
}