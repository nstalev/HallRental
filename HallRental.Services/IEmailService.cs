
using System;
using HallRental.Services.Admin.Models.Events;

namespace HallRental.Services
{
    public interface IEmailService
    {
        string GetEmailTextBodyFromContactForm(string email, string name, string message);
        string GetTextBodyForEmailForReservation(DateTime date, string fullName, string email, string phoneNumber, int numberOfPeople, decimal totalPrice);
        string GetTextBodyForTenant(DateTime date, string fullName, int numberOfPeople, decimal totalPrice);
        string GetEmailConfirmationTextBody(EventDetailsAdminSM currentEvent);
    }
}
