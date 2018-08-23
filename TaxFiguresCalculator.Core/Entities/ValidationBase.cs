using TaxFiguresCalculator.Core.Repositories;

namespace TaxFiguresCalculator.Core.Entities
{
    public abstract class ValidationBase<T> : IValidationRepository where T : class
    {
        protected T Context { get; private set; }

        protected ValidationBase(T context)
        {
            Context = context;
        }

        public string Validate()
        {
            return Message;
        }

        public virtual bool Condition
        {
            get
            {
                // If the condition is not overriden, the requirent always needs to be true
                return true;
            }
        }

        public abstract bool Requirement { get; }

        public bool IsValid { get { return Implication(Condition, Requirement); } }

        public abstract string Message { get; }

        private static bool Implication(bool condition, bool requirement)
        {
            return !condition || requirement;
        }
    }
}
