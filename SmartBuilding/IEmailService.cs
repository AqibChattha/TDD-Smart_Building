using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    /// <summary>
    /// Interface for the email service. This interface is used to allow the Building Controller to interact with the email service class without having to know the implementation details of the email service class.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Method to send an email to the given email address.
        /// </summary>
        /// <param name="emailAddress">address to mailded</param>
        /// <param name="subject">mail subject</param>
        /// <param name="message">message or body of the mail</param>
        void SendMail(string emailAddress, string subject, string message);
    }
}
