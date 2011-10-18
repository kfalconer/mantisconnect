using System;

namespace FalconerDevelopment.MantisConnect.Model
{
    public interface INote
    {
        DateTime? DateSubmitted { get; }

        long Id { get; }

        DateTime? DateLastModified { get; }

        IAccount Reporter { get; set; }

        String Text { get; set; }

        ViewState ViewState { get; set; }
    }
}