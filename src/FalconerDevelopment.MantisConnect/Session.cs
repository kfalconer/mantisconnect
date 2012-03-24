#region Copyright © 2004 Victor Boctor
//
// MantisConnect is copyrighted to Victor Boctor
//
// This program is distributed under the terms and conditions of the GPL
// See LICENSE file for details.
//
// For commercial applications to link with or modify MantisConnect, they require the
// purchase of a MantisConnect commerical license.
//
#endregion

using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using FalconerDevelopment.MantisConnect.Model;
using FalconerDevelopment.MantisConnect.Util;
using FalconerDevelopment.MantisConnect.Model.MantisConnectWebservice;
using System.Linq;

namespace FalconerDevelopment.MantisConnect
{
    /// <summary>
    /// Represents a connection session between the webservice client and server.
    /// </summary>
    public class Session : ISession
    {
        /// <summary>
        /// User name specified on construction of the session.
        /// </summary>
        public string Username
        {
            get;
            private set;
        }

        /// <summary>
        /// User password specified on construction of the session.
        /// </summary>
        public string Password
        {
            get;
            private set;
        }

        /// <summary>
        /// MantisConnect webservice URL
        /// </summary>
        /// <remarks>
        /// eg: http://www.example.com/mantis/api/soap/mantisconnect.php
        /// </remarks>
        public string Url
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructs a session given a url, username and password.
        /// </summary>
        /// <param name="url">URL of MantisConnect webservice (eg: http://www.example.com/mantis/api/soap/mantisconnect.php</param>
        /// <param name="username">User name to connect as.</param>
        /// <param name="password">Password for the specified user.</param>
        public Session( string url, string username, string password)
        {
            Username = username;
            Password = password;
            Url = url;
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        ///<summary>
        /// Create a new Issue with default values set.
        ///</summary>
        ///<exception cref="MCException"></exception>
        public IIssue NewIssue(long projectId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return new Issue(projectId)
                           {
                               Severity = GetDefaultIssueSeverity(),
                               Priority = GetDefaultIssuePriority(),
                               ViewState = GetDefaultIssueViewState()
                           };
            }
            catch (Exception ex)
            {
                throw new MCException("Could not create new issue.", ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Create a new Note with default values set.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public INote NewNote(string text)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return new Note(text) {ViewState = GetDefaultIssueViewState()};
            }
            catch (Exception ex)
            {
                throw new MCException("Could not create new note.", ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get Version of MantisConnect this session is connected to.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public string GetVersion()
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return mc.mc_version();
            }
            catch (Exception ex)
            {
                throw new MCException("Could not get version", ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the specified enumeration.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IMCAttribute> GetEnum(FieldType enumeration)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                ObjectRef[] data = null;
                switch( enumeration)
                {
                    case FieldType.AccessLevel:                      
                        data = mc.mc_enum_access_levels(Username, Password);                        
                        break;
                    case FieldType.CustomFieldTypes:
                        data = mc.mc_enum_custom_field_types(Username, Password);
                        break;
                    case FieldType.ETA:
                        data = mc.mc_enum_etas(Username, Password);
                        break;
                    case FieldType.Priority:
                        data = mc.mc_enum_priorities(Username, Password);
                        break;
                    case FieldType.Projection:
                        data = mc.mc_enum_projections(Username, Password);
                        break;
                    case FieldType.ProjectStatus:
                        data = mc.mc_enum_project_status(Username, Password);
                        break;
                    case FieldType.ProjectViewStates:
                        data = mc.mc_enum_project_view_states(Username, Password);
                        break;
                    case FieldType.Reproducibility:
                        data = mc.mc_enum_reproducibilities(Username, Password);
                        break;
                    case FieldType.Resolution:
                        data = mc.mc_enum_resolutions(Username, Password);
                        break;
                    case FieldType.Severity:
                        data = mc.mc_enum_severities(Username, Password);
                        break;
                    case FieldType.Status:
                        data = mc.mc_enum_status(Username, Password);
                        break;
                    case FieldType.ViewStates:
                        data = mc.mc_enum_view_states(Username, Password);
                        break;
                }
                IList<IMCAttribute> result = new List<IMCAttribute>();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        result.Add(new MCAttribute(Convert.ToInt64(item.id), item.name));
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Error getting enumeration for fieldtype {0}", enumeration), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Check there exists an issue with the specified id.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public bool IssueExists(long issueId)
        {
            var mc = new MantisConnectPortTypeClient("MantisConnectPort", Url);
            try
            {
                return mc.mc_issue_exists(Username, Password, issueId.ToString());
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Error checking issue {0}", issueId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the issue with the specified id.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IIssue GetIssue(long issueId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return new Issue(mc.mc_issue_get(Username, Password, issueId.ToString()));
            }
            catch(Exception ex)
            {
                throw new MCException(String.Format("Error getting Issue {0}", issueId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the latest submitted issue in the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public long GetBiggestIssueId(long projectId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return Convert.ToInt64(mc.mc_issue_get_biggest_id(Username, Password, projectId.ToString()));
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get BiggestIssueId for Project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the id of the issue with the specified summary.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public long GetIdFromSummary(string summary)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return Convert.ToInt64(mc.mc_issue_get_id_from_summary(Username, Password, summary));
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get Id from summary. \r\n {0}", summary), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Submit the specified issue.
        ///</summary>
        /// <remarks>If the mantis issue being updated does not have a category and MantisConnect is configured with 
        /// $g_mc_error_when_category_not_found = ON, an exception will be thrown by the server.</remarks>
        /// <exception cref="MCException"></exception>
        public long AddIssue(IIssue issue)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return Convert.ToInt64(mc.mc_issue_add(Username, Password, ((Issue)issue).Data));
            }
            catch (Exception ex)
            {
                throw new MCException("Could not add issue", ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Submit a new note.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public long AddNote(long issueId, INote note)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return Convert.ToInt64(mc.mc_issue_note_add(Username, Password, issueId.ToString(), ((Note)note).Data));
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not add Note to issue {0}", issueId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Delete the issue with the specified id.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public bool DeleteIssue(long issueId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return mc.mc_issue_delete(Username, Password, issueId.ToString());
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not delete issue {0}", issueId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Add a new project to the tracker (must have admin privileges).
        ///</summary>
        /// <exception cref="MCException"></exception>
        public long AddProject(IProject project)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return
                    Convert.ToInt64(mc.mc_project_add(Username, Password, ((Project)project).Data));
            }
            catch (Exception ex)
            {
                if (project != null)
                {
                    throw new MCException(String.Format("Could not add project {0}", project.Name), ex);
                }
                throw new MCException("Could not add project", ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Delete the project with the specified id (must have admin privileges).
        ///</summary>
        /// <exception cref="MCException"></exception>
        public bool DeleteProject(long projectId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return mc.mc_project_delete(Username, Password, projectId.ToString());
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not delete project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the list of projects that are accessible to the logged in user.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IProject> GetAccessibleProjects()
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                ProjectData[] data = mc.mc_projects_get_user_accessible(Username, Password);
                IList<IProject> result = new List<IProject>();
                foreach (var item in data)
                {
                    result.Add(new Project(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException("Could not get accessible projects", ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the categories belonging to the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<string> GetCategories(long projectId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return new List<string>(mc.mc_project_get_categories(Username, Password, projectId.ToString()));
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get categories for project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the versions belonging to the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IProjectVersion> GetVersions(long projectId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                ProjectVersionData[] data = mc.mc_project_get_versions(Username, Password, projectId.ToString());
                IList<IProjectVersion> result = new List<IProjectVersion>();
                foreach(var item in data)
                {
                    result.Add(new ProjectVersion(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get versions for project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the released versions that belong to the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IEnumerable<IProjectVersion> GetReleasedVersions(long projectId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                ProjectVersionData[] data = mc.mc_project_get_released_versions(Username, Password, projectId.ToString());
                IList<IProjectVersion> result = new List<IProjectVersion>();
                foreach (var item in data)
                {
                    result.Add(new ProjectVersion(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get released versions for project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the unreleased version that belong to the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IProjectVersion> GetUnreleasedVersions(long projectId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                ProjectVersionData[] data = mc.mc_project_get_unreleased_versions(Username, Password, projectId.ToString());
                IList<IProjectVersion> result = new List<IProjectVersion>();
                foreach (var item in data)
                {
                    result.Add(new ProjectVersion(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get unreleased versions for project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the filters defined for the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IFilter> GetFilters(long projectId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                FilterData[] data = mc.mc_filter_get(Username, Password, projectId.ToString());
                IList<IFilter> result = new List<IFilter>();
                foreach (var item in data)
                {
                    result.Add(new Filter(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get filters for project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get all issues that match the specified filter.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IIssue> GetIssues(long projectId, long filterId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                IssueData[] data = mc.mc_filter_get_issues(Username, Password, projectId.ToString(), filterId.ToString(), null, null);
                IList<IIssue> result = new List<IIssue>();
                foreach (var item in data)
                {
                    result.Add(new Issue(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get issues for project {0} using filer {1}", projectId, filterId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get issues that match the specified filter. Constrain the number of
        /// issues to <code>limit</code>.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IIssue> GetIssues(long projectId, long filterId, int limit)
        {
            return GetIssues(projectId, filterId, 0, limit);
        }

        ///<summary>
        /// Get all issues that match the specified filter and paging details.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IIssue> GetIssues(long projectId, long filterId, int pageNumber, int perPage)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                IssueData[] data = mc.mc_filter_get_issues(Username, Password, projectId.ToString(), filterId.ToString(), pageNumber.ToString(), perPage.ToString());
                IList<IIssue> result = new List<IIssue>();
                foreach (var item in data)
                {
                    result.Add(new Issue(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get issues for project {0} using filter {1}", projectId, filterId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get all issue headers that match the specified filter.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IIssueHeader> GetIssueHeaders(long projectId, long filterId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                IssueHeaderData[] data = mc.mc_filter_get_issue_headers(Username, Password, projectId.ToString(), filterId.ToString(), null, null);
                IList<IIssueHeader> result = new List<IIssueHeader>();
                foreach (var item in data)
                {
                    result.Add(new IssueHeader(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get issue headers for project {0} using filter {1}", projectId, filterId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get issue headers that match the specified filter. Constrain the number
        /// of issues to <code>limit</code>.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IIssueHeader> GetIssueHeaders(long projectId, long filterId, int limit)
        {
            return GetIssueHeaders(projectId, filterId, 0, limit);
        }

        ///<summary>
        /// Get all issue headers that match the specified filter and paging details.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IIssueHeader> GetIssueHeaders(long projectId, long filterId, int pageNumber, int perPage)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                IssueHeaderData[] data = mc.mc_filter_get_issue_headers(Username, Password, projectId.ToString(), filterId.ToString(), pageNumber.ToString(), perPage.ToString());
                IList<IIssueHeader> result = new List<IIssueHeader>();
                foreach (var item in data)
                {
                    result.Add(new IssueHeader(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get issue headers for project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get all issues that match the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IIssue> GetProjectIssues(long projectId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                IssueData[] data = mc.mc_project_get_issues(Username, Password, projectId.ToString(), null, null);
                IList<IIssue> result = new List<IIssue>();
                foreach (var item in data)
                {
                    result.Add(new Issue(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get issues for project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the issues that match the specified project id and paging details
        /// Constrain the number of issues to <code>limit</code>.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IIssue> GetProjectIssues(long projectId, int limit)
        {
            return GetProjectIssues(projectId, 0, limit);
        }

        ///<summary>
        /// Get the issues that match the specified project id and paging details
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IIssue> GetProjectIssues(long projectId, int pageNumber, int perPage)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                IssueData[] data = mc.mc_project_get_issues(Username, Password, projectId.ToString(), pageNumber.ToString(), perPage.ToString());
                IList<IIssue> result = new List<IIssue>();
                foreach (var item in data)
                {
                    result.Add(new Issue(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get issues for project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get all issue headers that match the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IIssueHeader> GetProjectIssueHeaders(long projectId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                IssueHeaderData[] data = mc.mc_project_get_issue_headers(Username, Password, projectId.ToString(), null, null);
                IList<IIssueHeader> result = new List<IIssueHeader>();
                foreach (var item in data)
                {
                    result.Add(new IssueHeader(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get issue headers for project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the issue headers that match the specified project id and paging
        /// details Constrain the number of issues to <code>limit</code>.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IIssueHeader> GetProjectIssueHeaders(long projectId, int limit)
        {
            return GetProjectIssueHeaders(projectId, 0, limit);
        }

        ///<summary>
        /// Get the issue headers that match the specified project id and paging
        /// details
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IIssueHeader> GetProjectIssueHeaders(long projectId, int pageNumber, int perPage)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                IssueHeaderData[] data = mc.mc_project_get_issue_headers(Username, Password, projectId.ToString(), pageNumber.ToString(), perPage.ToString());
                IList<IIssueHeader> result = new List<IIssueHeader>();
                foreach (var item in data)
                {
                    result.Add(new IssueHeader(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get issue headers for project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the attachments that belong to the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IProjectAttachment> GetProjectAttachments(long projectId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                ProjectAttachmentData[] data = mc.mc_project_get_attachments(Username, Password, projectId.ToString());
                IList<IProjectAttachment> result = new List<IProjectAttachment>();
                foreach (var item in data)
                {
                    result.Add(new ProjectAttachment(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get attachments for project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the data for the specified project attachment.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public byte[] GetProjectAttachment(long attachmentId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return mc.mc_project_attachment_get(Username, Password, attachmentId.ToString());
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get project attachment {0}", attachmentId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Submit a project attachment
        ///</summary>
        /// <exception cref="MCException"></exception>
        public long AddProjectAttachment(long projectId, string name, string fileType, string title, string description, byte[] content)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return
                    Convert.ToInt64(mc.mc_project_attachment_add(Username, Password, projectId.ToString(), name, title,
                                                                 description, fileType, content));
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not add project attachment {0} to project {1}", name, projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Delete the project attachment with the specified id.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public bool DeleteProjectAttachment(long attachmentId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return mc.mc_project_attachment_delete(Username, Password, attachmentId.ToString());
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not delete project attachment {0}", attachmentId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get appropriate users assigned to a project by access level.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<IAccount> GetProjectUsers(long projectId, AccessLevel access)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                AccountData[] data = mc.mc_project_get_users(Username, Password, projectId.ToString(), access.ToString());
                IList<IAccount> result = new List<IAccount>();
                foreach (var item in data)
                {
                    result.Add(new Account(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get project users for project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the value for the specified configuration variable.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public string GetConfigString(string configVar)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return mc.mc_config_get_string(Username, Password, configVar);
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get configuration {0}", configVar), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Delete the note with the specified id.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public bool DeleteNote(long noteId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return mc.mc_issue_note_delete(Username, Password, noteId.ToString());
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not delete note {0}", noteId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Update issue.
        ///</summary>
        /// <remarks>If the mantis issue being updated does not have a category and MantisConnect is configured with 
        /// $g_mc_error_when_category_not_found = ON, an exception will be thrown by the server.</remarks>
        /// <exception cref="MCException"></exception>
        public bool UpdateIssue(IIssue issue)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                // web service updates all notedata and not just the changes
                IIssue existingIssue = GetIssue(issue.Id);
                issue.Notes = issue.Notes.Except(existingIssue.Notes);
                
                return mc.mc_issue_update(Username, Password, issue.Id.ToString(), ((Issue)issue).Data);
            }
            catch (Exception ex)
            {                
                if (issue != null)
                {
                    throw new MCException(String.Format("Could not update issue {0}", issue.Id), ex);
                }
                throw new MCException("Could not update issue", ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the data for the specified issue attachment.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public byte[] GetIssueAttachment(long attachmentId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return mc.mc_issue_attachment_get(Username, Password, attachmentId.ToString());
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get issue attachment {0}", attachmentId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Submit an issue attachment
        ///</summary>
        /// <exception cref="MCException"></exception>
        public long AddIssueAttachment(long issueId, string name, string fileType, byte[] content)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return
                    Convert.ToInt64(mc.mc_issue_attachment_add(Username, Password, issueId.ToString(), name, fileType,
                                                               content));
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not add attachment {0} to issue {1}", name, issueId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Delete the issue attachment with the specified id.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public bool DeleteIssueAttachment(long attachmentId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return mc.mc_issue_attachment_delete(Username, Password, attachmentId.ToString());
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not delete issue attachment {0}", attachmentId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the custom fields that belong to the specified project.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IList<ICustomFieldDefinition> GetCustomFieldDefinitions(long projectId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                CustomFieldDefinitionData[] data = mc.mc_project_get_custom_fields(Username, Password, projectId.ToString());
                IList<ICustomFieldDefinition> result = new List<ICustomFieldDefinition>();
                foreach (var item in data)
                {
                    result.Add(new CustomFieldDefinition(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not get custom fields for project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Submit the specified version details.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public long AddVersion(IProjectVersion version)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return Convert.ToInt64(mc.mc_project_version_add(Username, Password, ((ProjectVersion)version).Data));
            }
            catch (Exception ex)
            {
                if (version != null)
                {
                    throw new MCException(String.Format("Could not add version {0}", version.Id), ex);
                }
                throw new MCException("Could not add version", ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Update version method.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public bool UpdateVersion(IProjectVersion version)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return mc.mc_project_version_update(Username, Password, version.Id.ToString(), ((ProjectVersion)version).Data);
            }
            catch (Exception ex)
            {
                if (version != null)
                {
                    throw new MCException(String.Format("Could not add version {0}", version.Id), ex);
                }
                throw new MCException("Could not add version", ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Delete the version with the specified id.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public bool DeleteVersion(long id)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return mc.mc_project_version_delete(Username, Password, id.ToString());
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not delete version {0}", id), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Get the default priority for new issues.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IMCAttribute GetDefaultIssuePriority()
        {
            return GetDefault(FieldType.Priority, "default_bug_priority");
        }

        private IMCAttribute GetDefault(FieldType field, string config)
        {            
            var data = (List<IMCAttribute>)GetEnum(field);
            long id = Convert.ToInt64(GetConfigString(config));
            return data.Find(item => item.Id == id);
        }

        private ViewState GetDefaultView(FieldType field, string config)
        {
            var data = (List<IMCAttribute>)GetEnum(field);
            long id = Convert.ToInt64(GetConfigString(config));
            IMCAttribute attrib = data.Find(item => item.Id == id);

            return (ViewState)Enum.Parse(typeof(ViewState), attrib.Id.ToString());
        }

        ///<summary>
        /// Get the default severity for new issues.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IMCAttribute GetDefaultIssueSeverity()
        {
            return GetDefault(FieldType.Severity, "default_bug_severity");
        }

        ///<summary>
        /// Get the default viewstate for new issues.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public ViewState GetDefaultIssueViewState()
        {
            return GetDefaultView(FieldType.ViewStates, "default_bug_view_status");
        }

        ///<summary>
        /// Get the default viewstate for new notes.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public ViewState GetDefaultNoteViewState()
        {
            return GetDefaultView(FieldType.ViewStates, "default_bugnote_view_status");
        }

        ///<summary>
        /// Submit a new realtionship.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public long AddRelationship(long issueId, IRelationship relationship)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return Convert.ToInt64(mc.mc_issue_relationship_add(Username, Password, issueId.ToString(),((Relationship)relationship).Data));
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not add relationship to issue {0}", issueId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Delete the relationship for the specified issue.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public bool DeleteRelationship(long issueId, long relationshipId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                return mc.mc_issue_relationship_delete(Username, Password, issueId.ToString(), relationshipId.ToString());
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not delete Relationship {0} with Issue {1}", relationshipId, issueId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Returns the project with id <code>projectId.</code> Subprojects are
        /// included (recursively) in the search.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IProject GetProject(long projectId)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                var projects = (List<IProject>)GetAccessibleProjects();
                return projects.Find(item => item.Id == projectId);
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not find project {0}", projectId), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        ///<summary>
        /// Returns the project with name <code>name.</code> Subprojects are
        /// included (recursively) in the search.
        ///</summary>
        /// <exception cref="MCException"></exception>
        public IProject GetProject(string name)
        {
            var mc = CreateMantisClientProxyInstance();
            try
            {
                var projects = (List<IProject>)GetAccessibleProjects();
                return projects.Find(item => item.Name == name);
            }
            catch (Exception ex)
            {
                throw new MCException(String.Format("Could not find project {0}", name), ex);
            }
            finally
            {
                mc.CloseSafely();
            }
        }

        private MantisConnectPortTypeClient CreateMantisClientProxyInstance()
        {
            Binding basicHttpBinding =
                new BasicHttpBinding(IsSecure()
                                         ? BasicHttpSecurityMode.TransportWithMessageCredential
                                         : BasicHttpSecurityMode.None);

            var client = new MantisConnectPortTypeClient(basicHttpBinding, new EndpointAddress(Url));

            if (IsSecure())
            {
                client.ClientCredentials.UserName.UserName = Username;
                client.ClientCredentials.UserName.Password = Password;
            }
            return client;
        }

        private bool IsSecure()
        {
            return !String.IsNullOrEmpty(Url) && Url.StartsWith("https://");
        }
    }
}