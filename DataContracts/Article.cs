﻿using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContracts
{
    [DataContract]
    public class Article
    {
        [DataMember]
        public string SourceLogo {get; set;}
        [DataMember]
        public string Source {get; set;}
        [DataMember]
        public string Date {get; set;}
        [DataMember]
        public string Image {get; set;}
        [DataMember]
        public string CardStyle {get; set;}
        [DataMember]
        public string Heading {get; set;}
        [DataMember]
        public string OriginalLink {get; set;}
        [DataMember]
        public int LikesCount {get; set;}
        [DataMember]
        public int ReTweetCount {get; set;}
        [DataMember]
        public int CommentsCount {get; set;}
        [DataMember]
        public DateTime LastActivity {get; set;}
        [DataMember]
        public double Score {get; set;}
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public int Id {get; set;}
        [DataMember]
        public string Email {get; set;}
        [DataMember]
        public string PrimaryLanguage {get; set;}    
}

    [DataContract]
    public class UserCredentials
    {
        [DataMember]
        public string Email {get; set;}
        [DataMember]
        public string Password {get; set;}    
    }

    [DataContract]
    public class VersionInfo
    {
        [DataMember]
        public string LatestVersion {get; set;}
        [DataMember]
        public string MinSupportedVersion {get; set;}
    }

    [DataContract]
    public class UserNotification
    {
        [DataMember]
        public string Text {get; set;}
        [DataMember]
        public bool IsRead {get; set;}       
}

    [DataContract]
    public class ConfigData
    {
        [DataMember]
        public Dictionary<string, Dictionary<string, string>> Labels  {get; set;}
        [DataMember]
        public string Url {get; set;}
    }

    [DataContract]
    public class CredentialsValidation
    {
        [DataMember]
        public string Id {get; set;}
        [DataMember]
        public bool Validated {get; set;}
        [DataMember]
        public string Message {get; set;}
}

    [DataContract]
    public class Stream : EntityWithText
    {
        [DataMember]
        public string Lang {get; set;}
        [DataMember]
        public bool IsAdmin {get; set;}
        [DataMember]
        public bool UserSelected {get; set;}
        [DataMember]
        public string backgroundImageUrl {get; set;}
    }

    [DataContract]
    public class Entity
    {
        [DataMember]
        public string Id {get; set;}    
 }

    [DataContract]
    public class EntityWithText : Entity
    {
        [DataMember]
        public string Text {get; set;}
    }
}