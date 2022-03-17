namespace SaasFunctions.Models;

using System;

public class LogTableModel
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime LoggedDate { get; set; } = DateTime.Now;
}