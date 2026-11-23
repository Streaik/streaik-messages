using System.Text.Json;
using Xunit;

namespace Streaik.Messages.ContractTests;

public class NativeMetadataTests
{
    [Theory]
    [InlineData("null")]
    [InlineData("{}")]
    [InlineData("[]")]
    [InlineData("false")]
    [InlineData("9007199254740993")]
    [InlineData("{\"nested\":[1,true,null]}")]
    public void MetadataAndRootExtensionsRemainSeparate(string value)
    {
        var json = JsonSerializer.Deserialize<JsonElement>("{\"model\":\"test\",\"messages\":[],\"max_tokens\":10,\"metadata\":{\"user_id\":\"user\"},\"future\":" + value + "}");
        var request = json.Deserialize<MessagesRequest>(MessagesJson.Default)!;
        Assert.Equal("user", request.Metadata!.UserId);
        Assert.True(JsonElement.DeepEquals(json.GetProperty("future"), request.AdditionalProperties!["future"]));
        var serialized = JsonSerializer.SerializeToElement(request, MessagesJson.Default);
        Assert.True(JsonElement.DeepEquals(json.GetProperty("future"), serialized.GetProperty("future")));
        Assert.True(JsonElement.DeepEquals(json.GetProperty("metadata"), serialized.GetProperty("metadata")));
        Assert.False(serialized.TryGetProperty("anthropic", out _));
    }
}
