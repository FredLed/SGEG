using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SGEGService
{
    /// <summary>
    /// Public fonctionnality for every users.
    /// </summary>
    [ServiceContract(Name = "ISGEGPublicService")]
    public interface ISGEGPublicService
    {
        [OperationContract(Name = "GetPublicMessage")]
        string GetPublicMessage();
    }

    /// <summary>
    /// Private fonctionnality for local users.
    /// </summary>
    [ServiceContract(Name = "ISGEGPrivateService")]
    public interface ISGEGPrivateService
    {
        [OperationContract(Name = "GetPrivateMessage")]
        string GetPrivateMessage();
    }
}
