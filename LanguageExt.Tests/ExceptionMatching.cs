﻿using Xunit;
using System;
using LanguageExt;
using static LanguageExt.Prelude;

namespace LanguageExtTests
{
    
    public class ExceptionMatching
    {
        [Fact]
        public void ExTest1()
        {
            string x = match( Number<ArgumentException>(10),
                              Succ: v  => "Worked",
                              Fail: ex => ex.Match<string>()
                                            .With<ArgumentException>(e => "It's a system exception")
                                            .With<ArgumentNullException>(e => "Arg null")
                                            .Otherwise("Not handled") );

            Assert.True(x == "Worked");
        }

        [Fact]
        public void ExTest2()
        {
            string x = match( Number<ArgumentException>(9),
                              Succ: v  => "Worked",
                              Fail: ex => ex.Match<string>()
                                            .With<ArgumentException>(e => "It's a system exception")
                                            .With<ArgumentNullException>(e => "Arg null")
                                            .Otherwise("Not handled") );

            Assert.True(x == "It's a system exception");
        }

        [Fact]
        public void ExTest3()
        {
            string x = match( Number<ArgumentNullException>(9),
                              Succ: v  => "Worked",
                              Fail: ex => ex.Match<string>()
                                            .With<ArgumentException>(e => "It's a system exception")
                                            .With<ArgumentNullException>(e => "Arg null")
                                            .Otherwise("Not handled") );

            Assert.True(x == "Arg null");
        }

        [Fact]
        public void ExTest4()
        {
            string x = match( Number<Exception>(9),
                              Succ: v  => "Worked",
                              Fail: ex => ex.Match<string>()
                                            .With<ArgumentException>(e => "It's a system exception")
                                            .With<ArgumentNullException>(e => "Arg null")
                                            .Otherwise("Not handled") );

            Assert.True(x == "Not handled");
        }

        private static Try<int> Number<T>(int x) where T : Exception, new() => () =>
            x % 2 == 0
                ? x
                : raise<int>(new T());
    }
}
