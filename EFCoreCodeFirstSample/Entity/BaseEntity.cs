using System.Runtime.CompilerServices;

namespace EFCoreCodeFirstSample.Entity
{
    public abstract class BaseEntity
    {
        public Guid ID { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsActive { get; set; }

        protected BaseEntity()
        {
        }

        protected BaseEntity(string id, string userType)
        {
            DateTime dateTime = DateTime.SpecifyKind(DateTime.Now.AddHours(8.0), DateTimeKind.Unspecified);
            ModifiedOn = (CreatedOn = new DateTimeOffset(dateTime, TimeSpan.FromHours(8.0)));
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("id");
            }

            CreatedBy = id;
            ModifiedBy = id;
            if (string.IsNullOrEmpty(userType) || string.IsNullOrWhiteSpace(userType))
            {
                throw new ArgumentException("userType");
            }

            IsActive = true;
        }

        protected void SetModifiedBy(string id, string userType)
        {
            DateTime dateTime = DateTime.SpecifyKind(DateTime.Now.AddHours(8.0), DateTimeKind.Unspecified);
            DateTimeOffset dateTimeOffset2 = (ModifiedOn = new DateTimeOffset(dateTime, TimeSpan.FromHours(8.0)));
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("id");
            }

            ModifiedBy = id;
            if (string.IsNullOrEmpty(userType) || string.IsNullOrWhiteSpace(userType))
            {
                throw new ArgumentException("userType");
            }
        }

        protected void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        protected void SetID(Guid id)
        {
            ID = id;
        }

        public override bool Equals(object? obj)
        {
            BaseEntity baseEntity = obj as BaseEntity;
            if ((object)baseEntity == null)
            {
                return false;
            }

            if ((object)this == baseEntity)
            {
                return true;
            }

            if (GetType() != baseEntity.GetType())
            {
                return false;
            }

            if (ID == default(Guid) || baseEntity.ID == default(Guid))
            {
                return false;
            }

            return ID == baseEntity.ID;
        }

        public static bool operator ==(BaseEntity left, BaseEntity right)
        {
            if (object.Equals(left, null))
            {
                return object.Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(BaseEntity left, BaseEntity right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 2);
            defaultInterpolatedStringHandler.AppendFormatted(GetType());
            defaultInterpolatedStringHandler.AppendFormatted(ID);
            return defaultInterpolatedStringHandler.ToStringAndClear().GetHashCode();
        }

        protected void SetModifiedOn(DateTime modifiedOnTimestamp)
        {
            DateTimeOffset dateTimeOffset2 = (ModifiedOn = new DateTimeOffset(modifiedOnTimestamp, TimeSpan.FromHours(8.0)));
        }

        protected void SetCreatedOn(DateTime createdOnTimeStamp)
        {
            DateTimeOffset dateTimeOffset2 = (CreatedOn = new DateTimeOffset(createdOnTimeStamp, TimeSpan.FromHours(8.0)));
        }
    }
}
