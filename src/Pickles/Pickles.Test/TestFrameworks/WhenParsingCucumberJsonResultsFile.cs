﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WhenParsingCucumberJsonResultsFile.cs" company="PicklesDoc">
//  Copyright 2011 Jeffrey Cameron
//  Copyright 2012-present PicklesDoc team and community contributors
//
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

using NFluent;
using NUnit.Framework;

using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.TestFrameworks;

namespace PicklesDoc.Pickles.Test.TestFrameworks
{
    [TestFixture]
    public class WhenParsingCucumberJsonResultsFile : WhenParsingTestResultFiles<CucumberJsonResults>
    {
        public WhenParsingCucumberJsonResultsFile()
            : base("results-example-json.json")
        {
        }

        [Test]
        public void ThenCanReadFeatureResultSuccesfully()
        {
            var results = ParseResultsFile();

            var feature = new Feature { Name = "Test Feature" };
            TestResult result = results.GetFeatureResult(feature);

            Check.That(result.WasExecuted).IsTrue();
            Check.That(result.WasSuccessful).IsFalse();
        }

        [Test]
        public void ThenCanReadScenarioResultSuccessfully()
        {
            var results = ParseResultsFile();

            var feature = new Feature { Name = "Test Feature" };

            var scenario1 = new Scenario { Name = "Passing", Feature = feature };
            TestResult result1 = results.GetScenarioResult(scenario1);

            Check.That(result1.WasExecuted).IsTrue();
            Check.That(result1.WasSuccessful).IsTrue();

            var scenario2 = new Scenario { Name = "Failing", Feature = feature };
            TestResult result2 = results.GetScenarioResult(scenario2);

            Check.That(result2.WasExecuted).IsTrue();
            Check.That(result2.WasSuccessful).IsFalse();
        }

        [Test]
        public void ThenCanReadFeatureWithoutScenariosSuccessfully_ShouldReturnInconclusive()
        {
            var results = ParseResultsFile();
            var feature = new Feature { Name = "Feature Without Scenarios" };

            TestResult result = results.GetFeatureResult(feature);

            Check.That(result).IsEqualTo(TestResult.Inconclusive);
        }
    }
}
