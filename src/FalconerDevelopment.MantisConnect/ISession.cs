using System;
using System.Collections.Generic;
using FalconerDevelopment.MantisConnect.Model;

namespace FalconerDevelopment.MantisConnect
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Used to estabblish connection to remote system
        /// </summary>
        void Connect();

        ///<summary>
        /// Flush cached information
        ///</summary>
        void Flush();

        ///<summary>
        /// Create a new Issue with default values set.
        ///</summary>
        ///<exception cref="MCException"></exception>
        IIssue NewIssue(long projectId);

        ///<summary>
        /// Create a new Note with default values set.
        ///</summary>
        /// <exception cref="MCException"></exception>
        INote NewNote(String text);

        ///<summary>
        /// Get Version of MantisConnect this session is connected to.
        ///</summary>
        /// <exception cref="MCException"></exception>
        String GetVersion();

        ///<summary>
        /// Get the specified enumeration.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IMCAttribute> GetEnum(FieldType enumeration);

        ///<summary>
        /// Check there exists an issue with the specified id.
        ///</summary>
        /// <exception cref="MCException"></exception>
        bool IssueExists(long issueId);

        ///<summary>
        /// Get the issue with the specified id.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IIssue GetIssue(long issueId);

        ///<summary>
        /// Get the latest submitted issue in the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        long GetBiggestIssueId(long projectId);

        ///<summary>
        /// Get the id of the issue with the specified summary.
        ///</summary>
        /// <exception cref="MCException"></exception>
        long GetIdFromSummary(String summary);

        ///<summary>
        /// Submit the specified issue.
        ///</summary>
        /// <exception cref="MCException"></exception>
        long AddIssue(IIssue issue);

        ///<summary>
        /// Submit a new note.
        ///</summary>
        /// <exception cref="MCException"></exception>
        long AddNote(long issueId, INote note);

        ///<summary>
        /// Delete the issue with the specified id.
        ///</summary>
        /// <exception cref="MCException"></exception>
        bool DeleteIssue(long issueId);

        ///<summary>
        /// Add a new project to the tracker (must have admin privileges).
        ///</summary>
        /// <exception cref="MCException"></exception>
        long AddProject(IProject project);

        ///<summary>
        /// Delete the project with the specified id (must have admin privileges).
        ///</summary>
        /// <exception cref="MCException"></exception>
        bool DeleteProject(long projectId);

        ///<summary>
        /// Get the list of projects that are accessible to the logged in user.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IProject> GetAccessibleProjects();

        ///<summary>
        /// Get the categories belonging to the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<String> GetCategories(long projectId);

        ///<summary>
        /// Get the versions belonging to the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IProjectVersion> GetVersions(long projectId);

        ///<summary>
        /// Get the released versions that belong to the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IEnumerable<IProjectVersion> GetReleasedVersions(long projectId);

        ///<summary>
        /// Get the unreleased version that belong to the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IProjectVersion> GetUnreleasedVersions(long projectId);

        ///<summary>
        /// Get the filters defined for the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IFilter> GetFilters(long projectId);

        ///<summary>
        /// Get all issues that match the specified filter.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IIssue> GetIssues(long projectId, long filterId);

        ///<summary>
        /// Get issues that match the specified filter. Constrain the number of
        /// issues to <code>limit</code>.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IIssue> GetIssues(long projectId, long filterId, int limit);

        ///<summary>
        /// Get all issues that match the specified filter and paging details.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IIssue> GetIssues(long projectId, long filterId, int pageNumber, int perPage);

        ///<summary>
        /// Get all issue headers that match the specified filter.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IIssueHeader> GetIssueHeaders(long projectId, long filterId);

        ///<summary>
        /// Get issue headers that match the specified filter. Constrain the number
        /// of issues to <code>limit</code>.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IIssueHeader> GetIssueHeaders(long projectId, long filterId, int limit);

        ///<summary>
        /// Get all issue headers that match the specified filter and paging details.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IIssueHeader> GetIssueHeaders(long projectId, long filterId, int pageNumber, int perPage);

        ///<summary>
        /// Get all issues that match the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IIssue> GetProjectIssues(long projectId);

        ///<summary>
        /// Get the issues that match the specified project id and paging details
        /// Constrain the number of issues to <code>limit</code>.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IIssue> GetProjectIssues(long projectId, int limit);

        ///<summary>
        /// Get the issues that match the specified project id and paging details
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IIssue> GetProjectIssues(long projectId, int pageNumber, int perPage);

        ///<summary>
        /// Get all issue headers that match the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IIssueHeader> GetProjectIssueHeaders(long projectId);

        ///<summary>
        /// Get the issue headers that match the specified project id and paging
        /// details Constrain the number of issues to <code>limit</code>.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IIssueHeader> GetProjectIssueHeaders(long projectId, int limit);

        ///<summary>
        /// Get the issue headers that match the specified project id and paging
        /// details
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IIssueHeader> GetProjectIssueHeaders(long projectId, int pageNumber, int perPage);

        ///<summary>
        /// Get the attachments that belong to the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IProjectAttachment> GetProjectAttachments(long projectId);

        ///<summary>
        /// Get the data for the specified project attachment.
        ///</summary>
        /// <exception cref="MCException"></exception>
        byte[] GetProjectAttachment(long attachmentId);

        ///<summary>
        /// Submit a project attachment
        ///</summary>
        /// <exception cref="MCException"></exception>
        long AddProjectAttachment(long projectId, String name, String fileType, String title,
                                  String description, byte[] content);

        ///<summary>
        /// Delete the project attachment with the specified id.
        ///</summary>
        /// <exception cref="MCException"></exception>
        bool DeleteProjectAttachment(long attachmentId);

        ///<summary>
        /// Get appropriate users assigned to a project by access level.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<IAccount> GetProjectUsers(long projectId, AccessLevel access);

        ///<summary>
        /// Get the value for the specified configuration variable.
        ///</summary>
        /// <exception cref="MCException"></exception>
        String GetConfigString(String configVar);

        ///<summary>
        /// Delete the note with the specified id.
        ///</summary>
        /// <exception cref="MCException"></exception>
        bool DeleteNote(long noteId);

        ///<summary>
        /// Update issue.
        ///</summary>
        /// <exception cref="MCException"></exception>
        bool UpdateIssue(IIssue issue);

        ///<summary>
        /// Get the data for the specified issue attachment.
        ///</summary>
        /// <exception cref="MCException"></exception>
        byte[] GetIssueAttachment(long attachmentId);

        ///<summary>
        /// Submit an issue attachment
        ///</summary>
        /// <exception cref="MCException"></exception>
        long AddIssueAttachment(long issueId, String name, String fileType, byte[] content);

        ///<summary>
        /// Delete the issue attachment with the specified id.
        ///</summary>
        /// <exception cref="MCException"></exception>
        bool DeleteIssueAttachment(long attachmentId);

        ///<summary>
        /// Get the custom fields that belong to the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IList<ICustomFieldDefinition> GetCustomFieldDefinitions(long projectId);

        ///<summary>
        /// Submit the specified version details.
        ///</summary>
        /// <exception cref="MCException"></exception>
        long AddVersion(IProjectVersion version);

        ///<summary>
        /// Update version method.
        ///</summary>
        /// <exception cref="MCException"></exception>
        bool UpdateVersion(IProjectVersion version);

        ///<summary>
        /// Delete the version with the specified id.
        ///</summary>
        /// <exception cref="MCException"></exception>
        bool DeleteVersion(long id);

        ///<summary>
        /// Get the default priority for new issues.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IMCAttribute GetDefaultIssuePriority();

        ///<summary>
        /// Get the default severity for new issues.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IMCAttribute GetDefaultIssueSeverity();

        ///<summary>
        /// Get the default viewstate for new issues.
        ///</summary>
        /// <exception cref="MCException"></exception>
        ViewState GetDefaultIssueViewState();

        ///<summary>
        /// Get the default viewstate for new notes.
        ///</summary>
        /// <exception cref="MCException"></exception>
        ViewState GetDefaultNoteViewState();

        ///<summary>
        /// Submit a new realtionship.
        ///</summary>
        /// <exception cref="MCException"></exception>
        long AddRelationship(long issueId, IRelationship relationship);

        ///<summary>
        /// Delete the relationship for the specified issue.
        ///</summary>
        /// <exception cref="MCException"></exception>
        bool DeleteRelationship(long issueId, long relationshipId);

        ///<summary>
        /// Returns the project with id <code>projectId.</code> Subprojects are
        /// included (recursively) in the search.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IProject GetProject(long projectId);

        ///<summary>
        /// Returns the project with name <code>name.</code> Subprojects are
        /// included (recursively) in the search.
        ///</summary>
        /// <exception cref="MCException"></exception>
        IProject GetProject(String name);
    }
}