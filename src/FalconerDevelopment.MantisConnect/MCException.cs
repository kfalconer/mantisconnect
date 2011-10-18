using System;

namespace FalconerDevelopment.MantisConnect
{
    /// <summary>
    /// 
    /// </summary>
    public class MCException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public MCException() : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public MCException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public MCException(string message, Exception ex) : base(message, ex)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public MCException(Exception ex) : this("Generic Exception", ex)
        {
        }
    }
}