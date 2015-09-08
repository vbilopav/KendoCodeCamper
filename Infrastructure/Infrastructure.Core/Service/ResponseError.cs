using System.Runtime.Serialization;

namespace Infrastructure.Service
{
    [DataContract]
    public class ResponseError
    {
        [DataMember] public object AttemptedValue { get; set; }
        [DataMember] public string ErrorMessage { get; set; }
        [DataMember] public string ErrorId { get; set; }
        [DataMember] public string PropertyName { get; set; }
    }
}
