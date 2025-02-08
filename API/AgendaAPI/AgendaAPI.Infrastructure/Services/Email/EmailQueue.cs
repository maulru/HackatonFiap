using System.Threading.Channels;

public class EmailQueue : IEmailQueue
{
    private readonly Channel<EmailQueueItem> _queue;

    public EmailQueue(int capacity = 100)
    {
        var options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        _queue = Channel.CreateBounded<EmailQueueItem>(options);
    }

    public void EnqueueEmail(EmailQueueItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        bool enfileirou = _queue.Writer.TryWrite(item);

    }

    public async Task<EmailQueueItem> DequeueAsync(CancellationToken cancellationToken)
    {
        return await _queue.Reader.ReadAsync(cancellationToken);
    }

}
