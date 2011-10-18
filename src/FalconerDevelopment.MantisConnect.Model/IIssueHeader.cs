using System;

namespace FalconerDevelopment.MantisConnect.Model
{
    public interface IIssueHeader
    {
        long AttachmentsCount { get; }

        String Category { get; }

        long Handler { get; }

        long Id { get; }

        DateTime DateLastUpdated { get; }

        long NotesCount { get; }

        long Priority { get; }

        long Project { get; }

        string Reporter { get; }

        long Resolution { get; }

        long Severity { get; }

        long Status { get; }

        String Summary { get; }

        bool Private { get; }
    }
}