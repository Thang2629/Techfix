using System.Collections.Generic;

namespace TechFix.Common.Constants;

public class InvoiceStatus
{
    public const string New = "new";
    public const string Expired = "expired";
    public const string Completed = "completed";
    public const string CompletedSynced = "completed_synced";
    public static readonly List<string> FinishedStatus = new() {Completed, CompletedSynced};
}