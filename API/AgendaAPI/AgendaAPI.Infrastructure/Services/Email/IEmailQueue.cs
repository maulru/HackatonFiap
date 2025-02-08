public interface IEmailQueue
{
    void EnqueueEmail(EmailQueueItem item);
    Task<EmailQueueItem> DequeueAsync(CancellationToken cancellationToken);
}
