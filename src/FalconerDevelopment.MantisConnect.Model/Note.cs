using System;
using System.Collections.Generic;
using FalconerDevelopment.MantisConnect.Model.MantisConnectWebservice;

namespace FalconerDevelopment.MantisConnect.Model
{
    public class Note : INote, IEqualityComparer<INote>
    {
        private readonly IssueNoteData data;

        internal IssueNoteData Data { get { return data; } }

        public DateTime? DateSubmitted
        {
            get { return data.date_submittedSpecified ? (DateTime?)data.date_submitted : null; }
        }

        public long Id
        {
            get { return Convert.ToInt64(data.id); }
        }

        public DateTime? DateLastModified
        {
            get { return data.last_modifiedSpecified ? (DateTime?) data.last_modified : null; }
        }

        public IAccount Reporter
        {
            get { return new Account(data.reporter);}
            set { data.reporter = ((Account)value).Data; }
        }

        public string Text
        {
            get { return data.text; }
            set { data.text = value; }
        }

        public ViewState ViewState
        {
            get { return (ViewState) Enum.Parse(typeof (ViewState), data.view_state.id); }
            set { data.view_state.id = Convert.ToInt32((ViewState)value).ToString(); }
        }

        internal Note(IssueNoteData noteData)
        {
            data = noteData;
        }

        public Note(string text)
        {
            data = new IssueNoteData {text = text, view_state = new ObjectRef()};
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            Note compareNote = obj as Note;
            if(compareNote == null)
            {
                return false;
            }
            return Id == compareNote.Id && Text == compareNote.Text;
        }

        public bool Equals(INote other)
        {
            return Equals(this, other);
        }

        public bool Equals(INote x, INote y)
        {
            // Check whether the compared object is null.
            if (ReferenceEquals(x, null)) return false;

            // Check whether the compared object is null.
            if (ReferenceEquals(y, null)) return false;

            // Check whether the compared object references the same data.
            if (ReferenceEquals(x, y)) return true;

            // Check whether the notes' properties are equal.
            return x.Id == y.Id && x.Text == y.Text;
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        public int GetHashCode(INote obj)
        {
            // Get the hash code for the Name field if it is not null.
            int hashText = obj.Text == null ? 0 : obj.Text.GetHashCode();

            // Get the hash code for the id field.
            int hashId = obj.Id.GetHashCode();

            // Calculate the hash code
            return hashText ^ hashId;
        }
    }
}