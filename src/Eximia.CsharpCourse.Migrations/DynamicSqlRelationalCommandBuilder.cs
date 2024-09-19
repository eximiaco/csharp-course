using Microsoft.EntityFrameworkCore.Storage;

namespace Eximia.CsharpCourse.Migrations;

public class DynamicSqlRelationalCommandBuilder : RelationalCommandBuilder
{
    public DynamicSqlRelationalCommandBuilder(RelationalCommandBuilderDependencies dependencies) : base(dependencies) { }

    public override IRelationalCommand Build()
    {
        var newCommandText = "EXECUTE ('" + base.ToString().Replace("'", "''") + "')";
        return new RelationalCommand(base.Dependencies, newCommandText, base.Parameters);
    }
}