using Crm.Application.Companies.RegisterCompany;
using System.Reflection;

namespace Crm.Infrastructure.Processing
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(RegisterCompanyCommand).Assembly;
    }
}