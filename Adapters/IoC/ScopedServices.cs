using Adapters.Persistencias;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Interfaces;

namespace Adapters.IoC
{
    public static class ScopedServices
    {
        public static void InjectScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonagemServices, PersonagemServices>();
            services.AddScoped<IPersonagemFavoritoJsonServices, PersonagemFavoritoJsonServices>();
        }
    }
}
