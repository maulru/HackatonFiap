namespace AgendaAPI.Infrastructure.Services.Email
{
    public interface IEmailQueuePublisher
    {
        void PublishEmail(EmailQueueItem emailItem);
    }

}
