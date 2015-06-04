using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roslynCastRepro
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = CSharpSyntaxTree.ParseText(@"
                class MyClass
                {
                    void MyMethod()
                    {
                        IEnumerable<string> foo = new List<string>();
                        var bar = foo.OfType<string>()?.FirstOrDefault()?.Length.ToString();

                        MethodDeclarationSyntax method = null;
                        var x = method?.TypeParameterList?.Parameters.ToList();
                    }
                }");

            var mscorlib = MetadataReference.CreateFromAssembly(typeof(object).Assembly);
            var compilation = CSharpCompilation.Create("MyCompilation",
                syntaxTrees: new[] { tree }, references: new[] { mscorlib });
            var model = compilation.GetSemanticModel(tree);

            var rewriter = new SampleRewriter();
            try
            {
                var rewrittenRoot = rewriter.Visit(tree.GetRoot());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
