using System;
using System.Runtime.Serialization;

namespace Infrastructure.Service
{
    [DataContract]
    public abstract class BaseDataModel
    {
        [DataMember] public string User { get; set; }
        [DataMember] public DateTime Modified { get; set; }
    }

    [DataContract]
    public abstract class IdentityDataModel
    {
        [DataMember] public string Id { get; set; }
    }

    [DataContract]
    public abstract class PagedRequest
    {
        [DataMember] public int Skip { get; set; }
        [DataMember] public int Take { get; set; }
    }   
}
