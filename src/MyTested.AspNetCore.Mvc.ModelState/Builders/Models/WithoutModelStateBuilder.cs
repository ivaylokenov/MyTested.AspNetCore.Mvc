namespace MyTested.AspNetCore.Mvc.Builders.Models
{
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Models;
    using MyTested.AspNetCore.Mvc.Internal.TestContexts;

    public class WithoutModelStateBuilder : BaseModelStateBuilder, IAndWithoutModelStateBuilder
    {
        public WithoutModelStateBuilder(ActionTestContext actionContext) 
            : base(actionContext)
        {
        }

        public IAndWithoutModelStateBuilder WithoutModelState()
        {
            this.ModelState.Clear();
            return this;
        }

        public IAndWithoutModelStateBuilder WithoutModelState(string key)
        {
            if(this.ModelState.ContainsKey(key))
                this.ModelState.Remove(key);

            return this;
        }

        public IWithoutModelStateBuilder AndAlso()
            => this;
    }
}
