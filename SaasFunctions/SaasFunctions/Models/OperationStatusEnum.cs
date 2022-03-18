using System.Text.Json.Serialization;

namespace SaasFunctions.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OperationStatusEnum
{
    /// <summary>
    /// The not started
    /// </summary>
    NotStarted,

    /// <summary>
    /// The in progress
    /// </summary>
    InProgress,

    /// <summary>
    /// The failed
    /// </summary>
    Failed,

    /// <summary>
    /// The succeeded
    /// </summary>
    Succeeded,

    /// <summary>
    /// The conflict
    /// </summary>
    Conflict
}