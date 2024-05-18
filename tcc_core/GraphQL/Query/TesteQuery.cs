using tcc_core.Models;
using tcc_core.Services;

namespace tcc_core.Query
{
    public class TesteQuery
    {
        public IQueryable<UserTeste> GetUserTestes([Service] UserTesteService usuarioService)
        {
            return usuarioService.ObterTodosUserTestes();
        }
    }

    public class UserTesteType : ObjectType<UserTeste>
    {
        protected override void Configure(IObjectTypeDescriptor<UserTeste> descriptor)
        {
            descriptor.Field(u => u.Id).Type<NonNullType<IntType>>();
            descriptor.Field(u => u.Nome).Type<NonNullType<StringType>>();
            descriptor.Field(u => u.Email).Type<NonNullType<StringType>>();
            descriptor.Field(u => u.Senha).Type<NonNullType<StringType>>();
        }
    }
}
