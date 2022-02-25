using System;
using System.Net;

public class RequestContext
{
	public Guid CorrelationId { get; }
	public IPAddress ipAddress { get; }
	public int UserId { get; }
}
