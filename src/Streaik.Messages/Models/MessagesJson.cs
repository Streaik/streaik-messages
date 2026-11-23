using System.Text.Json;
using System.Text.Json.Serialization;

namespace Streaik.Messages;

public static class MessagesJson
{
    public static readonly JsonSerializerOptions Default = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters =
        {
            new MessagesContentJsonConverter()
        }
    };
}
