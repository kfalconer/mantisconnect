using System;
using System.ServiceModel;

namespace FalconerDevelopment.MantisConnect.Util
{
    ///<summary>
    ///</summary>
    public static class WcfExtensions
    {
        ///<summary>
        ///</summary>
        ///<param name="obj"></param>
        public static void CloseSafely(this ICommunicationObject obj)
        {
            if (obj != null)
            {
                if (obj.State != CommunicationState.Faulted &&
                    obj.State != CommunicationState.Closed)
                {
                    try { obj.Close(); }
                    catch (CommunicationObjectFaultedException)
                    { obj.Abort(); }
                    catch (TimeoutException)
                    { obj.Abort(); }
                    catch (Exception)
                    {
                        obj.Abort();
                        throw;
                    }
                }
                else
                    obj.Abort();
            }
        }
    }
}