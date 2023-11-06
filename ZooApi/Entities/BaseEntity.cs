namespace Zoo.Entities
{
    public abstract class BaseEntity
    {
        public readonly Guid Id = Guid.NewGuid();

        public override bool Equals(object obj)
        {
            if (obj is BaseEntity)
            {
                return Id == ((BaseEntity)obj).Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ((BaseEntity)this).Id.GetHashCode();
        }
    }
}
