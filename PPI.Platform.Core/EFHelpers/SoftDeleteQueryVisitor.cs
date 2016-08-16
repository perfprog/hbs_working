using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace PPI.Platform.Core.EFHelpers
{
    using PPI.Platform.Core.Attributes;
    public class SoftDeleteQueryVisitor : DefaultExpressionVisitor
    {
        public override DbExpression Visit(DbScanExpression expression)
        {

            var Column = SoftDeleteAttribute.GetSoftDeleteColumnName(expression.Target.ElementType);
            if (Column != null)
            {
                var binding = DbExpressionBuilder.Bind(expression);
                return DbExpressionBuilder.Filter(binding,
                    DbExpressionBuilder.NotEqual(
                        DbExpressionBuilder.Property(
                            DbExpressionBuilder.Variable(binding.VariableType, binding.VariableName),
                            Column
                            ),
                        DbExpression.FromBoolean(true)));
            }
            else
                return base.Visit(expression);
        }
        
    }
}
