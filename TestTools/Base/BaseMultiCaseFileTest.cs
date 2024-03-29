﻿using System.Collections.Generic;
using Xunit.Abstractions;

namespace TestTools.Base
{
    public abstract class BaseMultiCaseFileTest<Case, Input> : BaseTest
        where Case : List<(Input Input, string Path)>
    {
        public BaseMultiCaseFileTest(ITestOutputHelper output)
            : base(output)
        {
        }

        public void TestAssertFile(Input input, string expected)
        {
            var result = Run(input);
            _helper.AssertByte(result, expected);
        }

        public void TestAssertFile(Case scenario)
        {
            var results = TestScenario(scenario);
            _helper.AssertByte(results);
        }

        public void TestAssertFile(List<Case> scenarios)
        {
            var results = new List<(byte[], string)>();
            foreach (var scenario in scenarios)
            {
                var result = TestScenario(scenario);
                results.AddRange(result);
            }
            _helper.AssertByte(results);
        }

        protected abstract byte[] Run(Input input);

        private List<(byte[], string)> TestScenario(Case scenario)
        {
            var results = new List<(byte[], string)>();
            foreach (var c in scenario)
            {
                var result = Run(c.Input);
                results.Add((result, c.Path));
            }
            return results;
        }
    }

    public abstract class BaseMultiCaseFileTest<Parameters, Case, Input> : BaseTest<Parameters>
      where Case : List<(Input Input, string Path)>
    {
        public BaseMultiCaseFileTest(Parameters parameters, ITestOutputHelper output)
            : base(parameters, output)
        {
        }

        public void TestAssertFile(Parameters parameters, Input input, string expected)
        {
            var result = Run(parameters, input);
            _helper.AssertByte(result, expected);
        }

        public void TestAssertFile((Parameters Parameters, Case Case) scenario)
        {
            var results = TestScenario(scenario);
            _helper.AssertByte(results);
        }

        public void TestAssertFile(List<(Parameters Parameters, Case Case)> scenarios)
        {
            var results = new List<(byte[], string)>();
            foreach (var scenario in scenarios)
            {
                var result = TestScenario(scenario);
                results.AddRange(result);
            }
            _helper.AssertByte(results);
        }

        protected abstract byte[] Run(Parameters parameters, Input input);

        private List<(byte[], string)> TestScenario((Parameters Parameters, Case Case) scenario)
        {
            var results = new List<(byte[], string)>();
            foreach (var c in scenario.Case)
            {
                var result = Run(scenario.Parameters, c.Input);
                results.Add((result, c.Path));
            }
            return results;
        }
    }
}
