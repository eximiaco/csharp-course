using Microsoft.EntityFrameworkCore.Storage;

namespace Eximia.CsharpCourse.Migrations;

public class DynamicSqlRelationalCommandBuilderFactory : RelationalCommandBuilderFactory
{
    public DynamicSqlRelationalCommandBuilderFactory(RelationalCommandBuilderDependencies dependencies) : base(dependencies) { }

    public override IRelationalCommandBuilder Create()
    {
        return new DynamicSqlRelationalCommandBuilder(base.Dependencies);
    }
}