
namespace HallRental.Services.Implementations
{
    using System;
    using System.Text;
    using HallRental.Services.Admin.Models.Events;

    public class EmailService : IEmailService
    {
        

        public string GetEmailTextBodyFromContactForm(string email, string name, string message)
        {
            var sb = new StringBuilder();
            sb.Append($"Email from: {name}");
            sb.Append(Environment.NewLine);
            sb.Append($"Email: {email}");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(message);

            return sb.ToString();
        }

        public string GetTextBodyForEmailForReservation(DateTime date,
                                                        string fullName,
                                                        string email,
                                                        string phoneNumber,
                                                        int numberOfPeople,
                                                        decimal totalPrice)
        {
            var sb = new StringBuilder();
            sb.Append($"Request for reservation on {date.ToShortDateString()}");
            sb.Append(Environment.NewLine);
            sb.Append($"Name: {fullName}");
            sb.Append(Environment.NewLine);
            sb.Append($"email: {email}");
            sb.Append(Environment.NewLine);
            sb.Append($"Phone Number: {phoneNumber}");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append($"Total Price: ${totalPrice.ToString("F")}");

            return sb.ToString();
        }

        public string GetTextBodyForTenant(DateTime date, string fullName, int numberOfPeople, decimal totalPrice)
        {
            var sb = new StringBuilder();
            sb.Append($"Your request for reservation has been received.");
            sb.Append(Environment.NewLine);
            sb.Append($"We will process your request and we will contat you.");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append($"The reservation is the name of: {fullName}");
            sb.Append(Environment.NewLine);
            sb.Append($"Event Date: {date.ToShortDateString()}");
            sb.Append(Environment.NewLine);
            sb.Append($"Total Price: ${totalPrice.ToString("F")}");

            return sb.ToString();
        }


        public string GetEmailConfirmationTextBody(EventDetailsAdminSM currentEvent)
        {
            var sb = new StringBuilder();
            sb.Append($"Dear, {currentEvent.FullName}");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("We would like to inform you that your reservation has been confirmed.");
            sb.Append(Environment.NewLine);
            sb.Append("Below you can find detailed information about the event and price calculation.");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append($"Event number: {currentEvent.Id}");
            sb.Append(Environment.NewLine);
            sb.Append($"Hall: {currentEvent.HallName}");
            sb.Append(Environment.NewLine);
            sb.Append($"Event Date: {currentEvent.EventDate}");
            sb.Append(Environment.NewLine);
            sb.Append($"Rent Time: {currentEvent.RentTimeDisplay}");
            sb.Append(Environment.NewLine);
            sb.Append($"Event Start Time: {currentEvent.EventStart}");
            sb.Append(Environment.NewLine);
            sb.Append($"Event End Time: {currentEvent.EventEnd}");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append($"Price Information:");
            sb.Append(Environment.NewLine);
            sb.Append($"Hall Rental price: ${currentEvent.HallRentalPrice.ToString("F")}");
            sb.Append(Environment.NewLine);
            if (currentEvent.UsingTablesAndChairs)
            {
                sb.Append(Environment.NewLine);
                sb.Append($"Using tables and chairs price: ${currentEvent.TablesAndChairsPrice.ToString("F")}");
                sb.Append(Environment.NewLine);
                sb.Append($"---Tables and chairs cost per person: ${currentEvent.TablesAndChairsCostPerPerson.ToString("F")}");
                sb.Append(Environment.NewLine);
                sb.Append($"---Number of people: {currentEvent.NumberOfPeople}");
                sb.Append(Environment.NewLine);
            }
            if (currentEvent.ParkingLotSecurityService)
            {
                sb.Append(Environment.NewLine);
                sb.Append($"Parking Lot Security Price: ${currentEvent.ParkingLotSecurityPrice.ToString("F")}");
                sb.Append(Environment.NewLine);
                sb.Append($"---Parking Lot Security Hours: {currentEvent.ParkingLotSecurityHours}");
                sb.Append(Environment.NewLine);
                sb.Append($"---Security service cost per hour: ${currentEvent.SecurityCostPerHour.ToString("F")}");
                sb.Append(Environment.NewLine);
                sb.Append($"---Security Start Time: {currentEvent.SecurityStartTime}");
                sb.Append(Environment.NewLine);
                sb.Append($"---Security End Time: {currentEvent.SecurityEndTime}");
                sb.Append(Environment.NewLine);
            }
            if (currentEvent.AdditionalCharges > 0)
            {
                sb.Append(Environment.NewLine);
                sb.Append($"Additional Charges: ${currentEvent.AdditionalCharges.ToString("F")}");
                sb.Append(Environment.NewLine);
                sb.Append($"---Additional Charges Info: {currentEvent.SecurityEndTime}");
                sb.Append(Environment.NewLine);


            }
            sb.Append(Environment.NewLine);
            sb.Append($"Security Deposit: ${currentEvent.SecurityDeposit.ToString("F")}");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append($"Total Price: ${currentEvent.TotalPrice.ToString("F")}");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("This is an automatically generated email – please do not reply to it.");
            sb.Append(Environment.NewLine);
            sb.Append("If you have any queries regarding your resevation please use our contact form or email to ...");

            return sb.ToString();
        }
    }
}
