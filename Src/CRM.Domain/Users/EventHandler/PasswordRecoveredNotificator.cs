using CRM.Data;
using CRM.EventSourcing;
using CRM.Mail;
using CRM.Users.Events;

namespace CRM.Domain.Users.EventHandler
{
	public class PasswordRecoveredNotificator : IDomainEventHandler<UserPasswordRecovered>
	{
		private readonly IMailService _mailService;
		private readonly IUserRepository _userRepository;

		public PasswordRecoveredNotificator(IMailService mailService, IUserRepository userRepository)
		{
			_mailService = mailService;
			_userRepository = userRepository;
		}

		public void Handle(UserPasswordRecovered @event)
		{
			var user = _userRepository.GetById(@event.AggregateId);

			var body = string.Format("Your password has been recovered.\nPlease, use this new one: <strong>{0}</strong>", @event.Password);

			var mail = new MailContract
								 {
									 Sender = "noreply.password.recovery@jiulius.com",
									 Recipient = user.Email,
									 Subject = "Password recovery",
									 Body = body
								 };

			_mailService.SendMail(mail);
		}
	}
}
