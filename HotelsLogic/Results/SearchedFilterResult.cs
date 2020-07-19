using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HotelsLogic
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SearchedFilterResult 
    {
        [EnumMember(Value = "NotFound")]
        NotFound,// we didnt managed to filter output by this
        [EnumMember(Value = "NotSearched")]
        NotSearched,//we didnt look for this filter
        [EnumMember(Value = "Possible")]
        Possible,//it is likely that we satify filter but we dont have confidence. E.g. in booking we look for bar but we found restaurant. Another one, in booking we look for fridge but we found kitchen.
        [EnumMember(Value = "Satisfied")]
        Satisfied//we found and checked filter- results have satisfied filter
    }
}
