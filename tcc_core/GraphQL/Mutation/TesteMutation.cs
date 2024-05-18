using tcc_core.Models;
using tcc_core.Query;
using tcc_core.Services;

namespace tcc_core.Mutation
{
    public class TesteMutation
    {
        public UserTeste CreateUserTeste([Service] UserTesteService usuarioService, string nome, string email, string senha)
        {
            var novoUserTeste = new UserTeste
            {
                Nome = nome,
                Email = email,
                Senha = senha
            };

            usuarioService.AdicionarUserTeste(novoUserTeste);

            return novoUserTeste;
        }
    }

    public class MutationType : ObjectType<TesteMutation>
    {
        protected override void Configure(IObjectTypeDescriptor<TesteMutation> descriptor)
        {
            descriptor.Field(f => f.CreateUserTeste(default, default, default, default))
                      .Argument("nome", a => a.Type<NonNullType<StringType>>())
                      .Argument("email", a => a.Type<NonNullType<StringType>>())
                      .Argument("senha", a => a.Type<NonNullType<StringType>>())
                      .Type<NonNullType<UserTesteType>>()
                      .Name("createUserTeste");
        }
    }
}
