using System;
using Xunit;
using Xunit.Abstractions;
using static LanguageExt.Prelude;

namespace LanguageExtTests
{
    
    public class FunTests
    {
        private readonly ITestOutputHelper _output;

        public FunTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact] public void LambdaInferTests()
        {
            var fn1 = fun( () => 123 );
            var fn2 = fun( (int a) => 123 + a );
            var fn3 = fun( (int a, int b) => 123 + a + b );
            var fn4 = fun( (int a, int b, int c) => 123 + a + b + c);
            var fn5 = fun( (int a, int b, int c, int d) => 123 + a + b + c + d);
            var fn6 = fun( (int a, int b, int c, int d, int e) => 123 + a + b + c + d + e);
            var fn7 = fun( (int a, int b, int c, int d, int e, int f) => 123 + a + b + c + d + e + f);
            var fn8 = fun( (int a, int b, int c, int d, int e, int f, int g) => 123 + a + b + c + d + e + f + g);

            var fnac1 = fun( () => { } );
            var fnac2 = fun( (int a) => _output.WriteLine((123 + a).ToString()));
            var fnac3 = fun( (int a, int b) => _output.WriteLine((123 + a + b).ToString()));
            var fnac4 = fun( (int a, int b, int c) => _output.WriteLine((123 + a + b + c).ToString()));
            var fnac5 = fun( (int a, int b, int c, int d) => _output.WriteLine((123 + a + b + c + d).ToString()));
            var fnac6 = fun( (int a, int b, int c, int d, int e) => _output.WriteLine((123 + a + b + c + d + e).ToString()));
            var fnac7 = fun( (int a, int b, int c, int d, int e, int f) => _output.WriteLine((123 + a + b + c + d + e + f).ToString()));
            var fnac8 = fun( (int a, int b, int c, int d, int e, int f, int g) => _output.WriteLine((123 + a + b + c + d + e + f + g).ToString()));

            var ac1 = act(() => { });
            var ac2 = act((int a) => _output.WriteLine((123 + a).ToString()));
            var ac3 = act((int a, int b) => _output.WriteLine((123 + a + b).ToString()));
            var ac4 = act((int a, int b, int c) => _output.WriteLine((123 + a + b + c).ToString()));
            var ac5 = act((int a, int b, int c, int d) => _output.WriteLine((123 + a + b + c + d).ToString()));
            var ac6 = act((int a, int b, int c, int d, int e) => _output.WriteLine((123 + a + b + c + d + e).ToString()));
            var ac7 = act((int a, int b, int c, int d, int e, int f) => _output.WriteLine((123 + a + b + c + d + e + f).ToString()));
            var ac8 = act((int a, int b, int c, int d, int e, int f, int g) => _output.WriteLine((123 + a + b + c + d + e + f + g).ToString()));
        }
    }
}
