using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roslynCastRepro
{
    class SampleRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitMemberBindingExpression(MemberBindingExpressionSyntax node)
        {
            Console.WriteLine("Visiting MemberBindingExpression");
            try
            {
                return base.VisitMemberBindingExpression(node);
            }
            catch
            {
                throw;
            }
        }
    }
}
