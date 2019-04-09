using DotNetCoreArchitecture.Model;
using System;

namespace DotNetCoreArchitecture.Domain
{
    public class UserLogEntity
    {
        protected internal UserLogEntity
        (
            long userLogId,
            long userId,
            LogType logType
        )
        {
            UserLogId = userLogId;
            UserId = userId;
            LogType = logType;
        }

        public UserLogEntity() { }

        public long UserLogId { get; private set; }

        public long UserId { get; private set; }

        public LogType LogType { get; private set; }

        public DateTime DateTime { get; private set; }

        public UserEntity User { get; private set; }

        public void Add()
        {
            UserLogId = 0;
            DateTime = DateTime.UtcNow;
        }
    }
}
