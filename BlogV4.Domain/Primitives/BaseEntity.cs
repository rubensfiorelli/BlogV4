namespace BlogV4.Domain.Primitives
{
    public abstract class BaseEntity
    {
        //public override bool Equals(object? obj)
        //{
        //    if (obj is null)
        //        return false;

        //    if (obj.GetType() != GetType())
        //        return false;

        //    if (obj is not BaseEntity entity)
        //        return false;

        //    return entity.Id == Id;
        //}
        //public override int GetHashCode()
        //{
        //    return Id.GetHashCode();
        //}

        private DateTimeOffset _createdAt;

        public DateTimeOffset CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = DateTimeOffset.UtcNow; }
        }

        private DateTimeOffset? _updatedAt;

        public DateTimeOffset? UpdatedAt
        {
            get { return _updatedAt; }
            set { _updatedAt = value; }
        }

    }
}
