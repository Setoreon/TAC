using System;
using accounts.tac.local.Attributes;
//using Sitecore.Analytics;
//using Sitecore.Foundation.DependencyInjection;
using Sitecore.Pipelines;
using Sitecore.Security.Accounts;

namespace accounts.tac.local.Services
{
    [Service]
    public class PipelineService
    {
        public bool RunLoggedIn(User user)
        {           
            var args = new LoggedInPipelineArgs()
            {
                User = user,
                Source = user.GetDomainName(),
                UserName = user.Name,
                ContactId = null //Tracker.Current?.Contact?.ContactId
            };
            CorePipeline.Run("accounts.loggedIn", args);
            return args.Aborted;
        }

        public bool RunLoggedOut(User user)
        {
            var args = new AccountsPipelineArgs()
            {
                User = user,
                UserName = user.Name
            };
            CorePipeline.Run("accounts.loggedOut", args);
            return args.Aborted;
        }

        public bool RunRegistered(User user)
        {
            var args = new AccountsPipelineArgs()
            {
                User = user,
                UserName = user.Name
            };
            CorePipeline.Run("accounts.registered", args);
            return args.Aborted;
        }
    }
    
    public class AccountsPipelineArgs : PipelineArgs
    {
        public User User
        {
            get; set;
        }

        public string UserName
        {
            get
            {
                return this.CustomData["UserName"]?.ToString();
            }
            set
            {
                this.CustomData["UserName"] = value;
            }
        }

        public Guid? ContactId
        {
            get
            {
                return (Guid)this.CustomData["ContactId"];
            }
            set
            {
                this.CustomData["ContactId"] = value;
            }
        }
    }
    
    public class LoggedInPipelineArgs : AccountsPipelineArgs
    {
        public string Source { get; set; }

        public Guid? PreviousContactId
        {
            get
            {
                return (Guid)this.CustomData["PreviousContactId"];
            }
            set
            {
                this.CustomData["PreviousContactId"] = value;
            }
        }
    }
}