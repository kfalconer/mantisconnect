using System;

namespace FalconerDevelopment.MantisConnect.Model
{
    public interface IAttachment
    {
        String ContentType { get; }

        DateTime? DateSubmitted { get; }

        Uri DownloadUri { get; }

        String Filename { get; }

        long Id { get; }

        long Size { get; }
    }
}