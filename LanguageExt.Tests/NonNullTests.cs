using System;
using LanguageExt;
using static LanguageExt.Prelude;
using Xunit;
using Xunit.Abstractions;

namespace LanguageExtTests
{
    
    public class NonNullTests
    {
        private readonly ITestOutputHelper _output;

        public NonNullTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ValueCastTest1()
        {
            Assert.Throws(
                typeof(ValueIsNullException),
                () =>
                {
                    Foo(null);
                }
            );

            Assert.Throws(
                typeof(ValueIsNullException),
                () =>
                {
                    string isnull = null;
                    Foo(isnull);
                }
            );
        }

        [Fact]
        public void ValueCastTest2()
        {
            // These should pass
            Foo("Hello");
            string world = "World";
            Foo(world);
        }

        [Fact]
        public void NotNullReferenceTypeTest()
        {
            Assert.Throws(
                typeof(ValueIsNullException),
                () =>
                {
                    Some<string> str = null;
                });
        }

        [Fact]
        public void SomeCastsToOptionTest()
        {
            Some<string> some = "Hello";
            Option<string> opt = some;

            Assert.True(opt.IsSome && opt.IfNone("") == "Hello");
        }


        private Option<string> GetValue(bool select) =>
            select
                ? Some("Hello")
                : None;

        public void Foo( Some<string> value )
        {
            if (value.Value == null)
            {
                failwith<Unit>("Value should never be null");
            }

            string doesItImplicitlyCastBackToAString = value;
        }

        public void Greet(Some<string> arg)
        {
            _output.WriteLine(arg);
        }

        [Fact]
        public void AssignToSomeAfterDeclaration()
        {
            Some<string> val;
            val = "Hello";
            Assert.True(val.Value != null);
            Greet(val);
        }

        [Fact]
        public void AssignToSomeMemberAfterDeclaration()
        {
            var obj = new SomeClass();

            obj.SomeOtherValue = "123";
            _output.WriteLine(obj.SomeOtherValue);
            Assert.True(obj.SomeValue == "Hello");
            Assert.True(obj.SomeOtherValue.IsSome);
            Greet(obj.SomeOtherValue);
        }

        [Fact]
        public void AccessUninitialisedEitherMember()
        {
            var obj = new EitherClass();

            match(obj.EitherValue,
                Right: r => _output.WriteLine(r.ToString()),
                Left: l => _output.WriteLine(l)
            );

            Assert.Throws(
                typeof(BottomException),
                () => {

                    match(obj.EitherOtherValue,
                        Right: r => _output.WriteLine(r.ToString()),
                        Left: l => _output.WriteLine(l)
                    );
                }
            );
        }


        [Fact]
        public void AccessUninitialisedSomeMember()
        {
            var obj = new SomeClass();
            Assert.Throws(
                typeof(SomeNotInitialisedException),
                () => {
                    Greet(obj.SomeOtherValue);
                }
            );
        }
    }

    class SomeClass
    {
        public Some<string> SomeValue = "Hello";
        public Some<string> SomeOtherValue;
    }

    #pragma warning disable CS0649
    class EitherClass
    {
        public Either<string, int> EitherValue = "Hello";
        public Either<string, int> EitherOtherValue;
    }

}
