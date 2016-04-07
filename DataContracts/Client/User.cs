using System.Runtime.Serialization;
using DataContracts.Search;
using System;

namespace DataContracts.Client
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Language { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ProfileImage { get; set; }
        [DataMember]
        public bool CanPost { get; set; }
        [DataMember]
        public string[] Streams { get; set; }
    }

    [DataContract]
    public class UserDeviceInfo
    {
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string JSON { get; set; }
    }

    [DataContract]
    public class UserContact
    {
        [DataMember]
        public bool IsOnNetwork { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string ProfileImg { get; set; }
        [DataMember]
        public bool IsFollowing { get; set; }
    }


    [DataContract]
    public class UserContactsInfo
    {
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public ContactProperties[] Contacts { get; set; }
    }

    [DataContract]
    public class ContactField
    {
        /** A string that indicates what type of field this is, home for example. */
        [DataMember]
        public string Type { get; set; }
        /** The value of the field, such as a phone number or email address. */
        [DataMember]
        public string Value { get; set; }
        /** Set to true if this ContactField contains the user's preferred value. */
        [DataMember]
        public bool Pref { get; set; }
    }


    [DataContract]
    public class ContactProperties
    {
        /** A globally unique identifier. */
        [DataMember]
        public string Id { get; set; }
        /** The name of this Contact, suitable for display to end users. */
        [DataMember]
        public string DisplayName { get; set; }
        /** An object containing all components of a persons name. */
        [DataMember]
        public string Name { get; set; }
        /** A casual name by which to address the contact. */
        [DataMember]
        public string Nickname { get; set; }
        /** An array of all the contact's phone numbers. */
        [DataMember]
        public ContactField[] PhoneNumbers { get; set; }
        /** An array of all the contact's email addresses. */
        [DataMember]
        public ContactField[] Emails { get; set; }
        /** An array of all the contact's addresses. */
        [DataMember]
        public ContactAddress[] Addresses { get; set; }
        /** An array of all the contact's IM addresses. */
        [DataMember]
        public ContactField[] IMs { get; set; }
        /** An array of all the contact's organizations. */
        [DataMember]
        public ContactOrganization[] Organizations { get; set; }
        /** The birthday of the contact. */
        [DataMember]
        public DateTime Birthday { get; set; }
        /** A note about the contact. */
        [DataMember]
        public string Note { get; set; }
        /** An array of the contact's photos. */
        [DataMember]
        public ContactField[] Photos;
        /** An array of all the user-defined categories associated with the contact. */
        [DataMember]
        public ContactField[] Categories { get; set; }
        /** An array of web pages associated with the contact. */
        [DataMember]
        public ContactField[] Urls { get; set; }
    }

    [DataContract]
    public class ContactAddress
    {
        /** Set to true if this ContactAddress contains the user's preferred value. */
        [DataMember]
        public bool Pref { get; set; }
        /** A string indicating what type of field this is, home for example. */
        [DataMember]
        public string Type { get; set; }
        /** The full address formatted for display. */
        [DataMember]
        public string Formatted { get; set; }
        /** The full street address. */
        [DataMember]
        public string StreetAddress { get; set; }
        /** The city or locality. */
        [DataMember]
        public string Locality { get; set; }
        /** The state or region. */
        [DataMember]
        public string Region { get; set; }
        /** The zip code or postal code. */
        [DataMember]
        public string PostalCode { get; set; }
        /** The country name. */
        [DataMember]
        public string Country { get; set; }
    }

    [DataContract]
    public class ContactOrganization
    {
        /** Set to true if this ContactOrganization contains the user's preferred value. */
        [DataMember]
        public bool Pref { get; set; }
        /** A string that indicates what type of field this is, home for example. */
        [DataMember]
        public string Type { get; set; }
        /** The name of the organization. */
        [DataMember]
        public string Name { get; set; }
        /** The department the contract works for. */
        [DataMember]
        public string Department { get; set; }
        /** The contact's title at the organization. */
        [DataMember]
        public string Title { get; set; }
    }

    [DataContract]
    public class UserGeoInfo
    {
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string JSON { get; set; }
    }

    [DataContract]
    public class UserNotification
    {
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public bool IsRead { get; set; }
    }

    [DataContract]
    public class UserCredentials
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Language { get; set; }
    }

    [DataContract]
    public class CredentialsValidation
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public bool Validated { get; set; }
        [DataMember]
        public string Message { get; set; }
    }

    [DataContract]
    public class VersionInfo
    {
        [DataMember]
        public string LatestVersion { get; set; }
        [DataMember]
        public string MinSupportedVersion { get; set; }
    }

    [DataContract]
    public class UserReaction
    {
        [DataMember]
        public string ArticleId { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string ReactionType { get; set; }
    }

    [DataContract]
    public enum ReactionType
    {
        [EnumMember]
        Like,
        [EnumMember]
        ReTweet,
        [EnumMember]
        UnLike,
        [EnumMember]
        UnReTweet
    }
}
