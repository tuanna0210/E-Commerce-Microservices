﻿namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; } = default!;
        public string LastName { get; } = default!;
        public string? EmailAddress { get; } = default!;
        public string AddressLine { get; } = default!;
        public string Country { get; } = default!;
        public string State { get; } = default!;
        public string Zipcode { get; } = default!;

        protected Address()
        {

        }

        private Address(string firstName, string lastName, string? emailAddress, string addressLine, string country, string state, string zipcode)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            Country = country;
            State = state;
            Zipcode = zipcode;
        }
        public static Address Of(string firstName, string lastName, string? emailAddress, string addressLine, string country, string state, string zipcode)
        {
            ArgumentException.ThrowIfNullOrEmpty(emailAddress);
            ArgumentException.ThrowIfNullOrEmpty(addressLine);


            return new Address(firstName, lastName, emailAddress, addressLine, country, state, zipcode);
        }
    }
}
