using System;
using System.Text.Json.Serialization;

namespace SaasFunctions.Models;
/// <summary>
/// webhook payload is taken from SaaS Accelerator
/// located here: https://github.com/Azure/Commercial-Marketplace-SaaS-Accelerator
/// </summary>
public class WebhookPayload
{
    /// <summary>
    /// Gets or sets the action.
    /// </summary>
    /// <value>
    /// The action.
    /// </value>
    [JsonPropertyName("action")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public WebhookAction Action { get; set; }

    /// <summary>
    /// Gets or sets the activity identifier.
    /// </summary>
    /// <value>
    /// The activity identifier.
    /// </value>
    [JsonPropertyName("activityId")]
    public Guid ActivityId { get; set; }

    /// <summary>
    /// Gets or sets the offer identifier.
    /// </summary>
    /// <value>
    /// The offer identifier.
    /// </value>
    [JsonPropertyName("offerId")]
    public string OfferId { get; set; }

    /// <summary>
    /// Gets or sets the operation identifier.
    /// </summary>
    /// <value>
    /// Operation Id is presented as Id property on the JSON payload.
    /// </value>
    [JsonPropertyName("Id")]
    public Guid OperationId { get; set; }

    /// <summary>
    /// Gets or sets the plan identifier.
    /// </summary>
    /// <value>
    /// The plan identifier.
    /// </value>
    public string PlanId { get; set; }

    /// <summary>
    /// Gets or sets the publisher identifier.
    /// </summary>
    /// <value>
    /// The publisher identifier.
    /// </value>
    public string PublisherId { get; set; }

    /// <summary>
    /// Gets or sets the quantity.
    /// </summary>
    /// <value>
    /// The quantity.
    /// </value>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    [JsonPropertyName("status")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OperationStatusEnum Status { get; set; }

    /// <summary>
    /// Gets or sets the subscription identifier.
    /// </summary>
    /// <value>
    /// The subscription identifier.
    /// </value>
    [JsonPropertyName("subscriptionId")]
    public Guid SubscriptionId { get; set; }

    /// <summary>
    /// Gets or sets the time stamp.
    /// </summary>
    /// <value>
    /// The time stamp.
    /// </value>
    [JsonPropertyName("timeStamp")]
    public DateTimeOffset TimeStamp { get; set; }
}